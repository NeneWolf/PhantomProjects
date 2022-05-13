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

        public Animation bossAnimation; //Boss Animation
        Texture2D bossRight, bossLeft, currentAnim; // Boss sprites for the animations

        //Animation parameters
        float elapsed;
        float delay = 120f;
        int Frames = 0;

        public Rectangle rectangle, sourceRect; // Canvas for texture movement
        Vector2 velocity; //Velocity of the boss movement
        bool right; // default movement
        float distance; // Distance fixed ( the distance that the boss will patrol )
        float oldDistance; // Compare old distance with the new distance
        float playerDistance, playerDistanceY; //Distance of Boss from the player
        int patrolDistance = 850, patrolDistanceY = 200;// Patrol distance

        ///Details
        public Vector2 position; // Enemy position
        public bool Active, canReceiveReward; // Check if the boss is active & if the player can receive the reward for defeating it
        public int Health;
        public int Damage;
        #endregion

        // Return dimentions of the boss based on the boss animation frame width/height
        public int Width
        {
            get { return bossAnimation.FrameWidth; }
        }
        public int Height
        {
            get { return bossAnimation.FrameHeight; }
        }

        //Get the position of the boss
        public Vector2 LocateBoss
        {
            get { return position; }
        }

        public void Initialize(Vector2 newPosition, ContentManager content)
        {
            //Initialise boss content
            bossLeft = content.Load<Texture2D>("Boss\\BossLeftWalk");
            bossRight = content.Load<Texture2D>("Boss\\BossRightWalk");

            //Set position
            position = newPosition;

            //Set boss Stats
            distance = 384f;
            Health = 300;
            Active = true;         
            canReceiveReward = false;

            //Set animation texture & initialise Animation
            oldDistance = distance;
            bossAnimation = new Animation();
            currentAnim = bossRight;

        }

        public void Update(GameTime gameTime, Player player, GUI guiInfo, Sounds SND)
        {
            //Check if the boss is active
            IsDead(guiInfo);

            if (Active == true)
            {
                //If active, set the velocity of the boss & rectangle
                position += velocity;
                rectangle = new Rectangle((int)position.X, (int)position.Y, 200, 180);

                //Set the animation position to the current boss position & update
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
            // Calculates the current position of the boss & add or subtract velocity
            // Making the boss walk left and right until it reaches a certain distance
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

            // update the distance
            if (right) distance += 1;
            else distance -= 1;
        }

        void HuntPlayer(GameTime gameTime, Sounds SND)
        {
            // Calculates if the player enters the patrol distance ( X and Y )
            // if the player at any point gets inside this distance, the boss will start "Hunting" the player
            if ((playerDistance >= -patrolDistance && playerDistance <= patrolDistance) &&
                (playerDistanceY >= -patrolDistanceY && playerDistanceY <= patrolDistanceY))
            {
                if (playerDistance < -1f)
                {
                    // if the player is in a certain distance of the boss, it will start attacking
                    FireballManager.FireBulletE(gameTime, this, SND);

                    //if the player moved a certain distance of the boss while in the attack mode
                    //Boss will move towards the player until that distance is again meet.
                    if (playerDistance > -200)
                    {
                        // if the previous action was moving to the left, the current Animation will be set to left;
                        currentAnim = bossLeft;
                        velocity.X = 0f;
                    }
                    else
                        velocity.X = -1f;
                }
                else if (playerDistance > 1f)
                {
                    // if the player is in a certain distance of the boss, it will start attacking
                    FireballManager.FireBulletE(gameTime, this, SND);

                    //if the player moved a certain distance of the boss while in the attack mode
                    //Boss will move towards the player until that distance is again meet.
                    if (playerDistance < 200)
                    {
                        // if the previous action was moving to the left, the current Animation will be set to right;
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
        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            // Checks if the boss is colliding with any tile
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
            // Changes animation depending of the velocity of the Boss
            // If x is position set it to the Right animation, negative will set to Left animation
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

        //Animation Methods
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
            // if at any point the boss health is below or 0, it will be considered dead
            // player will get the points rewards
            if (Health <= 0)
            {
                Active = false;
                guiInfo.UPGRADEPOINTS += 1000;
            }
        }

        public void CleanBoss()
        {
            // Restarting a lvl - Its important to clean all elements, including bosses
            Active = false;
        }

        //Returns the boss rectangle
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
