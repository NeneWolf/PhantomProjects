using Microsoft.Xna.Framework.Audio;

namespace PhantomProjects
{
    class Sounds
    {
        #region Declarations
        private SoundEffectInstance bulletSoundInstance;
        private SoundEffectInstance bloodSoundInstance;
        private SoundEffectInstance fireballSoundInstance;
        #endregion

        #region Constructor
        public void Initialize(SoundEffect bulletSound, SoundEffect bloodSound, SoundEffect fireballSound)
        {
            //inits 
            bulletSoundInstance = bulletSound.CreateInstance();
            bloodSoundInstance = bloodSound.CreateInstance();

            //reduce the volume
            bulletSoundInstance.Volume = 0.3f;
            bloodSoundInstance.Volume = 0.3f;

            if(fireballSound != null)
            {
                fireballSoundInstance = fireballSound.CreateInstance();
                fireballSoundInstance.Volume = 0.3f;
            }

        }
        #endregion

        #region Methods
        //Return the sounds to any scene that requires
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
        #endregion
    }
}
