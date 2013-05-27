using SlimDX.DirectSound;

namespace MY3DEngine
{
    internal class SoundSystem
    {
        private float _scale;
        private bool _loop = false;
        private bool _play = false;
        DirectSound sound;
        SecondarySoundBuffer secondBuffer;

        public SoundSystem(float scale = 1.0f)
        {
            this._scale = scale;
            sound = new DirectSound();
            sound.SetCooperativeLevel(Engine.GameEngine.Window, CooperativeLevel.Priority);
        }
    }
}