using SlimDX;
using SlimDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MY3DEngine
{
    public class LightClass : ObjectClass
    {
        private Light light;
        private Material material;
        private bool isLightEnabled;
        private Matrix world;

        public string Type { get; private set; }
        public Vector3 Position { get; set; }
        public Vector3 Direction { get; set; }
        public bool IsLightEnabled
        {
            get { return isLightEnabled; }
            private set { isLightEnabled = value; }
        }

        /// <summary>
        /// Constructor or added a new light to the scene
        /// </summary>
        /// <param name="type">the light type you wish to have or default of point</param>
        public LightClass(string type = "Point")
        {
            if (type == LightType.Point.ToString())
            {
                light.Type = LightType.Point;
                light.Diffuse = Color.White;
                light.Ambient = Color.White;
                light.Specular = Color.White;
                light.Position = Vector3.Zero;
                light.Range = 100.0f;

            }
            else if (type == LightType.Directional.ToString())
            {
                light.Type = LightType.Directional;
                light.Direction = Vector3.Zero;
                light.Ambient = Color.White;
                light.Diffuse = Color.White;
                light.Specular = Color.White;
                light.Range = 100.0f;
            }

            isLightEnabled = false;
            Type = type.ToString();
            Position = Vector3.Zero;
            Direction = Vector3.Zero;
            world = Matrix.Identity;
            //mesh = Mesh.CreateSphere(Engine.GameEngine.LocalDevice.LocalDevice, .1f, 10, 10);
            MeshObject = new MeshClass(MeshType.Sphere);

            material.Diffuse = Color.White;
            material.Ambient = Color.White;

            Engine.GameEngine.LocalDevice.ThisDevice.Material = material;
        }

        /// <summary>
        /// Turn the specific light light on/off
        /// </summary>
        /// <param name="index">the light in which you want to turn on or off</param>
        public void LightOnOff(int index)
        {
            isLightEnabled = isLightEnabled == true ? false : true;
            Engine.GameEngine.LocalDevice.ThisDevice.SetLight(index, light);
            Engine.GameEngine.LocalDevice.ThisDevice.EnableLight(index, isLightEnabled);
        }

        /// <summary>
        /// Render the location if global light is on
        /// </summary>
        public void Render()
        {
            world = Matrix.Translation(Position);
            Engine.GameEngine.LocalDevice.ThisDevice.SetTransform(TransformState.World, world);

            //mesh.DrawSubset(0);
        }

        public override void Dispose()
        {
            //mesh.Dispose();
            base.Dispose();
        }

        /// <summary>
        /// Positions the light (point) or Positions the direction (directional)
        /// </summary>
        /// <param name="position">vector 3 position</param>
        public void GlobalLightTranslation(Vector3 position)
        {
            Position = position;
            if (Type.Equals(LightType.Point.ToString()))
            {
                light.Position = Position;
            }
            else if (Type.Equals(LightType.Directional.ToString()))
            {
                light.Direction = Position;
            }
        }
    }
}