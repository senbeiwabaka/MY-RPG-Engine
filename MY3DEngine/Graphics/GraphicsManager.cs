using MY3DEngine.GeneralManagers;
using SharpDX.Direct3D11;
using System;

namespace MY3DEngine.Graphics
{
    sealed class GraphicsManager : IGraphicManager
    {
        public Device GetDevice => this.GetDirectXManager?.GetDevice;
        public DeviceContext GetDeviceContext => this.GetDirectXManager?.GetDeviceContext;
        public DirectXManager GetDirectXManager { get; private set; } = new DirectXManager();

        public IntPtr GetWindowHandle { get; private set; }
        

        /// <inheritdoc/>
        public void BeginScene(float red, float green, float blue, float alpha)
        {
            this.GetDirectXManager.BeginScene(red, green, blue, alpha);
        }

        /// <inheritdoc/>
        public void ChangeVSyncState(bool vSync = false)
        {
            this.GetDirectXManager.VSync = vSync;
        }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(true);
        }

        /// <inheritdoc/>
        public void EnableAlphaBlending(bool enable = false)
        {
            this.GetDirectXManager.EnableAlphaBlending(enable);
        }

        /// <inheritdoc/>
        public void EnableZBuffer(bool enable = false)
        {
            this.GetDirectXManager.EnableZBuffer(enable);
        }

        /// <inheritdoc/>
        public void EndScene()
        {
            this.GetDirectXManager.EndScene();
        }

        /// <inheritdoc/>
        public void Initialize()
        {
        }

        /// <inheritdoc/>
        public bool InitializeDirectXManager(IntPtr windowHandle, int screenWidth = 720, int screenHeight = 480, bool vsyncEnabled = true, bool fullScreen = false)
        {
            if (!this.GetDirectXManager.Initialize(windowHandle, screenWidth, screenHeight, vsyncEnabled, fullScreen))
            {
                return false;
            }

            this.GetWindowHandle = windowHandle;

            return true;
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.GetDirectXManager?.Dispose();
            }
        }
    }
}