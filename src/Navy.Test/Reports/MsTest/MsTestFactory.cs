using System.Xml.Linq;
using Navy.Core.Extensions;

namespace Navy.Test.Reports.MsTest
{
    public class MsTestFactory
    {
        public static TestRun FromFile(string file)
        {
            var xdoc = XDocument.Load(file);
            var testResult = xdoc.Root.XmlDeserialize<TestRun>();
            return testResult;
        }

        public static TestRun FromBytes(byte[] file)
        {
            var testResult = file.XmlDeserialize<TestRun>();
            return testResult;
        }
    }
}