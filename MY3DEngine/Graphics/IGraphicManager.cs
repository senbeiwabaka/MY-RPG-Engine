using MY3DEngine.GeneralManagers;
using SharpDX.Direct3D11;
using System;

namespace MY3DEngine.Graphics
{
    public interface IGraphicManager : IDisposable
    {
        Device GetDevice { get; }
        DeviceContext GetDeviceContext { get; }
        DirectXManager GetDirectXManager { get; }

        void BeginScene(float red, float green, float blue, float alpha);

        /// <summary>
        /// Change the VSync state
        /// </summary>
        /// <param name="vSync"></param>
        void ChangeVSyncState(bool vSync = false);

        /// <summary>
        /// Set this to true to view all of the objects in their wireframe mode
        /// </summary>
        /// <param name="enableWireFrame"></param>
        void EnableWireFrameMode(bool enableWireFrame = false);

        /// <summary>
        /// End the game scene
        /// </summary>
        void EndScene();
        
        bool InitializeDirectXManager(IntPtr windowHandle, int screenWidth = 720, int screenHeight = 480, bool vsyncEnabled = true, bool fullScreen = false);
    }
}