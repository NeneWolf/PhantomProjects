using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PhantomProjects.Player
{
    class Bullet
    {
        public Animation BulletAnimation;
        float bulletMoveSpeed;
        public Vector2 Position;
        public int Damage = 20;
        public bool Active;

        public int Width
        {
            get { return BulletAnimation.FrameWidth; }
        }

        public int Height
        {
            get { return BulletAnimation.FrameHeight; }
        }

        public void Initialize(Animation animation, Vector2 position, Player p)
        {
            BulletAnimation = animation;
            Position = position;
            Active = true;

            if (p.currentAnim == p.playerRight)
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

        public void Draw(SpriteBatch spriteBatch)
        {
            BulletAnimation.Draw(spriteBatch);
        }
    }
}
