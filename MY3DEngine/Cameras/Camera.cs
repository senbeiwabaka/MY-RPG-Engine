using SharpDX;
using System;

namespace MY3DEngine.Cameras
{
    public class Camera : ICamera, IDisposable
    {
        public Camera()
        {
        }

        public Vector3 Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector3 Rotation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Dispose()
        {
            this.Disposing(true);

            GC.SuppressFinalize(true);
        }

        public void Initialize()
        {
        }

        public bool RayIntersection(Vector2 mousePosition)
        {
            var mouse = new Vector3();

            //mouse.X = (((2.0f * mousePosition.X) / Engine.GameEngine.LocalDevice.ThisDevice.Viewport.Width) - 1) / projection.M11;
            //mouse.Y = -(((2.0f * mousePosition.Y) / Engine.GameEngine.LocalDevice.ThisDevice.Viewport.Height) - 1) / projection.M22;
            //mouse.Z = 1.0f;

            Matrix worldView = new Matrix(); // this.View * Engine.GameEngine.LocalDevice.GetDevice.ImmediateContext.(TransformState.World);

            var m = new Matrix();

            m = Matrix.Invert(worldView);

            var direction = new Vector3();

            direction.X = mouse.X * m.M11 + mouse.Y * m.M21 + mouse.Z * m.M31;
            direction.Y = mouse.X * m.M12 + mouse.Y * m.M22 + mouse.Z * m.M32;
            direction.Z = mouse.X * m.M13 + mouse.Y * m.M23 + mouse.Z * m.M33;

            mouse.X = m.M41;
            mouse.Y = m.M42;
            mouse.Z = m.M43;

            var selectionRay = new Ray(mouse, direction);

            //if (mesh != null)
            //{
            //    return mesh.ObjectMesh.Intersects(selectionRay);
            //}

            return false;
        }

        public void ResetEye()
        {
        }

        private void Disposing(bool disposing)
        {
            if (disposing)
            {
            }
        }
    }
}