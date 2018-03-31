using NLog;
using System;
using System.Collections.Generic;
using System.IO;

namespace MY3DEngine.Utilities
{
    public static class FileIO
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static string GetCurrentDirectory => Environment.CurrentDirectory;

        /// <summary>
        /// Create a new directory
        /// </summary>
        /// <returns></returns>
        public static bool CreateDirectory(string folderPath)
        {
            logger.Debug($"Starting {nameof(CreateDirectory)}");

            var sucessful = false;

            try
            {
                if (!DirectoryExists(folderPath))
                {
                    var directoryInfo = Directory.CreateDirectory(folderPath);

                    if (directoryInfo != null && directoryInfo.Exists)
                    {
                        sucessful = true;
                    }
                }

                sucessful = true;
            }
            catch (Exception e)
            {
                logger.Error(e, nameof(CreateDirectory));
            }

            logger.Debug($"Finished {nameof(CreateDirectory)}");

            return sucessful;
        }

        public static IReadOnlyCollection<string> GetFiles(string folderLocation, string searchString)
        {
            logger.Debug($"Starting {nameof(GetFiles)}");

            if (!DirectoryExists(folderLocation))
            {
                return new List<string>();
            }

            var dlls = new List<string>();

            try
            {
                dlls.AddRange(Directory.GetFiles(folderLocation, searchString, SearchOption.AllDirectories));
            }
            catch (Exception e)
            {
                logger.Error(e, nameof(GetFiles));
            }

            logger.Debug($"Finished {nameof(GetFiles)}");

            return dlls;
        }

        /// <summary>
        /// Checks to see if a directory exists
        /// </summary>
        /// <param name="directory">The directory to check</param>
        /// <returns></returns>
        public static bool DirectoryExists(string directory)
        {
            logger.Debug($"{nameof(DirectoryExists)}");

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
            logger.Debug($"{nameof(FileExists)}");

            if (string.IsNullOrWhiteSpace(file))
            {
                return false;
            }

            return File.Exists(file);
        }

        public static string GetFileContent(string file)
        {
            logger.Debug($"Starting {nameof(GetFileContent)}");

            var contents = string.Empty;

            try
            {
                contents = File.ReadAllText(file);
            }
            catch (Exception e)
            {
                logger.Error(e, nameof(GetFileContent));
            }

            logger.Debug($"Finished {nameof(GetFileContent)}");

            return contents;
        }

        public static bool WriteFileContent(string filePath, string fileContents)
        {
            logger.Debug($"{nameof(WriteFileContent)}");

            try
            {
                File.AppendAllText(filePath, fileContents);
            }
            catch (Exception e)
            {
                logger.Error(e, nameof(WriteFileContent));

                return false;
            }

            return true;
        }
    }
}