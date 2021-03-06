﻿// <copyright file="SettingsManager.cs" company="MY Soft Games LLC">
//      Copyright (c) MY Soft Games LLC. All rights reserved.
// </copyright>

namespace MY3DEngine.Managers
{
    using System;
    using MY3DEngine.Models;
    using My3DEngine.Utilities;
    using My3DEngine.Utilities.Interfaces;
    using NLog;

    /// <summary>
    /// Manages the games settings
    /// </summary>
    public sealed class SettingsManager
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private const string OverrideFolderPath = "\\Override";
        private const string DefaultLevelsPath = "\\Levels";
        private const string DefaultIniFileName = "\\DefaultSettings.ini";
        private const string DefaultAssetsPath = "\\Assets";
        private const string DefaultShaderPath = "\\Assets\\Shaders";

        private readonly IFileService fileService;

        private bool isLoaded;

        public SettingsManager(IFileService fileService)
        {
            this.fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }

        public SettingsModel Settings { get; private set; } = new SettingsModel();

        public bool Initialize(string mainFolderLocation, string gameName, string settings, IFileService fileIo)
        {
            Logger.Info($"Starting {nameof(SettingsManager)}.{nameof(this.Initialize)}");

            if (!this.isLoaded)
            {
                SettingsModel model;

                if (string.IsNullOrWhiteSpace(mainFolderLocation))
                {
                    Logger.Error(new ArgumentNullException(nameof(mainFolderLocation)), $"Starting {nameof(SettingsManager)}.{nameof(this.Initialize)}");

                    throw new ArgumentNullException(nameof(mainFolderLocation));
                }

                string fullPath = $"{mainFolderLocation}{DefaultIniFileName}";

                // The settings parameter has data so just parse it
                if (!string.IsNullOrWhiteSpace(settings))
                {
                    model = Deserialize.DeserializeStringAsT<SettingsModel>(settings);
                }

                // The settings parameter doesn't have data so we need to build the location then
                // parse the data
                else
                {
                    if (!fileIo.FileExists(fullPath))
                    {
                        // TODO: FIX
                        return this.isLoaded = false;
                    }

                    model = Deserialize.DeserializeFileAsT<SettingsModel>(fullPath, fileService);
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

                Logger.Debug($"Settings: {model}");

                Logger.Info($"Finished {nameof(SettingsManager)}.{nameof(this.Initialize)}");
            }

            return this.isLoaded = true;
        }
    }
}
