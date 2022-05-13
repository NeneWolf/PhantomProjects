using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using PhantomProjects.Player_;
using PhantomProjects.Interactables_;
using PhantomProjects.Enemy_;
using PhantomProjects.Explosion_;
using PhantomProjects.Map_;
using PhantomProjects.Menus_;

namespace PhantomProjects.States
{
    class CompanyIntro : State
    {
        Texture2D background, logoTexture;
        Rectangle bgRectangle, logoRectangle;
        int menuTimer;

        int logoWidth
        {
            get { return logoTexture.Width; }
        }

        int logoHeight
        {
            get { return logoTexture.Height; }
        }

        public CompanyIntro(Game1 game, GraphicsDevice graphics, ContentManager content)
            : base(game, graphics, content)
        {
            background = content.Load<Texture2D>("SelectCharacterBackground");
            logoTexture = content.Load<Texture2D>("Logos\\Future App-logos_white");

            bgRectangle = new Rectangle(0, 0, 1280, 640);
            logoRectangle = new Rectangle(225, -100, 800, 800);

            menuTimer = 60 * 3;
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            menuTimer--;
            ChangeScene(menuTimer);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, bgRectangle, Color.White);
            spriteBatch.Draw(logoTexture, logoRectangle, Color.White);
            spriteBatch.End();
        }

        void ChangeScene(int timer)
        {
            if (timer == 0)
            {
                _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
            }
        }
    }
}