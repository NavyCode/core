#region

using System;
using System.Diagnostics;
using System.Threading;

#endregion

namespace Navy.Core.Logger
{
    /// <summary>
    ///     Abstract Logger
    /// </summary>
    [Serializable]
    public abstract class LoggerBase : MarshalByRefObject, ILogger
    {
        private readonly int _processId = Process.GetCurrentProcess().Id;
        private LogType _level = LogType.Info;

        public string TextFormat { get; set; } = "{0:HH:mm:ss.fff}  {1,4:X} {2} {3}";

        public virtual LogType Level
        {
            get => _level;
            set => _level = value;
        }

        public void WriteLine(string text, params object[] args)
        {
            WriteLine(LogType.Info, text, args);
        }

        public void Warning(string text, params object[] args)
        {
            WriteLine(LogType.Warning, text, args);
        }

        public void Error(string text, params object[] args)
        {
            WriteLine(LogType.Error, text, args);
        }

        public void Error(Exception err)
        {
            WriteLine(LogType.Error, err.ToString());
        }

        public void Trace(string text, params object[] args)
        {
            WriteLine(LogType.Trace, text, args);
        }

        public void Debug(string text, params object[] args)
        {
            WriteLine(LogType.Debug, text, args);
        }

        public virtual void WriteLine(LogType level, string text, params object[] args)
        {
            if (level > Level)
                return;
            var txt = args.Length == 0
                ? text
                : string.Format(text, args);
            var message = CreateTextFormat(txt, level);
            WriteText(message);
        }

        public void WriteFormattedLine(string text)
        {
            WriteText(text);
        }

        protected abstract void WriteText(string text);

        private string CreateTextFormat(string text, LogType txtType)
        {
            var txt = "[INF]";
            if (txtType == LogType.Debug)
                txt = "[DBG]";
            else if (txtType == LogType.Error)
                txt = "[ERR]";
            else if (txtType == LogType.Trace)
                txt = "[TRC]";
            else if (txtType == LogType.Warning)
                txt = "[WRN]";
            var date = DateTime.Now;
            var thread = Thread.CurrentThread.ManagedThreadId;
            return string.Format(TextFormat, date, thread, txt, text, _processId);
        }
    }
}