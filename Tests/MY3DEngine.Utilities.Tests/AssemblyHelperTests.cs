// <copyright file="AssemblyHelperTests.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MY3DEngine.Utilities.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using MY3DEngine.Utilities.Interfaces;
    using System;
    using System.IO;

    [TestClass]
    public sealed class AssemblyHelperTests
    {
        private static TestContext testContext;

        private readonly Mock<IFileIO> fileIOMock = new Mock<IFileIO>();

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Console.WriteLine("ClassInitialize");

            testContext = context;
        }

        [TestMethod]
        public void GetAssemblies_ThrowsException_Test()
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(() => AssemblyHelper.GetAssemblies(null));

            Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: fileIo", exception.Message);
        }

        [TestMethod]
        public void GetAssemblies_Test()
        {
            var files = Directory.GetFiles(Environment.CurrentDirectory, "*.dll", SearchOption.AllDirectories);

            fileIOMock.SetupGet(x => x.GetCurrentDirectory).Returns(Environment.CurrentDirectory);

            fileIOMock.Setup(x => x.GetFiles(It.IsAny<string>(), It.IsAny<string>())).Returns(files);

            var assemblies = AssemblyHelper.GetAssemblies(fileIOMock.Object);

            Assert.IsNotNull(assemblies);
            Assert.IsTrue(assemblies.Count > 0);
            Assert.AreEqual(files.Length, assemblies.Count);
        }
    }
}
