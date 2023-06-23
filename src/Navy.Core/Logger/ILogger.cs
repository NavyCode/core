#region

using System;

#endregion

namespace Navy.Core.Logger
{
    /// <summary>
    ///     Log interface
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        ///     Level
        /// </summary>
        LogType Level { get; set; }

        /// <summary>
        ///     Write
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="args"></param>
        void WriteLine(string text, params object[] args);


        /// <summary>
        ///     Write
        /// </summary>
        /// <param name="level"></param>
        /// <param name="text">Text</param>
        /// <param name="args"></param>
        void WriteLine(LogType level, string text, params object[] args);

        /// <summary>
        ///     Write Warning
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="args"></param>
        void Warning(string text, params object[] args);

        /// <summary>
        ///     Write Error
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="args"></param>
        void Error(string text, params object[] args);

        /// <summary>
        ///     Write Error
        /// </summary>
        void Error(Exception err);

        /// <summary>
        ///     Write Trace
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="args"></param>
        void Trace(string text, params object[] args);

        /// <summary>
        ///     Write Debug
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="args"></param>
        void Debug(string text, params object[] args);
    }
}