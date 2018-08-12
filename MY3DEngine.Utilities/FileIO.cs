using NLog;
using System;
using System.Collections.Generic;
using System.IO;

namespace MY3DEngine.Utilities
{
    // TODO: Make into interface so that it can be changed
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
            logger.Info($"Starting {nameof(CreateDirectory)}");

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

            logger.Info($"Finished {nameof(CreateDirectory)}");

            return sucessful;
        }

        public static IReadOnlyList<string> GetFiles(string folderLocation, string searchString)
        {
            logger.Info($"Starting {nameof(GetFiles)}");

            if (!DirectoryExists(folderLocation))
            {
                return new List<string>();
            }

            var files = new List<string>();

            try
            {
                files.AddRange(Directory.GetFiles(folderLocation, searchString, SearchOption.AllDirectories));
            }
            catch (Exception e)
            {
                logger.Error(e, nameof(GetFiles));
            }

            logger.Info($"Finished {nameof(GetFiles)}");

            return files;
        }

        /// <summary>
        /// Checks to see if a directory exists
        /// </summary>
        /// <param name="directory">The directory to check</param>
        /// <returns></returns>
        public static bool DirectoryExists(string directory)
        {
            logger.Info($"{nameof(DirectoryExists)}");

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
            logger.Info($"{nameof(FileExists)}");

            if (string.IsNullOrWhiteSpace(file))
            {
                return false;
            }

            return File.Exists(file);
        }

        /// <summary>
        /// Get all of the contents from the specified file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetFileContent(string file)
        {
            logger.Info($"Starting {nameof(GetFileContent)}");

            var contents = string.Empty;

            try
            {
                contents = File.ReadAllText(file);
            }
            catch (Exception e)
            {
                logger.Error(e, nameof(GetFileContent));
            }

            logger.Info($"Finished {nameof(GetFileContent)}");

            return contents;
        }

        /// <summary>
        /// Write all of the contents to the specified file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileContents"></param>
        /// <returns></returns>
        public static bool WriteFileContent(string filePath, string fileContents)
        {
            return WriteFileContent(filePath, fileContents, true);
        }

        /// <summary>
        /// Write all of the contents to the specified file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileContents"></param>
        /// <param name="appendContents"></param>
        /// <returns></returns>
        public static bool WriteFileContent(string filePath, string fileContents, bool appendContents)
        {
            logger.Info($"Starting Method: {nameof(WriteFileContent)}");

            try
            {
                if (appendContents)
                {
                    File.AppendAllText(filePath, fileContents);
                }
                else
                {
                    File.WriteAllText(filePath, fileContents);
                }
            }
            catch (Exception e)
            {
                logger.Error(e, nameof(WriteFileContent));

                return false;
            }

            logger.Info($"Finished Method: {nameof(WriteFileContent)}");

            return true;
        }

        public static bool CopyFile(string sourceFileName, string destinationFileName, bool overwriteFile = default(bool))
        {
            logger.Info($"Starting Method: {nameof(CopyFile)}");

            try
            {
                File.Copy(sourceFileName, destinationFileName, overwriteFile);
            }
            catch (Exception e)
            {
                logger.Error(e, nameof(CopyFile));

                return false;
            }

            logger.Info($"Finished Method: {nameof(CopyFile)}");

            return true;
        }

        public static bool DeleteFile(string filePath)
        {
            logger.Info($"Starting Method: {nameof(DeleteFile)}");

            try
            {
                if (FileExists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception e)
            {
                logger.Error(e, nameof(DeleteFile));

                return false;
            }

            logger.Info($"Finished Method: {nameof(DeleteFile)}");

            return true;
        }
    }
}
