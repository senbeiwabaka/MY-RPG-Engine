using NLog;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MY3DEngine.Utilities
{
    public static class AssemblyHelper
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static IReadOnlyCollection<Assembly> GetAssemblies()
        {
            var folderLocation = FileIO.GetCurrentDirectory;
            var dlls = FileIO.GetFiles(folderLocation, "*.dll");
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