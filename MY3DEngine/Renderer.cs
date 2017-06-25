using SlimDX;
using SlimDX.Direct3D9;
using System.Drawing;

namespace MY3DEngine
{
    internal class Renderer
    {
        /// <summary>
        /// Renders the scene to the user
        /// </summary>
        public static void RenderScene()
        {
            if (Engine.GameEngine.LocalDevice.Device==null)
            {
                if (Engine.GameEngine.LocalDevice.Device.TestCooperativeLevel() == ResultCode.DeviceLost)
                {
                    Engine.GameEngine.LocalDevice.ResetDevice();
                }
            }

            while (!Engine.GameEngine.IsNotShutDown)
            {
                Engine.GameEngine.LocalDevice.Device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Black, 1.0f, 0);
                Engine.GameEngine.LocalDevice.Device.BeginScene();

                lock (Engine.GameEngine.Manager.GameObjects)
                {
                    foreach (var item in Engine.GameEngine.Manager.GameObjects)
                    {
                        if (item is LightClass)
                        {
                            if (!Engine.GameEngine.LocalDevice.Device.GetRenderState<bool>(RenderState.Lighting))
                            {
                                item.Renderer();
                            }
                        }
                        else
                        {
                            item.Renderer();
                        }
                    }
                }

                Engine.GameEngine.LocalDevice.Device.EndScene();
                Engine.GameEngine.LocalDevice.Device.Present();
            }
        }
    }
}