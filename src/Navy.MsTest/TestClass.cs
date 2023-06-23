using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Navy.Core.Logger;

namespace Navy.MsTest
{
    public abstract class TestClass
    { 
        public const string Description = "Description";

        private static readonly Random Random = new Random(Environment.TickCount); 

        private ILogger _logger;

        /// <summary>
        ///     MSUnit TestContext
        /// </summary>
        public virtual TestContext TestContext { get; set; }

        /// <summary>
        ///     Logger
        /// </summary>
        public ILogger Logger
        {
            get
            {
                if (_logger != null)
                    return _logger;
                _logger = new MultiLogger(new TraceLogger(), new FileLogger("Run", isNew: false, addTimeToFile: false))
                {
                    Level = LogType.Trace
                };
                return _logger;
            }
            set => _logger = value;
        }

        [TestInitialize]
        public void TestClassInit()
        {
            Logger.WriteLine(TestContext.TestName);
        }

        [TestCleanup]
        public void TestClassCleanup()
        {
            try
            {
                foreach (var item in (_logger as MultiLogger)?.Loggers?.OfType<IDisposable>())
                    item.Dispose();
            }
            catch
            {
            }
        }

        public static string OutputPath(string localPath = null)
        {
            var dir = DeployPath($"Output{Path.DirectorySeparatorChar}{Random.Next(1, 10000)}");
            var path = localPath != null ? Path.Combine(dir, localPath) : dir;
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            return path;
        }

        public static string DeployPath(string localPath = null)
        {
            var baseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (string.IsNullOrWhiteSpace(localPath))
                return baseDirectory;
            return Path.Combine(baseDirectory, localPath);
        }
    }
}