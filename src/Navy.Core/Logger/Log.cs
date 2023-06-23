#region

using System;

#endregion

namespace Navy.Core.Logger
{
    /// <summary>
    ///     Application Logger
    /// </summary>
    public static class Log
    {
        public static ILogger Logger { get; set; } = new NullLogger();

        public static LogType Level
        {
            get => Logger.Level;
            set => Logger.Level = value;
        }

        public static void WriteLine(string text, params object[] args)
        {
            Logger.WriteLine(text, args);
        }

        public static void Warning(string text, params object[] args)
        {
            Logger.WriteLine(LogType.Warning, text, args);
        }

        public static void Error(string text, params object[] args)
        {
            Logger.WriteLine(LogType.Error, text, args);
        }

        public static void Error(Exception err)
        {
            Logger.WriteLine(LogType.Error, err.ToString());
        }

        public static void Trace(string text, params object[] args)
        {
            Logger.WriteLine(LogType.Trace, text, args);
        }

        public static void Debug(string text, params object[] args)
        {
            Logger.WriteLine(LogType.Debug, text, args);
        }

        public static void ShowStartInfo()
        {
            Logger.ShowStartInfo();
        }
    }
}