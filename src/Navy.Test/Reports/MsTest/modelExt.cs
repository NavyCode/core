using System.Collections.Generic;
using System.Xml.Serialization;

namespace Navy.Test.Reports.MsTest
{
    // Navy edit

    [XmlRoot(ElementName = "ResultFile", Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public class ResultFile
    {
        [XmlAttribute(AttributeName = "path")] public string Path { get; set; }
    }

    [XmlRoot(ElementName = "ResultFiles", Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public class ResultFiles
    {
        [XmlElement(ElementName = "ResultFile", Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
        public List<ResultFile> ResultFile { get; set; }
    }

    public partial class TestRunUnitTestResult
    {
        [XmlElement(ElementName = "ResultFiles", Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
        public ResultFiles ResultFiles { get; set; }
    }
}