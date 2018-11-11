namespace MY3DEngine.BuildTools
{
    using System;
    using System.Collections.Generic;
    using MY3DEngine.BuildTools.Models;
    using MY3DEngine.BuildTools.Properties;
    using MY3DEngine.Logging;
    using MY3DEngine.Utilities.Interfaces;
    using Newtonsoft.Json;

    public static class GameEngineSave
    {
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
            IFileIO fileIo)
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
                    .Replace("{ScreenWidth}", width.ToString())
                    .Replace("{ScreenHeight}", height.ToString());

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
                FolderLocation = fullPath,
                Settings = settingsContent
            };
        }

        // TODO: UPDATE Needs the class files saved Needs the game objects saved Needs the settings saved
        public static bool SaveProject(string filePath, IReadOnlyList<object> gameObjects)
        {
            StaticLogger.Info($"Starting {nameof(GameEngineSave)}.{nameof(SaveProject)}");

            StaticLogger.Info($"Finished {nameof(GameEngineSave)}.{nameof(SaveProject)}");

            return false;
        }

        public static bool SaveSettings(string filePath, string fileName, object settingsData, IFileIO fileIo)
        {
            StaticLogger.Info($"Starting {nameof(GameEngineSave)}.{nameof(SaveSettings)}");

            try
            {
                var jsonSerializedData = JsonConvert.SerializeObject(settingsData);

                fileIo.WriteFileContent($"{filePath}\\{fileName}", jsonSerializedData, false);

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
