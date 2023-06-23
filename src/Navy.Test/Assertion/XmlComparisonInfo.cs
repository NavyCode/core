using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Navy.Test.Assertion
{
    public class XmlComparisonInfo
    {
        public XmlComparisonInfo()
        {
            IsEquals = true;
            Filters = new string[0];
        }

        public XmlComparisonInfo(XElement expected, XElement actual) : this()
        {
            Expected = expected;
            Actual = actual;
        }

        public XElement Expected { get; set; }
        public XElement Actual { get; set; }
        public ICollection<string> Filters { get; set; }
        public string Result { get; set; }
        public ConformityType Conformity { get; set; }
        public CompareResultType BreakResult { get; set; }
        public bool IsEquals { get; set; }

        internal void AddComment(string str, int level)
        {
            var strLog = new string(' ', level) + str;
            Result += strLog + Environment.NewLine;
        }

        internal void SetFailed(string str, int level)
        {
            if (!IsEquals && BreakResult == CompareResultType.None)
                return;
            AddComment("Error: " + str, level);
            IsEquals = false;
        }
    }
}