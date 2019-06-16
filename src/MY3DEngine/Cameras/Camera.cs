// <copyright file="Camera.cs" company="MY Soft Games LLC">
//      Copyright (c) MY Soft Games LLC. All rights reserved.
// </copyright>

namespace MY3DEngine.Cameras
{
    using System.Diagnostics;
    using System.Numerics;
    using MY3DEngine.Interfaces;
    using Veldrid.Utilities;

    /// <inherietdoc/>
    public sealed class Camera : ICamera
    {
        /// <inherietdoc/>
        public Vector3 Position { get; private set; }

        /// <inherietdoc/>
        public Vector3 Rotation { get; private set; }

        /// <inherietdoc/>
        public Matrix4x4 ViewMatrix { get; set; }

        private Stopwatch Clock { get; } = new Stopwatch();

        /// <inherietdoc/>
        public void Initialize()
        {
            Initialize(default, default);
        }

        /// <inherietdoc/>
        public void Initialize(int width, int height)
        {
            Clock.Start();
        }

        /// <inheritdoc/>
        public void OnResize(float new_width, float new_height)
        {
            // this.ClientWidth = new_width;
            // this.ClientHeight = new_height;
            // this.InitProjectionMatrix(this.Angle, new_width, new_height, this.Nearest, this.Farthest);
            // this.InitOrthoMatrix(new_width, new_height, 0.0f, this.Farthest);
        }

        public bool RayIntersection(Vector2 mousePosition)
        {
            var mouse = default(Vector3);

            // mouse.X = (((2.0f * mousePosition.X) / Engine.GameEngine.LocalDevice.ThisDevice.Viewport.Width) - 1) / projection.M11;
            // mouse.Y = -(((2.0f * mousePosition.Y) / Engine.GameEngine.LocalDevice.ThisDevice.Viewport.Height) - 1) / projection.M22;
            // mouse.Z = 1.0f;

            Matrix4x4 worldView = default(Matrix4x4); // this.View * Engine.GameEngine.LocalDevice.GetDevice.ImmediateContext.(TransformState.World);

            var m = default(Matrix4x4);

            Matrix4x4.Invert(worldView, out m);

            var direction = new Vector3
            {
                X = mouse.X * m.M11 + mouse.Y * m.M21 + mouse.Z * m.M31,
                Y = mouse.X * m.M12 + mouse.Y * m.M22 + mouse.Z * m.M32,
                Z = mouse.X * m.M13 + mouse.Y * m.M23 + mouse.Z * m.M33,
            };

            mouse.X = m.M41;
            mouse.Y = m.M42;
            mouse.Z = m.M43;

            var selectionRay = new Ray(mouse, direction);

            // if (mesh != null)
            // {
            //    return mesh.ObjectMesh.Intersects(selectionRay);
            // }

            return false;
        }

        /// <inheritdoc/>
        public void Render()
        {
            // Setup where the camera is looking by default.
            var lookAt = new Vector3(0, 0, 1);

            // Set the yaw (Y axis), pitch (X axis), and roll (Z axis) rotations in radians.
            var pitch = Rotation.X * 0.0174532925f;
            var yaw = Rotation.Y * 0.0174532925f;
            var roll = Rotation.Z * 0.0174532925f;

            // Create the rotation matrix from the yaw, pitch, and roll values.
            var rotationMatrix = Matrix4x4.CreateFromYawPitchRoll(yaw, pitch, roll);

            // Transform the lookAt and up vector by the rotation matrix so the view is correctly
            // rotated at the origin.
            lookAt = Vector3.Transform(lookAt, rotationMatrix);
            var up = Vector3.Transform(Vector3.UnitY, rotationMatrix);

            // Translate the rotated camera position to the location of the viewer.
            lookAt = Position + lookAt;

            // Finally create the view matrix from the three updated vectors.
            this.ViewMatrix = Matrix4x4.CreateLookAt(Position, lookAt, up);
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
            Position = new Vector3(x, y, z);
        }

        /// <inherietdoc/>
        public void SetRotation(Vector3 rotation)
        {
            this.SetRotation(rotation.X, rotation.Y, rotation.Z);
        }

        /// <inherietdoc/>
        public void SetRotation(float x, float y, float z)
        {
            Rotation = new Vector3(x, y, z);
        }
    }
}
