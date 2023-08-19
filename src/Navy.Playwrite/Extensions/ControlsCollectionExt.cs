using System;
using System.Linq; 
using System.Threading.Tasks; 
using Navy.Core.Assertion;
using Navy.Playwrite.Controls;

namespace Navy.Playwright
{
    public static class ControlsCollectionExt
    {
        public static async Task<ControlsCollection<T>> WaitCountAsync<T>(this ControlsCollection<T> el, int expected, Func<T, bool> predicate = null
            , TimeSpan? timeout = null, string errorMessage = null) where T : Control
        {
            await Asserts.WaitNoErrorAsync(async () => 
            {
                var all = await el.AllAsync();
                var actual = predicate == null ? all.Count : all.Count(predicate);
                if (expected != actual)
                    throw new Exception($"Ожидалось {expected} элементов, пришло {actual}");
            }, timeout ?? TimeSpan.FromSeconds(10), errorMessage, TimeSpan.FromMilliseconds(200));
            return el;
        }

        public static async Task<T> WaitFirstAsync<T>(this ControlsCollection<T> el, Func<T, bool> func = null
            , TimeSpan? timeout = null, string errorMessage = null) where T : Control
        {
            T result = default;
            await Asserts.WaitNotNullAsync(async () =>
            {
                var all = await el.AllAsync();
                result = func == null ? all.FirstOrDefault() : all.FirstOrDefault(func);
                return result;
            },
                timeout ?? TimeSpan.FromSeconds(10), errorMessage, TimeSpan.FromMilliseconds(200));
            return result;
        }
    }
}
