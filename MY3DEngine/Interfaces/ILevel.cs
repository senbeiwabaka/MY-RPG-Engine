﻿// <copyright file="ILevel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MY3DEngine.Interfaces
{
    using System;
    using MY3DEngine.BaseObjects;
    using MY3DEngine.Enums;

    internal interface ILevel : IDisposable
    {
        IObservable<BaseObject> LevelObjects { get; set; }

        LevelType LevelType { get; set; }

        string Name { get; set; }

        bool LoadLevel(string path);

        bool UnLoadLevel();
    }
}