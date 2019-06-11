// <copyright file="IFileService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace My3DEngine.Utilities.Interfaces
{
    public interface IFileService
    {
        string GetCurrentDirectory { get; }

        /// <summary>
        /// Code a source file to a destination location. Overwrite an already existing file, if one
        /// is there, if the last value is set to true.
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destinationFileName"></param>
        /// <param name="overwriteFile"></param>
        /// <returns></returns>
        bool CopyFile(string sourceFileName, string destinationFileName, bool overwriteFile);

        /// <summary>
        /// Code a source file to a destination location. Overwrite an already existing file, if one
        /// is there, if the last value is set to true.
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destinationFileName"></param>
        /// <returns></returns>
        bool CopyFile(string sourceFileName, string destinationFileName);

        /// <summary>
        /// Create a new directory
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        bool CreateDirectory(string folderPath);

        bool DeleteFile(string filePath);

        /// <summary>
        /// Checks to see if a directory exists
        /// </summary>
        /// <param name="directory">The directory to check</param>
        /// <returns></returns>
        bool DirectoryExists(string directory);

        /// <summary>
        /// Check to see if a exists
        /// </summary>
        /// <param name="file">The file to check</param>
        /// <returns></returns>
        bool FileExists(string file);

        /// <summary>
        /// Get all of the contents from the specified file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        string GetFileContent(string file);

        IReadOnlyList<string> GetFiles(string folderLocation, string searchString);

        /// <summary>
        /// Write all of the contents to the specified file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileContents"></param>
        /// <returns></returns>
        bool WriteFileContent(string filePath, string fileContents);

        /// <summary>
        /// Write all of the contents to the specified file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileContents"></param>
        /// <param name="appendContents"></param>
        /// <returns></returns>
        bool WriteFileContent(string filePath, string fileContents, bool appendContents);
    }
}
