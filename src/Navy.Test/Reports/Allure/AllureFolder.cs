using System;
using System.IO;
using Navy.Core.Logger; 

//using Navy.RemoteTest.Client;

//using Navy.RemoteTest.Client.Core;

namespace Navy.Test.Reports.Allure
{
    public class AllureFolder
    {
        private static string SuiteFileSuffix = "-testsuite.xml";
        private readonly ILogger _logger;
        public string Path;

        public AllureFolder(string reportFolder, ILogger logger)
        {
            Path = reportFolder;
            _logger = logger;
        }

        public void Save(testsuiteresult allureReport, string fileName)
        {
            var file = System.IO.Path.Combine(Path, $"{fileName}{SuiteFileSuffix}");
            _logger.WriteLine($"Save report '{allureReport.title}' to '{file}'");
            new AllureRepository().Save(file, allureReport);
        }

        public string Attach(string runId, string testId, AllureResultAttachment attachment)
        {
            var file = "";
            var localPath = "";
            var trying = 0;
            while (trying < 100)
            {
                var localFileName = $"{testId}_{trying:#}_{ReplaceIllagelCharts(attachment.Name)}";
                localPath = System.IO.Path.Combine(@"data", "attachments", DateTime.Now.ToString("yyyy.MM.dd"), runId,
                    localFileName);
                file = System.IO.Path.Combine(Path, localPath);
                if (!File.Exists(file))
                    break;
                trying++;
            }

            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(file));
            File.WriteAllBytes(file, attachment.Bytes);
            return localPath;
        }

        private string ReplaceIllagelCharts(string fileName)
        {
            return string.Concat(fileName.Split(System.IO.Path.GetInvalidFileNameChars()));
        }
    }
}