using MY3DEngine.Cameras;
using MY3DEngine.Graphics;
using MY3DEngine.Interfaces;
using MY3DEngine.Managers;
using MY3DEngine.Shaders;
using NLog;
using System;
using System.Threading;

namespace MY3DEngine
{
    /// <summary>
    /// The main entry point for the engine
    /// </summary>
    public sealed class Engine : IDisposable
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        #region Fields

        private Thread renderThread;
        private IShader shader;

        #endregion Fields

        ~Engine()
        {
            Dispose(false);
        }

        /// <summary>
        ///Game engine instance
        /// </summary>
        public static Engine GameEngine => new Engine();

        #region Properties

        /// <summary>
        /// The world camera
        /// </summary>
        public ICamera Camera { get; private set; }

        /// <summary>
        /// This is an instance of the exception manager class that manages exceptions
        /// </summary>
        public IExceptionManager Exception { get; private set; }

        /// <summary>
        /// This is the graphics manager that manages the graphics
        /// </summary>
        /// <remarks>It is override-able</remarks>
        public IGraphicManager GraphicsManager { get; private set; }

        /// <summary>
        /// Boolean stating whether or not the engine has been shutdown yet
        /// </summary>
        public bool IsNotShutDown { get; set; }

        /// <summary>
        /// This manages the game objects
        /// </summary>
        /// <remarks>It is override-able</remarks>
        public IObjectManager Manager { get; private set; }

        /// <summary>
        /// This manages the games settings
        /// </summary>
        public SettingsManager SettingsManager { get; } = new SettingsManager();

        /// <summary>
        /// This is the memory pointer to the window where the engine is rendering its contents
        /// </summary>
        public IntPtr Window { get; }

        /// <summary>
        /// If set to true then debugging information is added to a collection
        /// </summary>
        public static bool IsDebugginTurnedOn { get; set; }

        #endregion Properties

        #region Methods

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        public bool Initialize()
        {
            try
            {
                shader = new TextureShader();
                Camera = new Camera();
                Exception = new ExceptionManager();
                Manager = new ObjectManager();

                Camera.SetPosition(0.0f, 0.0f, -10.0f);

                shader.Initialize();

                Start();
            }
            catch (Exception exception)
            {
                Exception.AddException(exception);

                Logger.Error(exception, $"{nameof(Engine)}.{nameof(Initialize)} method errored.");

                return false;
            }

            return true;
        }

        /// <summary>
        /// Initialize the game engines graphics
        /// </summary>
        /// <param name="windowHandle"></param>
        /// <param name="screenWidth"></param>
        /// <param name="screenHeight"></param>
        /// <param name="vsyncEnabled"></param>
        /// <param name="fullScreen"></param>
        /// <returns></returns>
        public bool InitliazeGraphics(IntPtr windowHandle, int screenWidth = 720, int screenHeight = 480, bool vsyncEnabled = true, bool fullScreen = false)
        {
            GraphicsManager = new GraphicsManager();

            return GraphicsManager.InitializeDirectXManager(windowHandle, screenWidth, screenHeight, vsyncEnabled, fullScreen);
        }

        /// <summary>
        /// Run the games update and render code if the game hasn't been shutdown
        /// </summary>
        public void Run()
        {
            while (GameEngine.IsNotShutDown)
            {
                Update();
                Render();
            }
        }

        /// <summary>
        /// Shutdown the thread and graphics and dispose of all resources
        /// </summary>
        public void Shutdown()
        {
            IsNotShutDown = true;

            if (renderThread != null)
            {
                while (renderThread.IsAlive)
                {
                    renderThread?.Abort();
                }
            }

            IsNotShutDown = false;
        }

        /// <summary>
        /// Toggle the wire-frame of all displayed objects
        /// </summary>
        public void WireFrame(bool enableWireFrameMode = false)
        {
            GraphicsManager.EnableWireFrameMode(enableWireFrameMode);
        }

        #endregion Methods

        #region Helper Methods

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                GraphicsManager?.Dispose();
                Manager?.Dispose();
                shader?.Dispose();
            }
        }

        /// <summary>
        /// Any game render logic
        /// </summary>
        private void Render()
        {
            GraphicsManager.BeginScene(0.0f, 0.0f, 0.0f, 1.0f);

            // Generate the view matrix based on the camera's position.
            Camera.Render();

            // Get the world, view, and projection matrices from camera and d3d objects.
            var viewMatrix = Camera.ViewMatrix;
            var worldMatrix = GraphicsManager.GetDirectXManager.WorldMatrix;
            var projectionMatrix = GraphicsManager.GetDirectXManager.ProjectionMatrix;

            // Rotate the world matrix by the rotation value so that the triangle will spin.
            //Matrix.RotationY(1.0f, out worldMatrix);

            shader.Render(Manager.GameObjects, worldMatrix, viewMatrix, projectionMatrix);

            GraphicsManager.EndScene();
        }

        /// <summary>
        /// Start the background thread for running the game engine in the frame. (Non-UI thread)
        /// </summary>
        private void Start()
        {
            IsNotShutDown = true;

            renderThread = new Thread(Run) { Name = "RenderingThread" };
            renderThread.Start();
        }

        /// <summary>
        /// And game update logic
        /// </summary>
        private void Update()
        {
            //CalculateFrameRateStats();
        }

        #endregion Helper Methods
    }
}
