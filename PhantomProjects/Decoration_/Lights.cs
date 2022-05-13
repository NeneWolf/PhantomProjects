using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PhantomProjects.Decoration_
{
    class Lights
    {
        #region Declarations

        public Animation lightAnimation;
        Texture2D lightPotionTexture;
        public Rectangle sourceRect;
        Vector2 position;

        //State of the health potion
        public bool Active;

        //Variables for the animation method
        float elapsed;
        float delay = 250f;
        int Frames = 0;

        //Get the width of the texture
        public int Width
        {
            get { return lightAnimation.FrameWidth; }
        }

        //Get the height of the texture
        public int Height
        {
            get { return lightAnimation.FrameHeight; }
        }
        #endregion

        #region Constructor
        //Health Potion Constructor
        public void Initialize(ContentManager Content, Vector2 pos)
        {
            //Load texture
            lightPotionTexture = Content.Load<Texture2D>("Decorations\\lights");

            //Initialize variables
            Active = true;
            position = pos;

            //Create Animation
            lightAnimation = new Animation();
        }
        #endregion

        #region Methods
        //Update Method
        public void Update(GameTime gameTime)
        {
            //Check if the health potion is available
            if (Active == true)
            {
                lightAnimation.Update(gameTime);
                Animate(gameTime);

                //Create a new rectangle around the health potion, this is to determine if the player is in range to pickup the item
                Rectangle potionRectangle = new Rectangle((int)position.X, (int)position.Y, Width, Height);

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

            sourceRect = new Rectangle((Frames * 150), 0, 150, 100);
        }

        //Draw Method
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active == true)
            {
                spriteBatch.Draw(lightPotionTexture, position, sourceRect, Color.White);
            }
        }
        #endregion
    }
}
