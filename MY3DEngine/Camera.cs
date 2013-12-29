using SlimDX;
using SlimDX.Direct3D9;
using System;

namespace MY3DEngine
{
    /// <summary>
    /// Holds all the properties and methods of the camera
    /// </summary>
    public class Camera
    {
        #region Properties

        /// <summary>
        /// the actual view from eye, lookat, and up
        /// </summary>
        public Matrix View { get; set; }

        /// <summary>
        /// Rotate the camera around the x, y, and z axis
        /// </summary>
        public Vector3 CameraRotation
        {
            get
            {
                return cameraRotation;
            }

            set
            {
                cameraRotation = new Vector3(cameraRotation.X + value.X, cameraRotation.Y + value.Y, cameraRotation.Z + value.Z);
                View = Matrix.RotationYawPitchRoll(cameraRotation.Y, cameraRotation.X, cameraRotation.Z) * Matrix.Translation(Eye);
                Engine.GameEngine.LocalDevice.ThisDevice.SetTransform(TransformState.View, View);
            }
        }

        #endregion

        #region Fields

        // how the screen is displayed such as wide screen
        private float aspectRatio;

        private Vector3 cameraRotation;
        private Vector3 Eye { get; set; }

        // how far away from us are things drawn
        private float farClipping;

        // field of view how wide can we see
        private float fov;

        // what we are looking at (location)
        private Vector3 lookAt;

        // how close to us ( camera ) are things drawn
        private float nearClipping;

        // project ( what is actually being seen ) from fov, aspect, near, and far
        private Matrix projection;

        // which way is up
        private Vector3 upDirection;

        #endregion

        /// <summary>
        /// Constructor that sets basic values for view and projection
        /// </summary>
        public Camera()
        {
            Engine.GameEngine.LocalDevice.ThisDevice.SetTransform(TransformState.World, Matrix.Identity);

            Eye = new Vector3(0, 0, 3.5f);
            lookAt = Vector3.Zero;
            upDirection = Vector3.UnitY;

            View = Matrix.Translation(Eye);
            Engine.GameEngine.LocalDevice.ThisDevice.SetTransform(TransformState.View, View);

            fov = (float)Math.PI / 4.0f;
            aspectRatio = (float)Engine.GameEngine.LocalDevice.ThisDevice.Viewport.Width / Engine.GameEngine.LocalDevice.ThisDevice.Viewport.Height;
            nearClipping = 1.0f;
            farClipping = 100.0f;

            projection = Matrix.PerspectiveFovLH(fov, aspectRatio, nearClipping, farClipping);
            Engine.GameEngine.LocalDevice.ThisDevice.SetTransform(TransformState.Projection, projection);

            cameraRotation = Vector3.Zero;
        }

        /// <summary>
        /// Gets the cameras location and rotation
        /// </summary>
        /// <returns>The concatenated string of the position and rotation to three decimal places</returns>
        public string CameraLocation()
        {
            var position = string.Format("Location - X: {0}, Y: {1}, Z: {2}", Eye.X.ToString("0.000"), Eye.Y.ToString("0.000"), Eye.Z.ToString("0.000"));
            position += Environment.NewLine;
            var rotation = string.Format("Rotation - X: {0}, Y: {1}, Z: {2}", cameraRotation.X.ToString("0.000"), cameraRotation.Y.ToString("0.000"), cameraRotation.Z.ToString("0.000"));
            return position + rotation;
        }

        /// <summary>
        /// change what the camera is looking at
        /// </summary>
        /// <param name="eye">where the camera is located</param>
        /// <param name="lookAt">what you want the camera to look at</param>
        public void ChangeView(Vector3 eye, Vector3 lookAt)
        {
            this.Eye = eye;
            this.lookAt = lookAt;
            View = Matrix.Translation(eye);
            Engine.GameEngine.LocalDevice.ThisDevice.SetTransform(TransformState.View, View);
        }

        /// <summary>
        /// Move the camera along the axis by the specified amount
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void MoveEye(float x = 0, float y = 0, float z = 0)
        {
            Eye += new Vector3(x, y, z);

            View = Matrix.RotationYawPitchRoll(cameraRotation.Y, cameraRotation.X, cameraRotation.Z) * Matrix.Translation(Eye);
            Engine.GameEngine.LocalDevice.ThisDevice.SetTransform(TransformState.View, View);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mousePosition"></param>
        /// <param name="mesh"></param>
        /// <returns></returns>
        public bool RayCalculation(Vector2 mousePosition, MeshClass mesh)
        {
            Vector3 mouse = new Vector3();

            mouse.X = (((2.0f * mousePosition.X) / Engine.GameEngine.LocalDevice.ThisDevice.Viewport.Width) - 1) / projection.M11;
            mouse.Y = -(((2.0f * mousePosition.Y) / Engine.GameEngine.LocalDevice.ThisDevice.Viewport.Height) - 1) / projection.M22;
            mouse.Z = 1.0f;

            Matrix worldView = View * Engine.GameEngine.LocalDevice.ThisDevice.GetTransform(TransformState.World);

            Matrix m = new Matrix();

            m = Matrix.Invert(worldView);

            Vector3 direction = new Vector3();

            direction.X = mouse.X * m.M11 + mouse.Y * m.M21 + mouse.Z * m.M31;
            direction.Y = mouse.X * m.M12 + mouse.Y * m.M22 + mouse.Z * m.M32;
            direction.Z = mouse.X * m.M13 + mouse.Y * m.M23 + mouse.Z * m.M33;

            mouse.X = m.M41;
            mouse.Y = m.M42;
            mouse.Z = m.M43;

            Ray selectionRay = new Ray(mouse, direction);

            if (mesh != null)
            {
                return mesh.ObjectMesh.Intersects(selectionRay);
            }

            return false;
        }

        /// <summary>
        /// Resets the Camera to look at the origin with a distance of 3.5 units from it
        /// </summary>
        public void ResetEye()
        {
            cameraRotation = Vector3.Zero;

            Engine.GameEngine.LocalDevice.ThisDevice.SetTransform(TransformState.World, Matrix.Identity);

            Eye = new Vector3(0, 0, 3.5f);
            lookAt = Vector3.Zero;
            upDirection = Vector3.UnitY;

            View = Matrix.Translation(Eye);
            Engine.GameEngine.LocalDevice.ThisDevice.SetTransform(TransformState.View, View);
        }

        /// <summary>
        /// Sets a new projection
        /// </summary>
        /// <param name="fov"> how wide do we see (in radians)</param>
        /// <param name="aspectRatio"> width/height</param>
        /// <param name="close"> how close do we draw objects</param>
        /// <param name="far"> how far away do we draw objects</param>
        public void SetProjection(float fov, float aspectRatio, float close = 0.01f, float far = 100.0f)
        {
            this.fov = fov;
            this.aspectRatio = aspectRatio;
            nearClipping = close;
            this.farClipping = far;
            projection = Matrix.PerspectiveFovLH(this.fov, this.aspectRatio,
                nearClipping, this.farClipping);
            Engine.GameEngine.LocalDevice.ThisDevice.SetTransform(TransformState.Projection,
                projection);
        }

        /// <summary>
        /// Sets a new view
        /// </summary>
        /// <param name="eye"> where are we</param>
        /// <param name="lookat"> what are we looking at</param>
        /// <param name="up"> which way is up</param>
        public void SetView(Vector3 eye, Vector3 lookat, Vector3 up)
        {
            this.Eye = eye;
            this.lookAt = lookat;
            upDirection = up;
            View = Matrix.Translation(eye);
            Engine.GameEngine.LocalDevice.ThisDevice.SetTransform(TransformState.View, View);
        }
    }
}