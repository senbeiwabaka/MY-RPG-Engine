using MY3DEngine.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace MY3DEngine.Utilities
{
    public static class FileIO
    {
        public static string GetCurrentDirectory => Environment.CurrentDirectory;

        /// <summary>
        /// Create a new directory
        /// </summary>
        /// <returns></returns>
        public static bool CreateDirectory(string folderPath)
        {
            WriteToLog.Debug($"Starting {nameof(CreateDirectory)}");

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
                WriteToLog.Exception(nameof(CreateDirectory), e);
            }

            WriteToLog.Debug($"Finished {nameof(CreateDirectory)}");

            return sucessful;
        }

        public static IReadOnlyCollection<string> GetFiles(string folderLocation, string searchString)
        {
            WriteToLog.Debug($"Starting {nameof(GetFiles)}");

            if (!DirectoryExists(folderLocation))
            {
                return new List<string>();
            }

            var dlls = new List<string>();

            try
            {
                dlls.AddRange(Directory.GetFiles(folderLocation, searchString, SearchOption.AllDirectories));
            }
            catch(Exception e)
            {
                WriteToLog.Exception($"{nameof(GetFiles)}", e);
            }

            WriteToLog.Debug($"Finished {nameof(GetFiles)}");

            return dlls;
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
            WriteToLog.Debug($"Starting {nameof(GetFileContent)}");

            var contents = string.Empty;

            try
            {
                contents = File.ReadAllText(file);
            }
            catch (Exception e)
            {
                WriteToLog.Exception(nameof(GetFileContent), e);
            }

            WriteToLog.Debug($"Finished {nameof(GetFileContent)}");

            return contents;
        }

        public static bool WriteFileContent(string filePath, string fileContents)
        {
            try
            {
                File.AppendAllText(filePath, fileContents);
            }
            catch (Exception e)
            {
                WriteToLog.Exception(nameof(WriteFileContent), e);

                return false;
            }

            return true;
        }
    }
}