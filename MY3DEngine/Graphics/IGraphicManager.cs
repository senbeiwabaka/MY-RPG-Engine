using System;

namespace MY3DEngine.Graphics
{
    public interface IGraphicManager
    {
        bool InitializeDirectXManager(IntPtr windowHandle);

        void Initialize();

        void BeginScene(float red, float green, float blue, float alpha);

        void EndScense();

        void EnableAlphaBlending(bool enable = false);

        void EnableZBuffer(bool enable = false);
    }
}