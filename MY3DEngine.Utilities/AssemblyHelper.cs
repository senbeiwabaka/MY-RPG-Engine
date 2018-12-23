// <copyright file="AssemblyHelper.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MY3DEngine.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using MY3DEngine.Logging;
    using MY3DEngine.Utilities.Interfaces;

    public static class AssemblyHelper
    {
        /// <summary>
        /// Get all of the assemblies from the toolkit
        /// </summary>
        /// <param name="fileIo"></param>
        /// <returns></returns>
        public static IReadOnlyCollection<Assembly> GetAssemblies(IFileIO fileIo)
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
                StaticLogger.Exception(nameof(GetAssemblies), exception);
            }

            return assemblies;
        }
    }
}
