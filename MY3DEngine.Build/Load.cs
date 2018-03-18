using MY3DEngine.Build.Models;
using MY3DEngine.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MY3DEngine.Build
{
    public static class Load
    {
        public static GameModel LoadProject(string folderLocation)
        {
            var model = new GameModel();

            try
            {
                model.Successfull = true;
            }
            catch (Exception e)
            {
                WriteToLog.Exception(nameof(LoadProject), e);

                model.Successfull = false;
            }

            return model;
        }
    }
}