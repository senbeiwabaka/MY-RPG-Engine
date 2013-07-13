using SlimDX;
using SlimDX.Direct3D9;
using System.Drawing;

namespace MY3DEngine
{
    internal class Renderer
    {
        public static void RenderScene()
        {
            while (!Engine.GameEngine.IsNotShutDown)
            {
                Engine.GameEngine.LocalDevice.ThisDevice.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Black, 1.0f, 0);
                Engine.GameEngine.LocalDevice.ThisDevice.BeginScene();

                lock (Engine.GameEngine.Manager.GameObjects)
                {
                    foreach (var item in Engine.GameEngine.Manager.GameObjects)
                    {
                        item.Renderer();
                    }
                }

                Engine.GameEngine.LocalDevice.ThisDevice.EndScene();
                Engine.GameEngine.LocalDevice.ThisDevice.Present();
            }
        }
    }
}