using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MY3DEngine.Build.Models
{
    public class GameModel
    {
        public string Name { get; set; }

        public string MainFileName { get; set; }

        public string MainFileFolderLocation { get; set; }

        public string FolderLocation { get; set; }

        public object Settings { get; set; }
        public bool Successfull { get; internal set; }
    }
}
