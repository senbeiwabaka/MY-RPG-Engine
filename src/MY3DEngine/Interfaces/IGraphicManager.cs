// <copyright file="IGraphicManager.cs" company="MY Soft Games LLC">
//      Copyright (c) MY Soft Games LLC. All rights reserved.
// </copyright>

namespace MY3DEngine.Interfaces
{
    using System;
    using MY3DEngine.Managers;
    using SharpDX.Direct3D11;

    public interface IGraphicManager : IDisposable
    {
        /// <summary>
        /// Gets get the device
        /// </summary>
        //Device GetDevice { get; }

        /// <summary>
        /// Gets get the device context from the device
        /// </summary>
        //DeviceContext GetDeviceContext { get; }

        //DirectXManager GetDirectXManager { get; }

        /// <summary>
        /// The begin scene logic
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <param name="alpha"></param>
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
