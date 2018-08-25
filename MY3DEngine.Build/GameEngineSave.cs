using MY3DEngine.Build.Models;
using MY3DEngine.Build.Properties;
using MY3DEngine.Logging;
using MY3DEngine.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MY3DEngine.Build
{
    public static class GameEngineSave
    {
        /// <summary>
        /// Creates the main game file with a new game project is selected
        /// </summary>
        /// <param name="mainFolderLocation">The location for the new game files</param>
        /// <param name="gameName">The name of the game for the INI file</param>
        /// <param name="settings"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static ToolsetGameModel CreateNewProject(string mainFolderLocation, string gameName, int width, int height, object settings)
        {
            StaticLogger.Info($"Starting {nameof(GameEngineSave)}.{nameof(CreateNewProject)}");

            if (string.IsNullOrWhiteSpace(mainFolderLocation))
            {
                StaticLogger.Exception($"{nameof(GameEngineSave)}.{nameof(CreateNewProject)}", new ArgumentNullException(nameof(mainFolderLocation)));

                throw new ArgumentNullException(nameof(mainFolderLocation));
            }

            if (settings == null)
            {
                StaticLogger.Exception($"{nameof(GameEngineSave)}.{nameof(CreateNewProject)}", new ArgumentNullException(nameof(settings)));

                throw new ArgumentNullException(nameof(settings));
            }

            var settingsContent = JsonConvert.SerializeObject(settings);
            var fullPathOfMainFile = $"{mainFolderLocation}\\{Constants.MainFileName}";

            try
            {
                var fileContents = Resources.MainFile
                    .Replace("{0}", $"@\"{mainFolderLocation}\\GameObjects.go\"")
                    .Replace("{1}", $"@\"{mainFolderLocation}\\ErrorLog.txt\"")
                    .Replace("{2}", $"@\"{mainFolderLocation}\\InformationLog.txt\"")
                    .Replace("{ScreenWidth}", width.ToString())
                    .Replace("{ScreenHeight}", height.ToString());

                var settingsFileName = Constants.SettingsFileName;

                if (!FileIO.DirectoryExists(mainFolderLocation))
                {
                    FileIO.CreateDirectory(mainFolderLocation);
                }

                FileIO.WriteFileContent(fullPathOfMainFile, fileContents);
                FileIO.WriteFileContent($"{mainFolderLocation}\\{settingsFileName}", settingsContent);
            }
            catch (ArgumentNullException ex)
            {
                StaticLogger.Exception($"{nameof(GameEngineSave)}.{nameof(CreateNewProject)}", ex);

                return new ToolsetGameModel(false);
            }
            catch (ArgumentException ex)
            {
                StaticLogger.Exception($"{nameof(GameEngineSave)}.{nameof(CreateNewProject)}", ex);

                return new ToolsetGameModel(false);
            }

            StaticLogger.Info($"Finished {nameof(GameEngineSave)}.{nameof(CreateNewProject)}");

            return new ToolsetGameModel(true)
            {
                MainFileFolderLocation = fullPathOfMainFile,
                MainFileName = Constants.MainFileName,
                GameName = gameName,
                FolderLocation = mainFolderLocation,
                Settings = settingsContent
            };
        }

        // TODO: UPDATE
        // Needs the class files saved
        // Needs the game objects saved
        // Needs the settings saved
        public static bool SaveProject(string filePath, IReadOnlyList<object> gameObjects)
        {
            StaticLogger.Info($"Starting {nameof(GameEngineSave)}.{nameof(SaveProject)}");
            
            StaticLogger.Info($"Finished {nameof(GameEngineSave)}.{nameof(SaveProject)}");

            return false;
        }

        public static bool SaveSettings(string filePath, string fileName, object settingsData)
        {
            StaticLogger.Info($"Starting {nameof(GameEngineSave)}.{nameof(SaveSettings)}");

            try
            {
                var jsonSerializedData = JsonConvert.SerializeObject(settingsData);

                FileIO.WriteFileContent($"{filePath}\\{fileName}", jsonSerializedData, false);

                return true;
            }
            catch (Exception ex)
            {
                StaticLogger.Exception($"{nameof(GameEngineSave)}.{nameof(SaveSettings)}", ex);
            }

            StaticLogger.Info($"Finished {nameof(GameEngineSave)}.{nameof(SaveSettings)}");

            return false;
        }
    }
}