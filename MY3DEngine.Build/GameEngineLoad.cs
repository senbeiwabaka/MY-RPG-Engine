using MY3DEngine.Build.Models;
using MY3DEngine.Logging;
using MY3DEngine.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MY3DEngine.Build
{
    public static class GameEngineLoad
    {
        // TODO: UPDATE
        public static bool LoadLevel(string path, List<object> gameObjects)
        {
            StaticLogger.Info($"Starting {nameof(GameEngineLoad)}.{nameof(LoadLevel)}");

            try
            {
                var contentsofFile = System.IO.File.ReadAllText(path);
                var jsonDeserializedData = JsonConvert.DeserializeObject(contentsofFile) as IEnumerable;

                foreach (var item in jsonDeserializedData)
                {
                    var gameObject = JsonConvert.DeserializeObject(
                        item.ToString(),
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.Auto
                        });

                    if (gameObject != null)
                    {
                        gameObjects.Add(gameObject);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                StaticLogger.Exception($"{nameof(GameEngineSave)}.{nameof(LoadLevel)}", ex);
            }

            StaticLogger.Info($"Finished {nameof(GameEngineLoad)}.{nameof(LoadLevel)}");

            return false;
        }

        // TODO: FINISH
        /// <summary>
        /// Load a game project
        /// </summary>
        /// <param name="folderLocation">The location for the game files</param>
        /// <returns>The load project result. <see cref="ToolsetGameModel"/></returns>
        public static ToolsetGameModel LoadProject(string folderLocation)
        {
            StaticLogger.Info($"Starting {nameof(GameEngineLoad)}.{nameof(LoadProject)}");

            ToolsetGameModel model;

            // The folder path passed in is empty or is not a valid directory
            if (string.IsNullOrWhiteSpace(folderLocation) || !FileIO.DirectoryExists(folderLocation))
            {
                model = new ToolsetGameModel(false);
            }
            else
            {
                model = new ToolsetGameModel(true)
                {
                    FolderLocation = folderLocation
                };

                var files = FileIO.GetFiles(folderLocation, "main.cs");

                if (files.Any(x => x.ToLowerInvariant().Contains("main.cs".ToLowerInvariant())))
                {
                    var mainFile = files.Single(x => x.ToLowerInvariant().Contains("main.cs".ToLowerInvariant()));
                    model.MainFileFolderLocation = mainFile.Substring(0, mainFile.IndexOf("main.cs".ToLowerInvariant(), StringComparison.Ordinal));
                    model.MainFileName = "main.cs";
                }

                files = FileIO.GetFiles(folderLocation, "settings".ToLowerInvariant());

                if (files.Any(x => x.ToLowerInvariant().Contains("settings".ToLowerInvariant())))
                {
                    model.Settings = FileIO.GetFileContent(files.First(x => x.ToLowerInvariant().Contains("settings".ToLowerInvariant())));
                }

                if (string.IsNullOrWhiteSpace(model.Settings))
                {
                    files = FileIO.GetFiles(folderLocation, "DefaultSettings.ini");

                    if (files.Any(x => x.ToLowerInvariant().Contains("DefaultSettings.ini".ToLowerInvariant())))
                    {
                        model.Settings = FileIO.GetFileContent(files.Single(x => x.ToLowerInvariant().Contains("DefaultSettings.ini".ToLowerInvariant())));
                    }
                }
            }

            StaticLogger.Info($"Finished {nameof(GameEngineLoad)}.{nameof(LoadProject)}");

            return model;
        }
    }
}
