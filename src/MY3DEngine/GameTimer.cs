// <copyright file="GameTimer.cs" company="MY Soft Games LLC">
//      Copyright (c) MY Soft Games LLC. All rights reserved.
// </copyright>

namespace MY3DEngine
{
    using System.Diagnostics;
    using MY3DEngine.Interfaces;

    /// <inheritdoc/>
    public sealed class GameTimer : IGameTimer
    {
        private long lastFrameTime;
        private float ticksPerMs;
        private Stopwatch stopwatch;

        /// <inheritdoc/>
        public float CumulativeFrameTime { get; private set; }

        /// <inheritdoc/>
        public float FrameTime { get; private set; }

        /// <inheritdoc/>
        public void Frame()
        {
            // Query the current time.
            long currentTime = this.stopwatch.ElapsedTicks;

            // Calculate the difference in time since the last time we queried for the current time.
            float timeDifference = currentTime - lastFrameTime;

            // Calculate the frame time by the time difference over the timer speed resolution.
            this.FrameTime = timeDifference / ticksPerMs;
            this.CumulativeFrameTime += this.FrameTime;

            // record this Frames durations to the LastFrame for next frame processing.
            lastFrameTime = currentTime;
        }

        /// <inheritdoc/>
        public bool Initialize()
        {
            // Check to see if this system supports high performance timers.
            if (!Stopwatch.IsHighResolution)
            {
                return false;
            }

            if (Stopwatch.Frequency == 0)
            {
                return false;
            }

            // Find out how many times the frequency counter ticks every millisecond.
            ticksPerMs = (float)(Stopwatch.Frequency / 1000.0f);

            this.stopwatch = Stopwatch.StartNew();

            return true;
        }
    }
}
