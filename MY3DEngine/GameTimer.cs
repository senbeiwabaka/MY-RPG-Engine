using MY3DEngine.Interfaces;
using System.Diagnostics;

namespace MY3DEngine
{
    /// <inheritdoc/>
    public sealed class GameTimer : IGameTimer
    {
        private float cumulativeFrameTime;
        private float frameTime;
        private long m_LastFrameTime;
        private float m_ticksPerMs;
        private Stopwatch stopwatch;

        /// <inheritdoc/>
        public float CumulativeFrameTime => this.cumulativeFrameTime;

        /// <inheritdoc/>
        public float FrameTime => this.frameTime;

        /// <inheritdoc/>
        public void Frame()
        {
            // Query the current time.
            long currentTime = this.stopwatch.ElapsedTicks;

            // Calculate the difference in time since the last time we queried for the current time.
            float timeDifference = currentTime - m_LastFrameTime;

            // Calculate the frame time by the time difference over the timer speed resolution.
            this.frameTime = timeDifference / m_ticksPerMs;
            this.cumulativeFrameTime += this.FrameTime;

            // record this Frames durations to the LastFrame for next frame processing.
            m_LastFrameTime = currentTime;
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
            m_ticksPerMs = (float)(Stopwatch.Frequency / 1000.0f);

            this.stopwatch = Stopwatch.StartNew();

            return true;
        }
    }
}