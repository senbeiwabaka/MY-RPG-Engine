using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MY3DEngine
{
    internal static class FileSystem
    {
        internal static bool FilePathExists(string path)
        {
            return File.Exists(path);
        }

        internal static bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }
    }
}
