namespace Navy.Test.Reports.MsTest
{
    /// <summary>
    ///     Test outcome.
    ///     From the TestOutcome type in the vstst.xsd
    /// </summary>
    public enum TestOutcome
    {
        Unknown,
        Error,
        Failed,
        Timeout,
        Aborted,
        Inconclusive,
        PassedButRunAborted,
        NotRunnable,
        NotExecuted,
        Disconnected,
        Warning,
        Passed,
        Completed,
        InProgress,
        Pending,
        NotApplicable,
        Blocked
    }
}