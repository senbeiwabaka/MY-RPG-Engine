using System;

namespace MY3DEngine.Graphics
{
    public interface IGraphicManager
    {
        bool InitializeDirectXManager(IntPtr windowHandle, int screenWidth = 720, int screenHeight = 480, bool vsyncEnabled = true, bool fullScreen = false);

        void Initialize();

        void BeginScene(float red, float green, float blue, float alpha);

        void EndScene();

        void EnableAlphaBlending(bool enable = false);

        void EnableZBuffer(bool enable = false);
    }
}