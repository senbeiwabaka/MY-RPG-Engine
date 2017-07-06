using MY3DEngine.Graphics;
using System;
using System.Threading;

namespace MY3DEngine
{
    public sealed class Engine : IDisposable
    {
        private GraphicsManager graphicsManager;
        private Input input;
        private bool lighting;
        private bool loaded;
        private ObjectManager manager;
        private Thread renderThread;
        private bool wireFrame;

        private static Engine gameEngine;

        /// <summary>
        ///
        /// </summary>
        public static Engine GameEngine => gameEngine ?? (gameEngine = new Engine());

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
        public GraphicsManager GraphicsManager => this.graphicsManager;

        /// <summary>
        ///
        /// </summary>
        public static readonly bool IsDebug;

        /// <summary>
        /// Engine Constructor
        /// </summary>
        public Engine()
        {
            this.graphicsManager = null;
        }

        // TODO: Update
        public void Dispose()
        {
            this.graphicsManager?.Dispose();
            this.input?.Dispose();
        }

        public bool InitliazeGraphics(IntPtr windowHandle, int screenWidth = 720, int screenHeight = 480, bool vsyncEnabled = true, bool fullScreen = false)
        {
            this.graphicsManager = new GraphicsManager();

            return this.graphicsManager.InitializeDirectXManager(windowHandle, screenWidth, screenHeight, vsyncEnabled, fullScreen);
        }

        public bool Initialize(IntPtr handle)
        {
            this.graphicsManager.Initialize();

            this.Camera = new Camera();
            this.Exception = new ExceptionHolder();
            this.Manager = new ObjectManager();

            this.Start();

            return true;
        }

        public void Run()
        {
            while (Engine.GameEngine.IsNotShutDown)
            {
                this.Update();
                this.Render();
            }
        }

        #region Old Code

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
        /// Shutdown the thread and graphics and dispose of all resources
        /// </summary>
        public void Shutdown()
        {
            this.IsNotShutDown = false;

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
            //if (!this.loaded)
            //{
            //    return;
            //}

            this.IsNotShutDown = true;

            this.renderThread = new Thread(this.Run) { Name = "RenderingThread" };
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

        #endregion Old Code

        #region Helper Methods

        private void Update()
        {
        }

        private void Render()
        {
            this.graphicsManager.BeginScene(0.0f, 0.0f, 0.0f, 1.0f);

            // render stuff goes here

            this.graphicsManager.EndScene();
        }

        #endregion Helper Methods
    }
}