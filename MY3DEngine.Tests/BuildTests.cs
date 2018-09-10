using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MY3DEngine.Utilities.Interfaces;
using System;
using System.Diagnostics.CodeAnalysis;

namespace MY3DEngine.BuildTools.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class BuildTests
    {
        [TestMethod]
        [TestCategory("Build")]
        public void BuildGame_Folder_Exception_Test()
        {
            var result = Assert.ThrowsException<ArgumentNullException>(() => Build.BuildGame(null, null, null));

            Assert.IsNotNull(result);
            Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: folderLocation", result.Message);
        }

        [TestMethod]
        [TestCategory("Build")]
        public void BuildGame_GameName_Exception_Test()
        {
            var result = Assert.ThrowsException<ArgumentNullException>(() => Build.BuildGame("FolderPath", null, null));

            Assert.IsNotNull(result);
            Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: gameName", result.Message);
        }

        [TestMethod]
        [TestCategory("Build")]
        public void BuildGame_fileIO_Exception_Test()
        {
            var result = Assert.ThrowsException<ArgumentNullException>(() => Build.BuildGame("FolderPath", "GameName", null));

            Assert.IsNotNull(result);
            Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: fileIo", result.Message);
        }

        [TestMethod]
        [TestCategory("Build")]
        public void BuildGameNoNameArgumentSuppliedTest()
        {
            var fileIo = A.Fake<IFileIO>();
            A.CallTo(() => fileIo.GetFileContent(string.Empty)).Returns("test");
            var result = Build.BuildGame(@"C:\Temp", "GameName", fileIo);

            Assert.IsFalse(result);
        }
    }
}
