#region

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

#endregion

namespace Navy.Core.Assertion
{
    public partial class Asserts
    {
        private static void ThrowException(string message, string userMessage)
        {
            var text = message; 
            if (!string.IsNullOrEmpty(userMessage))
                text += ". " + userMessage;
            throw new AssertException(text);
        }
    }
}