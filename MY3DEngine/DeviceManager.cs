using SlimDX.Direct3D9;

namespace MY3DEngine
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DeviceManager
    {
        public Device ThisDevice { get; set; }

        public PresentParameters PresentationParameters { get; set; }

        public DeviceManager(System.IntPtr windowHandle, int width, int height)
        {
            PresentationParameters = new PresentParameters()
            {
                BackBufferWidth = width,
                BackBufferHeight = height,
                BackBufferFormat = Format.A8R8G8B8,
                BackBufferCount = 1,
                Multisample = MultisampleType.None,
                MultisampleQuality = 0,
                SwapEffect = SwapEffect.Discard,
                Windowed = true,
                DeviceWindowHandle = windowHandle,
                EnableAutoDepthStencil = true,
                AutoDepthStencilFormat = Format.D16,
                PresentFlags = PresentFlags.None,
                FullScreenRefreshRateInHertz = 0,
                PresentationInterval = PresentInterval.Default
            };

            ThisDevice = new Device(new Direct3D(), 0, DeviceType.Hardware, windowHandle, CreateFlags.HardwareVertexProcessing, PresentationParameters);
        }

        public void ResetDevice()
        {
            ThisDevice.Reset(PresentationParameters);
        }
    }
}