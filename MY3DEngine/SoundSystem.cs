using System;
using System.Collections.Generic;
using SlimDX;
using SlimDX.DirectSound;
using SlimDX.XACT3;

namespace MY3DEngine
{
    internal class SoundSystem
    {
        private float _scale;
        private SoundListener3D _listener;
        private Wave wave;
        private List<Wave> lWave;

        public SoundSystem(float scale = 1.0f)
        {
            this._scale = scale;
        }

        ~SoundSystem()
        {
            
        }

        public void UpdateListener(Vector3 forward, Vector3 position, Vector3 velocity)
        {
            
        }

        public void GarbageCollection()
        {
            
        }

        public void SetVolume(long volume)
        {
            
        }
    }
}