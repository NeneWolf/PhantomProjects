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
        private SoundEffectInstance fireballSoundInstance;

        public void Initialize(SoundEffect bulletSound, SoundEffect bloodSound, SoundEffect fireballSound)
        {
            bulletSoundInstance = bulletSound.CreateInstance();
            bloodSoundInstance = bloodSound.CreateInstance();

            if(fireballSound != null)
            {
                fireballSoundInstance = fireballSound.CreateInstance();
            }

        }
        //HERE WE WILL RETURN ALL TH SOUNDS ACROSS ALL THE CLASSES
        public SoundEffectInstance BULLET
        {
            get { return bulletSoundInstance; }
        }

        public SoundEffectInstance FIREBALL
        {
            get { return fireballSoundInstance; }
        }

        public SoundEffectInstance BLOOD
        {
            get { return bloodSoundInstance; }
        }

    }
}
