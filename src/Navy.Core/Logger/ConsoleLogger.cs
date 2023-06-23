#region

using System;

#endregion

namespace Navy.Core.Logger
{
    /// <summary>
    ///     Console Logger
    /// </summary>
    public class ConsoleLogger : LoggerBase
    {
        private static ConsoleLogger _instance;

        /// <summary>
        ///     System.Console Logger
        /// </summary>
        public static ConsoleLogger Instance => _instance ?? (_instance = new ConsoleLogger());

        protected override void WriteText(string text)
        {
            Console.WriteLine(text);
        }
    }
}