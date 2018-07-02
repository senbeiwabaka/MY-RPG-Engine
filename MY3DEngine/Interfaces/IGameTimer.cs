namespace MY3DEngine
{
    public interface IGameTimer
    {
        float CumulativeFrameTime { get; }
        float FrameTime { get; }

        void Frame();

        bool Initialize();
    }
}