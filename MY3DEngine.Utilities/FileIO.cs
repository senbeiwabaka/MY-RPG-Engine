using MY3DEngine.Utilities.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;

namespace MY3DEngine.Utilities
{
    public class FileIO : IFileIO
    {
        public static readonly string GetCurrentDirectory = Environment.CurrentDirectory;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Code a source file to a destination location. Overwrite an already existing file, if one
        /// is there, if the last value is set to true.
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destinationFileName"></param>
        /// <param name="overwriteFile"></param>
        /// <returns></returns>
        public bool CopyFile(string sourceFileName, string destinationFileName, bool overwriteFile)
        {
            logger.Info($"Starting Method: {nameof(CopyFile)}");

            try
            {
                File.Copy(sourceFileName, destinationFileName, overwriteFile);
            }
            catch (Exception exception)
            {
                logger.Error(exception, nameof(CopyFile));

                return false;
            }

            logger.Info($"Finished Method: {nameof(CopyFile)}");

            return true;
        }

        public bool CopyFile(string sourceFileName, string destinationFileName)
        {
            return CopyFile(sourceFileName, destinationFileName, false);
        }

        /// <summary>
        /// Create a new directory
        /// </summary>
        /// <returns></returns>
        public bool CreateDirectory(string folderPath)
        {
            logger.Info($"Starting {nameof(CreateDirectory)}");

            try
            {
                if (DirectoryExists(folderPath))
                {
                    logger.Info($"Finished {nameof(CreateDirectory)}");

                    return true;
                }

                var directoryInfo = Directory.CreateDirectory(folderPath);

                if (directoryInfo != null && directoryInfo.Exists)
                {
                    logger.Info($"Finished {nameof(CreateDirectory)}");

                    return true;
                }
            }
            catch (ArgumentNullException exception)
            {
                logger.Error(exception, nameof(CreateDirectory));
            }
            catch (ArgumentException exception)
            {
                logger.Error(exception, nameof(CreateDirectory));
            }
            catch (UnauthorizedAccessException exception)
            {
                logger.Error(exception, nameof(CreateDirectory));
            }
            catch (DirectoryNotFoundException exception)
            {
                logger.Error(exception, nameof(CreateDirectory));
            }
            catch (PathTooLongException exception)
            {
                logger.Error(exception, nameof(CreateDirectory));
            }
            catch (IOException exception)
            {
                logger.Error(exception, nameof(CreateDirectory));
            }
            catch (NotSupportedException exception)
            {
                logger.Error(exception, nameof(CreateDirectory));
            }

            logger.Info($"Finished {nameof(CreateDirectory)}");

            return false;
        }

        public bool DeleteFile(string filePath)
        {
            logger.Info($"Starting Method: {nameof(DeleteFile)}");

            try
            {
                if (FileExists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception exception)
            {
                logger.Error(exception, nameof(DeleteFile));

                return false;
            }

            logger.Info($"Finished Method: {nameof(DeleteFile)}");

            return true;
        }

        /// <summary>
        /// Checks to see if a directory exists
        /// </summary>
        /// <param name="directory">The directory to check</param>
        /// <returns></returns>
        public bool DirectoryExists(string directory)
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
        public bool FileExists(string file)
        {
            logger.Info($"Starting {nameof(FileExists)}");

            if (string.IsNullOrWhiteSpace(file))
            {
                return false;
            }

            var fileExists = File.Exists(file);

            logger.Info($"File: '{file}' exists? {fileExists}");

            logger.Info($"Finished {nameof(FileExists)}");

            return fileExists;
        }

        /// <summary>
        /// Get all of the contents from the specified file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public string GetFileContent(string file)
        {
            logger.Info($"Starting {nameof(GetFileContent)}");

            var contents = string.Empty;

            try
            {
                contents = File.ReadAllText(file);
            }
            catch (Exception exception)
            {
                logger.Error(exception, nameof(GetFileContent));
            }

            logger.Info($"Finished {nameof(GetFileContent)}");

            return contents;
        }

        public IReadOnlyList<string> GetFiles(string folderLocation, string searchString)
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
            catch (ArgumentNullException exception)
            {
                logger.Error(exception, nameof(GetFiles));
            }
            catch (ArgumentOutOfRangeException exception)
            {
                logger.Error(exception, nameof(GetFiles));
            }
            catch (ArgumentException exception)
            {
                logger.Error(exception, nameof(GetFiles));
            }
            catch (UnauthorizedAccessException exception)
            {
                logger.Error(exception, nameof(GetFiles));
            }
            catch (DirectoryNotFoundException exception)
            {
                logger.Error(exception, nameof(GetFiles));
            }
            catch (PathTooLongException exception)
            {
                logger.Error(exception, nameof(GetFiles));
            }
            catch (IOException exception)
            {
                logger.Error(exception, nameof(GetFiles));
            }

            logger.Info($"Finished {nameof(GetFiles)}");

            return files;
        }

        /// <summary>
        /// Write all of the contents to the specified file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileContents"></param>
        /// <returns></returns>
        public bool WriteFileContent(string filePath, string fileContents)
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
        public bool WriteFileContent(string filePath, string fileContents, bool appendContents)
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
            catch (Exception exception)
            {
                logger.Error(exception, nameof(WriteFileContent));

                return false;
            }

            logger.Info($"Finished Method: {nameof(WriteFileContent)}");

            return true;
        }
    }
}
