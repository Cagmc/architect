using System.Collections.Generic;
using System.Linq;

namespace System
{
    public static class TypeExtensions
    {

        public static IEnumerable<Type> GetConcreteTypes(this Type type)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p))
                .Where(p => !p.IsAbstract);

            return types;
        }

        /// <summary>
        /// Gets instances of every concrete type which derives from the given
        /// type parameter and has a parameterless constructor.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetInstances<T>()
            => typeof(T)
            .GetConcreteTypes()
            .Select(p => p.GetConstructor(Type.EmptyTypes))
            .Where(x => x != null)
            .Select(x => (T)x.Invoke(Array.Empty<object>()));
    }
}
