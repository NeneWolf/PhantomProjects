using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using PhantomProjects.Menus_;

namespace PhantomProjects.Player_
{
    class Bullet
    {
        #region Definitions
        public Animation BulletAnimation;
        float bulletMoveSpeed;
        public Vector2 Position;
        public int Damage = 10;
        public int DMGUpgrade = 20;
        public bool Active;
        #endregion

        public int Width
        {
            get { return BulletAnimation.FrameWidth; }
        }

        public int Height
        {
            get { return BulletAnimation.FrameHeight; }
        }

        public void Initialize(Animation animation, Vector2 position, Player p, UpgradeMenu upgrade)
        {
            BulletAnimation = animation;
            Position = position;
            Active = true;

            if (!upgrade.ReturnDMG())
            {
                Damage += DMGUpgrade;
            }

            if (p.currentAnim == p.playerRight || p.currentAnim == p.idleRight)
            {
                bulletMoveSpeed = 30f;
            }
            else bulletMoveSpeed = -30f;
        }

        public void Update(GameTime gameTime)
        {
            Position.X += bulletMoveSpeed;
            BulletAnimation.Position = Position;
            BulletAnimation.Update(gameTime);
        }

        public int DamageReturn() { return Damage; }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            BulletAnimation.Draw(spriteBatch);
        }
    }
}
