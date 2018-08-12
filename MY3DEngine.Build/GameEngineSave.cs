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
        /// <param name="folderLocation">The location for the new game files</param>
        /// <param name="gameName">The name of the game for the INI file</param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static bool CreateNewProject(string folderLocation, string gameName, int width, int height, object settings)
        {
            StaticLogger.Info($"Starting {nameof(GameEngineSave)}.{nameof(CreateNewProject)}");

            if (string.IsNullOrWhiteSpace(folderLocation))
            {
                StaticLogger.Exception($"{nameof(GameEngineSave)}.{nameof(CreateNewProject)}", new ArgumentNullException(nameof(folderLocation)));

                throw new ArgumentNullException(nameof(folderLocation));
            }

            if (settings == null)
            {
                StaticLogger.Exception($"{nameof(GameEngineSave)}.{nameof(CreateNewProject)}", new ArgumentNullException(nameof(settings)));

                throw new ArgumentNullException(nameof(settings));
            }

            try
            {
                var fileName = Constants.MainFileName;
                var fileContents = Resources.MainFile
                    .Replace("{0}", $"@\"{folderLocation}\\GameObjects.go\"")
                    .Replace("{1}", $"@\"{folderLocation}\\ErrorLog.txt\"")
                    .Replace("{2}", $"@\"{folderLocation}\\InformationLog.txt\"")
                    .Replace("{ScreenWidth}", width.ToString())
                    .Replace("{ScreenHeight}", height.ToString());
                var fullFolderLocation = $"{folderLocation}\\{gameName}";
                var fullPath = $"{fullFolderLocation}\\{fileName}";
                var settingsFileName = Constants.SettingsFileName;
                var settingsContent = JsonConvert.SerializeObject(settings);

                if (FileIO.CreateDirectory(fullFolderLocation))
                {
                    FileIO.WriteFileContent(fullPath, fileContents);
                    FileIO.WriteFileContent($"{fullFolderLocation}\\{settingsFileName}", settingsContent);
                }
            }
            catch (Exception ex)
            {
                StaticLogger.Exception($"{nameof(GameEngineSave)}.{nameof(CreateNewProject)}", ex);

                return false;
            }

            StaticLogger.Info($"Finished {nameof(GameEngineSave)}.{nameof(CreateNewProject)}");

            return true;
        }

        // TODO: UPDATE
        public static bool SaveLevel(string filePath, IReadOnlyList<object> gameObjects)
        {
            StaticLogger.Info($"Starting {nameof(GameEngineSave)}.{nameof(SaveLevel)}");

            try
            {
                var jsonSerializedData = JsonConvert.SerializeObject(
                    gameObjects,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });

                System.IO.File.WriteAllText(string.Format("{0}\\GameObjects.go", filePath), jsonSerializedData);

                return true;
            }
            catch (Exception ex)
            {
                StaticLogger.Exception($"{nameof(GameEngineSave)}.{nameof(SaveLevel)}", ex);
            }

            StaticLogger.Info($"Finished {nameof(GameEngineSave)}.{nameof(SaveLevel)}");

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
