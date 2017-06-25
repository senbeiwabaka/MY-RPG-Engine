using System;

using SharpDX.Direct3D11;

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

        public Device GetDevice => this.GetDirectXManager?.Device;

        public DeviceContext GetDeviceContext => this.GetDirectXManager?.DeviceContext;

        public void Dispose()
        {
            this.GetDirectXManager?.Dispose();
        }

        public bool InitializeDirectXManager(IntPtr windowHandle)
        {
            this.GetDirectXManager = new DirectXManager();

            return this.GetDirectXManager.Initialize(this.GetWindowHandle, 0, 0, true, true);
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void BeginScene(float red, float green, float blue, float alpha)
        {
            this.GetDirectXManager.BeginScene(red, green, blue, alpha);
        }

        public void EndScense()
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