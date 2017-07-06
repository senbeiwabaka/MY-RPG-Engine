using SharpDX.Direct3D11;
using System;

namespace MY3DEngine.Graphics
{
    public class GraphicsManager : IGraphicManager, IDisposable
    {
        public GraphicsManager()
        {
            this.GetDirectXManager = null;
        }

        public DirectXManager GetDirectXManager { get; private set; }

        public IntPtr GetWindowHandle { get; private set; }

        public Device GetDevice => this.GetDirectXManager?.GetDevice;

        public DeviceContext GetDeviceContext => this.GetDirectXManager?.GetDeviceContext;

        public void Dispose()
        {
            this.GetDirectXManager?.Dispose();
            this.GetDirectXManager = null;
        }

        public bool InitializeDirectXManager(IntPtr windowHandle, int screenWidth = 720, int screenHeight = 480, bool vsyncEnabled = true, bool fullScreen = false)
        {
            this.GetDirectXManager = new DirectXManager();

            if (!this.GetDirectXManager.Initialize(windowHandle, screenWidth, screenHeight, vsyncEnabled, fullScreen))
            {
                return false;
            }

            this.GetWindowHandle = windowHandle;

            return true;
        }

        public void Initialize()
        {
        }

        public void BeginScene(float red, float green, float blue, float alpha)
        {
            this.GetDirectXManager.BeginScene(red, green, blue, alpha);
        }

        public void EndScene()
        {
            this.GetDirectXManager.EndScene();
        }

        public void EnableAlphaBlending(bool enable = false)
        {
            this.GetDirectXManager.EnableAlphaBlending(enable);
        }

        public void EnableZBuffer(bool enable = false)
        {
            this.GetDirectXManager.EnableZBuffer(enable);
        }
    }
}