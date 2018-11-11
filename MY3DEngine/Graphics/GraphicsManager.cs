namespace MY3DEngine.Graphics
{
    using System;
    using MY3DEngine.Interfaces;
    using MY3DEngine.Managers;
    using SharpDX.Direct3D11;

    internal sealed class GraphicsManager : IGraphicManager
    {
        public Device GetDevice => GetDirectXManager?.GetDevice;
        public DeviceContext GetDeviceContext => GetDirectXManager?.GetDeviceContext;
        public DirectXManager GetDirectXManager { get; private set; } = new DirectXManager();

        public IntPtr GetWindowHandle { get; private set; }

        ~GraphicsManager()
        {
            Dispose(false);
        }

        /// <inheritdoc/>
        public void BeginScene(float red, float green, float blue, float alpha)
        {
            GetDirectXManager.BeginScene(red, green, blue, alpha);
        }

        /// <inheritdoc/>
        public void ChangeVSyncState(bool vSync = false)
        {
            GetDirectXManager.VerticalSync = vSync;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        public void EnableWireFrameMode(bool enableWireFrame = false)
        {
            GetDirectXManager.EnableWireFrameMode(enableWireFrame);
        }

        /// <inheritdoc/>
        public void EndScene()
        {
            GetDirectXManager.EndScene();
        }

        /// <inheritdoc/>
        public bool InitializeDirectXManager(IntPtr windowHandle, int screenWidth = 720, int screenHeight = 480, bool vsyncEnabled = true, bool fullScreen = false)
        {
            if (!GetDirectXManager.Initialize(windowHandle, screenWidth, screenHeight, vsyncEnabled, fullScreen))
            {
                return false;
            }

            GetWindowHandle = windowHandle;

            return true;
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                GetDirectXManager?.Dispose();
            }
        }
    }
}
