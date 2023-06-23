#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Navy.Core.Logger
{
    /// <summary>
    ///     Multi Logger
    /// </summary>
    public class MultiLogger : ILogger
    {
        public MultiLogger(IEnumerable<ILogger> loggers)
        {
            Loggers.AddRange(loggers);
        }

        public MultiLogger(params ILogger[] loggers)
        {
            Loggers.AddRange(loggers);
        }

        public List<ILogger> Loggers { get; } = new List<ILogger>();

        public void Add(ILogger logger)
        {
            Loggers.Add(logger);
        }

        #region Члены ILogger

        public LogType Level
        {
            get
            {
                if (Loggers.Count > 0)
                    return Loggers[0].Level;
                return LogType.Debug;
            }
            set { Loggers.ForEach(p => p.Level = value); }
        }

        public string TextFormat
        {
            get => Loggers.OfType<LoggerBase>().FirstOrDefault()?.TextFormat;
            set
            {
                foreach (var log in Loggers.OfType<LoggerBase>())
                    log.TextFormat = value;
                foreach (var log in Loggers.OfType<MultiLogger>())
                    log.TextFormat = value;
            }
        }

        public void WriteLine(string text, params object[] args)
        {
            Loggers.ForEach(p => p.WriteLine(text, args));
        }

        public void WriteLine(LogType level, string text, params object[] args)
        {
            Loggers.ForEach(p => p.WriteLine(level, text, args));
        }

        public void Warning(string text, params object[] args)
        {
            Loggers.ForEach(p => p.Warning(text, args));
        }

        public void Error(string text, params object[] args)
        {
            Loggers.ForEach(p => p.Error(text, args));
        }

        public void Error(Exception err)
        {
            Loggers.ForEach(p => p.Error(err));
        }

        public void Trace(string text, params object[] args)
        {
            Loggers.ForEach(p => p.Trace(text, args));
        }

        public void Debug(string text, params object[] args)
        {
            Loggers.ForEach(p => p.Debug(text, args));
        }

        #endregion
    }
}