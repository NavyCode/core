#region

using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

#endregion

namespace Navy.Core.Assertion
{
    public partial class Asserts
    {
        public static void IsTrue(bool item, string message = null)
        {
            if (item)
                return;
            var comment = string.IsNullOrWhiteSpace(message)
                ? "object is false"
                : "";
            ThrowException(comment, message);
        }

        public static void IsFalse(bool item, string message = null)
        {
            if (!item)
                return;
            var comment = string.IsNullOrWhiteSpace(message)
                ? "object is true"
                : "";
            ThrowException(comment, message);
        } 

        public static void Fail(string message)
        {
            var comment = string.IsNullOrWhiteSpace(message)
                ? "Fail"
                : "";
            ThrowException(comment, message);
        }

        public static void AreEqual(object expected, object actual, string message = null)
        {
            if ((expected == null) & (actual == null))
                return;
            if (expected != null && expected.Equals(actual))
                return;
            ThrowEqualsError(expected, actual, message);
        }

        private static void ThrowEqualsError(object expected, object actual, string message)
        {
            var error = NotEqualsText(expected, actual);
            ThrowException(error, message);
        }

        private static string TimeOutText(TimeSpan timeout, string message, Func<string> lastMessage)
        {
            var result = message;
            if (lastMessage != null)
            {
                if (result != null)
                    result += ". ";
                result += lastMessage.Invoke();
            }
            if (result != null)
                result += ". ";
            result += $"TimeOut {timeout.TotalSeconds.ToString(CultureInfo.InvariantCulture)} sec";
            return result;
        }

        private static string NotEqualsText(object expected, object actual)
        {
            return
                $"Expected '{AsString(expected)}' {expected?.GetType()?.Name}. Actual: '{AsString(actual)}'. {actual?.GetType()?.Name + " " + nameof(actual)}";
        }


        private static string AsString(object item)
        {
            const int maxLength = 255;
            var str = item?.ToString() ?? "";
            if (str.Length > maxLength)
                str = str.Substring(0, maxLength) + " ...";
            return str;
        }

        public static void AreNotEqual(object expected, object actual, string message = null)
        {
            if (!actual.Equals(expected))
                return;
            var error =
                $"Any is expected except '{AsString(expected)}' {expected.GetType().Name}. Actual: '{AsString(actual)}' {actual.GetType().Name}";
            ThrowException(error, message);
        }

        public static void AreEqual(double expected, double actual, double distance, string message = null)
        {
            if (Math.Abs(expected - actual) <= distance)
                return;
            var error = $"Expected: {expected}. Actual: {actual}. Distance: {distance}";
            ThrowException(error, message);
        }

        public static void AreEqual(DateTime expected, DateTime actual, TimeSpan distance, string message = null)
        {
            if (Math.Abs((expected - actual).Ticks) <= distance.Ticks)
                return;
            var error = $"Expected '{expected}'. Actual: '{actual}'. Distance {distance}";
            ThrowException(error, message);
        }

        public static void AreHexEqual(uint expected, uint actual, string message = null)
        {
            if (expected == actual)
                return;
            ThrowException($"Expected: 0x{expected:X}. Actual: 0x{actual:X}", message);
        }

        public static void AreHexNotEqual(uint expected, uint actual, string message = null)
        {
            if (expected != actual)
                return;
            ThrowException($"Any is expected except: 0x{expected:X}. Actual: 0x{actual:X}", message);
        }

        public static void AreEqual(TimeSpan expected, TimeSpan actual, TimeSpan distance, string message = null)
        {
            if (Math.Abs((expected - actual).Ticks) <= distance.Ticks)
                return;
            var error = $"Expected '{expected}'. Actual: '{actual}'. Distance {distance}";
            ThrowException(error, message);
        }

        public static void IsNotNull(object item, string message = null)
        {
            if (item != null)
                return;
            var comment = string.IsNullOrWhiteSpace(message)
                ? "object is null"
                : "";
            ThrowException(comment, message);
        }

        public static void IsNull(object item, string message = null)
        {
            if (item == null)
                return;
            var comment = string.IsNullOrWhiteSpace(message)
                ? "object is not null"
                : "";
            ThrowException(comment, message);
        }
    }
}