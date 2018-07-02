namespace MY3DEngine.Models
{
    /// <summary>
    /// Settings for the running game
    /// </summary>
    public struct Settings
    {
        /// <summary>
        /// The location of the assets folder
        /// </summary>
        public string AssetsPath { get; set; }

        /// <summary>
        /// The name of the game
        /// </summary>
        public string GameName { get; set; }

        /// <summary>
        /// Screen height
        /// </summary>
        public int Height { get; set; }

        public string PixelShaderName { get; set; }

        /// <summary>
        /// The location of the shader folder
        /// </summary>
        public string ShaderPath { get; set; }

        public string VertexShaderName { get; set; }

        /// <summary>
        /// Screen width
        /// </summary>
        public int Width { get; set; }

        public override string ToString()
        {
            return $"AssetsPath: {AssetsPath} ;; " +
                $"GameName: {GameName} ;; " +
                $"Height: {Height} ;; " +
                $"PixelShaderName: {PixelShaderName} ;; " +
                $"ShaderPath: {ShaderPath} ;; " +
                $"VertexShaderName: {VertexShaderName} ;; " +
                $"Width: {Width}";
        }
    }
}