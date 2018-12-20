using System;
using System.Threading.Tasks;

namespace AsyncMonads.Extensions
{
    public static class TaskExtensions
    {
        public static async Task<TResult> Select<T, TResult>(this Task<T> task, Func<T, TResult> selector)
        {
            var t = await task;

            return selector(t);
        }

        public static async Task<TResult> SelectMany<T, TResult>(this Task<T> task, Func<T, Task<TResult>> selector)
        {
            var t = await task;

            return await selector(t);
        }

        public static async Task<TResult> Match<T, TResult>(this Task<Maybe<T>> maybe, TResult nothing, Func<T, TResult> just)
        {
            var m = await maybe;

            return m.Match(nothing, just);
        }
    }
}