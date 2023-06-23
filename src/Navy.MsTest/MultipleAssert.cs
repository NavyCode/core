using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Navy.Core.Assertion;
using Navy.Test.Assertion;

namespace Navy.MsTest
{
    /// <summary>
    ///     Multiple Assert
    /// </summary>
    public class MultipleAssert
    {
        private StringBuilder _comment = new StringBuilder();
        private UnitTestOutcome _result = UnitTestOutcome.Passed;

        /// <summary>
        ///     Result
        /// </summary>
        public UnitTestOutcome Result
        {
            get => _result;
            set
            {
                if (_result != UnitTestOutcome.Failed && _result != UnitTestOutcome.Inconclusive) _result = value;
            }
        }

        public string Comment => _comment.ToString();

        public void SetResult(UnitTestOutcome result, string message)
        {
            Result = result;
            _comment.AppendLine(message);
        }

        public void AreEqual(object expected, object actual, string message = null, params object[] args)
        {
            Assert(() => Asserts.AreEqual(expected, actual, message, args));
        }

        public void AreEqual(double expected, double actual, double delta, string message = null, params object[] args)
        {
            Assert(() => Asserts.AreEqual(expected, actual, delta, message, args));
        }

        public void AreNotEqual(object expected, object actual, string message = null, params object[] args)
        {
            Assert(() => Asserts.AreNotEqual(expected, actual, message, args));
        }

        public void IsTrue(bool actual, string message = null, params object[] args)
        {
            Assert(() => Asserts.IsTrue(actual, message, args));
        }

        public void IsNull(object actual, string message = null, params object[] args)
        {
            Assert(() => Asserts.IsNull(actual, message, args));
        }

        public void IsNotNull(object actual, string message = null, params object[] args)
        {
            Assert(() => Asserts.IsNotNull(actual, message, args));
        }


        private static string AsString(object item)
        {
            const int maxLength = 255;
            var str = item.ToString();
            if (str.Length > maxLength)
                str = str.Substring(0, maxLength) + " ...";
            return str;
        }

        /// <summary>
        ///     Reset
        /// </summary>
        public void Reset()
        {
            _result = UnitTestOutcome.Passed;
        }

        public void SetFailed(string message)
        {
            SetResult(UnitTestOutcome.Failed, message);
        }

        public void Inconclusive(string message)
        {
            SetResult(UnitTestOutcome.Inconclusive, message);
        }

        public void ThrowIsFailed()
        {
            if (Result != UnitTestOutcome.Passed)
                throw new AssertException(Comment);
        }

        private void Assert(Action act)
        {
            try
            {
                act.Invoke();
            }
            catch (Exception err)
            {
                SetResult(UnitTestOutcome.Failed, err.Message);
            }
        }
    }
}