#region
 
using System;
using System.IO;
using System.Reflection;
using System.Text;

#endregion

namespace Navy.Core.Logger
{
    /// <summary>
    ///     FileStream Logger
    /// </summary>
    public class FileLogger : LoggerBase, IDisposable
    {
        public static FileLogger Instance = new FileLogger();

        private static TimeSpan _maxLogTime = TimeSpan.FromDays(31); 
        /// <summary>
        ///     Application Name
        /// </summary>
        private string _application;

        /// <summary>
        ///     Date write
        /// </summary>
        private DateTime _dateWrite = new DateTime();

        private bool _disposed;

        /// <summary>
        ///     File
        /// </summary>
        private string _fileFullName;

        /// <summary>
        ///     File stream
        /// </summary>
        private FileStream _fileStreamFile;

        /// <summary>
        ///     Directory
        /// </summary>
        private string _startupPath;

        /// <summary>
        ///     Stream Writer
        /// </summary>
        private StreamWriter _streamWriterStream;

        bool _addTime;

        public static string AssemblyDirectory => Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);


        /// <summary>
        ///     FileStream Logger
        /// </summary>
        /// <param name="application">Application</param>
        /// <param name="directory">directory</param>
        /// <param name="isNew">Clear in start</param>
        public FileLogger(string application = "", string directory = "", bool isNew = false, bool addTimeToFile = true)
        {
            _startupPath = !string.IsNullOrWhiteSpace(directory)
                ? directory
                : Path.Combine(AssemblyDirectory, "Log");
            _application = application;
            _addTime = addTimeToFile;
            if (string.IsNullOrWhiteSpace(_application))
            {
                var assemblity = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
                _application = assemblity.ManifestModule.Name;
                var indexPoint = _application.LastIndexOf('.');
                if (indexPoint > 0)
                    _application = _application.Substring(0, indexPoint);
            }
            _fileFullName = GetFileFullPath();
            try
            {
                if (isNew)
                    File.Delete(_fileFullName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine($"Error log '{_fileFullName}' deleting: {ex.Message} ");
            }
        }

        private string GetFileFullPath()
        {
            var result = $"{Path.Combine(_startupPath, Application)}.";
            if (_addTime)
                result += $"{DateTime.Now:yyyyMMdd}.";
            result += "log";
            return result;
        }

        /// <summary>
        ///     Application
        /// </summary>
        public string Application
        {
            get => _application;
            set
            {
                if (!string.IsNullOrEmpty(value) && _application != value)
                {
                    _application = value;
                    RemapStream();
                }
            }
        }

        /// <summary>
        ///     Directory
        /// </summary>
        public string StartupPath
        {
            get => _startupPath;
            set
            {
                if (!string.IsNullOrEmpty(value) && _startupPath != value)
                {
                    _startupPath = value;
                    RemapStream();
                }
            }
        }

        public void Dispose()
        {
            _disposed = true;
            if (_fileStreamFile != null)
            {
                _streamWriterStream.Dispose();
                _fileStreamFile.Dispose();
            }
        }

        /// <summary>
        ///     Clear
        /// </summary>
        public void Clear()
        {
            if (_streamWriterStream == null)
                RemapStream();
            _streamWriterStream.Close();
            _fileStreamFile.Close();
            _fileStreamFile.Dispose();
            _streamWriterStream.Dispose();
            File.Delete(_fileFullName);
            RemapStream();
        }


        protected override void WriteText(string text)
        {
            if (_disposed)
                return;
            try
            {
                var date = DateTime.Now;
                if (_streamWriterStream == null || _dateWrite.Day != date.Day)
                    RemapStream();
                if (_streamWriterStream == null)
                    return;
                lock (_streamWriterStream)
                {
                    _streamWriterStream.WriteLine(text);
                }
            }
            catch (Exception ex)
            {
                _streamWriterStream = null;
                System.Diagnostics.Trace.WriteLine("Error of log writing - " + ex);
            }
        }

        /// <summary>
        ///     Start new file writing
        /// </summary>
        private void RemapStream()
        {
            try
            { 
                Directory.CreateDirectory(_startupPath); 
                _fileFullName = GetFileFullPath();
                ClearFolder();
                _fileStreamFile = new FileStream(_fileFullName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                _streamWriterStream = new StreamWriter(_fileStreamFile, Encoding.Default) {AutoFlush = true};
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("Error of creating directory - " + ex);
            }
        }

        private void ClearFolder()
        {
            foreach (var file in Directory.EnumerateFiles(_startupPath, Application + "*.log"))
            {
                var info = new FileInfo(file);
                if (info.IsReadOnly)
                    continue;
                if (_dateWrite - info.LastWriteTime > _maxLogTime)
                    File.Delete(file);
            }
        }
    }
}