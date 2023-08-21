#region

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

#endregion

namespace Navy.Core.Assertion
{
    public partial class Asserts
    {
        public static async Task WaitTrueAsync(Func<Task<bool>> func, TimeSpan timeout, string message = null, TimeSpan? interval = null, Func<string> comment = null)
        {
            var result = await ThreadHelperAsync.WaitTrueAsync(func, timeout, interval);
            if (!result)
                throw new AssertException(TimeOutText(timeout, message, comment));
        }

        public static async Task WaitFalseAsync(Func<Task<bool>> func, TimeSpan timeout, string message = null, TimeSpan? interval = null, Func<string> comment = null)
        {
            var result = await ThreadHelperAsync.WaitFalseAsync(func, timeout, interval);
            if (!result)
                throw new AssertException(TimeOutText(timeout, message, comment));
        }

        public static async Task WaitNotNullAsync(Func<Task<object>> func, TimeSpan timeout, string message = null, TimeSpan? interval = null, Func<string> comment = null)
            => await WaitTrueAsync(async () => await func() != null, timeout, message, interval, comment);

        public static async Task WaitNullAsync(Func<Task<object>> func, TimeSpan timeout, string message = null, TimeSpan? interval = null, Func<string> comment = null)
            => await WaitTrueAsync(async () => await func() == null, timeout, message, interval, comment);

        public static async Task WaitEqualAsync(object expected, Func<Task<object>> actual, TimeSpan timeout, string message = null, TimeSpan? interval = null, Func<string> comment = null)
        => await WaitTrueAsync(async () =>
            {
                var actualValue = await actual();
                return expected.Equals(actualValue);
            }, timeout, message, interval, comment);

        public static async Task WaitNotEqualAsync(object expected, Func<Task<object>> actual, TimeSpan timeout, string message = null, TimeSpan? interval = null, Func<string> comment = null)
           => await WaitTrueAsync(async () =>
           {
               var actualValue = await actual();
               return !expected.Equals(actualValue);
           }, timeout, message, interval, comment);


        public static async Task WaitNoErrorAsync(Func<Task> action, TimeSpan timeout, string message = null, TimeSpan? interval = null, Func<string> comment = null)
        {
            Exception lastError = null;
            var func = new Func<Task<bool>>(async () =>
            {
                try
                {
                    await action();
                    return true;
                }
                catch (Exception err)
                {
                    lastError = err;
                    return false;
                }
            });
            var result = await ThreadHelperAsync.WaitTrueAsync(func, timeout, interval);
            if (!result)
            {
                throw new AssertException(lastError.Message + ". " + TimeOutText(timeout, message, comment), lastError);
            }
        } 

        public static async Task WaitErrorAsync<TExc>(Func<Task> action, TimeSpan timeout, string message = null, TimeSpan? interval = null, Func<string> comment = null) where TExc : Exception
        {
            Exception lastError = null;
            var func = new Func<Task<bool>>(async () =>
            {
                try
                {
                    await action();
                    return false;
                }
                catch (TExc)
                {
                    return true;
                }
                catch (Exception err)
                {
                    lastError = err;
                    return false;
                }
            });
            var result = await ThreadHelperAsync.WaitTrueAsync(func, timeout, interval);
            if (!result)
            {
                var text = $"The specified error was not caught";
                if (lastError != null)
                    text += $". Last error: {lastError.Message}"; 
                text += ". " + TimeOutText(timeout, message, comment); 
                if (lastError != null)
                    throw new AssertException(text, lastError);
                else
                    throw new AssertException(text);
            }
        } 
    }
}