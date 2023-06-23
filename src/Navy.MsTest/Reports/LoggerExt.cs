using System.Collections.Generic;
using System.Linq;
using Navy.Core.Logger;

namespace Navy.MsTest.Reports
{
    public static class LoggerExt
    {
        public static ILogger Scenario(this ILogger logger, string text)
        {
            foreach (var log in ReportLoggers(logger))
                log.Scenario(text);
            return logger;
        }


        private static IEnumerable<ITestReport> ReportLoggers(ILogger logger)
        {
            if (logger is TestReportLogger trl)
                yield return trl;
            if (logger is MultiLogger ml)
                foreach (var tr in ml.Loggers.OfType<ITestReport>())
                    yield return tr;
        }

        public static ILogger AndDesciption(this ILogger logger, string text)
        {
            foreach (var log in ReportLoggers(logger))
                log.AndDescription(text);
            return logger;
        }

        public static ILogger When(this ILogger logger, string text)
        {
            foreach (var log in ReportLoggers(logger))
                log.When(text);
            return logger;
        }

        public static ILogger And(this ILogger logger, string text)
        {
            foreach (var log in ReportLoggers(logger))
                log.And(text);
            return logger;
        }

        public static ILogger Then(this ILogger logger, string text)
        {
            foreach (var log in ReportLoggers(logger))
                log.Then(text);
            return logger;
        }
    }
}