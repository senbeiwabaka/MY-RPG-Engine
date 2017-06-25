using System;
using System.Drawing;
using System.Threading;

namespace MY3DEngine
{
    public sealed class Engine : IDisposable
    {
        private Input input;
        private bool lighting;
        private bool loaded;
        private ObjectManager manager;
        private Thread renderThread;
        private bool wireFrame;

        /// <summary>
        /// 
        /// </summary>
        public static Engine GameEngine { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Camera Camera { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ExceptionHolder Exception { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsNotShutDown { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DirectXManager LocalDevice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ObjectManager Manager
        {
            get
            {
                return this.manager;
            }

            set
            {
                if (this.manager == null)
                {
                    this.manager = new ObjectManager();
                }

                this.manager = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IntPtr Window { get; }

        /// <summary>
        /// 
        /// </summary>
        public static readonly bool IsDebug;

        /// <summary>
        /// Engine Constructor. Must be called first
        /// </summary>
        /// <param name="window">The pointer of the window that the engine will be rendered too</param>
        public Engine(IntPtr window, bool isDebug = true)
        {
            IsNotShutDown = false;

            this.loaded = false;

            Window = new IntPtr();

            Window = window;

            Exception = new ExceptionHolder();

            this.lighting = false;
            this.wireFrame = true;
        }

        public void Dispose()
        {
            this.input.Dispose();

            while (!this.renderThread.IsAlive)
            {
                LocalDevice.Dispose();
            }
        }

        /// <summary>
        /// Toggle all lights
        /// </summary>
        public void GlobalLights()
        {
            this.lighting = this.lighting == false ? true : false;
            //LocalDevice.ThisDevice.SetRenderState(RenderState.Lighting, _lighting);
            //LocalDevice.ThisDevice.SetRenderState(RenderState.Ambient, new SlimDX.Color4(Color.Gray).ToArgb());
        }

        /// <summary>
        /// Must be called after the constructor. Finishes initalizing the variables
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void Initliaze(int width, int height)
        {
            this.input = new Input();

            this.loaded = true;

            this.IsNotShutDown = false;

            this.LocalDevice = new DirectXManager(this.Window, width, height);

            this.Camera = new Camera();

            this.Manager = new ObjectManager();

            this.Start();
            
            //LocalDevice.ThisDevice.SetRenderState(RenderState.Lighting, _lighting);
            //LocalDevice.ThisDevice.SetRenderState(RenderState.CullMode, Cull.Counterclockwise);
            //LocalDevice.ThisDevice.SetRenderState(RenderState.ZEnable, ZBufferType.UseZBuffer);
            //LocalDevice.ThisDevice.SetRenderState(RenderState.NormalizeNormals, true);

            //LocalDevice.ThisDevice.SetRenderState(RenderState.SpecularEnable, false);
        }

        /// <summary>
        /// Shutdown the thread and graphics and dispose of all resources
        /// </summary>
        public void Shutdown()
        {
            this.IsNotShutDown = true;

            while (this.renderThread.IsAlive)
            {
                this.renderThread.Abort();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            if (!this.loaded)
            {
                return;
            }

            this.renderThread = new Thread(Renderer.RenderScene) { Name = "RenderingThread" };
            this.renderThread.Start();
        }

        /// <summary>
        /// Toggle the wireframe of all displayed objects
        /// </summary>
        public void WireFrame()
        {
            //if (_wireFrame)
            //{
            //    LocalDevice.ThisDevice.SetRenderState(RenderState.FillMode, FillMode.Wireframe);
            //    _wireFrame = false;
            //}
            //else
            //{
            //    LocalDevice.ThisDevice.SetRenderState(RenderState.FillMode, FillMode.Solid);
            //    _wireFrame = true;
            //}
        }
    }
}