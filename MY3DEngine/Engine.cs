using SlimDX.Direct3D9;
using System;
using System.Drawing;
using System.Threading;

namespace MY3DEngine
{
    public sealed class Engine : IDisposable
    {
        private Input _input;
        private bool _lighting;
        private bool _loaded;
        private ObjectManager _manager;
        private Thread _renderThread;
        private bool _wireFrame;

        public static Engine GameEngine { get; set; }

        public Camera Camera { get; set; }

        public ExceptionHolder Exception { get; set; }

        public bool IsNotShutDown { get; set; }

        public DeviceManager LocalDevice { get; set; }

        public ObjectManager Manager
        {
            get
            {
                return _manager;
            }
            set
            {
                if (_manager == null)
                {
                    _manager = new ObjectManager();
                }
            }
        }

        public IntPtr Window { get; private set; }

        /// <summary>
        /// Engine Constructor. Must be called first
        /// </summary>
        /// <param name="window">The pointer of the window that the engine will be rendered too</param>
        public Engine(IntPtr window)
        {
            IsNotShutDown = false;

            _loaded = false;

            Window = new IntPtr();

            Window = window;

            Exception = new ExceptionHolder();

            _lighting = false;
            _wireFrame = true;
        }

        public void Dispose()
        {
            _input.Dispose();
        }

        /// <summary>
        /// Toggle all lights
        /// </summary>
        public void GlobalLights()
        {
            _lighting = _lighting == false ? true : false;
            LocalDevice.ThisDevice.SetRenderState(RenderState.Lighting, _lighting);
            LocalDevice.ThisDevice.SetRenderState(RenderState.Ambient, Color.Gray.ToArgb());
        }

        /// <summary>
        /// Must be called after the constructor. Finishes initalizing the variables
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void Initliaze(int width, int height)
        {
            _input = new Input();

            _loaded = true;

            IsNotShutDown = false;

            LocalDevice = new DeviceManager(Window, width, height);

            Camera = new Camera();

            Manager = new ObjectManager();

            Start();

            LocalDevice.ThisDevice.SetRenderState(RenderState.Lighting, _lighting);
            LocalDevice.ThisDevice.SetRenderState(RenderState.CullMode, Cull.Counterclockwise);
            LocalDevice.ThisDevice.SetRenderState(RenderState.ZEnable, ZBufferType.UseZBuffer);
            LocalDevice.ThisDevice.SetRenderState(RenderState.NormalizeNormals, true);

            LocalDevice.ThisDevice.SetRenderState(RenderState.SpecularEnable, false);
        }

        /// <summary>
        /// Shutdown the thread and graphics and dispose of all resources
        /// </summary>
        public void Shutdown()
        {
            IsNotShutDown = true;

            while (_renderThread.IsAlive)
            {
                _renderThread.Abort();
            }

            while (!LocalDevice.ThisDevice.Disposed && !_renderThread.IsAlive)
            {
                LocalDevice.ThisDevice.EvictManagedResources();
                LocalDevice.ThisDevice.Direct3D.Dispose();
                LocalDevice.ThisDevice.Dispose();
            }
        }

        public void Start()
        {
            if (_loaded)
            {
                _renderThread = new Thread(Renderer.RenderScene) { Name = "Rendering Thread" };
                _renderThread.Start();
            }
        }

        /// <summary>
        /// Toggle the wireframe of all displayed objects
        /// </summary>
        public void WireFrame()
        {
            if (_wireFrame)
            {
                LocalDevice.ThisDevice.SetRenderState(RenderState.FillMode, FillMode.Wireframe);
                _wireFrame = false;
            }
            else
            {
                LocalDevice.ThisDevice.SetRenderState(RenderState.FillMode, FillMode.Solid);
                _wireFrame = true;
            }
        }
    }
}