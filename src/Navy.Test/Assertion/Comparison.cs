#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Navy.Core.Assertion;
using Navy.Core.Assertion;

#endregion

namespace Navy.Test.Assertion
{
    /// <summary>
    ///     Сomparison of objects, files
    /// </summary>
    public class Comparison
    {
        public static void AssertFiles(string expectedPath, string actualPath, Encoding encoding)
        {
            Asserts.IsTrue(File.Exists(expectedPath), $"Expected file '{expectedPath}' not found");
            Asserts.IsTrue(File.Exists(actualPath), $"Actual file '{actualPath}' not found");
            var actualLines = NotNullLines(File.ReadLines(actualPath, encoding));
            var expectedLines = NotNullLines(File.ReadLines(expectedPath, encoding)).ToList();
            var errSuffix = $"in files '{expectedPath}' and '{actualPath}' not equals";
            var actualCount = 0;
            foreach (var actualLine in actualLines)
            {
                var expectedLine = expectedLines.Count > actualCount ? expectedLines[actualCount] : string.Empty;
                Asserts.AreEqual(expectedLine.Trim(), actualLine.Trim(), $"Line ~{actualCount} {errSuffix}");
                actualCount++;
            }

            Asserts.AreEqual(expectedLines.Count, actualCount, $"Error in count of nonempty lines {errSuffix}");
        }


        private static IEnumerable<string> NotNullLines(IEnumerable<string> strings)
        {
            var result = new List<string>();
            foreach (var item in strings)
            {
                var remove = item.Replace('\t', ' ');
                if (!string.IsNullOrWhiteSpace(remove))
                    result.Add(remove);
            }

            return result;
        }

        /// <summary>
        ///     Comparison of 2 objects
        /// </summary>
        /// <param name="expected">expected</param>
        /// <param name="actual">actual</param>
        /// <param name="filters">filtered</param>
        public static void Assert(object expected, object actual, PropertyInfo[] filters = null)
        {
            Assert(expected, actual, filters, new HashSet<object>());
        }

        private static void Assert(object expected, object actual, PropertyInfo[] filters, HashSet<object> cache)
        {
            if (expected == null && actual == null)
                return;
            Asserts.IsNotNull(expected, "expected is Null Object");
            Asserts.IsNotNull(actual, "actual is Null Object");
            var expectedType = expected.GetType();
            var actualType = actual.GetType();
            Asserts.AreEqual(expectedType, actualType, "Error in objects types");
            // if class type is native i.e. string, int, boolean, etc. 
            if (Convert.GetTypeCode(expected) == TypeCode.Object)
            {
                if (InCache(cache, expected))
                    return;
                if (expectedType.IsArray)
                {
                    var expectedArray = (Array) expected;
                    var actualArray = (Array) actual;
                    Asserts.AreEqual(expectedArray.Length, actualArray.Length, "Array length");
                    for (var count = 0; count < expectedArray.Length; count++)
                    {
                        var innerMessage = "Error in array " + expectedType.Name + " [" + count + "]";
                        AssertWithMessage(expectedArray.GetValue(count), actualArray.GetValue(count), filters, cache,
                            innerMessage);
                    }
                }
                else if (expectedType.IsClass)
                {
                    var expectedProps = expectedType.GetProperties().OrderBy(p => p.Name).ToArray();
                    var actualProps = actualType.GetProperties().OrderBy(p => p.Name).ToArray();
                    for (var count = 0; count < expectedProps.Length; count++)
                    {
                        var prop = expectedProps[count];
                        if (filters != null && filters.Contains(prop))
                            continue;
                        //check if it is an indexed object -- special case
                        var key = prop.GetGetMethod().GetParameters().FirstOrDefault();
                        if (key != null)
                        {
                            if (key.ParameterType == typeof(int))
                            {
                                var indexersTotal = (int) expectedProps.First(t => t.Name == "Count")
                                    .GetValue(expected, null);
                                for (var curIndex = 0; curIndex < indexersTotal; curIndex++)
                                {
                                    var expectedObject = prop.GetValue(expected, new object[] {curIndex});
                                    var actualObject = actualProps[count].GetValue(actual, new object[] {curIndex});
                                    var innerMessage = "in array " + expectedType.Name + " [" + curIndex + "]: ";
                                    AssertWithMessage(expectedObject, actualObject, filters, cache, innerMessage);
                                }
                            }
                            else
                            {
                                throw new NotSupportedException($"{key.ParameterType} in collection not supported");
                            }
                        }
                        else //not an indexed object
                        {
                            var expectedObject = prop.GetValue(expected, null);
                            var actualObject = actualProps[count].GetValue(actual, null);
                            var innerMessage = "Field: '" + prop.DeclaringType.Name + "." + prop.Name + "'";
                            AssertWithMessage(expectedObject, actualObject, filters, cache, innerMessage);
                        }
                    }
                }
                else
                {
                    Asserts.AreEqual(expected, actual);
                }

                cache.Remove(expected);
            }
            else if (expected is double doubleExpected && actual is double doubleActual)
            {
                Asserts.AreEqual(doubleExpected, doubleActual, 0.0001);
            }
            else
            {
                Asserts.AreEqual(expected, actual);
            }
        }

        private static bool InCache(HashSet<object> cache, object expected)
        {
            if (cache.Contains(expected))
                return true;
            cache.Add(expected);
            return false;
        }

        private static void AssertWithMessage(object expectedObject, object actualObject, PropertyInfo[] filters,
            HashSet<object> cache, string innerMessage)
        {
            try
            {
                Assert(expectedObject, actualObject, filters, cache);
            }
            catch (Exception err)
            {
                throw new AssertException($"{innerMessage}. {err.Message}", err);
            }
        }
    }
}