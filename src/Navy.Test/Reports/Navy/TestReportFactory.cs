using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace Navy.Test.Reports.Navy
{
    public class TestReportFactory
    {
        public static string ToHtml(TestReport report)
        {
            using (var resource = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("Navy.Test.Reports.Navy.ReportTemplate.html"))
            using (var reader = new StreamReader(resource))
            {
                var template = reader.ReadToEnd();
                var json = JsonConvert.SerializeObject(report.AllureReport)
                    .Replace("\\n", "<br>")
                    .Replace("\\r", "")
                    .Replace("'", "&apos;");
                var str = template.Replace("#allurereport", json);
                return str;
            }
        }
    }
}