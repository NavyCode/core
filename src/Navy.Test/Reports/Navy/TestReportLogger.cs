using System;
using Navy.Core.Logger;

namespace Navy.Test.Reports.Navy
{
    public class TestReportLogger : LoggerBase, ITestReport
    {
        private TestReport _report;

        public TestReportLogger(TestReport report)
        {
            _report = report;
        }

        public ITestReport And(string text)
        {
            return _report.And(text);
        }

        public ITestReport AndDescription(string text)
        {
            return _report.AndDescription(text);
        }

        public ITestReport Fail(string error = null)
        {
            return _report.Fail(error);
        }


        [Obsolete("use Scenario")]
        public ITestReport Given(string text) => Scenario(text);

        public ITestReport Scenario(string text)
        {
            return _report.Scenario(text);
        }

        public ITestReport Then(string text)
        {
            return _report.Then(text);
        }

        public ITestReport When(string text)
        {
            return _report.When(text);
        }

        protected override void WriteText(string text)
        {
        }
    }
}