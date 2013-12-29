using SlimDX.Direct3D9;

namespace MY3DEngine
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DeviceManager
    {
        /// <summary>
        /// The link to the graphics
        /// </summary>
        public Device ThisDevice { get; set; }

        /// <summary>
        /// The values to sent to the graphics for rendering capabilities
        /// </summary>
        public PresentParameters PresentationParameters { get; set; }

        /// <summary>
        /// Constructor for a new device
        /// </summary>
        /// <param name="windowHandle">The window handle you want to connect to the graphics</param>
        /// <param name="width">The width of the window</param>
        /// <param name="height">The height of the window</param>
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

        /// <summary>
        /// Reset the device upon losing it
        /// </summary>
        public void ResetDevice()
        {
            ThisDevice.Reset(PresentationParameters);
        }
    }
}