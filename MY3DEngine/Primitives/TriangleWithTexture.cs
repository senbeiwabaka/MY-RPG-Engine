﻿using MY3DEngine.BaseObjects;
using MY3DEngine.Common;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System.IO;
using wic = SharpDX.WIC;

namespace MY3DEngine.Primitives
{
    internal struct MatrixBufferType
    {
        Matrix world;
        Matrix view;
        Matrix projection;
    };

    internal struct VertexPos
    {
        public Vector3 pos;
        public Vector2 tex0;

        public VertexPos(Vector3 pos, Vector2 tex0)
        {
            this.pos = pos;
            this.tex0 = tex0;
        }
    };
    
    public class TriangleWithTexture : GameObjectWithTexture
    {
        public TriangleWithTexture()
        {
            this.Name = "Triangle with Texture";
        }

        /// <inheritdoc/>
        public override void LoadContent(bool isNewObject = true)
        {
            base.LoadContent(isNewObject);

            // Instantiate Vertex buffer from vertex data
            this.Buffer = Buffer.Create(
                Engine.GameEngine.GraphicsManager.GetDevice,
                BindFlags.VertexBuffer,
                new[]
                {
                        new VertexPos(new Vector3(0.5f, 0.5f, 0.5f), new Vector2(1.0f, 1.0f)),
                        new VertexPos(new Vector3(0.5f, -0.5f, 0.5f), new Vector2(1.0f, 0.0f)),
                        new VertexPos(new Vector3(-0.5f, -0.5f, 0.5f), new Vector2(0.0f, 0.0f)),

                        new VertexPos(new Vector3(-0.5f, -0.5f, 0.5f), new Vector2(0.0f, 0.0f)),
                        new VertexPos(new Vector3(-0.5f, 0.5f, 0.5f), new Vector2(0.0f, 1.0f)),
                        new VertexPos(new Vector3(0.5f, 0.5f, 0.5f), new Vector2(1.0f, 1.0f))
                });

            var path = Path.GetFullPath("Assets");
            path = string.Format("{0}\\decal.png", path);

            // initialize the WIC factory
            using (var imagingFactory = new wic.ImagingFactory2())
            {
                //using (var decoder = new SharpDX.WIC.PngBitmapDecoder(imagingFactory))
                //{
                //    decoder.Initialize(new wic.WICStream(imagingFactory, path, NativeFileAccess.All), wic.DecodeOptions.CacheOnDemand);

                //    using (var formatConverter = new SharpDX.WIC.FormatConverter(imagingFactory))
                //    {
                //        formatConverter.Initialize(decoder.GetFrame(0), SharpDX.WIC.PixelFormat.Format32bppPRGBA, SharpDX.WIC.BitmapDitherType.None, null, 0.0, SharpDX.WIC.BitmapPaletteType.Custom);

                //        SharpDX.DataStream dataStream = new SharpDX.DataStream(formatConverter.Size.Height * formatConverter.Size.Width * 4, true, true);
                //        formatConverter.CopyPixels(formatConverter.Size.Width * 4, dataStream);

                //        //_icons.Add(iconInfo.Name, new SharpDX.Direct2D1.Bitmap(renderTarget, new SharpDX.Size2(formatConverter.Size.Width, formatConverter.Size.Height), dataStream,
                //        //    formatConverter.Size.Width * 4, bitmapProperties));
                //    }
                //}


                //var inputStream = new wic.WICStream(imagingFactory, path, NativeFileAccess.All); // open the image file for reading
                // var decoder = new wic.BitmapDecoder(imagingFactory, inputStream, wic.DecodeOptions.CacheOnDemand);

                //var t =  decoder.QueryInterface<Texture2D>();


                using (var texture = TextureLoader.CreateTexture2DFromBitmap(Engine.GameEngine.GraphicsManager.GetDevice, TextureLoader.LoadBitmap(new wic.ImagingFactory2(), path)))
                {
                    this.ColorMap = new ShaderResourceView(Engine.GameEngine.GraphicsManager.GetDevice, texture);
                }
            }

            var samplerStateDescription = new SamplerStateDescription()
            {
                AddressU = TextureAddressMode.Wrap,
                AddressV = TextureAddressMode.Wrap,
                AddressW = TextureAddressMode.Wrap,
                ComparisonFunction = Comparison.Never,
                Filter = Filter.MinMagMipLinear,
                MaximumLod = float.MaxValue
            };

            this.ColorMapSampler = new SamplerState(Engine.GameEngine.GraphicsManager.GetDevice, samplerStateDescription);
        }

        /// <inheritdoc/>
        public override void Render()
        {
            Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.InputLayout = this.InputLayout;
            Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
            Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(this.Buffer, 32, 0));

            Engine.GameEngine.GraphicsManager.GetDeviceContext.VertexShader.Set(this.VertextShader);

            Engine.GameEngine.GraphicsManager.GetDeviceContext.PixelShader.Set(this.PixelShader);
            Engine.GameEngine.GraphicsManager.GetDeviceContext.PixelShader.SetShaderResource(0, this.ColorMap);
            Engine.GameEngine.GraphicsManager.GetDeviceContext.PixelShader.SetSampler(0, this.ColorMapSampler);

            Engine.GameEngine.GraphicsManager.GetDeviceContext.Draw(6, 0);
        }
    }
}