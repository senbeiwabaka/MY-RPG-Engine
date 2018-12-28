// <copyright file="DirectXManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MY3DEngine.Managers
{
    using System;
    using SharpDX;
    using SharpDX.Direct3D;
    using SharpDX.Direct3D11;
    using SharpDX.DXGI;
    using SharpDX.Mathematics.Interop;
    using Device = SharpDX.Direct3D11.Device;

    /// <summary>
    /// The device manager.
    /// </summary>
    public sealed class DirectXManager : IDisposable
    {
        private SwapChain swapChain;
        private RenderTargetView renderTargetView;
        private Texture2D depthStencilBuffer;
        private DepthStencilState depthStencilState;
        private DepthStencilView depthStencilView;
        private RasterizerState rasterizerState;

        ~DirectXManager()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets load content in the background
        /// </summary>
        public Device GetDevice { get; private set; }

        /// <summary>
        /// Gets renders content to the screen while loading can be happening
        /// </summary>
        public DeviceContext GetDeviceContext { get; private set; }

        /// <summary>
        ///
        /// Gets </summary>
        public long VideoCardMemory { get; private set; }

        /// <summary>
        ///
        /// Gets </summary>
        public string VideoCardDescription { get; private set; }

        internal bool VerticalSync { get; set; }

        internal Matrix WorldMatrix { get; set; }

        internal Matrix ProjectionMatrix { get; set; }

        /// <summary>
        ///
        /// </summary>
/// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="windowHandle"></param>
        /// <param name="screenWidth"></param>
        /// <param name="screenHeight"></param>
        /// <param name="vsync"></param>
        /// <param name="fullScreen"></param>
        /// <returns></returns>
        public bool Initialize(IntPtr windowHandle, int screenWidth = 800, int screenHeight = 600, bool vsync = true, bool fullScreen = false)
        {
            int numerator = 0, denominator = 0;

            this.VerticalSync = vsync;

            // create directx graphics interface factory
            using (var factory = new Factory1())
            {
                //factory.MakeWindowAssociation(windowHandle, WindowAssociationFlags.IgnoreAll);

                using (var adapter = factory.GetAdapter1(0))
                {
                    using (var adapterOutput = adapter.GetOutput(0))
                    {
                        // get the adapter description
                        var adapterDescription = adapter.Description;

                        // store the video card memory in megabytes
                        this.VideoCardMemory = (long)adapterDescription.DedicatedVideoMemory >> 10 >> 10;

                        // store the name of the video card array
                        this.VideoCardDescription = adapterDescription.Description.Trim('\0');

                        // get number of modes the fit the dxgi_format_r8gab88a8_uniform display format for the adapter output (monitor)
                        // create a list to hold all of the possible modes for this monitor/video card combination
                        var displayModeList = adapterOutput.GetDisplayModeList(Format.R8G8B8A8_UNorm, DisplayModeEnumerationFlags.Interlaced);

                        // TODO: update to use custom modes if possible
                        foreach (var displayMode in displayModeList)
                        {
                            if (displayMode.Width != screenWidth || displayMode.Height != screenHeight)
                            {
                                continue;
                            }

                            numerator = displayMode.RefreshRate.Numerator;
                            denominator = displayMode.RefreshRate.Denominator;

                            break;
                        }

                        // release memory
                        displayModeList = null;
                    }
                }
            }

            if (numerator == 0 || denominator == 0)
            {
                numerator = 0;
                denominator = 1;
            }

            var rational = this.VerticalSync ? new Rational(numerator, denominator) : new Rational(0, 1);
            var modeDescription = new ModeDescription(screenWidth, screenHeight, rational, Format.R8G8B8A8_UNorm);

            var swapChainDescription = new SwapChainDescription
            {
                // Set to a single back buffer.
                BufferCount = 1,

                // Set the width and height of the back buffer.
                ModeDescription = modeDescription,

                // Set the usage of the back buffer.
                Usage = Usage.RenderTargetOutput,

                // Set the handle for the window to render to.
                OutputHandle = windowHandle,

                // Turn multisampling off.
                SampleDescription = new SampleDescription(1, 0),

                // Set to full screen or windowed mode.
                IsWindowed = fullScreen,

                // Don't set the advanced flags.
                Flags = SwapChainFlags.None,

                // Discard the back buffer content after presenting.
                SwapEffect = SwapEffect.Discard
            };

            // set the feature level to directx 11
            var featureLevel = FeatureLevel.Level_11_0;
            var creationFlags = DeviceCreationFlags.None;

#if DEBUG
            creationFlags = DeviceCreationFlags.Debug;
#endif

            // create swapchain, device, and device context
            Device.CreateWithSwapChain(DriverType.Hardware, creationFlags, new[] { featureLevel }, swapChainDescription, out var d, out var sc);

            if (d == null || sc == null)
            {
                return false;
            }

            this.GetDevice = d;
            this.swapChain = sc;
            this.GetDeviceContext = this.GetDevice.ImmediateContext;

            // initialize back buffer
            var backBuffer = SharpDX.Direct3D11.Resource.FromSwapChain<Texture2D>(this.swapChain, 0);
            if (backBuffer == null)
            {
                return false;
            }

            // create render target view
            this.renderTargetView = new RenderTargetView(this.GetDevice, backBuffer);

            backBuffer.Dispose();
            backBuffer = null;

            if (this.renderTargetView == null)
            {
                return false;
            }

            // setup depth buffer description
            var depthBufferDescription = new Texture2DDescription
            {
                Width = screenWidth,
                Height = screenHeight,
                MipLevels = 1,
                ArraySize = 1,
                Format = Format.D24_UNorm_S8_UInt,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default,
                BindFlags = BindFlags.DepthStencil,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None
            };

            // create the texture for the depth buffer
            this.depthStencilBuffer = new Texture2D(this.GetDevice, depthBufferDescription);

            // Now we need to setup the depth stencil description. This allows us to control what type of depth test Direct3D will do for each pixel.
            // Initialize and set up the description of the stencil state.
            var depthStencilDesc = new DepthStencilStateDescription
            {
                IsDepthEnabled = true,
                DepthWriteMask = DepthWriteMask.All,
                DepthComparison = Comparison.Less,
                IsStencilEnabled = true,
                StencilReadMask = 0xFF,
                StencilWriteMask = 0xFF,

                // Stencil operation if pixel front-facing.
                FrontFace = new DepthStencilOperationDescription()
                {
                    FailOperation = StencilOperation.Keep,
                    DepthFailOperation = StencilOperation.Increment,
                    PassOperation = StencilOperation.Keep,
                    Comparison = Comparison.Always
                },

                // Stencil operation if pixel is back-facing.
                BackFace = new DepthStencilOperationDescription()
                {
                    FailOperation = StencilOperation.Keep,
                    DepthFailOperation = StencilOperation.Decrement,
                    PassOperation = StencilOperation.Keep,
                    Comparison = Comparison.Always
                }
            };

            this.depthStencilState = new DepthStencilState(this.GetDevice, depthStencilDesc);

            if (this.depthStencilState == null)
            {
                return false;
            }

            // set depth stencil state
            this.GetDeviceContext.OutputMerger.SetDepthStencilState(this.depthStencilState, 1);

            // The next thing we need to create is the description of the view of the depth stencil buffer. We do this so that Direct3D knows to use the depth buffer as a depth stencil texture.
            // After filling out the description we then call the function CreateDepthStencilView to create it.
            var depthStencilViewDescription = new DepthStencilViewDescription
            {
                Format = Format.D24_UNorm_S8_UInt,
                Dimension = DepthStencilViewDimension.Texture2D,
                Texture2D = new DepthStencilViewDescription.Texture2DResource
                {
                    MipSlice = 0
                }
            };

            this.depthStencilView = new DepthStencilView(this.GetDevice, this.depthStencilBuffer, depthStencilViewDescription);

            // bind render target view and depth stenchil buffer to the output render pipeline
            this.GetDeviceContext.OutputMerger.SetRenderTargets(this.depthStencilView, this.renderTargetView);

            /*
             * Now that the render targets are setup we can continue on to some extra functions that will give us more control over our scenes for future tutorials.
             * First thing is we'll create is a rasterizer state.
             * This will give us control over how polygons are rendered.
             * We can do things like make our scenes render in wireframe mode or have DirectX draw both the front and back faces of polygons.
             * By default DirectX already has a rasterizer state set up and working the exact same as the one below but you have no control to change it unless you set up one yourself.
            */
            var rasterizerStateDescription = new RasterizerStateDescription
            {
                IsAntialiasedLineEnabled = false,
                CullMode = CullMode.Back,
                DepthBias = 0,
                DepthBiasClamp = .0f,
                IsDepthClipEnabled = true,
                FillMode = FillMode.Solid,
                IsFrontCounterClockwise = false,
                IsMultisampleEnabled = false,
                IsScissorEnabled = false,
                SlopeScaledDepthBias = .0f
            };

            this.rasterizerState = new RasterizerState(this.GetDevice, rasterizerStateDescription);

            if (this.rasterizerState == null)
            {
                return false;
            }

            // set the rasterizer
            this.GetDeviceContext.Rasterizer.State = this.rasterizerState;

            this.InitializeViewport(screenWidth, screenHeight);

            // Setup and create the projection matrix.
            this.ProjectionMatrix = Matrix.PerspectiveFovLH((float)(Math.PI / 4), ((float)screenWidth / (float)screenHeight), 0.1f, 1000.0f);

            // Initialize the world matrix to the identity matrix.
            WorldMatrix = Matrix.Identity;

            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <param name="alpha"></param>
        public void BeginScene(float red, float green, float blue, float alpha)
        {
            // clear the depth buffer
            this.GetDeviceContext.ClearDepthStencilView(this.depthStencilView, DepthStencilClearFlags.Depth, 1.0f, 0);

            // clear the back buffer
            this.GetDeviceContext.ClearRenderTargetView(this.renderTargetView, new RawColor4(red, green, blue, alpha));
        }

        /// <summary>
        ///
        /// </summary>
        public void EndScene()
        {
            if (this.VerticalSync)
            {
                // lock to screen
                this.swapChain.Present(1, PresentFlags.None);
            }
            else
            {
                // present as fast as possible
                this.swapChain.Present(0, PresentFlags.None);
            }
        }

        /// <summary>
        /// Allow the write frame of objects to be displayed
        /// </summary>
        /// <param name="enable"></param>
        public void EnableWireFrameMode(bool enable = false)
        {
            var rasterizerStateDescription = new RasterizerStateDescription
            {
                IsAntialiasedLineEnabled = false,
                CullMode = CullMode.Back,
                DepthBias = 0,
                DepthBiasClamp = 0.0f,
                IsDepthClipEnabled = true,
                FillMode = FillMode.Solid,
                IsFrontCounterClockwise = false,
                IsMultisampleEnabled = false,
                IsScissorEnabled = false,
                SlopeScaledDepthBias = 0.0f
            };

            if (enable)
            {
                rasterizerStateDescription.FillMode = FillMode.Wireframe;
            }

            this.rasterizerState = new RasterizerState(this.GetDevice, rasterizerStateDescription);

            this.GetDeviceContext.Rasterizer.State = this.rasterizerState;
        }

        private void InitializeViewport(int screenWidth, int screenHeight)
        {
            var viewport = new Viewport(0, 0, screenWidth, screenHeight, 0.0f, 1.0f);

            // create viewport
            this.GetDeviceContext.Rasterizer.SetViewport(viewport);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                // before shutting down set to windowed mode or exception will be thrown
                this.swapChain?.SetFullscreenState(false, null);

                this.swapChain?.Dispose();
                this.GetDevice?.Dispose();
                this.GetDeviceContext?.Dispose();
                this.renderTargetView?.Dispose();
                this.depthStencilBuffer?.Dispose();
                this.depthStencilState?.Dispose();
                this.depthStencilView?.Dispose();
                this.rasterizerState?.Dispose();
            }
        }
    }
}
