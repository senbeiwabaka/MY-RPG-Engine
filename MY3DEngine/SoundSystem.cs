using SlimDX.DirectSound;
using SlimDX.Multimedia;
using SlimDX.X3DAudio;
using System;

namespace MY3DEngine
{
    internal class SoundSystem : IDisposable
    {
        private SecondarySoundBuffer sound;
        private PrimarySoundBuffer sound1;
        private DirectSound soundDevice;

        public SoundSystem(float scale = 1.0f)
        {
        }

        public void Dispose()
        {
        }

        public void InitAudio()
        {
        }

        public void SetSearchDirectory(string path = "")
        {
        }
    }
}