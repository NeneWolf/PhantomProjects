using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PhantomProjects.Boss_
{
    class Fireball
    {
        #region Declarations
        public Animation FireballAnimation; // animation the represents the butter of the enemy
        float fireballMoveSpeed;
        public Vector2 Position;
        public int damage;
        public bool Active;

        public int Width
        {
            get { return FireballAnimation.FrameWidth; }
        }
        public int Height
        {
            get { return FireballAnimation.FrameHeight; }
        }
        #endregion

        public void Initialize(Animation animation, Vector2 position)
        {
            FireballAnimation = animation;
            Position = position;
            Active = true;
            damage = 15;
            fireballMoveSpeed = 10f;
        }

        public void Update(GameTime gameTime)
        {
            Position.Y += fireballMoveSpeed;
            FireballAnimation.Position = Position;
            FireballAnimation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            FireballAnimation.Draw(spriteBatch);
        }
    }
}
