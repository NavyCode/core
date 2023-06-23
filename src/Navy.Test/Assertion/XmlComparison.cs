#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Navy.Core.Assertion;

#endregion

namespace Navy.Test.Assertion
{
    /// <summary>
    ///     Comparison of 2 xml of Files
    /// </summary>
    public class XmlComparison
    {
        public static bool Equals(XElement expected, XElement actual)
        {
            var info = new XmlComparisonInfo(expected, actual);
            return Equals(info);
        }

        public static bool Equals(XElement expected, XElement actual, ConformityType conformity)
        {
            var info = new XmlComparisonInfo(expected, actual)
            {
                Conformity = conformity
            };
            return Equals(info);
        }

        public static bool Equals(XmlComparisonInfo info)
        {
            return Compare(info, 0);
        }

        public static void Assert(XElement expected, XElement actual)
        {
            var info = new XmlComparisonInfo(expected, actual);
            Asserts.IsTrue(Equals(info), info.Result);
        }

        public static void Assert(XmlComparisonInfo info)
        {
            Asserts.IsTrue(Equals(info), info.Result);
        }


        public static void Assert(XElement expected, XElement actual, ConformityType conformity)
        {
            var info = new XmlComparisonInfo(expected, actual)
            {
                Conformity = conformity
            };
            Asserts.IsTrue(Equals(info), info.Result);
        }

        /// <summary>
        ///     Compare 2 xml of Files
        /// </summary>
        private static bool Compare(XmlComparisonInfo info, int level)
        {
            try
            {
                var expectedAttribures = info.Expected.Attributes().ToList();
                var actualAttribures = info.Actual.Attributes().ToList();
                level++;
                AssertJoinedAttributes(info, level, expectedAttribures, actualAttribures, info.Expected.Name.LocalName);
                var leftJoin = (ConformityType.Full | ConformityType.Expected).HasFlag(info.Conformity);
                var rightJoin = (ConformityType.Full | ConformityType.Actual).HasFlag(info.Conformity);
                if (leftJoin)
                    AssertLeftJoinAttributes(info, level, expectedAttribures, actualAttribures, true);
                if (rightJoin)
                    AssertLeftJoinAttributes(info, level, actualAttribures, expectedAttribures, false);
                var expectedElems = info.Expected.Elements().ToList();
                var actualElems = info.Actual.Elements().ToList();
                if (expectedElems.Count == 0 && actualElems.Count == 0)
                {
                    if (!info.Filters.Contains(info.Expected.Name.LocalName) && info.Expected.Value != info.Actual.Value)
                        info.SetFailed($"{info.Expected.Value} != {info.Actual.Value} ", level);
                }
                else
                {
                    if (info.Conformity == ConformityType.Full && expectedElems.Count != actualElems.Count)
                        info.SetFailed(
                            $"Children count {info.Expected.Name} doesn't coincide. {expectedElems.Count} != {actualElems.Count}",
                            level);
                    if (leftJoin)
                        CompareLeftJoinedElements(info, level, expectedElems, actualElems);
                    if (rightJoin)
                        CompareLeftJoinedElements(info, level, actualElems, expectedElems);
                    level--;
                }
            }
            catch (Exception err)
            {
                info.SetFailed("Error files comparison => " + err.Message, level);
                return false;
            }

            return info.IsEquals;
        }

        private static void CompareLeftJoinedElements(XmlComparisonInfo info, int level, List<XElement> expectedElems,
            List<XElement> actualElems)
        {
            var i = 0;
            foreach (var item in expectedElems)
            {
                i++;
                var finded = false;
                XmlComparisonInfo firstResult = null;
                var j = 0;
                foreach (var actual in actualElems.Where(p => p.Name.Equals(item.Name)))
                {
                    j++;
                    var childInfo = new XmlComparisonInfo(item, actual)
                    {
                        Filters = info.Filters,
                        Conformity = info.Conformity,
                        BreakResult = info.BreakResult
                    };
                    Compare(childInfo, level + 1);
                    if (firstResult == null)
                        firstResult = childInfo;
                    finded = childInfo.IsEquals;
                    if (finded)
                        break;
                }

                if (!finded)
                    info.SetFailed(
                        firstResult == null
                            ? $"Can not find element '{item}'"
                            : string.Format("{0} [{2} in {3}]: {1}", item.Name, firstResult.Result, i, j), level);
            }
        }

        private static void AssertLeftJoinAttributes(XmlComparisonInfo info, int level,
            List<XAttribute> expectedAttribures, List<XAttribute> actualAttribures, bool isLeftTemplate)
        {
            foreach (var a in expectedAttribures)
            {
                var e = actualAttribures.FirstOrDefault(p => p.Name == a.Name);
                if (e != null)
                    continue;
                var template = isLeftTemplate ? "actual" : "expected";
                if(!info.Filters.Contains(info.Expected.Name.LocalName + "." + a.Name.LocalName))
                    info.SetFailed($"Attribute '{a.Name.LocalName}' is not found in '{template}'", level);
            }
        }

        private static void AssertJoinedAttributes(XmlComparisonInfo info, int level,
            List<XAttribute> expectedAttributes, List<XAttribute> actualAttributes, string elementName)
        {
            if (info.Filters.Contains(elementName))
                return;

            var lq = from e in expectedAttributes
                join a in actualAttributes
                    on e.Name equals a.Name
                select new {e, a};

            foreach (var item in lq)
            {
                var e = item.e;
                var a = item.a;
                var localName = elementName + "." + e.Name.LocalName;

                if (info.Filters.Contains(localName))
                    continue;
                if (e.Value != a.Value)
                    info.SetFailed($"Attribute {e.Name.LocalName}: {e.Value} != {a.Value}", level);
            }
        }
    }
}