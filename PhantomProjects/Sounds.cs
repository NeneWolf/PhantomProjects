using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace PhantomProjects
{
    class Sounds
    {
        private SoundEffectInstance bulletSoundInstance;
        private SoundEffectInstance bloodSoundInstance;

        public void Initialize(SoundEffect bulletSound, SoundEffect bloodSound)
        {
            bulletSoundInstance = bulletSound.CreateInstance();
            bloodSoundInstance = bloodSound.CreateInstance();

        }
        //HERE WE WILL RETURN ALL TH SOUNDS ACROSS ALL THE CLASSES
        public SoundEffectInstance BULLET
        {
            get { return bulletSoundInstance; }
        }
        public SoundEffectInstance BLOOD
        {
            get { return bloodSoundInstance; }
        }

    }
}
