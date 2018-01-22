using MY3DEngine.BaseObjects;
using MY3DEngine.Cameras;
using MY3DEngine.Managers;
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

        private IShader shader;
        private ICamera camera;
        private IGraphicManager graphicsManager;
        private bool lighting;
        private IObjectManager manager;
        private Thread renderThread;
        
        /// <summary>
        /// Engine Constructor
        /// </summary>
        public Engine()
        {
            this.graphicsManager = null;
        }

        /// <summary>
        ///
        /// </summary>
        public static Engine GameEngine => gameEngine ?? (gameEngine = new Engine());

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
        public IObjectManager Manager
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
        /// Add compiler errors to the exception list
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="line"></param>
        /// <param name="column"></param>
        /// <param name="errorNumber"></param>
        /// <param name="errorText"></param>
        public void AddCompilerErrors(string fileName, int line, int column, string errorNumber, string errorText)
        {
            if (IsDebugginTurnedOn)
            {
                GameEngine.Exception.Exceptions.Add(new ExceptionData($"{fileName} has had an error compiling.", $"On line {line} in column {column}. The error code is {errorNumber}.", errorText));
            }
        }

        /// <summary>
        /// Add exceptions to the list if debugging is enabled.
        /// </summary>
        /// <param name="e"></param>
        public void AddException(Exception e)
        {
            if (IsDebugginTurnedOn)
            {
                var exception = e;

                GameEngine.Exception.Exceptions.Add(new ExceptionData(exception.Message, exception.Source, exception.StackTrace));

                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;

                    GameEngine.Exception.Exceptions.Add(new ExceptionData(exception.Message, exception.Source, exception.StackTrace));
                }
            }
        }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(true);
        }

        public bool Initialize(int screenWidth = 720, int screenHeight = 480)
        {
            try
            {
                this.shader = new Shader();
                this.camera = new Camera();
                this.Exception = new ExceptionManager();
                this.Manager = new ObjectManager();

                //this.Camera.Initialize(screenWidth, screenHeight);
                this.Camera.SetPosition(0.0f, 0.0f, -10.0f);

                if(!this.shader.Initialize())
                {
                    return false;
                }

                this.Start();
            }
            catch(Exception e)
            {
                this.AddException(e);

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
            this.graphicsManager = new GraphicsManager();

            return this.GraphicsManager.InitializeDirectXManager(windowHandle, screenWidth, screenHeight, vsyncEnabled, fullScreen);
        }

        public bool Load(string path)
        {
            try
            {
                var contentsofFile = System.IO.File.ReadAllText(path);
                var jsonDeserializedData = JsonConvert.DeserializeObject(contentsofFile) as IEnumerable;

                foreach (var item in jsonDeserializedData)
                {
                    var gameObject = JsonConvert.DeserializeObject<GameObject>(
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

        public void Run()
        {
            while (GameEngine.IsNotShutDown)
            {
                this.Update();
                this.Render();
            }
        }

        /// <summary>
        /// Save the game to a folder
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
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
        /// Toggle the wireframe of all displayed objects
        /// </summary>
        public void WireFrame(bool enableWireFrameMode = false)
        {
            this.GraphicsManager.EnableWireFrameMode(enableWireFrameMode);
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
        ///
        /// </summary>
        private void Start()
        {
            this.IsNotShutDown = true;
            
            this.renderThread = new Thread(this.Run) { Name = "RenderingThread" };
            this.renderThread.Start();
        }

        private void Update()
        {
            //this.CalculateFrameRateStats();
        }

        #endregion Helper Methods
    }
}