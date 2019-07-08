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
    }
}
