// <copyright file="Input.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MY3DEngine
{
    using System;
    using System.Drawing;
    using MY3DEngine.Models;
    using SharpDX.Multimedia;
    using SharpDX.RawInput;

    internal class Input : IDisposable
    {
        public Input()
        {
            try
            {
                Engine.GameEngine.Exception.Exceptions.Add(new ErrorModel("Beginning setting up input", "Input", string.Empty));

                Device.RegisterDevice(UsagePage.Generic, UsageId.GenericMouse, DeviceFlags.None);
                Device.MouseInput += Device_MouseInput;

                Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard, DeviceFlags.None);
                Device.KeyboardInput += Device_KeyboardInput;

                Engine.GameEngine.Exception.Exceptions.Add(new ErrorModel("Ending setting up input", "Input", string.Empty));
            }
            catch (Exception e)
            {
                Engine.GameEngine.Exception.Exceptions.Add(new ErrorModel(e.Message, e.Source, e.StackTrace));
            }
            finally
            {
                this.Dispose();
            }
        }

        private void Device_KeyboardInput(object sender, KeyboardInputEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Device_MouseInput(object sender, MouseInputEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
        }
    }
}