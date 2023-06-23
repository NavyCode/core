namespace Navy.Core.Logger
{
    /// <summary>
    ///     Null Logger
    /// </summary>
    public class NullLogger : LoggerBase
    {
        private static NullLogger _instance;

        /// <summary>
        ///     Null Logger
        /// </summary>
        public static NullLogger Instance => _instance ?? (_instance = new NullLogger());

        protected override void WriteText(string text)
        {
        }
    }
}