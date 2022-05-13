using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using PhantomProjects.Player_;
using PhantomProjects.Map_;


namespace PhantomProjects.Enemy_
{
    class EnemyA
    {
        #region Declarations
        public Animation enemyAnimation;//Enemy Animation
        Texture2D enemyRight, enemyLeft, currentAnim; //Enemy Textures for the animation

        //Animation parameters
        float elapsed;
        float delay = 120f;
        int Frames = 0;


        public Rectangle rectangle, sourceRect; // Canvas for texture movement
        Vector2 velocity; //Velocity of the enemy movement
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

        // Return dimentions of the enemy based on the enemy animation frame width/height
        public int Width
        {
            get { return enemyAnimation.FrameWidth; }
        }

        public int Height
        {
            get { return enemyAnimation.FrameHeight; }
        }

        //Get the position of the enemy
        public Vector2 LocationEnemy
        {
            get { return position; }
        }
        #endregion

        #region Constructor
        public void Initialize(Animation animation, Vector2 newPosition, ContentManager content)
        {
            //Initialise enemy content
            enemyLeft = content.Load<Texture2D>("EnemyA\\enemyALeft");
            enemyRight = content.Load<Texture2D>("EnemyA\\enemyARight");

            //Set position
            position = newPosition;

            //Set animation texture & initialise Animation
            enemyAnimation = animation;
            currentAnim = enemyRight;

            //Set enemy Stats
            distance = 384f;
            Health = 100;
            Active = true;
            Damage = 10;
            oldDistance = distance;

        }
        #endregion

        #region Methods
        public void Update(GameTime gameTime, Player player, Sounds SND)
        {
            //If active, set the velocity of the enemy & rectangle
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, 100, 93);

            //Set the animation position to the current enemy position & update
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

            //Check Enemy health
            IsDead();

            // check if its fauling
            if (velocity.Y < 10)
                velocity.Y += 0.4f;

        }

        void Patrol(GameTime gameTime)
        {
            // Calculates the current position of the enemy & add or subtract velocity
            // Making the enemy walk left and right until it reaches a certain distance

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
            // if the player at any point gets inside this distance, the enemy will start "Hunting" the player

            if ((playerDistance >= -patrolDistance && playerDistance <= patrolDistance) &&
                (playerDistanceY >= -patrolDistanceY && playerDistanceY <= patrolDistanceY))
            {
                if (playerDistance < -1f)
                {
                    //Left Behaviour
                    // if the player is in a certain distance of the enemy, it will start attacking
                    BulletEManager.FireBulletE(gameTime, this, false, SND);

                    //if the player moved a certain distance of the boss while in the attack mode
                    //Enemy will move towards the player until that distance is again meet.
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
                    //Right Behaviour
                    // if the player is in a certain distance of the enemy, it will start attacking
                    BulletEManager.FireBulletE(gameTime, this, true, SND);

                    //if the player moved a certain distance of the boss while in the attack mode
                    //Enemy will move towards the player until that distance is again meet.
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
            // Checks if the Enemy is colliding with any tile
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
            // Changes animation depending of the velocity of the enemy
            // If x is position set it to the Right animation, negative will set to Left animation
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
            sourceRect = new Rectangle((Frames * 100), 0, 100, 93);
        }

        void IsDead()
        {
            // if at any point the enemy health is below or 0, it will be considered dead
            if (Health <= 0)
            {
                Active = false;
            }            
        }

        //Returns the enemy rectangle
        public Rectangle RECTANGLE
        {
            get { return rectangle; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currentAnim, rectangle, sourceRect, Color.White);
        }
        #endregion
    }
}
 