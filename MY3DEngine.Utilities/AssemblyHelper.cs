using MY3DEngine.Logging;
using MY3DEngine.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MY3DEngine.Utilities
{
    public static class AssemblyHelper
    {
        /// <summary>
        /// Get all of the assemblies from the toolkit
        /// </summary>
        /// <returns></returns>
        public static IReadOnlyCollection<Assembly> GetAssemblies(IFileIO fileIo)
        {
            var dlls = fileIo.GetFiles(FileIO.GetCurrentDirectory, "*.dll");
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
