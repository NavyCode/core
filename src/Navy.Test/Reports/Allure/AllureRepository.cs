using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Navy.Core.Extensions;

namespace Navy.Test.Reports.Allure
{
    /// <summary>
    ///     The base class for the Allure adapter.
    /// </summary>
    public class AllureRepository
    {
        public void Save(string file, testsuiteresult testSuite)
        {
            var xelem = testSuite.XmlSerialize();
            var xdoc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                XElement.Parse(xelem)
            );
            Directory.CreateDirectory(Path.GetDirectoryName(file));
            File.Delete(file);
            using (var stream = new FileStream(file, FileMode.OpenOrCreate))
            using (var sw = new StreamWriter(stream, Encoding.UTF8))
            {
                using (var w = XmlWriter.Create(sw, new XmlWriterSettings
                {
                    Indent = false,
                    IndentChars = "",
                    NewLineHandling = NewLineHandling.None,
                    Encoding = Encoding.UTF8
                }))
                {
                    xdoc.Save(w);
                }
            }
        }
    }
}