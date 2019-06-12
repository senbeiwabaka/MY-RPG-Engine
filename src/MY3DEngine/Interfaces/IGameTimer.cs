// <copyright file="IGameTimer.cs" company="MY Soft Games LLC">
//      Copyright (c) MY Soft Games LLC. All rights reserved.
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