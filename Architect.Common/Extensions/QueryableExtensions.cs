using System;
using System.Linq;
using System.Linq.Expressions;

using Architect.Common.Enums;
using Architect.Common.Infrastructure.DataTransfer.Request;

namespace Microsoft.EntityFrameworkCore
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, PaginationFilter filter)
        {
            if (filter.OrderByColumn.IsNotNullOrEmpty())
            {
                query = query.OrderBy(filter.OrderByColumn, filter.OrderByDirection);
            }

            if (filter.PageSize.HasValue && filter.PageSize.Value > 0)
            {
                var skip = (filter.Page.Value - 1) * filter.PageSize.Value;
                var take = filter.PageSize.Value;

                query = query.Skip(skip).Take(take);
            }

            return query;
        }

        // https://stackoverflow.com/a/31959568/2531463
        public static IOrderedQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> query,
            string propertyName = "Id", OrderDirection direction = OrderDirection.Ascending)
        {
            var entityType = typeof(TSource);

            //Create x=>x.PropName
            var propertyInfo = entityType.GetProperty(propertyName);

            if(propertyInfo == null)
            {
                propertyInfo = entityType.GetProperty("Id");
            }

            var arg = Expression.Parameter(entityType, "x");
            var property = Expression.Property(arg, propertyName);
            var selector = Expression.Lambda(property, new ParameterExpression[] { arg });

            //Get System.Linq.Queryable.OrderBy() method.
            var enumarableType = typeof(Queryable);
            var method = enumarableType.GetMethods()
                 .Where(m => m.Name == (direction == OrderDirection.Ascending ? "OrderBy" : "OrderByDescending") 
                    && m.IsGenericMethodDefinition)
                 .Where(m =>
                 {
                     var parameters = m.GetParameters().ToList();
                     //Put more restriction here to ensure selecting the right overload                
                     return parameters.Count == 2;//overload that has 2 parameters
                 }).Single();
            //The linq's OrderBy<TSource, TKey> has two generic types, which provided here
            var genericMethod = method
                 .MakeGenericMethod(entityType, propertyInfo.PropertyType);

            /*Call query.OrderBy(selector), with query and selector: x=> x.PropName
              Note that we pass the selector as Expression to the method and we don't compile it.
              By doing so EF can extract "order by" columns and generate SQL for it.*/
            var newQuery = (IOrderedQueryable<TSource>)genericMethod
                 .Invoke(genericMethod, new object[] { query, selector });
            return newQuery;
        }

        public static IQueryable<T> DateFilter<T>(this IQueryable<T> query,
            DateTime? startDate, DateTime? endDate, Expression<Func<T, DateTime>> expression)
        {
            if (endDate.HasValue)
            {
                Expression<Func<T, DateTime>> right = arg => endDate.Value;

                var param1 = Expression.Parameter(typeof(T));

                var equals = Expression.Lambda<Func<T, bool>>(
                Expression.LessThanOrEqual(
                    Expression.Invoke(expression, param1),
                    Expression.Invoke(right, param1)),
                param1);

                query = query.Where(equals);
            }

            if (startDate.HasValue)
            {
                Expression<Func<T, DateTime>> right = arg => startDate.Value;

                var param1 = Expression.Parameter(typeof(T));

                var equals = Expression.Lambda<Func<T, bool>>(
                Expression.GreaterThanOrEqual(
                    Expression.Invoke(expression, param1),
                    Expression.Invoke(right, param1)),
                param1);

                query = query.Where(equals);
            }

            return query;
        }

        public static IQueryable<T> KeyFilter<T>(this IQueryable<T> query,
            long? key, Expression<Func<T, long?>> expression)
        {
            if (key.HasValue)
            {
                Expression<Func<T, long?>> right = arg => key;

                var param1 = Expression.Parameter(typeof(T));

                var equals = Expression.Lambda<Func<T, bool>>(
                Expression.Equal(
                    Expression.Invoke(expression, param1),
                    Expression.Invoke(right, param1)),
                param1);

                query = query.Where(equals);
            }

            return query;
        }

        public static IQueryable<T> KeyFilter<T>(this IQueryable<T> query,
            long? key, Expression<Func<T, long>> expression)
        {
            if (key.HasValue)
            {
                Expression<Func<T, long>> right = arg => key.Value;

                var param1 = Expression.Parameter(typeof(T));

                var equals = Expression.Lambda<Func<T, bool>>(
                Expression.Equal(
                    Expression.Invoke(expression, param1),
                    Expression.Invoke(right, param1)),
                param1);

                query = query.Where(equals);
            }

            return query;
        }

        public static IQueryable<T> StringFilter<T>(this IQueryable<T> query,
           string filter, params Expression<Func<T, string>>[] stringProperties)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                // Get the names of the properties
                var publicProperties = stringProperties.Select(i => i.Body
                    .ToString()
                    .Replace(i.Parameters[0].ToString() + ".", "")).ToList();

                // Collect information about types, methods etc.
                var like = filter.SqlLike();
                var likeConstant = Expression.Constant(like, typeof(string));
                var param1 = Expression.Parameter(typeof(T));
                var likeMethod = typeof(DbFunctionsExtensions).GetMethod("Like",
                    new[] { typeof(DbFunctions), typeof(string), typeof(string) });

                // Create an "empty" OR expression vith constant values for starting point
                var orExpressions = Expression.Equal(Expression.Constant(1), Expression.Constant(0));

                // Add the LIKE function for every given property with OR expression
                foreach (var property in publicProperties)
                {
                    var searchKeyExpression = Expression.Property(param1, property);
                    var likeExp = Expression.Call(null, likeMethod,
                        Expression.Constant(EF.Functions), searchKeyExpression, likeConstant);

                    orExpressions = Expression.OrElse(orExpressions, likeExp);
                }

                // Create a lambda from the ORs
                var lambda = Expression.Lambda<Func<T, bool>>(orExpressions, param1);

                query = query.Where(lambda);
            }

            return query;
        }
    }
}
