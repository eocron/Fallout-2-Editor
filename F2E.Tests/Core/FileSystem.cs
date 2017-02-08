using System;
using System.IO;

namespace F2E.Tests.Core
{
    public static class FileSystem
    {
        public static string GetDataPath(string localPath)
        {
            var baseP = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var result = Path.Combine(baseP, "Data", localPath);
            return result;
        }
    }
}
