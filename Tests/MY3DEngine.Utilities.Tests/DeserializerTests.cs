// <copyright file="DeserializerTests.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MY3DEngine.Utilities.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MY3DEngine.Utilities.Tests.Models;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    [TestClass]
    public sealed class DeserializerTests
    {
        private const string AssetsPath = "AssetsPath";
        private const string GameName = "GameName";

        [TestMethod]
        public void DeserializeStringAsT_Test()
        {
            var result = Deserialize.DeserializeStringAsT<TestSettingsModel>("{ 'AssetsPath': 'AssetsPath', 'GameName': 'GameName'}");

            Assert.IsNotNull(result);
            Assert.AreEqual(AssetsPath, result.AssetsPath);
            Assert.AreEqual(GameName, result.GameName);
        }
    }
}
