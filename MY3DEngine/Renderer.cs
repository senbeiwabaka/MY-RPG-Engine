using System.Drawing;

using SharpDX.DXGI;

namespace MY3DEngine
{
    internal class Renderer
    {
        /// <summary>
        /// Renders the scene to the user
        /// </summary>
        public static void RenderScene()
        {
            //if (Engine.GameEngine.LocalDevice.GetDevice==null)
            //{
            //    if (Engine.GameEngine.GraphicsManager.GetDevice.DeviceRemovedReason == ResultCode.DeviceRemoved)
            //    {
            //        //Engine.GameEngine.LocalDevice.ResetDevice();
            //    }
            //}

            while (!Engine.GameEngine.IsNotShutDown)
            {
                Engine.GameEngine.GraphicsManager.BeginScene((float)Color.Black.R, (float)Color.Black.G, (float)Color.Black.B, (float)Color.Black.A);

                lock (Engine.GameEngine.Manager.GameObjects)
                {
                    foreach (var item in Engine.GameEngine.Manager.GameObjects)
                    {
                        //if (item is LightClass)
                        //{
                        //    if (!Engine.GameEngine.LocalDevice.GetDevice.GetRenderState<bool>(RenderState.Lighting))
                        //    {
                        //        item.Renderer();
                        //    }
                        //}
                        //else
                        //{
                            item.Renderer();
                        //}
                    }
                }

                Engine.GameEngine.GraphicsManager.EndScene();
            }
        }
    }
}