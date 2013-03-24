using System;
using System.Threading;

namespace MY3DEngine
{
    public sealed class Engine : IDisposable
    {
        private bool _loaded;
        private Thread _renderThread;
        private Input _input;
        

        public static Engine GameEngine { get; set; }

        public IntPtr Window { get; set; }
        public ExceptionHolder Exception { get; set; }
        public bool IsNotShutDown { get; set; }

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

            IsNotShutDown = true;
        }

        public void Initliaze()
        {
            _input = new Input(Window);

            _loaded = true;

            IsNotShutDown = false;
        }

        public void Start()
        {
            if (_loaded)
            {
                _renderThread = new Thread(Run) {Name = "Rendering Thread"};
                _renderThread.Start();
            }
        }

        private void Run()
        {
            while (IsNotShutDown)
            {
                
            }
        }
    }
}