using System;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MY3DEngine.Utilities.Interfaces;

namespace MY3DEngine.Utilities.Tests
{
    [TestClass]
    public class AssemblyHelperTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var fileIo = A.Fake<IFileIO>();

            //A.CallTo(() => fileIo.GetFileInfo())

            //AssemblyHelper.GetAssemblies()
        }
    }
}
