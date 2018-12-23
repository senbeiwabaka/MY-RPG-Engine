// <copyright file="ToolsetGameModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MY3DEngine.BuildTools.Models
{
    public sealed class ToolsetGameModel
    {
        public ToolsetGameModel(bool successful)
        {
            Successful = successful;
        }

        /// <summary>
        /// Gets or sets the game name
        /// </summary>
        public string GameName { get; set; }

        /// <summary>
        /// Gets or sets the game entry point file
        /// </summary>
        public string MainFileName { get; set; }

        /// <summary>
        /// Gets or sets the folder path of <see cref="MainFileName"/>
        /// </summary>
        public string MainFileFolderLocation { get; set; }

        /// <summary>
        /// Gets or sets the root of the project
        /// </summary>
        public string FolderLocation { get; set; }

        /// <summary>
        /// Gets or sets holds the string contents of the settings file
        /// </summary>
        public string Settings { get; set; }

        /// <summary>
        /// Gets a value indicating whether true if it was able to fine the folder directory, false otherwise
        /// </summary>
        public bool Successful { get; }
    }
}
