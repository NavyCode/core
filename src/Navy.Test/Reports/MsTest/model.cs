using System.Xml.Serialization;

namespace Navy.Test.Reports.MsTest
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true,
        Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    [XmlRoot(Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010",
        IsNullable = false)]
    public partial class TestRun
    {

        private TestRunTimes timesField;

        private TestRunTestSettings testSettingsField;

        private TestRunUnitTestResult[] resultsField;

        private TestRunUnitTest[] testDefinitionsField;

        private TestRunTestEntry[] testEntriesField;

        private TestRunTestList[] testListsField;

        private TestRunResultSummary resultSummaryField;


        private string idField;

        private string nameField;

        private string runUserField;

        /// <remarks/>
        public TestRunTimes Times
        {
            get { return this.timesField; }
            set { this.timesField = value; }
        }

        /// <remarks/>
        public TestRunTestSettings TestSettings
        {
            get { return this.testSettingsField; }
            set { this.testSettingsField = value; }
        }

        /// <remarks/>
        [XmlArrayItem("UnitTestResult", IsNullable = false)]
        public TestRunUnitTestResult[] Results
        {
            get { return this.resultsField; }
            set { this.resultsField = value; }
        }

        /// <remarks/>
        [XmlArrayItem("UnitTest", IsNullable = false)]
        public TestRunUnitTest[] TestDefinitions
        {
            get { return this.testDefinitionsField; }
            set { this.testDefinitionsField = value; }
        }

        /// <remarks/>
        [XmlArrayItem("TestEntry", IsNullable = false)]
        public TestRunTestEntry[] TestEntries
        {
            get { return this.testEntriesField; }
            set { this.testEntriesField = value; }
        }

        /// <remarks/>
        [XmlArrayItem("TestList", IsNullable = false)]
        public TestRunTestList[] TestLists
        {
            get { return this.testListsField; }
            set { this.testListsField = value; }
        }

        /// <remarks/>
        public TestRunResultSummary ResultSummary
        {
            get { return this.resultSummaryField; }
            set { this.resultSummaryField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string id
        {
            get { return this.idField; }
            set { this.idField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string name
        {
            get { return this.nameField; }
            set { this.nameField = value; }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string runUser
        {
            get { return this.runUserField; }
            set { this.runUserField = value; }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true,
        Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunTimes
    {

        private System.DateTime creationField;

        private System.DateTime queuingField;

        private System.DateTime startField;

        private System.DateTime finishField;

        /// <remarks/>
        [XmlAttribute()]
        public System.DateTime creation
        {
            get { return this.creationField; }
            set { this.creationField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public System.DateTime queuing
        {
            get { return this.queuingField; }
            set { this.queuingField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public System.DateTime start
        {
            get { return this.startField; }
            set { this.startField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public System.DateTime finish
        {
            get { return this.finishField; }
            set { this.finishField = value; }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true,
        Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunTestSettings
    {

        private TestRunTestSettingsDeployment deploymentField;

        private string nameField;

        private string idField;

        /// <remarks/>
        public TestRunTestSettingsDeployment Deployment
        {
            get { return this.deploymentField; }
            set { this.deploymentField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string name
        {
            get { return this.nameField; }
            set { this.nameField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string id
        {
            get { return this.idField; }
            set { this.idField = value; }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true,
        Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunTestSettingsDeployment
    {

        private string runDeploymentRootField;

        /// <remarks/>
        [XmlAttribute()]
        public string runDeploymentRoot
        {
            get { return this.runDeploymentRootField; }
            set { this.runDeploymentRootField = value; }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true,
        Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunUnitTestResult
    {

        private TestRunUnitTestResultOutput outputField;

        private string executionIdField;

        private string testIdField;

        private string testNameField;

        private string computerNameField;

        private System.DateTime durationField;

        private System.DateTime startTimeField;

        private System.DateTime endTimeField;

        private string testTypeField;

        private string outcomeField;

        private string testListIdField;

        private string relativeResultsDirectoryField;

        /// <remarks/>
        public TestRunUnitTestResultOutput Output
        {
            get { return this.outputField; }
            set { this.outputField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string executionId
        {
            get { return this.executionIdField; }
            set { this.executionIdField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string testId
        {
            get { return this.testIdField; }
            set { this.testIdField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string testName
        {
            get { return this.testNameField; }
            set { this.testNameField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string computerName
        {
            get { return this.computerNameField; }
            set { this.computerNameField = value; }
        }

        /// <remarks/>
        [XmlAttribute(DataType = "time")]
        public System.DateTime duration
        {
            get { return this.durationField; }
            set { this.durationField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public System.DateTime startTime
        {
            get { return this.startTimeField; }
            set { this.startTimeField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public System.DateTime endTime
        {
            get { return this.endTimeField; }
            set { this.endTimeField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string testType
        {
            get { return this.testTypeField; }
            set { this.testTypeField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string outcome
        {
            get { return this.outcomeField; }
            set { this.outcomeField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string testListId
        {
            get { return this.testListIdField; }
            set { this.testListIdField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string relativeResultsDirectory
        {
            get { return this.relativeResultsDirectoryField; }
            set { this.relativeResultsDirectoryField = value; }
        }


        private TestRunUnitTestResult[] innerResultsField;

        /// <remarks/>
        [XmlArrayItem("UnitTestResult", IsNullable = false)]
        public TestRunUnitTestResult[] InnerResults
        { 
            get { return this.innerResultsField; }
            set { this.innerResultsField = value; }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true,
        Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunUnitTestResultOutput
    {

        private string stdOutField;

        private TestRunUnitTestResultOutputErrorInfo errorInfoField;

        /// <remarks/>
        public string StdOut
        {
            get { return this.stdOutField; }
            set { this.stdOutField = value; }
        }

        /// <remarks/>
        public TestRunUnitTestResultOutputErrorInfo ErrorInfo
        {
            get { return this.errorInfoField; }
            set { this.errorInfoField = value; }
        }
    }


    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true,
        Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunUnitTestResultOutputErrorInfo
    {

        private string messageField;

        private string stackTraceField;

        /// <remarks/>
        public string Message
        {
            get { return this.messageField; }
            set { this.messageField = value; }
        }

        /// <remarks/>
        public string StackTrace
        {
            get { return this.stackTraceField; }
            set { this.stackTraceField = value; }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true,
        Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunUnitTest
    {

        private TestRunUnitTestExecution executionField;

        private TestRunUnitTestTestMethod testMethodField;

        private string nameField;

        private string storageField;

        private string idField;

        /// <remarks/>
        public TestRunUnitTestExecution Execution
        {
            get { return this.executionField; }
            set { this.executionField = value; }
        }

        /// <remarks/>
        public TestRunUnitTestTestMethod TestMethod
        {
            get { return this.testMethodField; }
            set { this.testMethodField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string name
        {
            get { return this.nameField; }
            set { this.nameField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string storage
        {
            get { return this.storageField; }
            set { this.storageField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string id
        {
            get { return this.idField; }
            set { this.idField = value; }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true,
        Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunUnitTestExecution
    {

        private string idField;

        /// <remarks/>
        [XmlAttribute()]
        public string id
        {
            get { return this.idField; }
            set { this.idField = value; }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true,
        Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunUnitTestTestMethod
    {

        private string codeBaseField;

        private string adapterTypeNameField;

        private string classNameField;

        private string nameField;

        /// <remarks/>
        [XmlAttribute()]
        public string codeBase
        {
            get { return this.codeBaseField; }
            set { this.codeBaseField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string adapterTypeName
        {
            get { return this.adapterTypeNameField; }
            set { this.adapterTypeNameField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string className
        {
            get { return this.classNameField; }
            set { this.classNameField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string name
        {
            get { return this.nameField; }
            set { this.nameField = value; }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true,
        Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunTestEntry
    {

        private string testIdField;

        private string executionIdField;

        private string testListIdField;

        /// <remarks/>
        [XmlAttribute()]
        public string testId
        {
            get { return this.testIdField; }
            set { this.testIdField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string executionId
        {
            get { return this.executionIdField; }
            set { this.executionIdField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string testListId
        {
            get { return this.testListIdField; }
            set { this.testListIdField = value; }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true,
        Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunTestList
    {

        private string nameField;

        private string idField;

        /// <remarks/>
        [XmlAttribute()]
        public string name
        {
            get { return this.nameField; }
            set { this.nameField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string id
        {
            get { return this.idField; }
            set { this.idField = value; }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true,
        Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunResultSummary
    {

        private TestRunResultSummaryCounters countersField;

        private TestRunResultSummaryOutput outputField;

        private TestRunResultSummaryRunInfos runInfosField;

        private string outcomeField;

        /// <remarks/>
        public TestRunResultSummaryCounters Counters
        {
            get { return this.countersField; }
            set { this.countersField = value; }
        }

        /// <remarks/>
        public TestRunResultSummaryOutput Output
        {
            get { return this.outputField; }
            set { this.outputField = value; }
        }

        /// <remarks/>
        public TestRunResultSummaryRunInfos RunInfos
        {
            get { return this.runInfosField; }
            set { this.runInfosField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string outcome
        {
            get { return this.outcomeField; }
            set { this.outcomeField = value; }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true,
        Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunResultSummaryCounters
    {

        private int totalField;

        private int executedField;

        private int passedField;

        private int failedField;

        private int errorField;

        private int timeoutField;

        private int abortedField;

        private int inconclusiveField;

        private int passedButRunAbortedField;

        private int notRunnableField;

        private int notExecutedField;

        private int disconnectedField;

        private int warningField;

        private int completedField;

        private int inProgressField;

        private int pendingField;

        /// <remarks/>
        [XmlAttribute()]
        public int total
        {
            get { return this.totalField; }
            set { this.totalField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public int executed
        {
            get { return this.executedField; }
            set { this.executedField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public int passed
        {
            get { return this.passedField; }
            set { this.passedField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public int failed
        {
            get { return this.failedField; }
            set { this.failedField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public int error
        {
            get { return this.errorField; }
            set { this.errorField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public int timeout
        {
            get { return this.timeoutField; }
            set { this.timeoutField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public int aborted
        {
            get { return this.abortedField; }
            set { this.abortedField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public int inconclusive
        {
            get { return this.inconclusiveField; }
            set { this.inconclusiveField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public int passedButRunAborted
        {
            get { return this.passedButRunAbortedField; }
            set { this.passedButRunAbortedField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public int notRunnable
        {
            get { return this.notRunnableField; }
            set { this.notRunnableField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public int notExecuted
        {
            get { return this.notExecutedField; }
            set { this.notExecutedField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public int disconnected
        {
            get { return this.disconnectedField; }
            set { this.disconnectedField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public int warning
        {
            get { return this.warningField; }
            set { this.warningField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public int completed
        {
            get { return this.completedField; }
            set { this.completedField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public int inProgress
        {
            get { return this.inProgressField; }
            set { this.inProgressField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public int pending
        {
            get { return this.pendingField; }
            set { this.pendingField = value; }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true,
        Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunResultSummaryOutput
    {

        private string stdOutField;

        /// <remarks/>
        public string StdOut
        {
            get { return this.stdOutField; }
            set { this.stdOutField = value; }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true,
        Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunResultSummaryRunInfos
    {

        private TestRunResultSummaryRunInfosRunInfo runInfoField;

        /// <remarks/>
        public TestRunResultSummaryRunInfosRunInfo RunInfo
        {
            get { return this.runInfoField; }
            set { this.runInfoField = value; }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true,
        Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public partial class TestRunResultSummaryRunInfosRunInfo
    {

        private string textField;

        private string computerNameField;

        private string outcomeField;

        private System.DateTime timestampField;

        /// <remarks/>
        public string Text
        {
            get { return this.textField; }
            set { this.textField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string computerName
        {
            get { return this.computerNameField; }
            set { this.computerNameField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string outcome
        {
            get { return this.outcomeField; }
            set { this.outcomeField = value; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public System.DateTime timestamp
        {
            get { return this.timestampField; }
            set { this.timestampField = value; }
        }
    }

}