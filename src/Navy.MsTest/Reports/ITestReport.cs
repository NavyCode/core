using System;

namespace Navy.MsTest.Reports
{
    public interface ITestReport
    {
        ITestReport And(string text);
        ITestReport Scenario(string text);
        [Obsolete("Use Scenario")]
        ITestReport Given(string text);
        ITestReport AndDescription(string text);
        ITestReport Then(string text);
        ITestReport When(string text);

        ITestReport Fail(string error = null);
    }
}