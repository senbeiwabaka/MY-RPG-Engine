using MY3DEngine.BaseObjects;
using MY3DEngine.Cameras;
using MY3DEngine.Graphics;
using MY3DEngine.Managers;
using MY3DEngine.Models;
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
        /// <summary>
        /// If set to true then debugging information is added to a collection
        /// </summary>
        public static bool IsDebugginTurnedOn;

        private static Engine gameEngine;
        private ICamera camera;
        private IGraphicManager graphicsManager;
        private bool lighting;
        private IObjectManager manager;
        private Thread renderThread;
        private SettingsManager settingsManager;
        private IShader shader;

        /// <summary>
        /// Engine Constructor
        /// </summary>
        public Engine()
        {
            this.graphicsManager = null;
            this.settingsManager = new SettingsManager();
        }

        /// <summary>
        ///
        /// </summary>
        public static Engine GameEngine => gameEngine ?? (gameEngine = new Engine());

        /// <summary>
        /// The world camera
        /// </summary>
        public ICamera Camera => this.camera;

        /// <summary>
        ///
        /// </summary>
        public ExceptionManager Exception { get; set; }

        /// <summary>
        /// Root location of where the game files are stored
        /// </summary>
        public string FolderLocation { get; set; }

        /// <summary>
        /// The name of the game. This will be used when the exe is created as well.
        /// </summary>
        public string GameName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public IGraphicManager GraphicsManager => this.graphicsManager;

        /// <summary>
        ///
        /// </summary>
        public bool IsNotShutDown { get; set; }

        /// <summary>
        ///
        /// </summary>
        public IObjectManager Manager => this.manager;

        public SettingsManager SettingsManager => this.settingsManager;

        /// <summary>
        ///
        /// </summary>
        public IntPtr Window { get; }

        internal static bool DoesIniFileExist { get; set; }

        #region Methods

        /// <summary>
        /// Add compiler error to the system error system for user display
        /// </summary>
        /// <param name="fileName">The name of the file with the error</param>
        /// <param name="line">The line of the error</param>
        /// <param name="column">The character column of the error</param>
        /// <param name="errorNumber">The CS error code #</param>
        /// <param name="errorText">The error description</param>
        public void AddCompilerErrors(string fileName, int line, int column, string errorNumber, string errorText)
        {
            this.AddErrorMessage($"{fileName} has had an error compiling.", $"On line {line} in column {column}. The error code is {errorNumber}.", errorText);
        }

        /// <summary>
        /// Add an error message to the system error system for user display
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="source">The source of the error</param>
        /// <param name="stackTrace">The StackTrace of the error</param>
        public void AddErrorMessage(string message, string source, string stackTrace)
        {
            if (IsDebugginTurnedOn)
            {
                GameEngine.Exception.Exceptions.Add(new ExceptionData(message, source, stackTrace));
            }
        }

        /// <summary>
        /// Add exception to the system error system for user display
        /// </summary>
        /// <param name="e">The exception to add</param>
        public void AddException(Exception e)
        {
            if (IsDebugginTurnedOn)
            {
                var exception = e;

                this.AddErrorMessage(exception.Message, exception.Source, exception.StackTrace);

                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;

                    this.AddErrorMessage(exception.Message, exception.Source, exception.StackTrace);
                }
            }
        }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(true);
        }

        public bool Initialize()
        {
            try
            {
                this.shader = new TextureShader();
                this.camera = new Camera();
                this.Exception = new ExceptionManager();
                this.manager = new ObjectManager();

                //this.Camera.Initialize(screenWidth, screenHeight);
                this.Camera.SetPosition(0.0f, 0.0f, -10.0f);

                if (!this.shader.Initialize())
                {
                    return false;
                }

                this.Start();
            }
            catch (Exception e)
            {
                this.AddException(e);

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
            return this.SettingsManager.Initialize();
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
            this.graphicsManager = new GraphicsManager();

            return this.GraphicsManager.InitializeDirectXManager(windowHandle, screenWidth, screenHeight, vsyncEnabled, fullScreen);
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
                                this.Manager.AddObject(gameObject as Triangle, false);
                            }

                            if (gameObject.IsCube)
                            {
                                this.Manager.AddObject(gameObject as Cube, false);
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                this.AddException(e);
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
                this.Update();
                this.Render();
            }
        }

        // TODO: Refactor
        public bool Save(string filePath)
        {
            try
            {
                var jsonSerializedData = JsonConvert.SerializeObject(
                    this.Manager.GetGameObjects,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });

                System.IO.File.WriteAllText(string.Format("{0}\\GameObjects.go", filePath), jsonSerializedData);

                return true;
            }
            catch (Exception e)
            {
                this.AddException(e);
            }

            return false;
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
        /// Toggle the wireframe of all displayed objects
        /// </summary>
        public void WireFrame(bool enableWireFrameMode = false)
        {
            this.GraphicsManager.EnableWireFrameMode(enableWireFrameMode);
        }

        #endregion Methods

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

        #endregion Old Code

        #region Helper Methods

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.graphicsManager?.Dispose();
                this.shader?.Dispose();
            }
        }

        /// <summary>
        /// Any game render logic
        /// </summary>
        private void Render()
        {
            this.GraphicsManager.BeginScene(0.0f, 0.0f, 0.0f, 1.0f);

            // Generate the view matrix based on the camera's position.
            this.Camera.Render();

            // Get the world, view, and projection matrices from camera and d3d objects.
            Matrix viewMatrix = this.Camera.ViewMatrix;
            Matrix worldMatrix = this.GraphicsManager.GetDirectXManager.WorldMatrix;
            Matrix projectionMatrix = this.GraphicsManager.GetDirectXManager.ProjectionMatrix;

            // Rotate the world matrix by the rotation value so that the triangle will spin.
            //Matrix.RotationY(1.0f, out worldMatrix);

            this.shader.Render(this.manager.GameObjects, worldMatrix, viewMatrix, projectionMatrix);

            this.GraphicsManager.EndScene();
        }

        /// <summary>
        /// Start the background thread for running the game engine in the frame. (Non-UI thread)
        /// </summary>
        private void Start()
        {
            this.IsNotShutDown = true;

            this.renderThread = new Thread(this.Run) { Name = "RenderingThread" };
            this.renderThread.Start();
        }

        /// <summary>
        /// And game update logic
        /// </summary>
        private void Update()
        {
            //this.CalculateFrameRateStats();
        }

        #endregion Helper Methods
    }
}