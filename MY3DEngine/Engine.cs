using MY3DEngine.BaseObjects;
using MY3DEngine.Cameras;
using MY3DEngine.Graphics;
using MY3DEngine.Interfaces;
using MY3DEngine.Managers;
using MY3DEngine.Primitives;
using MY3DEngine.Shaders;
using Newtonsoft.Json;
using SharpDX;
using System;
using System.Collections;
using System.Threading;

namespace MY3DEngine
{
    /// <summary>
    /// The main entry point for the engine
    /// </summary>
    public sealed class Engine : IDisposable
    {
        private static Engine gameEngine;

        private readonly SettingsManager settingsManager;

        private ICamera camera;
        private IGraphicManager graphicsManager;
        private bool lighting;
        private IObjectManager manager;
        private Thread renderThread;
        private IShader shader;

        /// <summary>
        /// Engine Constructor
        /// </summary>
        public Engine()
        {
            graphicsManager = null;
            settingsManager = new SettingsManager();
        }

        ~Engine()
        {
            Dispose(false);
        }

        #region Properties

        /// <summary>
        /// If set to true then debugging information is added to a collection
        /// </summary>
        public static bool IsDebugginTurnedOn;

        /// <summary>
        ///Game engine instance
        /// </summary>
        public static Engine GameEngine => gameEngine ?? (gameEngine = new Engine());

        /// <summary>
        /// The world camera
        /// </summary>
        public ICamera Camera => camera;

        /// <summary>
        /// This is an instance of the exception manager class that manages exceptions
        /// </summary>
        public IExceptionManager Exception { get; set; }

        /// <summary>
        /// Root location of where the game files are stored
        /// </summary>
        public string FolderLocation { get; set; }

        /// <summary>
        /// The name of the game. This will be used when the exe is created as well.
        /// </summary>
        public string GameName { get; internal set; }

        /// <summary>
        /// This is the graphics manager that manages the graphics
        /// </summary>
        /// <remarks>It is overrideable</remarks>
        public IGraphicManager GraphicsManager => graphicsManager;

        /// <summary>
        /// Boolean stating whether or not the engine has been shutdown yet
        /// </summary>
        public bool IsNotShutDown { get; set; }

        /// <summary>
        /// This manages the game objects
        /// </summary>
        /// <remarks>It is overrideable</remarks>
        public IObjectManager Manager => manager;

        /// <summary>
        /// This manages the games settings
        /// </summary>
        public SettingsManager SettingsManager => settingsManager;

        /// <summary>
        /// This is the memory pointer to the window where the engine is rendering its contents
        /// </summary>
        public IntPtr Window { get; }

        #endregion Properties

        internal static bool DoesIniFileExist { get; set; }

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
                camera = new Camera();
                Exception = new ExceptionManager();
                manager = new ObjectManager();

                //Camera.Initialize(screenWidth, screenHeight);
                Camera.SetPosition(0.0f, 0.0f, -10.0f);

                shader.Initialize();

                Start();
            }
            catch (Exception e)
            {
                Exception.AddException(e);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Initialize the settings for the game
        /// </summary>
        /// <returns></returns>
        public bool InitializeSettings()
        {
            return SettingsManager.Initialize();
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
            graphicsManager = new GraphicsManager();

            return GraphicsManager.InitializeDirectXManager(windowHandle, screenWidth, screenHeight, vsyncEnabled, fullScreen);
        }

        // TODO: Refactor
        public bool Load(string path)
        {
            try
            {
                var contentsofFile = System.IO.File.ReadAllText(path);
                var jsonDeserializedData = JsonConvert.DeserializeObject(contentsofFile) as IEnumerable;

                foreach (var item in jsonDeserializedData)
                {
                    var gameObject = JsonConvert.DeserializeObject<BaseObject>(
                        item.ToString(),
                        new JsonSerializerSettings()
                        {
                            TypeNameHandling = TypeNameHandling.Auto
                        });

                    if (gameObject != null)
                    {
                        if (gameObject.IsPrimitive)
                        {
                            if (gameObject.IsTriangle)
                            {
                                Manager.AddObject(gameObject as Triangle, false);
                            }

                            if (gameObject.IsCube)
                            {
                                Manager.AddObject(gameObject as Cube, false);
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Exception.AddException(e);
            }

            return false;
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

        // TODO: Refactor
        public bool Save(string filePath)
        {
            try
            {
                var jsonSerializedData = JsonConvert.SerializeObject(
                    Manager.GetGameObjects,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });

                System.IO.File.WriteAllText(string.Format("{0}\\GameObjects.go", filePath), jsonSerializedData);

                return true;
            }
            catch (Exception e)
            {
                Exception.AddException(e);
            }

            return false;
        }

        /// <summary>
        /// Shutdown the thread and graphics and dispose of all resources
        /// </summary>
        public void Shutdown()
        {
            IsNotShutDown = false;

            if (renderThread != null)
            {
                while (renderThread.IsAlive)
                {
                    renderThread?.Abort();
                }
            }
        }

        /// <summary>
        /// Toggle the wireframe of all displayed objects
        /// </summary>
        public void WireFrame(bool enableWireFrameMode = false)
        {
            GraphicsManager.EnableWireFrameMode(enableWireFrameMode);
        }

        #endregion Methods

        #region Old Code

        /// <summary>
        /// Toggle all lights
        /// </summary>
        public void GlobalLights()
        {
            lighting = lighting == false ? true : false;
            //LocalDevice.ThisDevice.SetRenderState(RenderState.Lighting, _lighting);
            //LocalDevice.ThisDevice.SetRenderState(RenderState.Ambient, new SlimDX.Color4(Color.Gray).ToArgb());
        }

        #endregion Old Code

        #region Helper Methods

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                graphicsManager?.Dispose();
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
            Matrix viewMatrix = Camera.ViewMatrix;
            Matrix worldMatrix = GraphicsManager.GetDirectXManager.WorldMatrix;
            Matrix projectionMatrix = GraphicsManager.GetDirectXManager.ProjectionMatrix;

            // Rotate the world matrix by the rotation value so that the triangle will spin.
            //Matrix.RotationY(1.0f, out worldMatrix);

            shader.Render(manager.GameObjects, worldMatrix, viewMatrix, projectionMatrix);

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
