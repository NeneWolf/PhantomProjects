using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using PhantomProjects.GUI_;

namespace PhantomProjects.States
{
    public class MenuState : State
    {
        #region Menu State - Declarations
        // Game Music.
        private Song menuMusic;

        //Static background
        Texture2D mainBackground, gameLogo, companyLogo;
        private List<Component> _components;
        #endregion

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            menuMusic = content.Load<Song>("Sounds\\MENU");
            MediaPlayer.Play(menuMusic);
            MediaPlayer.IsRepeating = true;

            mainBackground = content.Load<Texture2D>("Backgrounds\\menuBackground");
            gameLogo = content.Load<Texture2D>("Logos\\Phantom Projects-logos_white");
            companyLogo = content.Load<Texture2D>("Logos\\Future App-logos_white");

            var playTexture = _content.Load<Texture2D>("Menu\\Play");
            var creditsTexture = _content.Load<Texture2D>("Menu\\Credits");
            var controls = _content.Load<Texture2D>("Menu\\Controls");
            var exit = _content.Load<Texture2D>("Menu\\Exit");
            var buttonFont = _content.Load<SpriteFont>("GUI\\MenuFont");

            var newGameButton = new Button(playTexture, buttonFont)
            {
                Position = new Vector2(570, 435),
                Text = "",
            };

            newGameButton.Click += NewGameButton_Click;

            var gameControlButton = new Button(controls, buttonFont)
            {
                Position = new Vector2(570, 485),
                Text = "",
            };

            gameControlButton.Click += LoadControlButton_Click;

            var loadCreditButton = new Button(creditsTexture, buttonFont)
            {
                Position = new Vector2(570, 535),
                Text = "",
            };

            loadCreditButton.Click += LoadCreditButton_Click;

            var quitGameButton = new Button(exit, buttonFont)
            {
                Position = new Vector2(570, 585),
                Text = "",
            };

            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Component>()
          {
            newGameButton,
            gameControlButton,
            loadCreditButton,
            quitGameButton,
          };
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            // Main background
            spriteBatch.Draw(mainBackground, new Rectangle(0, 0, 1280, 700), Color.White);
            spriteBatch.Draw(gameLogo, new Rectangle(475, -80, 350, 350), Color.White);
            spriteBatch.Draw(companyLogo, new Rectangle(0, 450, 300, 300), Color.White);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime) { }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        // Select Character
        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new SelectPlayer(_game, _graphicsDevice, _content));
        }

        // Controls Scene
        private void LoadControlButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameControls(_game, _graphicsDevice, _content));
        }

        // Credit Scene
        private void LoadCreditButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new CreditState(_game, _graphicsDevice, _content));
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}
