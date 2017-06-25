using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;

using SharpDX;
using SharpDX.Mathematics.Interop;

using Device = SharpDX.Direct3D11.Device;

namespace MY3DEngine
{
    /// <summary>
    /// The device manager.
    /// </summary>
    public sealed class DirectXManager : IDisposable
    {
        private bool vsyncEnabled;
        private int videoCardMemory = 0;
        private string videoCardDescription = null;
        private SwapChain swapChain;
        private RenderTargetView renderTargetView;
        private Texture2D depthStencilBuffer;
        private DepthStencilState depthStencilState;
        private DepthStencilView depthStencilView;
        private RasterizerState rasterizerState;
        private BlendState blendStateAlphaEnabled;
        private BlendState blendStateAlphaDisabled;
        private DepthStencilState depthDisabledStencilState;

        /// <summary>
        /// Load content in the background
        /// </summary>
        public Device Device { get; private set; }

        /// <summary>
        /// Renders content to the screen while loading can be happening
        /// </summary>
        public DeviceContext DeviceContext { get; private set; }
        
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
            this.DeviceContext.ClearRenderTargetView(this.renderTargetView, new RawColor4(red, green, blue, alpha));

            // clear the depth buffer
            this.DeviceContext.ClearDepthStencilView(this.depthStencilView, DepthStencilClearFlags.Depth, 1.0f, 0);
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

            this.DeviceContext.OutputMerger.SetBlendState(
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
            this.DeviceContext.OutputMerger.SetDepthStencilState(
                enable ? this.depthStencilState : this.depthDisabledStencilState,
                1);
        }

        public void Dispose()
        {
            this.swapChain?.SetFullscreenState(false, null);

            this.swapChain?.Dispose();
            this.Device?.Dispose();
            this.DeviceContext?.Dispose();
            this.renderTargetView?.Dispose();
            this.depthStencilBuffer?.Dispose();
            this.depthStencilState?.Dispose();
            this.depthStencilView?.Dispose();
            this.rasterizerState?.Dispose();
            this.blendStateAlphaEnabled?.Dispose();
            this.blendStateAlphaDisabled?.Dispose();
            this.depthDisabledStencilState?.Dispose();
        }

        public bool Initialize(IntPtr windowHandle, int screenWidth, int screenHeight, bool vsync = true, bool fullScreen = false)
        {
            int numerator = 0, denominator = 0;

            this.vsyncEnabled = vsync;

            // create directx graphics interface
            var factory = new Factory(windowHandle);

            // user factory above to create an adapter for the primary graphics interface
            var adapter = factory.GetAdapter(0);

            // enumerate the primary adapter output
            var adapterOutput = adapter.GetOutput(0);

            // get number of modes the fit the dxgi_format_r8gab88a8_uniform display format for the adapter output (monitor)
            // create a list to hold al lof the possible modes for this monitor/video card combination
            var displayModeList = adapterOutput.GetDisplayModeList(Format.R8G8B8A8_UNorm, DisplayModeEnumerationFlags.Interlaced);

            foreach (var displayMode in displayModeList)
            {
                if (displayMode.Width == screenWidth && displayMode.Height == screenHeight)
                {
                    numerator = displayMode.RefreshRate.Numerator;
                    denominator = displayMode.RefreshRate.Denominator;
                }
            }

            if (numerator == 0 || denominator == 0)
            {
                return false;
            }

            // get the adapter description
            var adapterDescription = adapter.Description;

            // store the video card memory in megabytes
            this.videoCardMemory = (int)adapterDescription.DedicatedVideoMemory / 1024 / 1024;

            // store the name of the video card array
            this.videoCardDescription = adapterDescription.Description;

            // release memory
            displayModeList = null;

            adapterOutput.Dispose();
            adapterOutput = null;

            adapter.Dispose();
            adapter = null;

            factory.Dispose();
            factory = null;

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
            this.renderTargetView = new RenderTargetView(this.Device, backBuffer);

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

            if (!this.InitializeStencilView())
            {
                return false;
            }

            // bind render target view and depth stenchil buffer to the output render pipeline
            this.DeviceContext.OutputMerger.SetRenderTargets(this.depthStencilView, this.renderTargetView);

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

            return true;
        }

        private bool InitializeSwapChain(
            IntPtr windowHandle,
            bool fullScreen,
            int screenWidth,
            int screenHieght,
            int numerator,
            int denominator)
        {
            ModeDescription modeDescription;

            if (this.vsyncEnabled)
            {
                modeDescription = new ModeDescription(
                    screenWidth,
                    screenHieght,
                    new Rational(numerator, denominator), // refresh rate
                    Format.B8G8R8A8_UNorm);
            }
            else
            {
                modeDescription = new ModeDescription(
                    screenWidth,
                    screenHieght,
                    new Rational(0, 1), // refresh rate
                    Format.B8G8R8A8_UNorm);
            }

            // set the scan line ordering and scaling to unspecified
            modeDescription.ScanlineOrdering = DisplayModeScanlineOrder.Unspecified;
            modeDescription.Scaling = DisplayModeScaling.Unspecified;

            var swapChainDescription = new SwapChainDescription()
                                           {
                                               BufferCount = 1,
                                               SampleDescription = new SampleDescription(1, 0), // multi-sampling
                                               IsWindowed = fullScreen,
                                               OutputHandle = windowHandle,
                                               ModeDescription = modeDescription,
                                               Usage = Usage.RenderTargetOutput,
                                               SwapEffect = SwapEffect.Discard,
                                               Flags = SwapChainFlags.None
                                           };

            // set the feature level to directx 11
            var featureLevel = FeatureLevel.Level_11_0;

            // create swapchain, device, and device context
            Device d;
            SwapChain sc;
            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.Debug, new[] { featureLevel }, swapChainDescription, out d, out sc);

            if(d == null || sc == null)
            {
                return false;
            }

            this.Device = d;
            this.swapChain = sc;
            this.DeviceContext = this.Device.ImmediateContext;

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
            this.depthStencilBuffer = new Texture2D(this.Device, depthBufferDescription);

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

            this.depthStencilState = new DepthStencilState(this.Device, depthStencilStateDescription);

            if (this.depthStencilState == null)
            {
                return false;
            }

            // set depth stencil state
            this.DeviceContext.OutputMerger.SetDepthStencilState(this.depthDisabledStencilState, 1);

            return true;
        }

        private bool InitializeStencilView()
        {
            var depthStencilViewDescription =
                new DepthStencilViewDescription
                    {
                        Format = Format.D24_UNorm_S8_UInt,
                        Dimension = DepthStencilViewDimension.Texture2D,
                        Texture2D = { MipSlice = 0 }
                    };


            this.depthStencilView = new DepthStencilView(this.Device, this.depthStencilBuffer, depthStencilViewDescription);

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


            this.rasterizerState = new RasterizerState(this.Device, rasterizerStateDescription);

            if (this.rasterizerState == null)
            {
                return false;
            }

            // set the rasterizer
            this.DeviceContext.Rasterizer.State = this.rasterizerState;

            return true;
        }

        private void InitializeViewport(int screenWidth, int screenHeight)
        {
            var viewport = new Viewport(0,0,screenWidth, screenHeight,0.0f, 1.0f);

            // create viewport
            this.DeviceContext.Rasterizer.SetViewport(viewport);
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

            this.blendStateAlphaEnabled = new BlendState(this.Device, blendStateDescription);

            if (this.blendStateAlphaEnabled == null)
            {
                return false;
            }

            // modify to create the disabled alpha blend state
            blendStateDescription.RenderTarget[0].IsBlendEnabled = false;

            this.blendStateAlphaDisabled = new BlendState(this.Device, blendStateDescription);

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

            this.depthDisabledStencilState = new DepthStencilState(this.Device, depthStencilStateDescription);

            return this.depthDisabledStencilState != null;
        }
    }
}