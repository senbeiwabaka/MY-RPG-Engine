using SharpDX;
using System;
using System.Diagnostics;

namespace MY3DEngine.Cameras
{
    public class Camera : ICamera
    {
        public Vector3 Position => this.position;
        public Matrix ViewMatrix { get; set; }

        private Stopwatch clock { get; } = new Stopwatch();
        private Vector3 position { get; set; }
        private Vector3 rotation { get; set; }

        /// <inherietdoc/>
        public void Initialize(int width = default(int), int height = default(int))
        {
            clock.Start();
        }

        public void OnResize(float new_width, float new_height)
        {
            //this.ClientWidth = new_width;
            //this.ClientHeight = new_height;
            //this.InitProjectionMatrix(this.Angle, new_width, new_height, this.Nearest, this.Farthest);
            //this.InitOrthoMatrix(new_width, new_height, 0.0f, this.Farthest);
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

            var direction = new Vector3
            {
                X = mouse.X * m.M11 + mouse.Y * m.M21 + mouse.Z * m.M31,
                Y = mouse.X * m.M12 + mouse.Y * m.M22 + mouse.Z * m.M32,
                Z = mouse.X * m.M13 + mouse.Y * m.M23 + mouse.Z * m.M33
            };

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

        public void Render()
        {
            // Setup where the camera is looking by default.
            Vector3 lookAt = new Vector3(0, 0, 1);

            // Set the yaw (Y axis), pitch (X axis), and roll (Z axis) rotations in radians.
            float pitch = this.rotation.X * 0.0174532925f;
            float yaw = this.rotation.Y * 0.0174532925f;
            float roll = this.rotation.Z * 0.0174532925f;

            // Create the rotation matrix from the yaw, pitch, and roll values.
            Matrix rotationMatrix = Matrix.RotationYawPitchRoll(yaw, pitch, roll);

            // Transform the lookAt and up vector by the rotation matrix so the view is correctly rotated at the origin.
            lookAt = Vector3.TransformCoordinate(lookAt, rotationMatrix);
            Vector3 up = Vector3.TransformCoordinate(Vector3.UnitY, rotationMatrix);

            // Translate the rotated camera position to the location of the viewer.
            lookAt = position + lookAt;

            // Finally create the view matrix from the three updated vectors.
            this.ViewMatrix = Matrix.LookAtLH(position, lookAt, up);
        }

        public void ResetEye()
        {
        }

        public void SetPosition(Vector3 newPosition)
        {
            this.SetPosition(newPosition.X, newPosition.Y, newPosition.Z);
        }

        public void SetPosition(float x, float y, float z)
        {
            this.position = new Vector3(x, y, z);
        }

        private float AngleBetween(Vector3 u, Vector3 v, bool returndegrees)
        {
            float toppart = 0;
            for (int d = 0; d < 3; d++) toppart += u[d] * v[d];

            float u2 = 0; //u squared
            float v2 = 0; //v squared
            for (int d = 0; d < 3; d++)
            {
                u2 += u[d] * u[d];
                v2 += v[d] * v[d];
            }

            float bottompart = 0;
            bottompart = (float)Math.Sqrt(u2 * v2);

            float rtnval = (float)Math.Acos(toppart / bottompart);
            if (returndegrees) rtnval *= 360.0f / (2.0f * (float)Math.PI);
            return rtnval;
        }
    }
}