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
                    Engine.GameEngine.LocalDevice.LocalDevice.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Black, 1.0f, 0);
                    Engine.GameEngine.LocalDevice.LocalDevice.BeginScene();

                    Engine.GameEngine.LocalDevice.LocalDevice.EndScene();
                    Engine.GameEngine.LocalDevice.LocalDevice.Present();
            }
        }
    }
}