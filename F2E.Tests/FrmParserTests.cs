using System;
using System.Collections;
using System.IO;
using F2E.Frm;
using F2E.Tests.Core;
using NUnit.Framework;

namespace F2E.Tests
{
    [TestFixture]
    public class FrmParserTests
    {
        public static IEnumerable GetBasicFrms()
        {
            foreach (var file in Directory.GetFiles(FileSystem.GetDataPath("Frm"), "*.frm", SearchOption.TopDirectoryOnly))
            {
                yield return new TestCaseData(file).SetName(Path.GetFileName(file));
            }
        }

        [Test, TestCaseSource(nameof(GetBasicFrms))]
        public void BasicParserTest(string file)
        {
            DeserializeSerializeAndCompare(File.ReadAllBytes(file));
        }

        private void DeserializeSerializeAndCompare(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                var frm = FrmParser.LoadFrom(stream);
                using (var str = new MemoryStream())
                {
                    FrmParser.SaveTo(str, frm);
                    var newBytes = str.ToArray();
                    Assert.AreEqual(bytes, newBytes);
                }
            }

        }
    }
}
