using MY3DEngine.Logging;
using MY3DEngine.Models;
using MY3DEngine.Utilities;

namespace MY3DEngine.Managers
{
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
        public Settings Settings => settings;

        /// <summary>
        /// Initialize the settings for the game engine
        /// </summary>
        /// <returns></returns>
        public bool Initialize()
        {
            StaticLogger.Info($"Starting {nameof(SettingsManager)}.{nameof(Initialize)}");

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

            settings = Deserialize.DeserializeFileAsT<Settings>(fullPath);

            if (FileIO.DirectoryExists($""))
            {
            }

            if (string.IsNullOrWhiteSpace(settings.ShaderPath))
            {
                settings.ShaderPath = $"{CurrentDirectory}{DefaultShaderPath}";
            }

            if (string.IsNullOrWhiteSpace(settings.AssetsPath))
            {
                settings.AssetsPath = $"{CurrentDirectory}{DefaultAssetsPath}";
            }

            Engine.GameEngine.GameName = Settings.GameName;

            StaticLogger.Debug($"Settings: {settings}");

            StaticLogger.Info($"Finished {nameof(SettingsManager)}.{nameof(Initialize)}");

            return isLoaded = true;
        }
    }
}
