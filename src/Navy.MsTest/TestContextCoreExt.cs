using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Navy.MsTest
{
    public static class TestContextCoreExt
    {
        public static void AddResultFile(this TestContext context, string path)
        {
            var method = context.GetType().GetMethod("AddResultFile");
            if (method != null)
            {
                method.Invoke(context, new[] {path});
                return;
            }

            var destination = TestClass.OutputPath(Path.GetFileName(path));
            File.Copy(path, destination, true);
            context.WriteLine($"[File] {destination}");
        }
    }
}