using Microsoft.VisualStudio.TestTools.UnitTesting;
using MY3DEngine.Cameras;
using SharpDX;

namespace Engine.Tests
{
    [TestClass]
    public class CameraTests
    {
        [TestMethod]
        public void SetPosition_Vector3_Test()
        {
            var position = new Vector3(1.0f, 1.0f, 1.0f);
            var camera = new Camera();

            camera.SetPosition(position);

            //Assert.AreEqual(position, camera.Position);
        }
    }
}
