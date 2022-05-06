﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace PhantomProjects.States
{
    public class MenuState : State
    {
        // Game Music.
        private Song menuMusic;

        //Static background
        Texture2D mainBackground, gameLogo, companyLogo;
        private List<Component> _components;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            menuMusic = content.Load<Song>("Sounds\\MENU");
            MediaPlayer.Play(menuMusic);

            mainBackground = content.Load<Texture2D>("menuBackground");
            gameLogo = content.Load<Texture2D>("Logos\\Phantom Projects-logos_white");
            companyLogo = content.Load<Texture2D>("Logos\\Future App-logos_white");

            var buttonTexture = _content.Load<Texture2D>("Menu\\button");
            var buttonFont = _content.Load<SpriteFont>("GUI\\MenuFont");

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(540, 445),
                Text = "New Game",
            };

            newGameButton.Click += NewGameButton_Click;

            var loadCreditButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(540, 500),
                Text = "Credits",
            };

            loadCreditButton.Click += LoadGameButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(540, 555),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Component>()
          {
            newGameButton,
            loadCreditButton,
            quitGameButton,
          };
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            // Main background
            spriteBatch.Draw(mainBackground, new Rectangle(0, 0, 1280, 700), Color.White);
            spriteBatch.Draw(gameLogo, new Rectangle(490, 10, 300, 300), Color.White);
            spriteBatch.Draw(companyLogo, new Rectangle(0, 450, 300, 300), Color.White);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
        

        private void LoadGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new CreditState(_game, _graphicsDevice, _content));
        }

        // Level 1
        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        public override void PostUpdate(GameTime gameTime) { }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}
