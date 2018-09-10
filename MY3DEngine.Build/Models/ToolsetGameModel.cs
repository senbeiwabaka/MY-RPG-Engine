namespace MY3DEngine.BuildTools.Models
{
    public sealed class ToolsetGameModel
    {
        public ToolsetGameModel(bool successful)
        {
            Successful = successful;
        }

        /// <summary>
        /// The game name
        /// </summary>
        public string GameName { get; set; }

        /// <summary>
        /// The game entry point file
        /// </summary>
        public string MainFileName { get; set; }

        /// <summary>
        /// The folder path of <see cref="MainFileName"/>
        /// </summary>
        public string MainFileFolderLocation { get; set; }

        /// <summary>
        /// The root of the project
        /// </summary>
        public string FolderLocation { get; set; }

        /// <summary>
        /// Holds the string contents of the settings file
        /// </summary>
        public string Settings { get; set; }

        /// <summary>
        /// True if it was able to fine the folder directory, false otherwise
        /// </summary>
        public bool Successful { get; }
    }
}
