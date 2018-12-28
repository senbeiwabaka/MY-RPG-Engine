// <copyright file="GameEngineLoad.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MY3DEngine.BuildTools
{
    using MY3DEngine.BuildTools.Models;
    using Newtonsoft.Json;
    using NLog;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities.Interfaces;

    public static class GameEngineLoad
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        // TODO: UPDATE
        public static bool LoadLevel(string path, List<object> gameObjects)
        {
            Logger.Info($"Starting {nameof(GameEngineLoad)}.{nameof(LoadLevel)}");

            try
            {
                var contentsofFile = System.IO.File.ReadAllText(path);
                var jsonDeserializedData = JsonConvert.DeserializeObject(contentsofFile) as IEnumerable;

                foreach (var item in jsonDeserializedData)
                {
                    var jsonSettings = new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    };
                    var gameObject = JsonConvert.DeserializeObject(item.ToString(), jsonSettings);

                    if (gameObject != null)
                    {
                        gameObjects.Add(gameObject);
                    }
                }

                return true;
            }
            catch (Exception exception)
            {
                Logger.Error(exception, $"{nameof(GameEngineLoad)}.{nameof(LoadLevel)}");
            }

            Logger.Info($"Finished {nameof(GameEngineLoad)}.{nameof(LoadLevel)}");

            return false;
        }

        // TODO: FINISH
        /// <summary>
        /// Load a game project
        /// </summary>
        /// <param name="folderLocation">The location for the game files</param>
        /// <returns>The load project result. <see cref="ToolsetGameModel"/></returns>
        public static ToolsetGameModel LoadProject(string folderLocation, IFileService fileIo)
        {
            Logger.Info($"Starting {nameof(GameEngineLoad)}.{nameof(LoadProject)}");

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

            Logger.Info($"Finished {nameof(GameEngineLoad)}.{nameof(LoadProject)}");

            return model;
        }
    }
}
