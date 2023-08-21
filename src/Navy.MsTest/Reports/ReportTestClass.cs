using System;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Navy.Core.Extensions;
using Navy.Core.Logger;
using Navy.Test.Reports.Allure;
using Navy.Test.Reports.Navy;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Navy.MsTest.Reports
{
    public abstract class ReportTestClass : TestClass
    {
        public const string AllureFileName = "alluretestcase.xml";

        public const string AllureFolderResultSuffix = "_testplan";
        public TestReport Report { get; set; }

        [TestInitialize]
        public void ReportTestInit()
        {
            var multilogger = (MultiLogger) Logger;
            Report = new TestReport(TestContext.FullyQualifiedTestClassName, NullLogger.Instance);
            var logger = new TestReportLogger(Report);
            multilogger.Loggers.Add(logger);
        }


        volatile bool _reportTestCleaned;
        [TestCleanup]
        public void ReportTestCleanUp()
        {
            if (_reportTestCleaned)
                return;
            _reportTestCleaned = true;
            try
            {
                var teststatus = AllureFactory.GetAllureResultFromMsTest(TestContext.CurrentTestOutcome.ToString());
                SetDefaultTitle();
                Report.Finish(teststatus);
                SaveXmlReport();
                SaveHtmlReport();
            }
            catch(Exception err)
            {
                Logger.Error($"ReportTestCleanUp: {err}");
            }
        }

        private void SetDefaultTitle()
        {
            var name = TestContext.FullyQualifiedTestClassName + "." + TestContext.TestName;
            var title = name;
            if (TestContext.Properties.Contains(Description))
                title = (string)TestContext.Properties[Description];
            if (Report.AllureReport?.testcases.Length > 0)
            {
                var test = Report.AllureReport.testcases[0];
                if (string.IsNullOrWhiteSpace(test.name))
                    test.name = name;
                if (string.IsNullOrWhiteSpace(test.title))
                    test.title = title;
                return;
            }

            Report.Scenario(title);
        }

        private void SaveHtmlReport()
        {
            var htmlFile = OutputPath("alluretestcase.html");
            var html = TestReportFactory.ToHtml(Report);
            File.WriteAllText(htmlFile, html);
            TestContext.AddResultFile(htmlFile);
        }

        protected string AllureReportFile; 

        protected string UiActionsReportFile;

        private void SaveXmlReport()
        {
            var xmlFile = OutputPath(AllureFileName);
            AllureReportFile = xmlFile;
            var xml = XElement.Parse(Report.AllureReport.XmlSerialize());
            new XDocument(xml).Save(xmlFile);
            TestContext.AddResultFile(xmlFile);
        }
    }
}