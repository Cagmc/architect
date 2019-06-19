using System;
using System.Net;

using Architect.Common.Infrastructure.DataTransfer.Response;

using Microsoft.EntityFrameworkCore;

namespace Architect.Common.Infrastructure
{
    public abstract class ResponsiveServiceBase<TEntity, TDbContext> : ServiceBase<TDbContext>
        where TEntity : class
        where TDbContext : DbContext
    {
        private readonly string notFoundTemplate = "{0} not found with id: {1}";
        private readonly string conflictTemplate = "{0} conflicted with data: {1}";

        public ResponsiveServiceBase(TDbContext context) : base(context)
        {

        }

        #region Status not found

        protected virtual IStatusResponse NotFoundStatusResponse(int id)
        {
            return CreateStatusResponse<TEntity>(notFoundTemplate, HttpStatusCode.NotFound, id, id);
        }

#pragma warning disable
        protected virtual IStatusResponse NotFoundStatusResponse<TEntity>(int id)
#pragma warning enable
            where TEntity : EntityBase
        {
            return CreateStatusResponse<TEntity>(notFoundTemplate, HttpStatusCode.NotFound, id, id);
        }

        protected virtual IDataResponse<TResponse> NotFoundDataResponse<TResponse>(int id)
        {
            return CreateDataStatusResponse<TEntity, TResponse>(notFoundTemplate, HttpStatusCode.NotFound, id, id);
        }

#pragma warning disable
        protected virtual IDataResponse<TResponse> NotFoundDataResponse<TEntity, TResponse>(int id)
#pragma warning enable
            where TEntity : class
        {
            return CreateDataStatusResponse<TEntity, TResponse>(notFoundTemplate, HttpStatusCode.NotFound, id, id);
        }

        #endregion

        #region Status conflict

        protected virtual IStatusResponse ConflictStatusResponse(int id, object data)
        {
            return CreateStatusResponse<TEntity>(
                conflictTemplate, HttpStatusCode.Conflict, id, data);
        }

#pragma warning disable
        protected virtual IStatusResponse ConflictStatusResponse<TEntity>(int id, object data)
#pragma warning enable
            where TEntity : EntityBase
        {
            return CreateStatusResponse<TEntity>(
                conflictTemplate, HttpStatusCode.Conflict, id, data);
        }

        protected virtual IDataResponse<TResponse> ConflictDataResponse<TResponse>(int id, object data)
        {
            return CreateDataStatusResponse<TEntity, TResponse>(
                conflictTemplate, HttpStatusCode.Conflict, id, data);
        }

#pragma warning disable
        protected virtual IDataResponse<TResponse> ConflictDataResponse<TEntity, TResponse>(int id, object data)
#pragma warning enable
            where TEntity : class
        {
            return CreateDataStatusResponse<TEntity, TResponse>(
                conflictTemplate, HttpStatusCode.Conflict, id, data);
        }

        #endregion

        #region Status rivate

        private IStatusResponse CreateStatusResponse<T>(string template, HttpStatusCode statusCode,
            int? id = null, object data = null)
        {
            return new StatusResponse(template
                .Format(typeof(T).Name, data), statusCode, id);
        }

        private IDataResponse<TResponse> CreateDataStatusResponse<T, TResponse>(
            string template, HttpStatusCode statusCode, int? id = null, object data = null)
        {
            return new DataResponse<TResponse>(template
                .Format(typeof(T).Name, data), statusCode, id);
        }

        #endregion
    }
}
