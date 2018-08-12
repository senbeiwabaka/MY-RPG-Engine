using MY3DEngine.Logging;
using MY3DEngine.Models;
using MY3DEngine.Utilities;
using System;

namespace MY3DEngine.Managers
{
    public sealed class SettingsManager
    {
        private const string OverrideFolderPath = "\\Override";
        private const string DefaultLevelsPath = "\\Levels";
        private const string DefaultIniFileName = "\\DefaultSettings.ini";
        private const string DefaultAssetsPath = "\\Assets";
        private const string DefaultShaderPath = "\\Assets\\Shaders";

        private readonly string CurrentDirectory = FileIO.GetCurrentDirectory;

        private bool isLoaded;

        public SettingsModel Settings { get; set; }

        /// <summary>
        /// Initialize the settings for the game engine
        /// </summary>
        /// <returns></returns>
        public bool Initialize(string folderLocation, string gameName, string settings)
        {
            StaticLogger.Info($"Starting {nameof(SettingsManager)}.{nameof(Initialize)}");

            if (isLoaded)
            {
                return isLoaded;
            }

            if (string.IsNullOrWhiteSpace(folderLocation))
            {
                StaticLogger.Exception($"Starting {nameof(SettingsManager)}.{nameof(Initialize)}", new ArgumentNullException(nameof(folderLocation)));

                throw new ArgumentNullException(nameof(folderLocation));
            }

            SettingsModel settingsModel;
            string fullPath = $"{folderLocation}{DefaultIniFileName}";

            // The settings parameter has data so just parse it
            if (!string.IsNullOrWhiteSpace(settings))
            {
                settingsModel = Deserialize.DeserializeStringAsT<SettingsModel>(settings);
            }
            // The settings parameter doesn't have data so we need to build the location then parse the data
            else
            {
                if (!FileIO.FileExists(fullPath))
                {
                    return (isLoaded = false);
                }

                settingsModel = Deserialize.DeserializeFileAsT<SettingsModel>(fullPath);
            }

            if (string.IsNullOrWhiteSpace(settingsModel.MainFolderLocation))
            {
                settingsModel.MainFolderLocation = folderLocation;
            }

            if (string.IsNullOrWhiteSpace(settingsModel.ShaderPath))
            {
                settingsModel.ShaderPath = $"{settingsModel.MainFolderLocation}{DefaultShaderPath}";
            }

            if (string.IsNullOrWhiteSpace(settingsModel.AssetsPath))
            {
                settingsModel.AssetsPath = $"{settingsModel.MainFolderLocation}{DefaultAssetsPath}";
            }

            if (string.IsNullOrWhiteSpace(settingsModel.LevelsPath))
            {
                settingsModel.LevelsPath = $"{settingsModel.MainFolderLocation}{DefaultLevelsPath}";
            }

            if (string.IsNullOrWhiteSpace(settingsModel.SettingsFileName))
            {
                settingsModel.SettingsFileName = $"{DefaultIniFileName}";
            }

            if (string.IsNullOrWhiteSpace(settingsModel.GameName))
            {
                settingsModel.SettingsFileName = gameName;
            }

            Settings = settingsModel;

            StaticLogger.Debug($"Settings: {Settings}");

            StaticLogger.Info($"Finished {nameof(SettingsManager)}.{nameof(Initialize)}");

            return isLoaded = true;
        }
    }
}
