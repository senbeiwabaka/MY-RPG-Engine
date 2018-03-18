using MY3DEngine.Build.Properties;
using MY3DEngine.Logging;
using MY3DEngine.Utilities;
using Newtonsoft.Json;
using System;

namespace MY3DEngine.Build
{
    public static class Create
    {
        /// <summary>
        /// Creates the main game file with a new game project is selected
        /// </summary>
        /// <param name="folderLocation">The location for the new game files</param>
        /// <param name="gameName">The name of the game for the ini file</param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static bool CreateNewProject(string folderLocation, string gameName, dynamic settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            try
            {
                var fileName = "main.cs";
                var fileContents = Resources.MainFile
                    .Replace("{0}", $"@\"{folderLocation}\\GameObjects.go\"")
                    .Replace("{1}", $"@\"{folderLocation}\\ErrorLog.txt\"")
                    .Replace("{2}", $"@\"{folderLocation}\\InformationLog.txt\"")
                    .Replace("{ScreenWidth}", settings.Width)
                    .Replace("{ScreenHeight}", settings.Height);
                var fullFolderLocation = $"{folderLocation}\\{gameName}";
                var fullPath = $"{fullFolderLocation}\\{fileName}";
                var settingsFileName = "DefaultSettings.ini";
                var settingsContent = JsonConvert.SerializeObject(settings);

                if (FileIO.CreateDirectory(fullFolderLocation))
                {
                    FileIO.WriteFileContent(fullPath, fileContents);
                    FileIO.WriteFileContent($"{fullFolderLocation}\\{settingsFileName}", settingsContent);
                }
            }
            catch (Exception e)
            {
                WriteToLog.Exception(nameof(CreateNewProject), e);

                return false;
            }

            return true;
        }
    }
}