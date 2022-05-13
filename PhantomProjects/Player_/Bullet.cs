using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PhantomProjects.Menus_;

namespace PhantomProjects.Player_
{
    class Bullet
    {
        #region Declarations
        public Animation BulletAnimation;
        float bulletMoveSpeed;
        public Vector2 Position;
        public int Damage = 10;
        public int DMGUpgrade = 20;
        public bool Active;

        public int Width
        {
            get { return BulletAnimation.FrameWidth; }
        }

        public int Height
        {
            get { return BulletAnimation.FrameHeight; }
        }
        #endregion

        #region Constructor
        public void Initialize(Animation animation, Vector2 position, Player p, UpgradeMenu upgrade)
        {
            BulletAnimation = animation;
            Position = position;
            Active = true;

            //Check if weapondamage has been upgraded
            if (!upgrade.ReturnDMG())
            {
                Damage += DMGUpgrade;
            }

            //Change direction of firing the bullet
            if (p.currentAnim == p.playerRight || p.currentAnim == p.idleRight)
            {
                bulletMoveSpeed = 30f;
            }
            else bulletMoveSpeed = -30f;
        }
        #endregion

        #region Methods
        //Update Method
        public void Update(GameTime gameTime)
        {
            Position.X += bulletMoveSpeed;
            BulletAnimation.Position = Position;
            BulletAnimation.Update(gameTime);
        }

        //Method to return bullet's damage
        public int DamageReturn() { return Damage; }

        //Draw Method
        public void Draw(SpriteBatch spriteBatch)
        {
            BulletAnimation.Draw(spriteBatch);
        }
        #endregion
    }
}
