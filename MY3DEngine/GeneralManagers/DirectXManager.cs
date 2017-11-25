using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using System;
using System.Linq;
using Device = SharpDX.Direct3D11.Device;

namespace MY3DEngine.GeneralManagers
{
    /// <summary>
    /// The device manager.
    /// </summary>
    public sealed class DirectXManager : IDisposable
    {
        private bool vsyncEnabled;
        private long videoCardMemory = default(long);
        private string videoCardDescription = default(string);
        private SwapChain swapChain;
        private RenderTargetView renderTargetView;
        private Texture2D depthStencilBuffer;
        private DepthStencilState depthStencilState;
        private DepthStencilView depthStencilView;
        private RasterizerState rasterizerState;
        private BlendState blendStateAlphaEnabled;
        private BlendState blendStateAlphaDisabled;
        private DepthStencilState depthDisabledStencilState;
        private Matrix m_projectionMatrix;
        private Matrix m_worldMatrix;

        /// <summary>
        /// Load content in the background
        /// </summary>
        public Device GetDevice { get; private set; }

        /// <summary>
        /// Renders content to the screen while loading can be happening
        /// </summary>
        public DeviceContext GetDeviceContext { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public long VideoCardMemory => this.videoCardMemory;

        /// <summary>
        ///
        /// </summary>
        public string VideoCardDescription => this.videoCardDescription;

        public Matrix GetProjectMatrix => this.m_projectionMatrix;

        public Matrix GetWorldMatrix => this.m_worldMatrix;

        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(true);
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
        public bool Initialize(IntPtr windowHandle, int screenWidth = 720, int screenHeight = 480, bool vsync = true, bool fullScreen = false)
        {
            int numerator = 0, denominator = 0;

            this.vsyncEnabled = vsync;

            // create directx graphics interface factory
            using (var factory = new Factory4())
            {
                factory.MakeWindowAssociation(windowHandle, WindowAssociationFlags.IgnoreAll);

                foreach (var adapter in factory.Adapters.Where(x => x.GetOutputCount() > 0))
                {
                    // enumerate the primary adapter output
                    using (var adapterOutput = adapter.GetOutput(0))
                    {
                        // get the adapter description
                        var adapterDescription = adapter.Description;

                        // store the video card memory in megabytes
                        this.videoCardMemory = (long)adapterDescription.DedicatedVideoMemory / 1024 / 1024;

                        // store the name of the video card array
                        this.videoCardDescription = adapterDescription.Description;

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

                        if (numerator == 0 || denominator == 0)
                        {
                            continue;
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

            if (!this.InitializeSwapChain(windowHandle, fullScreen, screenWidth, screenWidth, numerator, denominator))
            {
                return false;
            }

            // initialize back buffer
            var backBuffer = this.swapChain.GetBackBuffer<Texture2D>(0);
            if (backBuffer == null)
            {
                return false;
            }

            // create render target view
            this.renderTargetView = new RenderTargetView(this.GetDevice, backBuffer);

            if (this.renderTargetView == null)
            {
                return false;
            }

            backBuffer.Dispose();
            backBuffer = null;

            if (!this.InitializeDepthBuffer(screenWidth, screenHeight))
            {
                return false;
            }

            if (!this.IntializeDepthStencilBuffer())
            {
                return false;
            }

            //if (!this.InitializeStencilView())
            //{
            //    return false;
            //}

            // bind render target view and depth stenchil buffer to the output render pipeline
            this.GetDeviceContext.OutputMerger.SetRenderTargets(this.depthStencilView, this.renderTargetView);

            if (!this.InitializeRasterizerState())
            {
                return false;
            }

            this.InitializeViewport(screenWidth, screenHeight);

            if (!this.InitializeAlphaBlending())
            {
                return false;
            }

            if (!this.InitializeZBuffer())
            {
                return false;
            }

            // Setup the projection matrix.
            var fieldOfView = 3.141592654f / 4.0f;
            var screenAspect = (float)screenWidth / (float)screenHeight;

            // Create the projection matrix for 3D rendering.
            m_projectionMatrix = Matrix.PerspectiveFovLH(fieldOfView, screenAspect, 0.0f, 1.0f);

            // Initialize the world matrix to the identity matrix.
            m_worldMatrix = Matrix.Identity;

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
            // clear the back buffer
            this.GetDeviceContext.ClearRenderTargetView(this.renderTargetView, new RawColor4(red, green, blue, alpha));

            // clear the depth buffer
            //this.GetDeviceContext.ClearDepthStencilView(this.depthStencilView, DepthStencilClearFlags.Depth, 1.0f, 0);
        }

        /// <summary>
        ///
        /// </summary>
        public void EndScene()
        {
            if (this.vsyncEnabled)
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
        ///
        /// </summary>
        /// <param name="enable"></param>
        public void EnableAlphaBlending(bool enable)
        {
            var blendFactor = new RawColor4(0f, 0f, 0f, 0f);

            this.GetDeviceContext.OutputMerger.SetBlendState(
                enable ? this.blendStateAlphaEnabled : this.blendStateAlphaDisabled,
                blendFactor,
                0xffffffff);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="enable"></param>
        public void EnableZBuffer(bool enable)
        {
            this.GetDeviceContext.OutputMerger.SetDepthStencilState(
                enable ? this.depthStencilState : this.depthDisabledStencilState,
                1);
        }

        #region Helper Methods

        // TODO: update this to figure out what device to use instead of hardcoding it
        private bool InitializeSwapChain(
            IntPtr windowHandle,
            bool fullScreen,
            int screenWidth,
            int screenHieght,
            int numerator,
            int denominator)
        {
            var rational = this.vsyncEnabled ? new Rational(numerator, denominator) : new Rational(0, 1);
            var modeDescription = new ModeDescription(
                                                  screenWidth,
                                                  screenHieght,
                                                  rational, // refresh rate
                                                  Format.B8G8R8A8_UNorm)
            {
                // set the scan line ordering and scaling to unspecified
                ScanlineOrdering = DisplayModeScanlineOrder.Unspecified,
                Scaling = DisplayModeScaling.Unspecified
            };

            var swapChainDescription = new SwapChainDescription()
            {
                BufferCount = 1,
                SampleDescription = new SampleDescription(1, 0), // multi-sampling
                IsWindowed = !fullScreen,
                OutputHandle = windowHandle,
                ModeDescription = modeDescription,
                Usage = Usage.RenderTargetOutput,
                SwapEffect = SwapEffect.Discard,
                Flags = SwapChainFlags.None
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

            return true;
        }

        private bool InitializeDepthBuffer(int screenWidth, int screenHeight)
        {
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

            return this.depthStencilBuffer != null;
        }

        private bool IntializeDepthStencilBuffer()
        {
            var depthStencilStateDescription =
                new DepthStencilStateDescription
                {
                    IsDepthEnabled = true,
                    DepthWriteMask = DepthWriteMask.All,
                    DepthComparison = Comparison.Less,
                    IsStencilEnabled = true,
                    StencilReadMask = 0xFF,
                    StencilWriteMask = 0xFF,
                    FrontFace = // stencil operations if pixel is front-facing
                            new DepthStencilOperationDescription
                            {
                                FailOperation = StencilOperation.Keep,
                                DepthFailOperation = StencilOperation.Increment,
                                PassOperation = StencilOperation.Keep,
                                Comparison = Comparison.Always
                            },
                    BackFace = // stencil operations if pixel is back-facing
                            new DepthStencilOperationDescription
                            {
                                FailOperation = StencilOperation.Keep,
                                DepthFailOperation = StencilOperation.Decrement,
                                PassOperation = StencilOperation.Keep,
                                Comparison = Comparison.Always
                            }
                };

            this.depthStencilState = new DepthStencilState(this.GetDevice, depthStencilStateDescription);

            if (this.depthStencilState == null)
            {
                return false;
            }

            // set depth stencil state
            this.GetDeviceContext.OutputMerger.SetDepthStencilState(this.depthDisabledStencilState, 1);

            return true;
        }

        private bool InitializeStencilView()
        {
            var depthStencilViewDescription = new DepthStencilViewDescription
            {
                Format = Format.D24_UNorm_S8_UInt,
                Dimension = DepthStencilViewDimension.Texture2D,
                Texture2D = { MipSlice = 0 }
            };

            this.depthStencilView = new DepthStencilView(this.GetDevice, this.depthStencilBuffer, depthStencilViewDescription);

            return this.depthStencilView != null;
        }

        private bool InitializeRasterizerState()
        {
            var rasterizerStateDescription =
                new RasterizerStateDescription
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

            this.rasterizerState = new RasterizerState(this.GetDevice, rasterizerStateDescription);

            if (this.rasterizerState == null)
            {
                return false;
            }

            // set the rasterizer
            this.GetDeviceContext.Rasterizer.State = this.rasterizerState;

            return true;
        }

        private bool InitializeAlphaBlending()
        {
            var blendStateDescription = new BlendStateDescription();

            blendStateDescription.RenderTarget[0].IsBlendEnabled = true;
            blendStateDescription.RenderTarget[0].SourceBlend = BlendOption.InverseSourceAlpha;
            blendStateDescription.RenderTarget[0].DestinationBlend = BlendOption.InverseSourceAlpha;
            blendStateDescription.RenderTarget[0].BlendOperation = BlendOperation.Add;
            blendStateDescription.RenderTarget[0].SourceAlphaBlend = BlendOption.One;
            blendStateDescription.RenderTarget[0].DestinationAlphaBlend = BlendOption.Zero;
            blendStateDescription.RenderTarget[0].AlphaBlendOperation = BlendOperation.Add;
            blendStateDescription.RenderTarget[0].RenderTargetWriteMask = ColorWriteMaskFlags.All;

            this.blendStateAlphaEnabled = new BlendState(this.GetDevice, blendStateDescription);

            if (this.blendStateAlphaEnabled == null)
            {
                return false;
            }

            // modify to create the disabled alpha blend state
            blendStateDescription.RenderTarget[0].IsBlendEnabled = false;

            this.blendStateAlphaDisabled = new BlendState(this.GetDevice, blendStateDescription);

            if (this.blendStateAlphaDisabled == null)
            {
                return false;
            }

            return true;
        }

        private bool InitializeZBuffer()
        {
            var depthStencilStateDescription =
                new DepthStencilStateDescription
                {
                    IsDepthEnabled = false,
                    DepthWriteMask = DepthWriteMask.All,
                    DepthComparison = Comparison.Less,
                    IsStencilEnabled = true,
                    StencilReadMask = 0xFF,
                    StencilWriteMask = 0xFF,
                    FrontFace = // stencil operations if pixel is front-facing
                            new DepthStencilOperationDescription
                            {
                                FailOperation = StencilOperation.Keep,
                                DepthFailOperation = StencilOperation.Increment,
                                PassOperation = StencilOperation.Keep,
                                Comparison = Comparison.Always
                            },
                    BackFace = // stencil operations if pixel is back-facing
                            new DepthStencilOperationDescription
                            {
                                FailOperation = StencilOperation.Keep,
                                DepthFailOperation = StencilOperation.Decrement,
                                PassOperation = StencilOperation.Keep,
                                Comparison = Comparison.Always
                            }
                };

            this.depthDisabledStencilState = new DepthStencilState(this.GetDevice, depthStencilStateDescription);

            return this.depthDisabledStencilState != null;
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
                this.blendStateAlphaEnabled?.Dispose();
                this.blendStateAlphaDisabled?.Dispose();
                this.depthDisabledStencilState?.Dispose();
            }
        }

        #endregion Helper Methods
    }
}