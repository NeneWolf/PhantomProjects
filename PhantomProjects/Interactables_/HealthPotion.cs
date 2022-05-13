using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhantomProjects.Player_;

namespace PhantomProjects.Interactables_
{
    class HealthPotion
    {
        #region Declarations
        public Animation healthPotionAnimation;
        Texture2D healthPotionTexture;
        public Rectangle sourceRect;
        Vector2 position;

        //State of the health potion
        public bool Active;

        //Variables for the animation method
        float elapsed;
        float delay = 120f;
        int Frames = 0;

        //Get the width of the texture
        public int Width
        {
            get { return healthPotionAnimation.FrameWidth; }
        }

        //Get the height of the texture
        public int Height
        {
            get { return healthPotionAnimation.FrameHeight; }
        }
        #endregion

        #region Constructor
        //Health Potion Constructor
        public void Initialize(ContentManager Content, Vector2 pos)
        {
            //Load texture
            healthPotionTexture = Content.Load<Texture2D>("GUI\\HealthPotion");

            //Initialize variables
            Active = true;
            position = pos;

            //Create Animation
            healthPotionAnimation = new Animation();
        }
        #endregion

        #region Methods
        //Update Method
        public void Update(GameTime gameTime, Player p)
        {
            //Check if the health potion is available
            if (Active == true)
            {
                healthPotionAnimation.Update(gameTime);
                Animate(gameTime);

                //Create a new rectangle around the health potion, this is to determine if the player is in range to pickup the item
                Rectangle potionRectangle = new Rectangle(
                                          (int)position.X,
                                          (int)position.Y,
                                          Width + 50,
                                          Height + 50);

                //If the player is in range of the health potion's rectangle, allow the player to pickup the item using the interact button
                if (potionRectangle.Intersects(p.rectangle) && (Keyboard.GetState().IsKeyDown(Keys.F) ||
                    GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed))
                {
                    //Remove the health potion from the level and restore the player's health by an amount
                    Active = false;
                    if (p.Health < 100)
                    {
                        p.Health += 10;
                        p.BarHealth += 15;

                        if (p.Health > 100)
                        {
                            p.Health = 100;
                            p.BarHealth = 150;
                        }
                    }
                }
            }
        }

        //Animate Method
        void Animate(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delay)
            {
                if (Frames >= 5)
                {
                    Frames = 0;
                }
                else
                {
                    Frames++;
                }
                elapsed = 0;
            }

            sourceRect = new Rectangle((Frames * 70), 0, 70, 70);
        }

        //Draw Method
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active == true)
            {
                spriteBatch.Draw(healthPotionTexture, position, sourceRect, Color.White);
            }
        }
        #endregion
    }
}
