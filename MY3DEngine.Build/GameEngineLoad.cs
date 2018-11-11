namespace MY3DEngine.BuildTools
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using MY3DEngine.BuildTools.Models;
    using MY3DEngine.Logging;
    using MY3DEngine.Utilities.Interfaces;
    using Newtonsoft.Json;

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
                StaticLogger.Exception($"{nameof(GameEngineLoad)}.{nameof(LoadLevel)}", ex);
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
        public static ToolsetGameModel LoadProject(string folderLocation, IFileIO fileIo)
        {
            StaticLogger.Info($"Starting {nameof(GameEngineLoad)}.{nameof(LoadProject)}");

            ToolsetGameModel model;

            // The folder path passed in is empty or is not a valid directory
            if (string.IsNullOrWhiteSpace(folderLocation) || !fileIo.DirectoryExists(folderLocation))
            {
                model = new ToolsetGameModel(false);
            }
            else
            {
                model = new ToolsetGameModel(true)
                {
                    FolderLocation = folderLocation
                };

                var files = fileIo.GetFiles(folderLocation, Constants.MainFileName);

                if (files.Any(x => x.ToUpperInvariant().Contains(Constants.MainFileName.ToUpperInvariant())))
                {
                    var mainFile = files.Single(x => x.ToUpperInvariant().Contains(Constants.MainFileName.ToUpperInvariant()));
                    model.MainFileFolderLocation = mainFile.Substring(0, mainFile.IndexOf(Constants.MainFileName, StringComparison.InvariantCultureIgnoreCase));
                    model.MainFileName = Constants.MainFileName;
                }

                files = fileIo.GetFiles(folderLocation, "settings".ToUpperInvariant());

                if (files.Any(x => x.ToUpperInvariant().Contains("settings".ToUpperInvariant())))
                {
                    model.Settings = fileIo.GetFileContent(files.First(x => x.ToUpperInvariant().Contains("settings".ToUpperInvariant())));
                }

                if (string.IsNullOrWhiteSpace(model.Settings))
                {
                    files = fileIo.GetFiles(folderLocation, "DefaultSettings.ini");

                    if (files.Any(x => x.ToUpperInvariant().Contains("DefaultSettings.ini".ToUpperInvariant())))
                    {
                        model.Settings = fileIo.GetFileContent(files.Single(x => x.ToUpperInvariant().Contains("DefaultSettings.ini".ToUpperInvariant())));
                    }
                }
            }

            StaticLogger.Info($"Finished {nameof(GameEngineLoad)}.{nameof(LoadProject)}");

            return model;
        }
    }
}
