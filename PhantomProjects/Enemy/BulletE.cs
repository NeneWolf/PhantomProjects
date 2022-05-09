using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PhantomProjects
{
    class BulletE
    {
        #region Declarations
        public Animation BulletAnimation; // animation the represents the butter of the enemy
        float bulletMoveSpeed;
        public Vector2 Position;
        public int damage;
        public bool Active;
        bool Right;


        public int Width
        {
            get { return BulletAnimation.FrameWidth; }
        }
        public int Height
        {
            get { return BulletAnimation.FrameHeight; }
        }
        #endregion

        public void Initialize(Animation animation, Vector2 position, bool Direction)
        {
            Right = Direction;
            BulletAnimation = animation;
            Position = position;
            Active = true;
            damage = 10;
            if (Right)
                bulletMoveSpeed = 10f;
            else
                bulletMoveSpeed = -10f;
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
