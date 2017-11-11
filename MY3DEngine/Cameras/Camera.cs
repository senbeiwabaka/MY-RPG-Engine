using SharpDX;
using System;

namespace MY3DEngine.Cameras
{
    public class Camera : ICamera, IDisposable
    {
        public Camera()
        {
        }

        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Matrix ViewMatrix { get; set; }

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

        public void Render()
        {
            Vector3 up, position, lookAt;
            float yaw, pitch, roll;
            Matrix rotationMatrix;

            // Setup the vector that points upwards.
            up = new Vector3(0.0f, 1.0f, 0.0f);
            
            // Setup the position of the camera in the world.
            position = new Vector3(this.Position.X, this.Position.Y, this.Position.Z);
            
            // Setup where the camera is looking by default.
            lookAt = new Vector3(0.0f, 0.0f, 1.0f);
            
            // Set the yaw (Y axis), pitch (X axis), and roll (Z axis) rotations in radians.
            pitch = this.Rotation.X * 0.0174532925f;
            yaw = this.Rotation.Y * 0.0174532925f;
            roll = this.Rotation.Z * 0.0174532925f;

            // Create the rotation matrix from the yaw, pitch, and roll values.
            rotationMatrix = Matrix.RotationYawPitchRoll(yaw, pitch, roll);

            // Transform the lookAt and up vector by the rotation matrix so the view is correctly rotated at the origin.
            lookAt = Vector3.TransformCoordinate(lookAt, rotationMatrix);
            up = Vector3.TransformCoordinate(up, rotationMatrix);

            // Translate the rotated camera position to the location of the viewer.
            lookAt = Vector3.Add(position, lookAt);

            // Finally create the view matrix from the three updated vectors.
            this.ViewMatrix = Matrix.LookAtLH(position, lookAt, up);
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