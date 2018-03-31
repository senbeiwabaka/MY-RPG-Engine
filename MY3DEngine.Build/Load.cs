using MY3DEngine.Build.Models;
using MY3DEngine.Logging;
using System;

namespace MY3DEngine.Build
{
    // TODO: FINISH
    public static class Load
    {
        public static GameModel LoadProject(string folderLocation)
        {
            StaticLogger.Debug($"Starting {nameof(LoadProject)}");

            var model = new GameModel();

            try
            {
                model.Successfull = true;
            }
            catch (Exception e)
            {
                StaticLogger.Exception(nameof(LoadProject), e);

                model.Successfull = false;
            }

            StaticLogger.Debug($"Finished {nameof(LoadProject)}");

            return model;
        }
    }
}