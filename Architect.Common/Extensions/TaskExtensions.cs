using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
    public static class TaskExtensions
    {
        public static ConfiguredTaskAwaitable ConfigureAwaitFalse(this Task task)
        {
            return task.ConfigureAwait(false);
        }

        public static ConfiguredTaskAwaitable<T> ConfigureAwaitFalse<T>(this Task<T> task)
        {
            return task.ConfigureAwait(false);
        }
    }
}
