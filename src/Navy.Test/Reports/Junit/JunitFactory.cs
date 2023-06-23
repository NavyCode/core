using System.Xml.Linq;
using Navy.Core.Extensions;

namespace Navy.Test.Reports.Junit
{
    public class JunitFactory
    {
        public static testsuites FromFile(string file)
        {
            var xdoc = XDocument.Load(file);
            var testResult = xdoc.Root.XmlDeserialize<testsuites>();
            return testResult;
        }

        public static testsuites FromBytes(byte[] bytes)
        {
            var testResult = bytes.XmlDeserialize<testsuites>();
            return testResult;
        }
    }
}