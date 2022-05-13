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
            bulletSoundInstance = bulletSound.CreateInstance();
            bloodSoundInstance = bloodSound.CreateInstance();

            if(fireballSound != null)
            {
                fireballSoundInstance = fireballSound.CreateInstance();
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
