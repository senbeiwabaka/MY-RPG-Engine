using MY3DEngine.Logging;
using System;
using System.Collections.Generic;

namespace MY3DEngine.Utilities
{
    public static class Assembly
    {
        public static IReadOnlyCollection<System.Reflection.Assembly> GetAssemblies()
        {
            var folderLocation = FileIO.GetCurrentDirectory;
            var dlls = FileIO.GetFiles(folderLocation, "*.dll");
            var assemblies = new List<System.Reflection.Assembly>(dlls.Count);

            try
            {
                foreach (var item in dlls)
                {
                    assemblies.Add(System.Reflection.Assembly.LoadFile(item));
                }
            }
            catch(Exception e)
            {
                WriteToLog.Exception($"{nameof(GetAssemblies)}", e);
            }

            return assemblies;
        }
    }
}