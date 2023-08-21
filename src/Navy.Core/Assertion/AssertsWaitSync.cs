#region

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

#endregion

namespace Navy.Core.Assertion
{
    public partial class Asserts
    {
        public static void WaitFalse(Func<bool> func, TimeSpan timeout, string message = null, TimeSpan? interval = null, Func<string> comment = null)
          => WaitEqual(false, () => func(), timeout, message, interval, comment);

        public static void WaitTrue(Func<bool> func, TimeSpan timeout, string message = null, TimeSpan? interval = null, Func<string> comment = null)
          => WaitEqual(true, () => func(), timeout, message, interval, comment);

        public static void WaitNotNull(Func<object> func, TimeSpan timeout, string message = null, TimeSpan? interval = null, Func<string> comment = null)
          => WaitNotEqual(null, () => func(), timeout, message, interval, comment);

        public static void WaitNull(Func<object> func, TimeSpan timeout, string message = null, TimeSpan? interval = null, Func<string> comment = null)
          => WaitEqual(null, () => func(), timeout, message, interval, comment);

        public static void WaitEqual(object expected, Func<object> actual, TimeSpan timeout, string message = null, TimeSpan? interval = null, Func<string> comment = null)
        {
            object lastResult = null;
            var func = new Func<bool>(() =>
            {
                lastResult = actual();
                if (expected == null && lastResult == null)
                    return true;
                if (expected == null && lastResult != null)
                    return false;
                return expected.Equals(lastResult);
            });
            var result = ThreadHelper.WaitTrue(func, timeout, interval);
            if (!result)
                throw new AssertException(TimeOutText(timeout, $"Expected:<{expected}>. Actual:<{lastResult}>. " + message, comment));
        }

        public static void WaitNotEqual(object expected, Func<object> actual, TimeSpan timeout, string message = null, TimeSpan? interval = null, Func<string> comment = null)
        {
            object lastResult = null;
            var func = new Func<bool>(() =>
            {
                lastResult = actual();
                if (expected == null && lastResult == null)
                    return false;
                if (expected == null && lastResult != null)
                    return true;
                return !expected.Equals(lastResult);
            });
            var result = ThreadHelper.WaitTrue(func, timeout, interval);
            if (!result)
                throw new AssertException(TimeOutText(timeout, $"Expected any value except:<{expected ?? "null"}>. Actual:<{lastResult ?? "null"}>. " + message, comment));
        }

        public static void WaitNoError(Action action, TimeSpan timeout, string message = null, TimeSpan? interval = null, Func<string> comment = null)
        {
            Exception lastError = null;
            var func = new Func<bool>(() =>
            {
                try
                {
                    action();
                    return true;
                }
                catch (Exception err)
                {
                    lastError = err;
                    return false;
                }
            });
            var result = ThreadHelper.WaitTrue(func, timeout, interval);
            if (!result)
            {
                throw new AssertException(lastError.Message + ". " + TimeOutText(timeout, message, comment), lastError);
            }
        }
        public static void WaitError<TExc>(Action action, TimeSpan timeout, string message = null, TimeSpan? interval = null, Func<string> comment = null) where TExc : Exception
        {
            Exception lastError = null;
            var func = new Func<bool>(() =>
           {
               try
               {
                   action();
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
            var result = ThreadHelper.WaitTrue(func, timeout, interval);
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