﻿using System;

namespace Navy.Test.Reports.Allure
{
    /// <summary>
    ///     Utility method
    ///     Allure results utils.
    /// </summary>
    public static class AllureResultsUtils
    {
        public static long ToUnixEpochTime(this DateTime time)
        {
            return (long) (time.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                .TotalMilliseconds;
        }
    }
}