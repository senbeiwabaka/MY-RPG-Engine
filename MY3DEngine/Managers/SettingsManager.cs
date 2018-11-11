namespace MY3DEngine.Managers
{
    using System;
    using MY3DEngine.Logging;
    using MY3DEngine.Models;
    using MY3DEngine.Utilities;
    using MY3DEngine.Utilities.Interfaces;

    /// <summary>
    /// Manages the games settings
    /// </summary>
    public sealed class SettingsManager
    {
        private const string OverrideFolderPath = "\\Override";
        private const string DefaultLevelsPath = "\\Levels";
        private const string DefaultIniFileName = "\\DefaultSettings.ini";
        private const string DefaultAssetsPath = "\\Assets";
        private const string DefaultShaderPath = "\\Assets\\Shaders";

        private readonly string CurrentDirectory = FileIO.GetCurrentDirectory;

        private bool isLoaded;

        /// <summary>
        /// Gets the games settings
        /// </summary>
        public SettingsModel Settings { get; private set; } = new SettingsModel();

        /// <summary>
        /// Initialize the settings for the game engine
        /// </summary>
        /// <param name="mainFolderLocation"></param>
        /// <param name="gameName"></param>
        /// <param name="settings"></param>
        /// <param name="fileIo"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public bool Initialize(string mainFolderLocation, string gameName, string settings, IFileIO fileIo)
        {
            StaticLogger.Info($"Starting {nameof(SettingsManager)}.{nameof(this.Initialize)}");

            if (!this.isLoaded)
            {
                SettingsModel model;

                if (string.IsNullOrWhiteSpace(mainFolderLocation))
                {
                    StaticLogger.Exception($"Starting {nameof(SettingsManager)}.{nameof(this.Initialize)}", new ArgumentNullException(nameof(mainFolderLocation)));

                    throw new ArgumentNullException(nameof(mainFolderLocation));
                }

                string fullPath = $"{mainFolderLocation}{DefaultIniFileName}";

                // The settings parameter has data so just parse it
                if (!string.IsNullOrWhiteSpace(settings))
                {
                    model = Deserialize.DeserializeStringAsT<SettingsModel>(settings);
                }

                // The settings parameter doesn't have data so we need to build the location then parse the data
                else
                {
                    if (!fileIo.FileExists(fullPath))
                    {
                        // TODO: FIX
                        return this.isLoaded = false;
                    }

                    model = Deserialize.DeserializeFileAsT<SettingsModel>(fullPath, new FileIO());
                }

                if (string.IsNullOrWhiteSpace(model.MainFolderLocation))
                {
                    model.MainFolderLocation = mainFolderLocation;
                }

                if (string.IsNullOrWhiteSpace(model.ShaderPath))
                {
                    model.ShaderPath = $"{model.MainFolderLocation}{DefaultShaderPath}";
                }

                if (string.IsNullOrWhiteSpace(model.AssetsPath))
                {
                    model.AssetsPath = $"{model.MainFolderLocation}{DefaultAssetsPath}";
                }

                if (string.IsNullOrWhiteSpace(model.LevelsPath))
                {
                    model.LevelsPath = $"{model.MainFolderLocation}{DefaultLevelsPath}";
                }

                if (string.IsNullOrWhiteSpace(model.SettingsFileName))
                {
                    model.SettingsFileName = $"{DefaultIniFileName}";
                }

                if (string.IsNullOrWhiteSpace(model.GameName))
                {
                    model.SettingsFileName = gameName;
                }

                this.Settings = model;

                StaticLogger.Debug($"Settings: {model}");

                StaticLogger.Info($"Finished {nameof(SettingsManager)}.{nameof(this.Initialize)}");
            }

            return this.isLoaded = true;
        }
    }
}
