using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Navy.Core.Extensions;
using Navy.Test.Reports.Junit;
using Navy.Test.Reports.MsTest;

namespace Navy.Test.Reports.Allure
{
    public class AllureFactory
    {
        public const string NewLine = @"\n";

        public static testsuiteresult FromMsTest(TestRun testRun, string suiteName, string runUrl)
        {
            var tests = testRun.Results != null
                ? new List<TestRunUnitTestResult>(testRun.Results)
                : new List<TestRunUnitTestResult>();
            var testSuite = new testsuiteresult
            {
                name = testRun.name,
                title = $"{suiteName}.{testRun.Times.finish.ToString("dd.MM.yyyy")}",

                description = new description {type = descriptiontype.text, Value = ""}
            };
            if (tests.Count == 0)
            {
                var fakeTest = new TestRunUnitTestResult
                {
                    startTime = testRun.Times.start,
                    endTime = testRun.Times.finish,
                    testId = "0",
                    testName = "Запуск тестов",
                    outcome = testRun.ResultSummary.RunInfos.RunInfo.outcome,
                    computerName = testRun.ResultSummary.RunInfos.RunInfo.computerName,
                    Output = new TestRunUnitTestResultOutput
                    {
                        ErrorInfo = new TestRunUnitTestResultOutputErrorInfo
                        {
                            Message = testRun.ResultSummary.RunInfos.RunInfo.Text
                        },
                        StdOut = testRun.ResultSummary.RunInfos.RunInfo.Text
                    }
                };
                tests.Add(fakeTest);
            }

            testSuite.start = testRun.Times.start.ToUnixEpochTime();
            testSuite.stop = testRun.Times.finish.ToUnixEpochTime();
            var testResults = new List<testcaseresult>();
            foreach (var test in tests)
            {
                var allureTestResult = new testcaseresult
                {
                    name = test.testId,
                    title = test.testName,
                    start = test.startTime.ToUnixEpochTime(),
                    stop = test.endTime.ToUnixEpochTime(),
                    severity = severitylevel.normal,
                    severitySpecified = false,
                    status = GetAllureResultFromMsTest(test.outcome),
                    description = new description {type = descriptiontype.html, Value = ""}
                };
                var runLink = $"<a href=\"{runUrl}\">Запуск</a>";
                allureTestResult.description.Value =
                    $@"{runLink}<br>Тестер: {testRun.runUser}<br>Хост: {test.computerName}<br>" +
                    allureTestResult.description.Value;
                var testDef = testRun.TestDefinitions?.FirstOrDefault(p => p.id == test.testId);
                if (testDef != null)
                    allureTestResult.title = testDef?.TestMethod?.className + "." + allureTestResult.title;
                if (test.Output?.ErrorInfo != null)
                    allureTestResult.failure = new failure
                    {
                        message = test.Output?.ErrorInfo.Message,
                        stacktrace = test.Output?.ErrorInfo.StackTrace
                    };
                testResults.Add(allureTestResult);
            }

            testSuite.testcases = testResults.ToArray();

            return testSuite;
        }

        public static testsuiteresult FromJunit(testsuites junitReport)
        {
            var firstSuite = junitReport.testsuite.FirstOrDefault();
            var tests = junitReport.testsuite.SelectMany(p => p.testcase).ToArray();
            var testSuite = new testsuiteresult
            {
                name = firstSuite?.name,
                title = firstSuite?.name,
                start = firstSuite.timestamp.ToUnixEpochTime(),
                stop = firstSuite.timestamp.AddSeconds((double) firstSuite.time).ToUnixEpochTime(),
                description = new description {type = descriptiontype.text, Value = ""}
            };

            var testResults = new List<testcaseresult>();
            foreach (var test in tests)
            {
                var allureTestResult = new testcaseresult
                {
                    name = test.classname + "." + test.name,
                    title = test.classname + "." + test.name,
                    start = testSuite.start,
                    stop = firstSuite.timestamp.AddSeconds((double) test.time).ToUnixEpochTime(),
                    severity = severitylevel.normal,
                    severitySpecified = false,
                    status = GetAllureResultFromCatch2(test),
                    description = new description {type = descriptiontype.html, Value = ""}
                };
                if (test.error != null)
                    allureTestResult.failure = new failure
                    {
                        message = test.error.message + "; " + test.error.Value
                    };
                if (test.failure != null)
                    allureTestResult.failure = new failure
                    {
                        message = test.failure.message + "; " + test.failure.Value
                    };
                if (test.skipped != null)
                    allureTestResult.description.Value += test.skipped.Value;
                testResults.Add(allureTestResult);
            }

            testSuite.testcases = testResults.ToArray();

            return testSuite;
        }

        private static status GetAllureResultFromCatch2(testsuiteTestcase test)
        {
            if (test.error == null && test.failure == null && test.skipped == null)
                return status.passed;
            if (test.skipped != null)
                return status.skipped;
            return status.failed;
        }

        public static status GetAllureResultFromMsTest(string stroutcome)
        {
            Enum.TryParse<TestOutcome>(stroutcome, out var outcome);
            if (outcome == TestOutcome.Passed)
                return status.passed;
            if (outcome == TestOutcome.Inconclusive || outcome == TestOutcome.NotApplicable)
                return status.broken;
            if (outcome == TestOutcome.NotExecuted || outcome == TestOutcome.NotRunnable)
                return status.skipped;
            if (outcome == TestOutcome.Failed || outcome == TestOutcome.Blocked)
                return status.failed;
            return status.failed;
        }

        public static testsuiteresult FromBytes(byte[] bytes)
        {
            using var memory = new MemoryStream(bytes);
            var xdoc = XDocument.Load(memory);
            return xdoc.Root.XmlDeserialize<testsuiteresult>();
        }
    }
}