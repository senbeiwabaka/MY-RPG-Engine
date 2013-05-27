using SlimDX.Direct3D9;

namespace MY3DEngine
{
    public class DeviceManager
    {
        public Device LocalDevice { get; set; }

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

            LocalDevice = new Device(new Direct3D(), 0, DeviceType.Hardware, windowHandle, CreateFlags.HardwareVertexProcessing, PresentationParameters);
        }

        public void ResetDevice()
        {
            LocalDevice.Reset(PresentationParameters);
        }
    }
}