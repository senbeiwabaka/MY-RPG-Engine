using SlimDX.Direct3D9;
using System;
using System.Drawing;
using System.Threading;

namespace MY3DEngine
{
    public sealed class Engine : IDisposable
    {
        private bool _loaded;
        private Thread _renderThread;
        private Input _input;
        
        
        public static Engine GameEngine { get; set; }

        public IntPtr Window { get; private set; }
        public ExceptionHolder Exception { get; set; }
        public bool IsNotShutDown { get; set; }
        public DeviceManager LocalDevice { get; set; }
        public Camera Camera { get; set; }

        public Engine(IntPtr window)
        {
            IsNotShutDown = false;

            _loaded = false;

            Window = window;

            Exception = new ExceptionHolder();
        }

        public void Dispose()
        {
            _input.Dispose();
        }

        public void Initliaze(int width, int height)
        {
            _input = new Input();

            _loaded = true;

            IsNotShutDown = false;

            LocalDevice = new DeviceManager(Window, width, height);

            Camera = new Camera();

            Start();

            LocalDevice.LocalDevice.SetRenderState(RenderState.Lighting, false);
            LocalDevice.LocalDevice.SetRenderState(RenderState.CullMode, Cull.Counterclockwise);
            LocalDevice.LocalDevice.SetRenderState(RenderState.ZEnable, ZBufferType.UseZBuffer);
            LocalDevice.LocalDevice.SetRenderState(RenderState.NormalizeNormals, true);
            LocalDevice.LocalDevice.SetRenderState(RenderState.Ambient, Color.Gray.ToArgb());
            LocalDevice.LocalDevice.SetRenderState(RenderState.SpecularEnable, false);
        }

        public void Start()
        {
            if (_loaded)
            {
                _renderThread = new Thread(Renderer.RenderScene) {Name = "Rendering Thread"};
                _renderThread.Start();
            }
        }

        public void Shutdown()
        {
            IsNotShutDown = true;

            while (_renderThread.IsAlive)
            {
                _renderThread.Abort();
            }

            while (!LocalDevice.LocalDevice.Disposed && !_renderThread.IsAlive)
            {
                LocalDevice.LocalDevice.EvictManagedResources();
                LocalDevice.LocalDevice.Direct3D.Dispose();
                LocalDevice.LocalDevice.Dispose();
            }
        }
    }
}