using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhantomProjects.States;
using PhantomProjects.PlayerBullets;
using Microsoft.Xna.Framework.Content;

namespace PhantomProjects
{
    class EnemyA
    {
        #region Declarations
        /////Enemy Canvas
        ///Animation
        public Animation enemyAnimation;
        Texture2D enemyRight, enemyLeft, currentAnim;
        float elapsed;
        float delay = 120f;
        int Frames = 0;


        public Rectangle rectangle, sourceRect; // Canvas for texture movement
        Vector2 origin; //Center of the enemy
        Vector2 velocity; //Velocity of the enemy movement
        float rotation = 0f; //Enemy rotation ( left and right )
        bool right; // default movement
        float distance; // Distance fixed ( the distance that the enemy will patrol )
        float oldDistance; // Compare old distance with the new distance
        float playerDistance, playerDistanceY; //Distance of Enemy from the player
        int patrolDistance = 384, patrolDistanceY = 200;// Patrol distance

        ///Details
        public Vector2 position; // Enemy position
        public bool Active;
        public int Health;
        public int Damage;
        #endregion

        //Get the dimentions of the enemy
        public int Width
        {
            get { return enemyAnimation.FrameWidth; }
        }
        public int Height
        {
            get { return enemyAnimation.FrameHeight; }
        }
        public Vector2 LocationEnemy
        {
            get { return position; }
        }

        public void Initialize(Animation animation, Vector2 newPosition, ContentManager content)
        {
            enemyLeft = content.Load<Texture2D>("EnemyA\\enemyALeft");
            enemyRight = content.Load<Texture2D>("EnemyA\\enemyARight");

            enemyAnimation = animation;
            position = newPosition;
            currentAnim = enemyRight;

            //Enemy Stats
            distance = 384f;
            Health = 100;
            Active = true;
            Damage = 10;

            oldDistance = distance;

        }

        public void Update(GameTime gameTime, Player player, Sounds SND)
        {
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, 100, 93);

            enemyAnimation.Position = position;
            enemyAnimation.Update(gameTime);

            // Animation Check
            AnimationCheck(gameTime);

            // Find player position
            playerDistance = player.Position.X - position.X;
            playerDistanceY = player.Position.Y - position.Y;

            //Patrol area calcs
            Patrol(gameTime);

            // if the player is inside the patrol distance & Active, otherwise disactivate
            if (player.Active == true)
                HuntPlayer(gameTime, SND);

            //Check health & dmg
            IsDead();

            // check if its fauling
            if (velocity.Y < 10)
                velocity.Y += 0.4f;

        }

        void Patrol(GameTime gameTime)
        {
            if (distance <= 0)
            {
                right = true;
                velocity.X = 1f;
            }
            else if (distance >= oldDistance)
            {
                right = false;
                velocity.X = -1f;
            }

            if (right) distance += 1; // update the distance
            else distance -= 1;
        }

        void HuntPlayer(GameTime gameTime, Sounds SND)
        {
            if ((playerDistance >= -patrolDistance && playerDistance <= patrolDistance) &&
                (playerDistanceY >= -patrolDistanceY && playerDistanceY <= patrolDistanceY))
            {
                if (playerDistance < -1f)
                {
                    BulletEManager.FireBulletE(gameTime, this, false, SND);

                    if (playerDistance > -200)
                    {
                        currentAnim = enemyLeft;
                        velocity.X = 0f;
                    }
                    else
                        velocity.X = -1f;
                }
                else if (playerDistance > 1f)
                {
                    BulletEManager.FireBulletE(gameTime, this, true, SND);
                    if (playerDistance < 200)
                    {
                        currentAnim = enemyRight;
                        velocity.X = 0f;
                    }
                    else
                        velocity.X = 1f;
                }
                else if (playerDistance == 0)
                {
                    velocity.X = 0f;
                }

            }
            rectangle = new Rectangle((int)position.X, (int)position.Y, 100, 93);
        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
            }

            if (rectangle.TouchLeftOf(newRectangle))
            {
                position.X = newRectangle.X - rectangle.Width - 2;
            }

            if (rectangle.TouchRightOf(newRectangle))
            {
                position.X = newRectangle.X + newRectangle.Width + 2;
            }

            if (rectangle.TouchBottomOf(newRectangle))
            {
                velocity.Y = 1f;
            }

            if (position.X < 0) position.X = 0;
            if (position.X > xOffset - rectangle.Width) position.X = xOffset - rectangle.Width;
            if (position.Y < 0) velocity.Y = 1f;
            if (position.Y > yOffset - rectangle.Height) position.Y = yOffset - rectangle.Height;
        }

        void AnimationCheck(GameTime gameTime)
        {
            if (velocity.X > 0f)
            {
                currentAnim = enemyRight;
                Animate(gameTime);
            }
            else if (velocity.X < 0f)
            {
                currentAnim = enemyLeft;
                Animate(gameTime);
            }
        }

        void Animate(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delay)
            {
                if (Frames >= 6)
                {
                    Frames = 0;
                }
                else
                {
                    Frames++;
                }
                elapsed = 0;
            }
            sourceRect = new Rectangle((Frames * 100), 0, 100, 93);
        }

        void IsDead()
        {
            if (Health <= 0)
            {
                Active = false;
            }            
        }

        public Rectangle RECTANGLE
        {
            get { return rectangle; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currentAnim, rectangle, sourceRect, Color.White);
        }

    }
}
