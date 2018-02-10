namespace MY3DEngine.Models
{
    /// <summary>
    /// Settings for the running game
    /// </summary>
    public struct Settings
    {
        public string AssetsPath { get; set; }

        /// <summary>
        /// Screen height
        /// </summary>
        public int Height { get; set; }

        public string PixelShaderName { get; set; }

        public string ShaderPath { get; set; }

        public string VertexShaderName { get; set; }

        /// <summary>
        /// Screen width
        /// </summary>
        public int Width { get; set; }
    }
}