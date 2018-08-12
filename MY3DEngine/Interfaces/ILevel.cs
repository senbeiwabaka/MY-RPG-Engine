using MY3DEngine.BaseObjects;
using MY3DEngine.Enums;
using System;

namespace MY3DEngine.Interfaces
{
    internal interface ILevel : IDisposable
    {
        IObservable<BaseObject> LevelObjects { get; set; }
        LevelType LevelType { get; set; }
        string Name { get; set; }

        bool LoadLevel(string path);

        bool UnLoadLevel();
    }
}