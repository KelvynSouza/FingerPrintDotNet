
    using System;
    using System.Xml.Serialization;
    using System.Collections.Generic;
    namespace Xml2CSharp
    {
        public class Times
        {
            public string Creation { get; set; }
            public string Queuing { get; set; }
            public string Start { get; set; }
            public string Finish { get; set; }
        }

        public class Deployment
        {
            public string RunDeploymentRoot { get; set; }
        }

        public class TestSettings
        {
            public Deployment Deployment { get; set; }
            public string Name { get; set; }
            public string Id { get; set; }
        }

        public class ErrorInfo
        {
            public string Message { get; set; }
            public string StackTrace { get; set; }
        }

        public class Output
        {
            public ErrorInfo ErrorInfo { get; set; }
            public string StdOut { get; set; }
        }

        public class UnitTestResult
        {
            public Output Output { get; set; }
            public string ExecutionId { get; set; }
            public string TestId { get; set; }
            public string TestName { get; set; }
            public string ComputerName { get; set; }
            public string Duration { get; set; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public string TestType { get; set; }
            public string Outcome { get; set; }
            public string TestListId { get; set; }
            public string RelativeResultsDirectory { get; set; }
        }

        public class Results
        {
            public List<UnitTestResult> UnitTestResult { get; set; }
        }

        public class Execution
        {
            public string Id { get; set; }
        }

        public class TestMethod
        {
            public string CodeBase { get; set; }
            public string AdapterTypeName { get; set; }
            public string ClassName { get; set; }
            public string Name { get; set; }
        }

        public class UnitTest
        {
            public Execution Execution { get; set; }
            public TestMethod TestMethod { get; set; }
            public string Name { get; set; }
            public string Storage { get; set; }
            public string Id { get; set; }
        }

        public class TestDefinitions
        {
            public List<UnitTest> UnitTest { get; set; }
        }

        public class TestEntry
        {
            public string TestId { get; set; }
            public string ExecutionId { get; set; }
            public string TestListId { get; set; }
        }

        public class TestEntries
        {
            public List<TestEntry> TestEntry { get; set; }
        }

        public class TestList
        {
            public string Name { get; set; }
            public string Id { get; set; }
        }

        public class TestLists
        {
            public List<TestList> TestList { get; set; }
        }

        public class Counters
        {
            public string Total { get; set; }
            public string Executed { get; set; }
            public string Passed { get; set; }
            public string Failed { get; set; }
            public string Error { get; set; }
            public string Timeout { get; set; }
            public string Aborted { get; set; }
            public string Inconclusive { get; set; }
            public string PassedButRunAborted { get; set; }
            public string NotRunnable { get; set; }
            public string NotExecuted { get; set; }
            public string Disconnected { get; set; }
            public string Warning { get; set; }
            public string Completed { get; set; }
            public string InProgress { get; set; }
            public string Pending { get; set; }
        }

        public class ResultSummary
        {
            public Counters Counters { get; set; }
            public Output Output { get; set; }
            public string Outcome { get; set; }
        }

        public class TestRun
        {
            public Times Times { get; set; }
            public TestSettings TestSettings { get; set; }
            public Results Results { get; set; }
            public TestDefinitions TestDefinitions { get; set; }
            public TestEntries TestEntries { get; set; }
            public TestLists TestLists { get; set; }
            public ResultSummary ResultSummary { get; set; }
            public string Id { get; set; }
            public string Name { get; set; }
            public string RunUser { get; set; }
            public string Xmlns { get; set; }
        }

    }


