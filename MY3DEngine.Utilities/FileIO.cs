// <copyright file="FileIO.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MY3DEngine.Utilities
{
    using MY3DEngine.Utilities.Interfaces;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public sealed class FileIO : IFileIO
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public string GetCurrentDirectory { get; } = Environment.CurrentDirectory;

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
            Logger.Info($"Starting Method: {nameof(CopyFile)}");

            try
            {
                File.Copy(sourceFileName, destinationFileName, overwriteFile);
            }
            catch (Exception exception)
            {
                Logger.Error(exception, nameof(CopyFile));

                return false;
            }

            Logger.Info($"Finished Method: {nameof(CopyFile)}");

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
            Logger.Info($"Starting {nameof(CreateDirectory)}");

            try
            {
                if (DirectoryExists(folderPath))
                {
                    Logger.Info($"Finished {nameof(CreateDirectory)}");

                    return true;
                }

                var directoryInfo = Directory.CreateDirectory(folderPath);

                if (directoryInfo != null && directoryInfo.Exists)
                {
                    Logger.Info($"Finished {nameof(CreateDirectory)}");

                    return true;
                }
            }
            catch (ArgumentNullException exception)
            {
                Logger.Error(exception, nameof(CreateDirectory));
            }
            catch (ArgumentException exception)
            {
                Logger.Error(exception, nameof(CreateDirectory));
            }
            catch (UnauthorizedAccessException exception)
            {
                Logger.Error(exception, nameof(CreateDirectory));
            }
            catch (DirectoryNotFoundException exception)
            {
                Logger.Error(exception, nameof(CreateDirectory));
            }
            catch (PathTooLongException exception)
            {
                Logger.Error(exception, nameof(CreateDirectory));
            }
            catch (IOException exception)
            {
                Logger.Error(exception, nameof(CreateDirectory));
            }
            catch (NotSupportedException exception)
            {
                Logger.Error(exception, nameof(CreateDirectory));
            }

            Logger.Info($"Finished {nameof(CreateDirectory)}");

            return false;
        }

        public bool DeleteFile(string filePath)
        {
            Logger.Info($"Starting Method: {nameof(DeleteFile)}");

            try
            {
                if (FileExists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception, nameof(DeleteFile));

                return false;
            }

            Logger.Info($"Finished Method: {nameof(DeleteFile)}");

            return true;
        }

        /// <summary>
        /// Checks to see if a directory exists
        /// </summary>
        /// <param name="directory">The directory to check</param>
        /// <returns></returns>
        public bool DirectoryExists(string directory)
        {
            Logger.Info($"{nameof(DirectoryExists)}");

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
            Logger.Info($"Starting {nameof(FileExists)}");

            if (string.IsNullOrWhiteSpace(file))
            {
                return false;
            }

            var fileExists = File.Exists(file);

            Logger.Info($"File: '{file}' exists? {fileExists}");

            Logger.Info($"Finished {nameof(FileExists)}");

            return fileExists;
        }

        /// <summary>
        /// Get all of the contents from the specified file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public string GetFileContent(string file)
        {
            Logger.Info($"Starting {nameof(GetFileContent)}");

            var contents = string.Empty;

            try
            {
                contents = File.ReadAllText(file);
            }
            catch (Exception exception)
            {
                Logger.Error(exception, nameof(GetFileContent));
            }

            Logger.Info($"Finished {nameof(GetFileContent)}");

            return contents;
        }

        public IReadOnlyList<string> GetFiles(string folderLocation, string searchString)
        {
            Logger.Info($"Starting {nameof(GetFiles)}");

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
                Logger.Error(exception, nameof(GetFiles));
            }
            catch (ArgumentOutOfRangeException exception)
            {
                Logger.Error(exception, nameof(GetFiles));
            }
            catch (ArgumentException exception)
            {
                Logger.Error(exception, nameof(GetFiles));
            }
            catch (UnauthorizedAccessException exception)
            {
                Logger.Error(exception, nameof(GetFiles));
            }
            catch (DirectoryNotFoundException exception)
            {
                Logger.Error(exception, nameof(GetFiles));
            }
            catch (PathTooLongException exception)
            {
                Logger.Error(exception, nameof(GetFiles));
            }
            catch (IOException exception)
            {
                Logger.Error(exception, nameof(GetFiles));
            }

            Logger.Info($"Finished {nameof(GetFiles)}");

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
            Logger.Info($"Starting Method: {nameof(WriteFileContent)}");

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
                Logger.Error(exception, nameof(WriteFileContent));

                return false;
            }

            Logger.Info($"Finished Method: {nameof(WriteFileContent)}");

            return true;
        }
    }
}
