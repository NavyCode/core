using System;

namespace Navy.Core.Assertion
{
    public class AssertException : Exception
    {
        public AssertException(string message)
            : base(message)
        {
        }

        public AssertException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}