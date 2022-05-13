using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using PhantomProjects.GUI_;

namespace PhantomProjects.States
{
    public class EndGame : State
    {
        #region End Game - Declarations
        // Game Music.
        private Song menuMusic;

        //Static background
        Texture2D mainBackground;

        int menuTimer;
        #endregion

        public EndGame(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
        : base(game, graphicsDevice, content)
        {
            //Background Music
            menuMusic = content.Load<Song>("Sounds\\MENU");
            MediaPlayer.Play(menuMusic);
            MediaPlayer.IsRepeating = true;

            //Background
            mainBackground = content.Load<Texture2D>("Menu\\GameComplete");

            menuTimer = 60 * 5;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            // Main background
            spriteBatch.Draw(mainBackground, new Rectangle(0, 0, 1280, 700), Color.White);
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime) { }

        public override void Update(GameTime gameTime)
        {
            menuTimer--;
            ChangeScene(menuTimer);
        }

        void ChangeScene(int timer)
        {
            if (timer < 0)
            {
                _game.ChangeState(new CreditState(_game, _graphicsDevice, _content));
            }
        }
    }
}
