using MY3DEngine.BaseObjects;
using MY3DEngine.Cameras;
using MY3DEngine.GeneralManagers;
using MY3DEngine.Graphics;
using MY3DEngine.Primitives;
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
        private GraphicsManager graphicsManager;
        private Input input;
        private bool lighting;
        private bool loaded;
        private ObjectManager manager;
        private Thread renderThread;
        private bool wireFrame;
        private Camera camera;

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

        /// <summary>
        ///
        /// </summary>
        public ExceptionHolder Exception { get; set; }

        /// <summary>
        ///
        /// </summary>
        public GraphicsManager GraphicsManager => this.graphicsManager;

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

        public Camera Camera => this.camera;

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

        public void AddCompilerErrors(string fileName, int line, int column, string errorNumber, string errorText)
        {
            if (IsDebugginTurnedOn)
            {
                GameEngine.Exception.Exceptions.Add(new ExceptionData($"{fileName} has had an error compiling.", $"On line {line} in column {column}. The error code is {errorNumber}.", errorText));
            }
        }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(true);
        }

        public bool Initialize(IntPtr handle)
        {
            this.graphicsManager.Initialize();

            this.camera = new Camera();
            this.Exception = new ExceptionHolder();
            this.Manager = new ObjectManager();

            this.Start();

            return true;
        }

        public bool InitliazeGraphics(IntPtr windowHandle, int screenWidth = 720, int screenHeight = 480, bool vsyncEnabled = true, bool fullScreen = false)
        {
            this.graphicsManager = new GraphicsManager();

            return this.graphicsManager.InitializeDirectXManager(windowHandle, screenWidth, screenHeight, vsyncEnabled, fullScreen);
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

                            if(gameObject.IsCube)
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
                    this.Manager.GetGameObjects(),
                    new JsonSerializerSettings()
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

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.graphicsManager?.Dispose();
                this.input?.Dispose();
            }
        }

        private void Render()
        {
            Matrix worldMatrix, viewMatrix, projectionMatrix;

            this.graphicsManager.BeginScene(0.0f, 0.0f, 0.0f, 1.0f);

            // Generate the view matrix based on the camera's position.
            this.Camera.Render();

            // Get the world, view, and projection matrices from the camera and d3d objects.
            //this.GraphicsManager.GetDevice.GetWorldMatrix(worldMatrix);
            //this.Camera.
            //m_Direct3D->GetProjectionMatrix(projectionMatrix);

            // render stuff goes here
            lock (this.manager.GameObjects)
            {
                foreach (var gameObject in this.manager.GameObjects)
                {
                    gameObject.Render();
                }
            }

            this.graphicsManager.EndScene();
        }

        private void Update()
        {
        }

        #endregion Helper Methods
    }
}