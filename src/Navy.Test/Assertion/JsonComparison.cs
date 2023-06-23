using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Navy.Core.Assertion;
using Newtonsoft.Json.Linq;
//using Navy.Core.Assertion;

namespace Navy.Test.Assertion
{
    public class JsonComparisonInfo
    {
        public JsonComparisonInfo(JToken expected, JToken actual, ConformityType conformity = ConformityType.Full) 
        {
            Expected = expected;
            Actual = actual;
            Conformity = conformity;
        }

        public JsonComparisonInfo(string expected, string actual)
        {
            Expected = JToken.Parse(expected);
            Actual = JToken.Parse(actual);
            IsEquals = true;
            Filters = Array.Empty<string>();
        }

        public JsonComparisonInfo(string expected, string actual, ConformityType conformity) : this(expected, actual)
        {
            Conformity = conformity;
        }
        public JToken Expected { get; set; }
        public JToken Actual { get; set; }
        public ICollection<string> Filters { get; set; }
        public string Result { get; set; }
        public ConformityType Conformity { get; set; } = ConformityType.Full;
        public CompareResultType BreakResult { get; set; }
        public bool IsEquals { get; set; }

        internal void AddComment(string str, int level = 0)
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

    /// <summary>
    /// Comparison of 2 Json of Files
    /// </summary>
    public static class JsonComparison
    {
        public static bool Equals(string expected, string actual)
        {
            var info = new JsonComparisonInfo(expected, actual);
            return Equals(info);
        }

        public static bool Equals(JsonComparisonInfo info)
        {
            return Compare(info);
        }

        public static void Assert(JsonComparisonInfo info)
        {
            Asserts.IsTrue(Equals(info), info.Result);
        }

        /// <summary>
        /// Compare 2 Json of Files
        /// </summary>
        static bool Compare(JsonComparisonInfo info)
        {
            var expected = info.Expected.DeepClone();
            var actual = info.Actual.DeepClone();
            RemoveCrossJoinObjects(expected, actual, info.Conformity);

            var fullData = info.Conformity == ConformityType.Full || info.Conformity == ConformityType.None;
            var result = true;
            if ((fullData || info.Conformity == ConformityType.Expected) 
                    && (expected.HasValues || (expected.Root is JValue ejval && ejval.Value != null)))
            {
                info.AddComment("Json expected has not finded result");
                result = false;
            }
            if ((fullData || info.Conformity == ConformityType.Actual) 
                    && (actual.HasValues || (actual.Root is JValue ajval && ajval.Value != null)))
            {
                info.AddComment("Json actual has not finded result");
                result = false;
            }
             
            if (!result)
            {
                Trace.WriteLine("expected");
                ShowAll(expected);
                Trace.WriteLine("actual");
                ShowAll(actual);
            }

            return result;
        }


        /// <summary>
        /// Compare 2 Json of Files
        /// </summary>
        static void ShowAll(IEnumerable<JToken> expected, int level = 0)
        {
            level++;
            string tab = Tab(level);
            foreach (var item in expected)
            {
                if (item is JObject obj)
                    ShowAll(obj, level);
                else if (item is JProperty prop)
                {
                    if (prop.Value is JValue propVal)
                        Trace.WriteLine($"{tab} {prop.Name} = {propVal.Value}");
                    else
                    {
                        Trace.WriteLine($"{tab} {prop.Value.GetType().Name} {prop.Name}");
                        ShowAll(new[] { prop.Value }, level);
                    }
                }
                else if (item is JArray array)
                {
                    var stringValues = string.Join(", ", array.Children<JValue>().Select(p => p.Value));
                    Trace.WriteLine($"{tab} Array: " + stringValues);
                    foreach (var child in array.Children().Where(p => !(p is JValue)))
                    {
                        ShowAll(child, level);
                        Trace.WriteLine($"{Tab(level + 1)} ----");
                    }
                }
                else
                {
                    throw new NotSupportedException(item.GetType().Name);
                }
            }
        }

        private static string Tab(int level)
        {
            return new string(' ', level * 3);
        }

        static void RemoveCrossJoinObjects(IEnumerable<JToken> expected, IEnumerable<JToken> actual, ConformityType conformity, string elementName = "", int level = 0)
        {
            level++;
            var tab = new string(' ', level * 3);
            level++;
            if (expected is JToken expTocken && actual is JToken actTocken)
            {
                if (JToken.DeepEquals(expTocken, actTocken))
                {
                    FullRemove(expTocken, actTocken);
                }
            }
            foreach (var val in expected.OfType<JValue>().ToArray())
            {
                var actualValue = actual.Values<JValue>().FirstOrDefault(p => JToken.DeepEquals(val, p));
                if (actualValue != null)
                    Remove(val, actualValue);
            }
            foreach (var extProp in expected.OfType<JProperty>().ToArray())
            {
                var actualProp = actual.OfType<JProperty>().FirstOrDefault(p => p.Name == extProp.Name);
                if (actualProp == null)
                    continue;
                if (extProp.Value is JValue)
                {
                    if (JToken.DeepEquals(extProp, actualProp))
                        Remove(extProp, actualProp);
                }
                else
                {
                    RemoveCrossJoinObjects(new[] { extProp.Value }, new[] { actualProp.Value }, conformity, elementName, level);
                    if (JToken.DeepEquals(extProp, actualProp))
                    {
                        FullRemove(extProp, actualProp);
                        continue;
                    }
                }
            }
            foreach (var obj in expected.OfType<JObject>().ToArray())
            {
                foreach (var actObj in actual.OfType<JObject>().ToArray())
                    RemoveCrossJoinObjects(obj, actObj, conformity, elementName, level);
            }
            foreach (var extArray in expected.OfType<JArray>())
            {
                var extArrayHasValues = extArray.Count > 0;
                foreach (var actArray in actual.OfType<JArray>())
                {
                    var actArrayHasValues = actArray.Count > 0;
                    foreach (var extChild in extArray.Children().ToArray())
                        foreach (var actChild in actArray.Children().ToArray())
                        {
                            if (JToken.DeepEquals(extChild, actChild))
                            {
                                Remove(extChild, actChild);
                                break;
                            }
                        }
                    if (actArrayHasValues && !actArray.Any())
                        Remove(actArray);
                }
                if (extArrayHasValues && !extArray.Any())
                    Remove(extArray);
            }
            if (conformity == ConformityType.Expected)
                CleanUp(expected);
            if (conformity == ConformityType.Actual)
                CleanUp(actual);
        }

        private static void CleanUp(IEnumerable<JToken> expected)
        {
            if (expected is JToken expTocken && !expected.Any())
            {
                FullRemove(expTocken);
            }
            foreach (var val in expected.OfType<JValue>().Where(p => !p.Any()))
            {
                FullRemove(val);
            }
            foreach (var extProp in expected.OfType<JProperty>().Where(p => !p.Any()))
            {
                FullRemove(extProp);
            }
            foreach (var obj in expected.OfType<JObject>().Where(p => !p.HasValues))
            {
                FullRemove(obj);
            }
            foreach (var extArray in expected.OfType<JArray>())
            {
                if (!extArray.Any())
                    Remove(extArray);
            }
        }

        private static void Remove(params JToken[] tockens)
        {
            foreach (var tocken in tockens)
            {
                if (tocken.Root == tocken && tocken is JValue jroot)
                    jroot.Value = null;
                if (tocken.Parent is JProperty prop)
                    Remove(prop);
                else if (tocken.Parent != null)
                    tocken.Remove();
            }
        }

        private static void FullRemove(params JToken[] tockens)
        {
            foreach (var tocken in tockens)
            {
                Remove(tocken);
                foreach (var child in tocken.Children().ToArray())
                    Remove(child);
            }
        }
    }
}
