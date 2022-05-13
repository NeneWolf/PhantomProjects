using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhantomProjects.Player_;
using PhantomProjects.GUI_;

namespace PhantomProjects.Interactables_
{
    class Keycard
    {
        #region Declarations
        public Animation keycardAnimation;
        Texture2D keycardTexture;
        public Rectangle sourceRect;
        Vector2 position;

        //State of the key card
        public bool Active;

        //Variables for the animation method
        float elapsed;
        float delay = 60f;
        int Frames = 0;

        //Get the width of the texture
        public int Width
        {
            get { return keycardAnimation.FrameWidth; }
        }

        //Get the height of the texture
        public int Height
        {
            get { return keycardAnimation.FrameHeight; }
        }
        #endregion

        #region Constructor
        //Key Card Constructor
        public void Initialize(ContentManager Content, Vector2 pos)
        {
            //Load texture
            keycardTexture = Content.Load<Texture2D>("GUI\\KeyCard");

            //Initialize varaibles
            Active = true;
            position = pos;

            //Create Animation
            keycardAnimation = new Animation();
        }
        #endregion

        #region Methods
        //Update Method
        public void Update(GameTime gameTime, Player p, GUI guiInfo)
        {
            //Check if the key card is available
            if (Active == true)
            {
                keycardAnimation.Update(gameTime);
                Animate(gameTime);

                //Create a new rectangle around the key card, this is to determine if the player is in range to pickup the item
                Rectangle cardRectangle = new Rectangle(
                                          (int)position.X,
                                          (int)position.Y,
                                          Width + 50,
                                          Height + 50);

                //If the player is in range of the key card's rectangle, allow the player to pickup the item using the interact button
                if (cardRectangle.Intersects(p.rectangle) && (Keyboard.GetState().IsKeyDown(Keys.F) ||
                    GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed))
                {
                    //Remove the key card from the level and add 1 to the key card GUI
                    Active = false;
                    guiInfo.KEYS += 1;
                }
            }
        }

        //Animate Method
        void Animate(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delay)
            {
                if (Frames >= 17)
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
                spriteBatch.Draw(keycardTexture, position, sourceRect, Color.White);
            }
        }
        #endregion
    }
}
