using MY3DEngine.Managers;
using SharpDX.Direct3D11;
using System;

namespace MY3DEngine.Graphics
{
    internal sealed class GraphicsManager : IGraphicManager
    {
        public Device GetDevice => this.GetDirectXManager?.GetDevice;
        public DeviceContext GetDeviceContext => this.GetDirectXManager?.GetDeviceContext;
        public DirectXManager GetDirectXManager { get; private set; } = new DirectXManager();

        public IntPtr GetWindowHandle { get; private set; }

        ~GraphicsManager()
        {
            Dispose(false);
        }

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

            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        public void EnableWireFrameMode(bool enableWireFrame = false)
        {
            this.GetDirectXManager.EnableWireFrameMode(enableWireFrame);
        }

        /// <inheritdoc/>
        public void EndScene()
        {
            this.GetDirectXManager.EndScene();
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
