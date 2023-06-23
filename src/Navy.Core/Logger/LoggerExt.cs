#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

#endregion

namespace Navy.Core.Logger
{
    public static class LoggerExt
    {
        public static void SetTextFormat(this ILogger logger, string format)
        {
            if (logger is MultiLogger multiLogger)
                multiLogger.TextFormat = format;
            if (logger is LoggerBase loggerBase)
                loggerBase.TextFormat = format;
        }

        public static void ShowStartInfo(this ILogger logger, string[] modulesFilter = null,
            Assembly[] assemblyList = null)
        {
            logger.WriteLine("-------------");
            logger.WriteLine("Start Program");
            logger.WriteLine(Environment.CommandLine);
            logger.WriteLine("Process type: {0}", Environment.Is64BitProcess ? "x64" : "x86");
            logger.WriteLine("Modules:");
            var proc = Process.GetCurrentProcess();
            var modules = new SortedDictionary<string, string>
            {
                {
                    proc.MainModule.FileName.ToLower(),
                    GetInfoString(proc.MainModule.FileName, proc.MainModule.FileVersionInfo)
                }
            };
            foreach (ProcessModule m in proc.Modules)
            {
                if (proc.MainModule.FileName == m.FileName || modules.ContainsKey(m.FileName.ToLower()))
                    continue;
                if (modulesFilter != null)
                    if (modulesFilter.Any(p =>
                        m.FileName.ToLower().IndexOf(p.ToLower(), StringComparison.Ordinal) >= 0))
                        continue;
                try
                {
                    modules.Add(m.FileName.ToLower(), GetInfoString(m.FileName, m.FileVersionInfo));
                }
                catch
                {
                    // 
                }
            }

            if (assemblyList != null)
                foreach (var assembly in assemblyList)
                {
                    if (modules.ContainsKey(assembly.Location.ToLower()))
                        continue;
                    try
                    {
                        var fileVer = FileVersionInfo.GetVersionInfo(assembly.Location);
                        modules.Add(assembly.Location.ToLower(), GetInfoString(assembly.Location, fileVer));
                    }
                    catch
                    {
                        // 
                    }
                }

            foreach (var info in modules)
                logger.WriteLine(info.Value);
        }

        private static string GetInfoString(string filename, FileVersionInfo fileInfo)
        {
            string processName;
            var fv = "?";
            var pv = "?";
            try
            {
                processName = fileInfo.FileName;
                fv = fileInfo.FileVersion;
                pv = fileInfo.ProductVersion;
            }
            catch
            {
                processName = filename;
            }
            return $"{processName}. File version {fv}. Product version {pv}";
        }
    }
}