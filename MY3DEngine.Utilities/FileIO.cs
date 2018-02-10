using System;
using System.IO;

namespace MY3DEngine.Utilities
{
    public static class FileIO
    {
        public static string GetCurrentDirectory => Environment.CurrentDirectory;

        /// <summary>
        /// Create a new game folder setup
        /// </summary>
        /// <returns></returns>
        public static bool CreateNewProjectFiles()
        {
            try
            {
                //if (!Directory.Exists(Engine.GameEngine.FolderLocation))
                //{
                //    Directory.CreateDirectory(Engine.GameEngine.FolderLocation);
                //}
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks to see if a directory exists
        /// </summary>
        /// <param name="directory">The directory to check</param>
        /// <returns></returns>
        public static bool DirectoryExists(string directory)
        {
            if (string.IsNullOrWhiteSpace(directory))
            {
                return false;
            }

            return Directory.Exists(directory);
        }

        /// <summary>
        /// Check to see if a exists
        /// </summary>
        /// <param name="file">The file to check</param>
        /// <returns></returns>
        public static bool FileExists(string file)
        {
            if (string.IsNullOrWhiteSpace(file))
            {
                return false;
            }

            return File.Exists(file);
        }

        public static string GetFileContent(string file)
        {
            return File.ReadAllText(file);
        }
    }
}