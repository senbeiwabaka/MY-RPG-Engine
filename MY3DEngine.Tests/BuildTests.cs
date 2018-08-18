using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MY3DEngine.Tests
{
    [TestClass]
    public class BuildTests
    {
        [TestMethod]
        [TestCategory("Build")]
        public void BuildGameNoFolderArgumentSuppliedTest()
        {
            var result = Build.Build.BuildGame(null, null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCategory("Build")]
        public void BuildGameNoNameArgumentSuppliedTest()
        {
            var result = Build.Build.BuildGame("test", null);

            Assert.IsFalse(result);
        }
    }
}
