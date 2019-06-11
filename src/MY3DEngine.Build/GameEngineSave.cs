// <copyright file="GameEngineSave.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using My3DEngine.Utilities.Interfaces;
using MY3DEngine.BuildTools.Models;
using MY3DEngine.BuildTools.Properties;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MY3DEngine.BuildTools
{
    public static class GameEngineSave
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Creates the main game file with a new game project is selected
        /// </summary>
        /// <param name="mainFolderLocation">The location for the new game files</param>
        /// <param name="gameName">The name of the game for the INI file</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="settings"></param>
        /// <param name="fileIo"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static ToolsetGameModel CreateNewProject(
            string mainFolderLocation,
            string gameName,
            int width,
            int height,
            object settings,
            IFileService fileIo)
        {
            Logger.Info($"Starting {nameof(GameEngineSave)}.{nameof(CreateNewProject)}");

            if (string.IsNullOrWhiteSpace(mainFolderLocation))
            {
                Logger.Error($"{nameof(GameEngineSave)}.{nameof(CreateNewProject)}", new ArgumentNullException(nameof(mainFolderLocation)));

                throw new ArgumentNullException(nameof(mainFolderLocation));
            }

            if (settings == null)
            {
                Logger.Error($"{nameof(GameEngineSave)}.{nameof(CreateNewProject)}", new ArgumentNullException(nameof(settings)));

                throw new ArgumentNullException(nameof(settings));
            }

            var fullPath = $"{mainFolderLocation}\\{gameName}";
            var fullPathOfMainFile = $"{fullPath}\\{Constants.MainFileName}";

            var folderLocation = settings.GetType().GetProperty("MainFolderLocation");
            folderLocation.SetValue(settings, fullPath);

            var settingsContent = JsonConvert.SerializeObject(settings);

            try
            {
                var mainFileContents = Resources.MainFile
                    .Replace("{0}", $"@\"{mainFolderLocation}\\GameObjects.go\"")
                    .Replace("{1}", $"@\"{mainFolderLocation}\\ErrorLog.txt\"")
                    .Replace("{2}", $"@\"{mainFolderLocation}\\InformationLog.txt\"")
                    .Replace("{ScreenWidth}", width.ToString(CultureInfo.CurrentCulture))
                    .Replace("{ScreenHeight}", height.ToString(CultureInfo.CurrentCulture));

                var settingsFileName = Constants.SettingsFileName;

                if (!fileIo.DirectoryExists(fullPath))
                {
                    fileIo.CreateDirectory(fullPath);
                }

                fileIo.WriteFileContent(fullPathOfMainFile, mainFileContents);
                fileIo.WriteFileContent($"{fullPath}\\{settingsFileName}", settingsContent);

                if (!fileIo.DirectoryExists($"{fullPath}\\Assets"))
                {
                    fileIo.CreateDirectory($"{fullPath}\\Assets");
                }

                if (!fileIo.DirectoryExists($"{fullPath}\\Assets\\Shaders"))
                {
                    fileIo.CreateDirectory($"{fullPath}\\Assets\\Shaders");
                }

                if (!fileIo.FileExists($"{fullPath}\\Assets\\Shaders\\Color.ps"))
                {
                    var contents = fileIo.GetFileContent($"{Environment.CurrentDirectory}\\Assets\\Shaders\\Color.ps");
                    fileIo.WriteFileContent($"{fullPath}\\Assets\\Shaders\\Color.ps", contents, false);
                }

                if (!fileIo.FileExists($"{fullPath}\\Assets\\Shaders\\Color.vs"))
                {
                    var contents = fileIo.GetFileContent($"{Environment.CurrentDirectory}\\Assets\\Shaders\\Color.vs");
                    fileIo.WriteFileContent($"{fullPath}\\Assets\\Shaders\\Color.vs", contents, false);
                }

                if (!fileIo.FileExists($"{fullPath}\\Assets\\Shaders\\texture.ps"))
                {
                    var contents = fileIo.GetFileContent($"{Environment.CurrentDirectory}\\Assets\\Shaders\\texture.ps");
                    fileIo.WriteFileContent($"{fullPath}\\Assets\\Shaders\\texture.ps", contents, false);
                }

                if (!fileIo.FileExists($"{fullPath}\\Assets\\Shaders\\texture.vs"))
                {
                    var contents = fileIo.GetFileContent($"{Environment.CurrentDirectory}\\Assets\\Shaders\\texture.vs");
                    fileIo.WriteFileContent($"{fullPath}\\Assets\\Shaders\\texture.vs", contents, false);
                }
            }
            catch (ArgumentNullException exception)
            {
                Logger.Error(exception, $"{nameof(GameEngineSave)}.{nameof(CreateNewProject)}");

                return new ToolsetGameModel(false);
            }
            catch (ArgumentException exception)
            {
                Logger.Error(exception, $"{nameof(GameEngineSave)}.{nameof(CreateNewProject)}");

                return new ToolsetGameModel(false);
            }

            Logger.Info($"Finished {nameof(GameEngineSave)}.{nameof(CreateNewProject)}");

            return new ToolsetGameModel(true)
            {
                MainFileFolderLocation = fullPathOfMainFile,
                MainFileName = Constants.MainFileName,
                GameName = gameName,
                FolderLocation = fullPath,
                Settings = settingsContent
            };
        }

        // TODO: UPDATE Needs the class files saved Needs the game objects saved Needs the settings saved
        public static bool SaveProject(string filePath, IReadOnlyList<object> gameObjects)
        {
            Logger.Info($"Starting {nameof(GameEngineSave)}.{nameof(SaveProject)}");

            Logger.Info($"Finished {nameof(GameEngineSave)}.{nameof(SaveProject)}");

            return false;
        }

        public static bool SaveSettings(string filePath, string fileName, object settingsData, IFileService fileIo)
        {
            Logger.Info($"Starting {nameof(GameEngineSave)}.{nameof(SaveSettings)}");

            try
            {
                var jsonSerializedData = JsonConvert.SerializeObject(settingsData);

                fileIo.WriteFileContent($"{filePath}\\{fileName}", jsonSerializedData, false);

                return true;
            }
            catch (Exception exception)
            {
                Logger.Error(exception, $"{nameof(GameEngineSave)}.{nameof(SaveSettings)}");
            }

            Logger.Info($"Finished {nameof(GameEngineSave)}.{nameof(SaveSettings)}");

            return false;
        }
    }
}
