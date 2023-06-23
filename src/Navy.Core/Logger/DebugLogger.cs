namespace Navy.Core.Logger
{
    /// <summary>
    ///     System.Diagnostics.Debug Logger
    /// </summary>
    public class DebugLogger : LoggerBase
    {
        private static DebugLogger _instance;

        /// <summary>
        ///     System.Diagnostics.Debug Logger
        /// </summary>
        public static DebugLogger Instance => _instance ?? (_instance = new DebugLogger());

        protected override void WriteText(string text)
        {
            System.Diagnostics.Debug.WriteLine(text);
        }
    }
}