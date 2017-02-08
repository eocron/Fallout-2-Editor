using System;
using System.Collections;
using System.IO;
using System.Linq;
using F2E.Frm;
using F2E.Frm.Dto;
using F2E.Tests.Core;
using NUnit.Framework;

namespace F2E.Tests
{
    [TestFixture]
    class FrmConverterTests
    {
        private FrmManager _frmManager;

        [SetUp]
        public void SetUp()
        {
            var palette = ImageHelper.LoadPalette(FileSystem.GetDataPath(@"Frm\color.pal"));
            _frmManager = new FrmManager(palette);
        }
        public static IEnumerable GetBasicFrms()
        {
            foreach (var file in Directory.GetFiles(FileSystem.GetDataPath("Frm"), "*.frm", SearchOption.TopDirectoryOnly))
            {
                yield return new TestCaseData(file).SetName(Path.GetFileName(file));
            }
        }

        [Test, TestCaseSource(nameof(GetBasicFrms))]
        public void BasicConverterTest(string file)
        {
            DumpToDirectory(file);
        }

        private void DumpToDirectory(string fileName)
        {
            var path = Path.Combine(@"A:\test", Path.GetFileNameWithoutExtension(fileName) + ".zip");

            var model = _frmManager.LoadFromFrm(fileName);
            _frmManager.SaveToEfa(path, model);

            using (var ms = new MemoryStream())
            {
                _frmManager.SaveToFrm(ms, model);
                ms.Seek(0, SeekOrigin.Begin);
                model = _frmManager.LoadFromFrm(ms);
            }

            path = Path.Combine(@"A:\test", Path.GetFileNameWithoutExtension(fileName) + ".new.zip");
            _frmManager.SaveToEfa(path, model);
        }
    }
}
