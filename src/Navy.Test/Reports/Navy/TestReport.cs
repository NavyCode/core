using System;
using System.Linq;
using Navy.Core.Logger;
using Navy.Test.Reports.Allure;

namespace Navy.Test.Reports.Navy
{
    public class TestReport : ITestReport
    {
        private readonly ILogger _logger;


        public TestReport(string name, ILogger logger)
        {
            AllureReport = new testsuiteresult
            {
                name = name,
                title = name,
                start = Now.ToUnixEpochTime(),
                description = new description {type = descriptiontype.text, Value = ""},
                testcases = new testcaseresult[0]
            };
            _logger = new PrefixLogger(logger ?? NullLogger.Instance, "[report] ");
            //_logger = NullLogger.Instance; 
        }

        public static TestReport Empty { get; } = new TestReport("", NullLogger.Instance);

        internal testsuiteresult AllureReport { get; } = new testsuiteresult();

        private testcaseresult _lastTestCaseResultResult => AllureReport.testcases?.LastOrDefault();
        private step _lastStepResult => _lastTestCaseResultResult?.steps?.LastOrDefault();

        private DateTime Now => DateTime.Now; 

        [Obsolete("use Scenario")]
        public ITestReport Given(string text) => Scenario(text);

        public ITestReport AndDescription(string text)
        {
            _logger.WriteLine($"And '{text}'");
            var lastTestCaseResultResult = GetLastTestCaseResultResult();
            lastTestCaseResultResult.description.Value = ConcatString(lastTestCaseResultResult.description.Value, text);
            return this;
        }

        public ITestReport When(string text)
        {
            if (_lastStepResult?.steps.Length == 0)
            {
                And(text);
                return this;
            }

            _logger.WriteLine($"When '{text}'");
            var step = new step
            {
                name = text,
                start = Now.ToUnixEpochTime(),
                status = status.pending,
                title = text,
                steps = new step[0]
            };
            FinishLastStep();
            var lastTestCaseResultResult = GetLastTestCaseResultResult();
            lastTestCaseResultResult.steps = lastTestCaseResultResult.steps.Union(new[] {step}).ToArray();
            return this;
        }

        public ITestReport And(string text)
        {
            _logger.WriteLine($"And '{text}'");
            var lastStepResult = GetLastStepResult();
            if (lastStepResult.steps.Length == 0)
                lastStepResult.title = ConcatString(lastStepResult.title, text);
            else
                lastStepResult.steps[lastStepResult.steps.Length - 1].title =
                    ConcatString(lastStepResult.steps[lastStepResult.steps.Length - 1].title, text);
            return this;
        }

        public ITestReport Then(string text)
        {
            _logger.WriteLine($"Then '{text}'");
            var step = new step
            {
                name = text,
                start = Now.ToUnixEpochTime(),
                status = status.pending,
                title = text,
                steps = new step[0]
            };
            var lastStepResult = GetLastStepResult();
            lastStepResult.steps = lastStepResult.steps.Union(new[] {step}).ToArray();
            return this;
        }

        public ITestReport Fail(string error = null)
        {
            Finish(status.failed);
            Then("##Error: " + error);
            return this;
        }

        private testcaseresult GetLastTestCaseResultResult()
        {
            if (_lastTestCaseResultResult != null)
                return _lastTestCaseResultResult;
            Scenario("");
            return _lastTestCaseResultResult;
        }

        private step GetLastStepResult()
        {
            if (_lastStepResult != null)
                return _lastStepResult;
            When("");
            return _lastStepResult;
        }

        private void Add(testcaseresult testCaseResultResult)
        {
            FinishLastStep();
            FinishTestResult();
            AllureReport.testcases = AllureReport.testcases.Union(new[] {testCaseResultResult}).ToArray();
        }

        private void FinishTestResult(status status = status.passed)
        {
            if (_lastTestCaseResultResult == null)
                return;
            _lastTestCaseResultResult.status = status;
            _lastTestCaseResultResult.stop = Now.ToUnixEpochTime();
        }

        private void FinishLastStep(status status = status.passed)
        {
            if (_lastStepResult == null)
                return;
            if (status == status.passed && _lastStepResult.status == status.failed)
                return;
            _lastStepResult.status = status;
            _lastStepResult.stop = Now.ToUnixEpochTime();
        }

        private static string ConcatString(string text1, string text2)
        {
            if (string.IsNullOrWhiteSpace(text1))
                return text2;
            return text1 + AllureFactory.NewLine + text2;
        }

        internal void Finish(status status)
        {
            FinishLastStep(status);
            FinishTestResult(status);
        }

        public ITestReport Scenario(string text)
        {
            _logger.WriteLine($"Given '{text}'");
            var testCaseResult = new testcaseresult
            {
                name = text,
                title = text,
                start = Now.ToUnixEpochTime(),
                severity = severitylevel.normal,
                severitySpecified = false,
                status = status.pending,
                steps = new step[0],
                description = new description { type = descriptiontype.html, Value = "" }
            };
            Add(testCaseResult);
            return this;
        }
    }
}