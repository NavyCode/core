using System;

namespace Navy.MsTest.Reports
{ 
    public static class ITestReportExt
    {
        public static ITestReport Test(this ITestReport report, string text)
        {
            return report.Scenario(text);
        }
         
        public static ITestReport Step(this ITestReport report, string text)
        {
            return report.When(text);
        }

        public static ITestReport Expect(this ITestReport report, string text)
        {
            return report.Then(text);
        }
    }
}