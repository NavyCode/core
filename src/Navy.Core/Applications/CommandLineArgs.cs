#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

#endregion

namespace Navy.Core.Applications
{
    public class CommandLineArgs
    {
        private readonly Dictionary<string, string> _keys = new Dictionary<string, string>();

        public CommandLineArgs(string[] args)
            : this(string.Join(" ", args))
        {
        }

        public CommandLineArgs(string cmdLine)
        {
            var pattern = @"\/(?<Name>[a-z]+)[\: ]";
            var matches = Regex.Matches(cmdLine, pattern,
                RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);
            for (var i = 0; i < matches.Count; i++)
            {
                var match = matches[i];
                var key = match.Groups["Name"].Value.ToUpper();
                var valueStartIndex = match.Index + match.Length;
                var valueLength = matches.Count > i + 1
                    ? matches[i + 1].Index - valueStartIndex
                    : cmdLine.Length - valueStartIndex;
                var value = cmdLine.Substring(match.Index + match.Length, valueLength).Trim();
                _keys[key] = value;
            }
        }

        public int Count => _keys.Count;


        public T GetValue<T>(string arg)
        {
            if (!TryFindByName(arg.ToUpper(), out var strValue))
                return default(T);
            return Convert<T>(arg, strValue);
        }

        public T? FindValue<T>(string arg) where T : struct
        {
            if (!TryFindByName(arg.ToUpper(), out var strValue))
                return null;
            return Convert<T>(arg, strValue);
        }

        private static T Convert<T>(string arg, string strValue)
        {
            try
            {
                return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(strValue);
            }
            catch
            {
                throw new FormatException($"Can not convert '{arg}' == '{strValue}' to {typeof(T)}");
            }
        }

        public string GetValue(string arg)
        {
            return GetValue<string>(arg);
        }

        private bool TryFindByName(string arg, out string value)
        {
            arg = arg.ToUpper();
            value = null;
            if (!_keys.ContainsKey(arg))
                return false;
            value = _keys[arg];
            return true;
        }
    }
}