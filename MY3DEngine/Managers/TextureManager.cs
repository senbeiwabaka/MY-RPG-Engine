namespace MY3DEngine.Managers
{
    using System;
    using NLog;
    using SharpDX.Direct3D11;
    using SharpDX.WIC;

    public static class TextureManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static Texture2D CreateTexture2DFromBitmap(Device device, BitmapSource bitmapSource)
        {
            // Allocate DataStream to receive the WIC image pixels
            int stride = bitmapSource.Size.Width * 4;
            using (var buffer = new SharpDX.DataStream(bitmapSource.Size.Height * stride, true, true))
            {
                // Copy the content of the WIC to the buffer
                bitmapSource.CopyPixels(stride, buffer);
                return new Texture2D(device, new Texture2DDescription
                {
                    Width = bitmapSource.Size.Width,
                    Height = bitmapSource.Size.Height,
                    ArraySize = 1,
                    BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                    Usage = ResourceUsage.Default,
                    CpuAccessFlags = CpuAccessFlags.None,
                    Format = SharpDX.DXGI.Format.R8G8B8A8_UNorm,
                    MipLevels = 1,
                    OptionFlags = ResourceOptionFlags.GenerateMipMaps, // ResourceOptionFlags.GenerateMipMap
                    SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                }, new SharpDX.DataRectangle(buffer.DataPointer, stride));
            }
        }

        public static bool Initialize(Device device, string fileName, ref ShaderResourceView textureResource)
        {
            try
            {
                using (var texture = LoadFromFile(device, new ImagingFactory(), fileName))
                {
                    ShaderResourceViewDescription srvDesc = new ShaderResourceViewDescription()
                    {
                        Format = texture.Description.Format,
                        Dimension = SharpDX.Direct3D.ShaderResourceViewDimension.Texture2D,
                    };
                    srvDesc.Texture2D.MostDetailedMip = 0;
                    srvDesc.Texture2D.MipLevels = -1;

                    textureResource = new ShaderResourceView(device, texture, srvDesc);
                    Engine.GameEngine.GraphicsManager.GetDeviceContext.GenerateMips(textureResource);
                }

                return true;
            }
            catch (Exception exception)
            {
                Engine.GameEngine.Exception.AddException(exception);

                Logger.Error(exception, $"{nameof(TextureManager)}.{nameof(Initialize)} method errored.");

                return false;
            }
        }

        public static BitmapSource LoadBitmap(ImagingFactory factory, string filename)
        {
            var bitmapDecoder = new BitmapDecoder(
                factory,
                filename,
                DecodeOptions.CacheOnDemand
                );

            var result = new FormatConverter(factory);

            result.Initialize(
                bitmapDecoder.GetFrame(0),
                PixelFormat.Format32bppPRGBA,
                BitmapDitherType.None,
                null,
                0.0,
                BitmapPaletteType.Custom);

            return result;
        }

        public static Texture2D LoadFromFile(Device device, ImagingFactory factory, string fileName)
        {
            using (var bs = LoadBitmap(factory, fileName))
            {
                return CreateTexture2DFromBitmap(device, bs);
            }
        }
    }
}
