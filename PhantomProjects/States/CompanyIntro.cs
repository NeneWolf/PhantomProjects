using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace PhantomProjects.States
{
    class CompanyIntro : State
    {
        #region Company Intro - Declarations
        Texture2D background, logoTexture;
        Rectangle bgRectangle, logoRectangle;
        int menuTimer, fadeTimer, menuDuration, timeBeforeFade;
        Color colour;
        #endregion

        public CompanyIntro(Game1 game, GraphicsDevice graphics, ContentManager content)
            : base(game, graphics, content)
        {
            //Load Textures
            background = content.Load<Texture2D>("SelectCharacterBackground");
            logoTexture = content.Load<Texture2D>("Logos\\Future App-logos_white");

            //Create Rectangles
            bgRectangle = new Rectangle(0, 0, 1280, 640);
            logoRectangle = new Rectangle(225, -100, 800, 800);

            //Create colour variable, this will be used to create a fade effect on the logo
            colour = new Color(255, 255, 255, 255);

            //How long this scene will run for
            menuDuration = 7;

            //Time before fade effect starts. This is so the logo is shown properly for a few seconds
            timeBeforeFade = 2;

            //Initialize Timers 
            fadeTimer = 60 * timeBeforeFade;
            menuTimer = 60 * menuDuration + timeBeforeFade;
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        //Update Method
        public override void Update(GameTime gameTime)
        {
            //Decrement Timers
            menuTimer--;
            fadeTimer--;

            //When fadeTimer is less than 0, then start the fade effect for the logo
            if (fadeTimer < 0)
            {
                if (colour.R > 0)
                    colour.R--;
                if (colour.G > 0)
                    colour.G--;
                if (colour.B > 0)
                    colour.B--;
                if (colour.A > 0)
                    colour.A--;
            }

            //Method to change to the next scene
            ChangeScene(menuTimer);
        }

        //Draw Method
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, bgRectangle, Color.White);
            spriteBatch.Draw(logoTexture, logoRectangle, colour);
            spriteBatch.End();
        }

        //Method to check if the menuTimer has reached 0 to move to the next scene
        void ChangeScene(int timer)
        {
            if (timer == 0)
            {
                _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
            }
        }
    }
}