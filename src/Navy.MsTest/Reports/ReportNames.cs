using System.Collections.Generic;

namespace Navy.MsTest.Reports
{
    public class ReportNames
    {
        private readonly string _prefix;
        private Dictionary<object, string> _values = new Dictionary<object, string>();

        public ReportNames(string prefix = "Y")
        {
            _prefix = prefix;
        }

        public string this[object key]
        {
            get
            {
                if (_values.ContainsKey(key))
                    return _values[key];
                return _values[key] = $"{_prefix}{_values.Count + 1}";
            }
        }
    }
}