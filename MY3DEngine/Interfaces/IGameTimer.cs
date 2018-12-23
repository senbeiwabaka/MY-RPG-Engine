// <copyright file="IGameTimer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MY3DEngine.Interfaces
{
    public interface IGameTimer
    {
        float CumulativeFrameTime { get; }

        float FrameTime { get; }

        void Frame();

        bool Initialize();
    }
}