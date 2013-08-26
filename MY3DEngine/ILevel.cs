using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MY3DEngine
{
    public enum LevelType
    {
        LoadingScreen,
        MainMenu,
        Credits,
        Level
    }

    interface ILevel : IDisposable
    {
        string Name { get; set; }
        LevelType LevelType { get; set; }

        bool LoadLevel();
        bool UnLoadLevel();

    }
}
