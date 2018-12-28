// <copyright file="AssemblyHelper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Utilities
{
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Utilities.Interfaces;

    public static class AssemblyHelper
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Get all of the assemblies from the toolkit
        /// </summary>
        /// <param name="fileIo">The file service to help finding assemblies</param>
        /// <returns>A read only list of assemblies being used</returns>
        public static IReadOnlyList<Assembly> GetAssemblies(IFileService fileIo)
        {
            if (fileIo == null)
            {
                throw new ArgumentNullException(nameof(fileIo));
            }

            var dlls = fileIo.GetFiles(fileIo.GetCurrentDirectory, "*.dll");
            var assemblies = new List<Assembly>(dlls.Count);

            try
            {
                foreach (var item in dlls)
                {
                    assemblies.Add(Assembly.LoadFile(item));
                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception, nameof(GetAssemblies));
            }

            return assemblies;
        }
    }
}
