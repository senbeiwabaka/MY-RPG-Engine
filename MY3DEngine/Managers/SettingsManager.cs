using MY3DEngine.Models;
using MY3DEngine.Utilities;
using Newtonsoft.Json;
using System;

namespace MY3DEngine.Managers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SettingsManager
    {
        private const string OverrideFolderName = "\\Override";

        private const string DefaultIniFileName = "\\DefaultSettings.ini";
        private const string DefaultAssetsPath = "\\Assets";
        private const string DefaultShaderPath = "\\Assets\\Shaders";

        private readonly string CurrentDirectory = FileIO.GetCurrentDirectory;

        private Settings settings;
        private bool isLoaded;

        /// <inherietdoc/>
        public Settings Settings => this.settings;

        /// <summary>
        /// Initialize the settings for the game engine
        /// </summary>
        /// <returns></returns>
        public bool Initialize()
        {
            if (isLoaded)
            {
                return isLoaded;
            }

            var path = Engine.GameEngine.FolderLocation ?? CurrentDirectory;
            var fullPath = $"{path}{DefaultIniFileName}";

            if (!FileIO.FileExists(fullPath))
            {
                return (isLoaded = false);
            }

            this.settings = Deserialize.DeserializeFileAsT<Settings>(fullPath);

            if (FileIO.DirectoryExists($""))
            {

            }

            if (string.IsNullOrWhiteSpace(this.settings.ShaderPath))
            {
                this.settings.ShaderPath = $"{CurrentDirectory}{DefaultShaderPath}";
            }

            if (string.IsNullOrWhiteSpace(this.settings.AssetsPath))
            {
                this.settings.AssetsPath = $"{CurrentDirectory}{DefaultAssetsPath}";
            }

            Engine.GameEngine.GameName = Settings.GameName;

            return (isLoaded = true);
        }
    }
}