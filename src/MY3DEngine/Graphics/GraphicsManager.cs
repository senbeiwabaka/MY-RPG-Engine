// <copyright file="GraphicsManager.cs" company="MY Soft Games LLC">
//      Copyright (c) MY Soft Games LLC. All rights reserved.
// </copyright>

namespace MY3DEngine.Graphics
{
    using System;
    using MY3DEngine.Interfaces;
    using MY3DEngine.Managers;
    using Veldrid;

    internal sealed class GraphicsManager : IGraphicManager
    {
        private GraphicsDevice graphicsDevice;
        private Swapchain swapChain;
        private CommandList commandList;

        public IntPtr GetWindowHandle { get; private set; }

        ~GraphicsManager()
        {
            Dispose(false);
        }

        /// <inheritdoc/>
        public void BeginScene(float red, float green, float blue, float alpha)
        {
            commandList.Begin();
            commandList.SetFramebuffer(swapChain.Framebuffer);
            commandList.ClearColorTarget(0, new RgbaFloat(red, green, blue, alpha));
        }

        /// <inheritdoc/>
        public void ChangeVSyncState(bool vSync = false)
        {
            //GetDirectXManager.VerticalSync = vSync;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        public void EnableWireFrameMode(bool enableWireFrame = false)
        {
            //GetDirectXManager.EnableWireFrameMode(enableWireFrame);
        }

        /// <inheritdoc/>
        public void EndScene()
        {
            commandList.End();
            graphicsDevice.SubmitCommands(commandList);
            graphicsDevice.SwapBuffers(swapChain);
        }

        /// <inheritdoc/>
        public bool Initializer(IntPtr windowHandle, IntPtr hInstance, int screenWidth = 720, int screenHeight = 480, bool vsyncEnabled = true, bool fullScreen = false)
        {
            graphicsDevice = GraphicsDevice.CreateVulkan(new GraphicsDeviceOptions(true));

            SwapchainSource source = SwapchainSource.CreateWin32(windowHandle, hInstance);

            swapChain = graphicsDevice.ResourceFactory.CreateSwapchain(new SwapchainDescription(source, (uint)screenWidth, (uint)screenHeight, null, false));
            commandList = graphicsDevice.ResourceFactory.CreateCommandList();

            GetWindowHandle = windowHandle;

            return true;
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                //GetDirectXManager?.Dispose();
            }
        }
    }
}
