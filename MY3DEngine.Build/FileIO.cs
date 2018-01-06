using System.IO;

namespace MY3DEngine.Build
{
    public static class FileIO
    {
        public static bool CreateNewProjectFiles()
        {
            try
            {
                if (!Directory.Exists(Engine.GameEngine.FolderLocation))
                {
                    Directory.CreateDirectory(Engine.GameEngine.FolderLocation);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}