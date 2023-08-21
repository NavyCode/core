#region

using System.Runtime.CompilerServices;
using Microsoft.Playwright;
using Navy.Core.Logger; 
using Navy.Test.Reports.Navy;

#endregion

namespace Navy.Playwright
{
    public class DriverReporter
    {
        public ILogger Logger;
        public ITestReport Report;
    }

    public static class IBrowserExt
    {
        static readonly ConditionalWeakTable<IBrowser, DriverReporter> Flags = new ConditionalWeakTable<IBrowser, DriverReporter>();
        public static ITestReport Report(this IBrowser driver) { return Flags.GetOrCreateValue(driver).Report; }
        public static ILogger Logger(this IBrowser driver) { return Flags.GetOrCreateValue(driver).Logger; }
        public static IBrowser SetInfo(this IBrowser driver, ILogger Logger, ITestReport Report)
        {
            Flags.GetOrCreateValue(driver).Report = Report;
            Flags.GetOrCreateValue(driver).Logger = Logger;
            return driver;
        }
    }
}