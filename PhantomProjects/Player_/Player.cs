using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhantomProjects.Map_;

namespace PhantomProjects.Player_
{
    class Player
    {
        #region Declarations

        public Animation playerAnimation;
        public Texture2D playerRight, playerLeft, idleRight, idleLeft, currentAnim;
        float elapsed;
        float delay = 120f, delayIdle = 800f;
        int Frames = 0;

        //Variable to hold player position
        private Vector2 position;

        //Variable to hold how fast the player will move in a given direction
        private Vector2 velocity;

        //Rectangle variables
        public Rectangle rectangle, sourceRect;

        //State of the player
        public bool Active;

        //Total player health
        public int Health, BarHealth;

        //Variable to check if the player has already jumped
        private bool hasJumped = false;

        //Variable to help change shooting direction
        bool right;

        //Gamepad states used to determine button presses
        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        //Get the width of the player
        public int Width
        {
            get { return playerAnimation.FrameWidth; }
        }

        //Get the height of the player 
        public int Height
        {
            get { return playerAnimation.FrameHeight; }
        }

        //Get the position of the player
        public Vector2 Position
        {
            get { return position; }
        }
        #endregion

        #region Constructor
        public void Initialize(ContentManager content, Vector2 newPosition, int playerSelected)
        {
            //Method to determine which player sprite to use
            SelectCharacterContent(content, playerSelected);

            //Set the player to be active
            Active = true;

            //Set the player health
            Health = 100;

            //Set the size of the health bar which will be reduced each time the player takes damage
            BarHealth = 150;

            //Initialize the player's position
            position = newPosition;

            //Initialize the player's animation
            playerAnimation = new Animation();

            //Initialize the Animation to be used on startup
            currentAnim = idleRight;
        }
        #endregion

        #region Methods
        //Update Method
        public void Update(GameTime gameTime)
        {
            //Check the state of the player
            IsDead();

            //Take input only if the player is active/alive
            if (Active == true)
            {
                // Gamepad controls
                previousGamePadState = currentGamePadState;
                currentGamePadState = GamePad.GetState(PlayerIndex.One);

                //Update the player's position using velocity
                position += velocity;
                rectangle = new Rectangle((int)position.X, (int)position.Y, 50, 100);
                playerAnimation.Position = position;
                playerAnimation.Update(gameTime);

                Input(gameTime);

                //Gravity
                if (velocity.Y < 10)
                    velocity.Y += 0.35f;
            }
        }

        //Player Input
        private void Input(GameTime gameTime)
        {
            //Player movement
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D) ||
                currentGamePadState.DPad.Right == ButtonState.Pressed || currentGamePadState.ThumbSticks.Left.X == 1)
            {
                velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;

                //Adjust the animation
                currentAnim = playerRight;
                Animate(gameTime);
                right = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A) ||
                currentGamePadState.DPad.Left == ButtonState.Pressed || currentGamePadState.ThumbSticks.Left.X == -1)
            {
                velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
                currentAnim = playerLeft;
                Animate(gameTime);
                right = false;
            }
            //Set velocity to 0 so player doesnt move if theres no input
            else
            {
                velocity.X = 0f;
                if (right == true)
                {
                    currentAnim = idleRight;
                    AnimateIdle(gameTime);
                }
                else
                {
                    currentAnim = idleLeft;
                    AnimateIdle(gameTime);
                }
            }

            //Jumping
            if ((Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W) ||
                currentGamePadState.Buttons.A == ButtonState.Pressed) && hasJumped == false)
            {
                position.Y -= 5f;
                velocity.Y = -11f;
                hasJumped = true;
            }
        }

        //Collision Detection with tiles
        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            //Player standing on a tile
            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
                hasJumped = false;
            }

            //Player rectangle hitting the right of side of a tile
            if (rectangle.TouchLeftOf(newRectangle))
            {
                position.X = newRectangle.X - rectangle.Width - 2;
            }

            //Player rectangle hitting the left side of a tile
            if (rectangle.TouchRightOf(newRectangle))
            {
                position.X = newRectangle.X + newRectangle.Width + 2;
            }

            //Player rectangle hitting the top of a tile
            if (rectangle.TouchBottomOf(newRectangle))
            {
                velocity.Y = 1f;
            }

            if (position.X < 0) position.X = 0;
            if (position.X > xOffset - rectangle.Width) position.X = xOffset - rectangle.Width;
            if (position.Y < 0) velocity.Y = 1f;
            if (position.Y > yOffset - rectangle.Height) position.Y = yOffset - rectangle.Height;
        }

        //Methhod to determine position on platforms
        public void ChangePositionOnPlatforms(float positionX, float positionY, bool Jump)
        {
            position.Y = positionY;
            position.X = positionX;
            hasJumped = Jump;
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

            sourceRect = new Rectangle((Frames * 50), 0, 50, 100);
        }

        void AnimateIdle(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delayIdle)
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

            sourceRect = new Rectangle((Frames * 50), 0, 50, 100);
        }

        //Method to remove player sprite if their health reaches 0
        private void IsDead()
        {
            if (Health <= 0)
            {
                Active = false;
            }
        }

        //Method to load Player sprite based on the selection made in the character select menu
        public void SelectCharacterContent(ContentManager content, int playerSelected)
        {
            if (playerSelected == 0)
            {
                playerRight = content.Load<Texture2D>("Player\\FemalePlayerRightWalk");
                playerLeft = content.Load<Texture2D>("Player\\FemalePlayerLeftWalk");
                idleRight = content.Load<Texture2D>("Player\\FemaleRightIdle");
                idleLeft = content.Load<Texture2D>("Player\\FemaleLeftIdle");
            }
            else
            {
                playerRight = content.Load<Texture2D>("Player\\MalePlayerRightWalk");
                playerLeft = content.Load<Texture2D>("Player\\MalePlayerLeftWalk");
                idleRight = content.Load<Texture2D>("Player\\MaleRightIdle");
                idleLeft = content.Load<Texture2D>("Player\\MaleLeftIdle");
            }
        }

        //Get the player's rectangle
        public Rectangle RECTANGLE
        {
            get { return rectangle; }
        }

        //Draw Method
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active == true)
            {
                spriteBatch.Draw(currentAnim, rectangle, sourceRect, Color.White);
            }
        }
        #endregion
    }
}