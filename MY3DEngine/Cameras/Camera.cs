// <copyright file="Camera.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MY3DEngine.Cameras
{
    using MY3DEngine.Interfaces;
    using SharpDX;
    using System.Diagnostics;

    /// <inherietdoc/>
    public sealed class Camera : ICamera
    {
        private Vector3 position;

        private Vector3 rotation;

        /// <inherietdoc/>
        public Vector3 Position => this.position;

        /// <inherietdoc/>
        public Vector3 Rotation => this.rotation;

        /// <inherietdoc/>
        public Matrix ViewMatrix { get; set; }

        private Stopwatch Clock { get; } = new Stopwatch();

        /// <inherietdoc/>
        public void Initialize()
        {
            Initialize(default(int), default(int));
        }

        /// <inherietdoc/>
        public void Initialize(int width, int height)
        {
            Clock.Start();
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public void Render()
        {
            // Setup where the camera is looking by default.
            var lookAt = new Vector3(0, 0, 1);

            // Set the yaw (Y axis), pitch (X axis), and roll (Z axis) rotations in radians.
            var pitch = this.rotation.X * 0.0174532925f;
            var yaw = this.rotation.Y * 0.0174532925f;
            var roll = this.rotation.Z * 0.0174532925f;

            // Create the rotation matrix from the yaw, pitch, and roll values.
            var rotationMatrix = Matrix.RotationYawPitchRoll(yaw, pitch, roll);

            // Transform the lookAt and up vector by the rotation matrix so the view is correctly
            // rotated at the origin.
            lookAt = Vector3.TransformCoordinate(lookAt, rotationMatrix);
            var up = Vector3.TransformCoordinate(Vector3.UnitY, rotationMatrix);

            // Translate the rotated camera position to the location of the viewer.
            lookAt = position + lookAt;

            // Finally create the view matrix from the three updated vectors.
            this.ViewMatrix = Matrix.LookAtLH(position, lookAt, up);
        }

        /// <inherietdoc/>
        public void ResetCamera()
        {
            this.SetPosition(Vector3.Zero);
            this.SetRotation(Vector3.Zero);
        }

        /// <inherietdoc/>
        public void SetPosition(Vector3 newPosition)
        {
            this.SetPosition(newPosition.X, newPosition.Y, newPosition.Z);
        }

        /// <inherietdoc/>
        public void SetPosition(float x, float y, float z)
        {
            this.position = new Vector3(x, y, z);
        }

        /// <inherietdoc/>
        public void SetRotation(Vector3 rotation)
        {
            this.SetRotation(rotation.X, rotation.Y, rotation.Z);
        }

        /// <inherietdoc/>
        public void SetRotation(float x, float y, float z)
        {
            this.rotation = new Vector3(x, y, z);
        }
    }
}
