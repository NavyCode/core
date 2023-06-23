namespace Navy.Core.Logger
{
    /// <summary>
    ///     System.Diagnostics.Debug Logger
    /// </summary>
    public class TraceLogger : LoggerBase
    {
        private static TraceLogger _instance;

        /// <summary>
        ///     System.Diagnostics.Debug Logger
        /// </summary>
        public static TraceLogger Instance => _instance ?? (_instance = new TraceLogger());

        protected override void WriteText(string text)
        {
            System.Diagnostics.Trace.WriteLine(text);
        }
    }
}