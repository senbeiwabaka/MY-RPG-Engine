// <copyright file="AssemblyHelperTests.cs" company="MY Soft Games LLC">
//      Copyright (c) MY Soft Games LLC. All rights reserved.
// </copyright>

namespace Utilities.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using Moq;
    using My3DEngine.Utilities;
    using My3DEngine.Utilities.Interfaces;
    using NUnit.Framework;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public sealed class AssemblyHelperTests
    {
        private readonly Mock<IFileService> fileIOMock = new Mock<IFileService>();

        [Test]
        public void GetAssemblies_ThrowsException_Test()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => AssemblyHelper.GetAssemblies(null));

            Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: fileIo", exception.Message);
        }

        [Test]
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
