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