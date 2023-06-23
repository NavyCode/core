#region

using System;
using Navy.Core.Threading;

#endregion

namespace Navy.Core.Assertion
{
    public class Asserts
    {
        public static void IsTrue(bool item, string message = null, params object[] args)
        {
            if (item)
                return;
            var comment = string.IsNullOrWhiteSpace(message)
                ? "object is false"
                : "";
            ThrowException(comment, message, args);
        }

        public static void WaitFalse(Func<bool> untilState, TimeSpan timeout, string message = null,
            params object[] args)
        {
            IsTrue(ThreadHelper.Sleep(untilState, timeout), TimeOutText(timeout, message, args));
        }

        public static void WaitFalse(Func<bool> untilState, TimeSpan timeout, TimeSpan interval, string message = null,
            params object[] args)
        {
            IsTrue(ThreadHelper.Sleep(untilState, timeout, interval), TimeOutText(timeout, message, args));
        }

        public static void WaitFalse(Func<bool> untilState, TimeSpan timeout, Func<string> message)
        {
            IsTrue(ThreadHelper.Sleep(untilState, timeout), TimeOutText(timeout, message()));
        }

        public static void WaitTrue(Func<bool> whileState, TimeSpan timeout, string message = null,
            params object[] args)
        {
            IsTrue(ThreadHelper.Sleep(() => !whileState(), timeout), TimeOutText(timeout, message, args));
        }

        public static void WaitNotNull(Func<object> whileState, TimeSpan timeout, string message = null,
            params object[] args)
        {
            IsTrue(ThreadHelper.Sleep(() => whileState() == null, timeout), TimeOutText(timeout, message, args));
        }

        public static void WaitNotNull(Func<object> whileState, TimeSpan timeout, TimeSpan interval,
            string message = null, params object[] args)
        {
            IsTrue(ThreadHelper.Sleep(() => whileState() == null, timeout, interval),
                TimeOutText(timeout, message, args));
        }

        public static void WaitTrue(Func<bool> whileState, TimeSpan timeout, TimeSpan interval, string message = null,
            params object[] args)
        {
            IsTrue(ThreadHelper.Sleep(() => !whileState(), timeout, interval), TimeOutText(timeout, message, args));
        }

        public static void WaitTrue(Func<bool> whileState, TimeSpan timeout, Func<string> message)
        {
            IsTrue(ThreadHelper.Sleep(() => !whileState(), timeout), TimeOutText(timeout, message()));
        }

        public static void WaitTrue(Func<bool> whileState, TimeSpan timeout, TimeSpan interval, Func<string> message)
        {
            IsTrue(ThreadHelper.Sleep(() => !whileState(), timeout, interval), TimeOutText(timeout, message()));
        }

        public static void WaitNoError(Action action, TimeSpan timeout, TimeSpan interval, string message = null,
            params object[] args)
        {
            Exception lastError = null;
            var func = new Func<bool>(() =>
            {
                try
                {
                    action.Invoke();
                    return false;
                }
                catch (Exception err)
                {
                    lastError = err;
                    return true;
                }
            });
            var result = ThreadHelper.Sleep(func, timeout, interval);
            if (!result)
            {
                var text = lastError.Message;
                var userText = UserText(message, args);
                if (!string.IsNullOrEmpty(userText))
                    text += ". " + userText;
                throw new AssertException(text, lastError);
            }
        }


        private static string TimeOutText(TimeSpan timeout, string message = null, params object[] args)
        {
            return $". TimeOut {timeout.TotalSeconds} sec. " + UserText(message, args);
        }

        public static void WaitEqual(object expected, object actual, TimeSpan timeout, string message = null,
            params object[] args)
        {
            if (ThreadHelper.Sleep(() => !expected.Equals(actual), timeout))
                return;
            ThrowNotEquals(expected, actual, timeout, message, args);
        }

        public static void WaitEqual(object expected, Func<object> actual, TimeSpan timeout, string message = null,
            params object[] args)
        {
            if (ThreadHelper.Sleep(() => !expected.Equals(actual()), timeout))
                return;
            ThrowNotEquals(expected, actual(), timeout, message, args);
        }

        public static void WaitEqual(object expected, Func<object> actual, TimeSpan timeout, TimeSpan interval,
            string message = null, params object[] args)
        {
            if (ThreadHelper.Sleep(() => !expected.Equals(actual()), timeout, interval))
                return;
            ThrowNotEquals(expected, actual(), timeout, message, args);
        }

        public static void WaitEqual(object expected, object actual, TimeSpan timeout, TimeSpan interval,
            string message = null, params object[] args)
        {
            if (ThreadHelper.Sleep(() => !expected.Equals(actual), timeout, interval))
                return;
            ThrowNotEquals(expected, actual, timeout, message, args);
        }

        public static void WaitNotEqual(object expected, object actual, TimeSpan timeout, string message = null,
            params object[] args)
        {
            if (ThreadHelper.Sleep(() => expected.Equals(actual), timeout))
                return;
            ThrowNotEquals(expected, actual, timeout, message, args);
        }

        public static void WaitNotEqual(object expected, Func<object> actual, TimeSpan timeout, string message = null,
            params object[] args)
        {
            if (ThreadHelper.Sleep(() => expected.Equals(actual()), timeout))
                return;
            ThrowNotEquals(expected, actual(), timeout, message, args);
        }

        public static void WaitNotEqual(object expected, object actual, TimeSpan timeout, TimeSpan interval,
            string message = null, params object[] args)
        {
            if (ThreadHelper.Sleep(() => expected.Equals(actual), timeout, interval))
                return;
            ThrowNotEquals(expected, actual, timeout, message, args);
        }

        public static void IsFalse(bool item, string message = null, params object[] args)
        {
            if (!item)
                return;
            var comment = string.IsNullOrWhiteSpace(message)
                ? "object is true"
                : "";
            ThrowException(comment, message, args);
        }


        public static void Fail(string message, params object[] args)
        {
            var comment = string.IsNullOrWhiteSpace(message)
                ? "Fail"
                : "";
            ThrowException(comment, message, args);
        }

        public static void AreEqual(object expected, object actual, string message = null, params object[] args)
        {
            if ((expected == null) & (actual == null))
                return;
            if (expected != null && expected.Equals(actual))
                return;
            ThrowNotEquals(expected, actual, message, args);
        }

        private static void ThrowNotEquals(object expected, object actual, string message, params object[] args)
        {
            var error = NotEqualsText(expected, actual);
            ThrowException(error, message, args);
        }

        private static void ThrowNotEquals(object expected, object actual, TimeSpan timeout, string message,
            params object[] args)
        {
            var error = NotEqualsText(expected, actual);
            ThrowException(error, TimeOutText(timeout, message, args));
        }

        private static string NotEqualsText(object expected, object actual)
        {
            return
                $"Expected '{AsString(expected)}' {expected.GetType().Name}. Actual: '{AsString(actual)}'. {actual.GetType().Name + " " + nameof(actual)}";
        }


        private static string AsString(object item)
        {
            const int maxLength = 255;
            var str = item.ToString();
            if (str.Length > maxLength)
                str = str.Substring(0, maxLength) + " ...";
            return str;
        }

        public static void AreNotEqual(object expected, object actual, string message = null, params object[] args)
        {
            if (!actual.Equals(expected))
                return;
            var error =
                $"Any is expected except '{AsString(expected)}' {expected.GetType().Name}. Actual: '{AsString(actual)}' {actual.GetType().Name}";
            ThrowException(error, message, args);
        }

        public static void AreEqual(double expected, double actual, double distance, string message = null,
            params object[] args)
        {
            if (Math.Abs(expected - actual) <= distance)
                return;
            var error = $"Expected: {expected}. Actual: {actual}. Distance: {distance}";
            ThrowException(error, message, args);
        }

        public static void AreEqual(DateTime expected, DateTime actual, TimeSpan distance, string message = null,
            params object[] args)
        {
            if (Math.Abs((expected - actual).Ticks) <= distance.Ticks)
                return;
            var error = $"Expected '{expected}'. Actual: '{actual}'. Distance {distance}";
            ThrowException(error, message, args);
        }

        public static void AreEqual(TimeSpan expected, TimeSpan actual, TimeSpan distance, string message = null,
            params object[] args)
        {
            if (Math.Abs((expected - actual).Ticks) <= distance.Ticks)
                return;
            var error = $"Expected '{expected}'. Actual: '{actual}'. Distance {distance}";
            ThrowException(error, message, args);
        }

        public static void IsNotNull(object item, string message = null, params object[] args)
        {
            if (item != null)
                return;
            var comment = string.IsNullOrWhiteSpace(message)
                ? "object is null"
                : "";
            ThrowException(comment, message, args);
        }

        public static void IsNull(object item, string message = null, params object[] args)
        {
            if (item == null)
                return;
            var comment = string.IsNullOrWhiteSpace(message)
                ? "object is not null"
                : "";
            ThrowException(comment, message, args);
        }


        private static void ThrowException(string message, string userMessage, params object[] args)
        {
            var text = message;
            var userText = UserText(userMessage, args);
            if (!string.IsNullOrEmpty(userText))
                text += ". " + userText;
            throw new AssertException(text);
        }

        private static string UserText(string userMessage, params object[] args)
        {
            if (userMessage == null)
                return string.Empty;
            return args.Length == 0
                ? userMessage
                : string.Format(userMessage, args);
        }
    }
}