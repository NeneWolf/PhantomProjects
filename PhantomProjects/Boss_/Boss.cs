using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using PhantomProjects.Player_;
using PhantomProjects.Map_;
using PhantomProjects.GUI_;

namespace PhantomProjects.Boss_
{
    class Boss
    {
        #region Declarations
        /////Enemy Canvas
        ///Animation
        public Animation bossAnimation;
        Texture2D bossRight, bossLeft, currentAnim;
        float elapsed;
        float delay = 120f;
        int Frames = 0;


        public Rectangle rectangle, sourceRect; // Canvas for texture movement
        Vector2 velocity; //Velocity of the enemy movement
        bool right; // default movement
        float distance; // Distance fixed ( the distance that the enemy will patrol )
        float oldDistance; // Compare old distance with the new distance
        float playerDistance, playerDistanceY; //Distance of Enemy from the player
        int patrolDistance = 850, patrolDistanceY = 200;// Patrol distance

        ///Details
        public Vector2 position; // Enemy position
        public bool Active, canReceiveReward;
        public int Health;
        public int Damage;
        #endregion

        //Get the dimentions of the enemy
        public int Width
        {
            get { return bossAnimation.FrameWidth; }
        }
        public int Height
        {
            get { return bossAnimation.FrameHeight; }
        }
        public Vector2 LocateBoss
        {
            get { return position; }
        }

        public void Initialize(Vector2 newPosition, ContentManager content)
        {
            bossLeft = content.Load<Texture2D>("Boss\\BossLeftWalk");
            bossRight = content.Load<Texture2D>("Boss\\BossRightWalk");

            position = newPosition;

            //Enemy Stats
            distance = 384f;
            Health = 300;
            Active = true;
            canReceiveReward = false;

            oldDistance = distance;

            bossAnimation = new Animation();
            currentAnim = bossRight;

        }

        public void Update(GameTime gameTime, Player player, GUI guiInfo, Sounds SND)
        {
            //Check health & dmg
            if (Active == true)
            {
                IsDead(guiInfo);

                position += velocity;
                rectangle = new Rectangle((int)position.X, (int)position.Y, 200, 180);

                bossAnimation.Position = position;
                bossAnimation.Update(gameTime);

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

                // check if its fauling
                if (velocity.Y < 10)
                    velocity.Y += 0.4f;
            }
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
                    FireballManager.FireBulletE(gameTime, this, SND);

                    if (playerDistance > -200)
                    {
                        currentAnim = bossLeft;
                        velocity.X = 0f;
                    }
                    else
                        velocity.X = -1f;
                }
                else if (playerDistance > 1f)
                {
                    FireballManager.FireBulletE(gameTime, this, SND);
                    if (playerDistance < 200)
                    {
                        currentAnim = bossRight;
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
            rectangle = new Rectangle((int)position.X, (int)position.Y, 200, 180);
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
                currentAnim = bossRight;
                Animate(gameTime);
            }
            else if (velocity.X < 0f)
            {
                currentAnim = bossLeft;
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
            sourceRect = new Rectangle((Frames * 200), 0, 200, 180);
        }

        void IsDead(GUI guiInfo)
        {
            if (Health <= 0)
            {
                Active = false;
                guiInfo.UPGRADEPOINTS += 1000;
            }
        }

        public void CleanBoss()
        {
            Active = false;
        }

        public Rectangle RECTANGLE
        {
            get { return rectangle; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(Active == true)
            {
                spriteBatch.Draw(currentAnim, rectangle, sourceRect, Color.White);
            }
        }
    }
}
