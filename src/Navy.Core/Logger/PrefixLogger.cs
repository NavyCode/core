#region

using System;

#endregion

namespace Navy.Core.Logger
{
    /// <summary>
    ///     Console Logger
    /// </summary>
    public class PrefixLogger : ILogger
    {
        private ILogger _logger;

        public PrefixLogger(ILogger logger)
        {
            _logger = logger;
        }

        public PrefixLogger(ILogger logger, string prefix)
        {
            Prefix = prefix;
            _logger = logger;
        }

        public string Prefix { get; set; }

        public LogType Level
        {
            get => _logger.Level;
            set => _logger.Level = value;
        }

        public void WriteLine(string text, params object[] args)
        {
            _logger.WriteLine(Prefix + text, args);
        }

        public void WriteLine(LogType level, string text, params object[] args)
        {
            _logger.WriteLine(Prefix + level, text, args);
        }

        public void Warning(string text, params object[] args)
        {
            _logger.Warning(Prefix + text, args);
        }

        public void Error(string text, params object[] args)
        {
            _logger.Error(Prefix + text, args);
        }

        public void Error(Exception err)
        {
            _logger.Error(err);
        }

        public void Trace(string text, params object[] args)
        {
            _logger.Trace(Prefix + text, args);
        }

        public void Debug(string text, params object[] args)
        {
            _logger.Debug(Prefix + text, args);
        }
    }
}