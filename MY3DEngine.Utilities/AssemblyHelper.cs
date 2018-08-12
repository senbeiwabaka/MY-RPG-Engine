using NLog;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MY3DEngine.Utilities
{
    public static class AssemblyHelper
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Get all of the assemblies from the toolkit
        /// </summary>
        /// <returns></returns>
        public static IReadOnlyCollection<Assembly> GetAssemblies()
        {
            var dlls = FileIO.GetFiles(FileIO.GetCurrentDirectory, "*.dll");
            var assemblies = new List<Assembly>(dlls.Count);

            try
            {
                foreach (var item in dlls)
                {
                    assemblies.Add(Assembly.LoadFile(item));
                }
            }
            catch (Exception e)
            {
                logger.Error(e, nameof(GetAssemblies));
            }

            return assemblies;
        }
    }
}
