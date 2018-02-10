using MY3DEngine.BaseObjects;
using MY3DEngine.Common;
using MY3DEngine.GraphicObjects;
using SharpDX;
using SharpDX.Direct3D11;
using System.IO;
using wic = SharpDX.WIC;

namespace MY3DEngine.Primitives
{
    public class TriangleWithTexture : GameObjectWithTexture
    {
        public TriangleWithTexture()
            :base(name: "Triangle with Texture")
        {
        }

        /// <inheritdoc/>
        public override void Draw()
        {
            Engine.GameEngine.GraphicsManager.GetDeviceContext.DrawIndexed(this.IndexCount, 0, 0);
        }

        /// <inheritdoc/>
        public override void LoadContent(bool isNewObject = true)
        {
            base.LoadContent(isNewObject);

            // Instantiate Vertex buffer from vertex data
            //this.VertexBuffer = Buffer.Create(
            //    Engine.GameEngine.GraphicsManager.GetDevice,
            //    BindFlags.VertexBuffer,
            //    new[]
            //    {
            //            new VertexPos(new Vector3(0.5f, 0.5f, 0.5f), new Vector2(1.0f, 1.0f)),
            //            new VertexPos(new Vector3(0.5f, -0.5f, 0.5f), new Vector2(1.0f, 0.0f)),
            //            new VertexPos(new Vector3(-0.5f, -0.5f, 0.5f), new Vector2(0.0f, 0.0f)),

            //            new VertexPos(new Vector3(-0.5f, -0.5f, 0.5f), new Vector2(0.0f, 0.0f)),
            //            new VertexPos(new Vector3(-0.5f, 0.5f, 0.5f), new Vector2(0.0f, 1.0f)),
            //            new VertexPos(new Vector3(0.5f, 0.5f, 0.5f), new Vector2(1.0f, 1.0f))
            //    });

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
        }

        /// <inheritdoc/>
        public override void Render()
        {
            // Set the vertex buffer to active in the input assembler so it can be rendered.
            Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(this.VertexBuffer, TextureVertex.Size, 0));

            // Set the index buffer to active in the input assembler so it can be rendered.
            Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.SetIndexBuffer(this.IndexBuffer, SharpDX.DXGI.Format.R32_UInt, 0);

            // Set the type of the primitive that should be rendered from this vertex buffer, in this case triangles.
            Engine.GameEngine.GraphicsManager.GetDeviceContext.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
        }
    }
}