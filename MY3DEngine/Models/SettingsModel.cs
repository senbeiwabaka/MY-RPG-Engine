namespace MY3DEngine.Models
{
    /// <summary>
    /// Settings for the running game
    /// </summary>
    public struct SettingsModel
    {
        /// <summary>
        /// Gets or sets the location of the assets folder
        /// </summary>
        public string AssetsPath { get; set; }

        /// <summary>
        /// Gets or sets the name of the game
        /// </summary>
        public string GameName { get; set; }

        /// <summary>
        /// Gets or sets screen height
        /// </summary>
        public int Height { get; set; }

        public string LevelsPath { get; set; }

        /// <summary>
        /// Gets or sets location of the root of the project/game
        /// </summary>
        public string MainFolderLocation { get; set; }

        public string PixelShaderName { get; set; }

        public string SettingsFileName { get; set; }

        /// <summary>
        /// Gets or sets the location of the shader folder
        /// </summary>
        public string ShaderPath { get; set; }

        public string VertexShaderName { get; set; }

        /// <summary>
        /// Gets or sets screen width
        /// </summary>
        public int Width { get; set; }

        /// <inheritdoc/>
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
