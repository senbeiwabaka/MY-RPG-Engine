using System;
using System.Drawing;
using System.Globalization;
using SlimDX;
using SlimDX.DirectInput;

namespace MY3DEngine
{
    internal class Input : IDisposable
    {
        private Keyboard _keyboard;
        private Mouse _mouse;
        private readonly DirectInput _directInput;

        public Point Point { get; set; }

        public Input(IntPtr window)
        {
            _directInput = new DirectInput();

            try
            {
                Result result;

                _keyboard = new Keyboard(_directInput);

                if ((result = _keyboard.SetCooperativeLevel(window, CooperativeLevel.Foreground | CooperativeLevel.Nonexclusive)) != ResultCode.Success)
                {
                    Engine.GameEngine.Exception.Exceptions.Add(new ExceptionData(result.Description,
                        result.Name, "keyboard cooperation"));
                }

                _mouse = new Mouse(_directInput);

                if ((result = _mouse.SetCooperativeLevel(window, CooperativeLevel.Foreground | CooperativeLevel.Nonexclusive)) != ResultCode.Success)
                {
                    Engine.GameEngine.Exception.Exceptions.Add(new ExceptionData(result.Description,
                        result.Name, "mouse cooperation"));
                }
                
                if ((result = _keyboard.Acquire()) != ResultCode.Success)
                {
                    Engine.GameEngine.Exception.Exceptions.Add(new ExceptionData(result.Description,
                        result.Name, "keyboard acquire"));
                }

                if ((result = _mouse.Acquire()) != ResultCode.Success)
                {
                    Engine.GameEngine.Exception.Exceptions.Add(new ExceptionData(result.Description,
                        result.Name, "mouse acquire"));
                }

                Engine.GameEngine.Exception.Exceptions.Add(new ExceptionData("worked", "worked", "worked"));
            }
            catch (DirectInputException e)
            {
                Engine.GameEngine.Exception.Exceptions.Add(new ExceptionData(e.Message, e.Source, e.StackTrace));
            }
            catch (Exception e)
            {
                Engine.GameEngine.Exception.Exceptions.Add(new ExceptionData(e.Message, e.Source, e.StackTrace));
            }
            finally
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            if (_keyboard != null)
            {
                _keyboard.Unacquire();
                _keyboard.Dispose();
                _keyboard = null;
            }

            if (_mouse != null)
            {
                _mouse.Unacquire();
                _mouse.Dispose();
                _mouse = null;
            }

            if (_directInput != null)
            {
                _directInput.Dispose();
            }
        }

        public void Update()
        {
            
        }

        public bool GetKeyPress(Key key, bool ignorePressStamp = false)
        {
            return false;
        }

        public bool GetButtonPress(MouseObject mouseObject, bool ignorePressStamp = false)
        {
            return false;
        }
    }
}