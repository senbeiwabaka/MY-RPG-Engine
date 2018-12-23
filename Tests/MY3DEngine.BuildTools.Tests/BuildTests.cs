// <copyright file="BuildTests.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MY3DEngine.BuildTools.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using MY3DEngine.Utilities.Interfaces;
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    [TestClass]
    public sealed class BuildTests
    {
        private readonly Mock<IFileIO> fileIoMock = new Mock<IFileIO>();

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
        public void BuildGame_NoName_ArgumentSupplied_Test()
        {
            fileIoMock.Setup(x => x.GetFileContent(string.Empty)).Returns("test");

            var result = Build.BuildGame(@"C:\Temp", "GameName", fileIoMock.Object);

            Assert.IsFalse(result);
        }
    }
}
