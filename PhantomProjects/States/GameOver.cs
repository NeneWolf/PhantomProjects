using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using PhantomProjects.GUI_;

namespace PhantomProjects.States
{
    public class GameOver : State
    {
        #region Game Over - Declarations
        // Game Music.
        private Song menuMusic;
        private List<Component> _components;

        //Static background
        Texture2D mainBackground;
        #endregion

        public GameOver(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
        : base(game, graphicsDevice, content)
        {
            //Background Music
            menuMusic = content.Load<Song>("Sounds\\MENU");
            MediaPlayer.Play(menuMusic);

            //Background
            mainBackground = content.Load<Texture2D>("Menu\\GameOver");

            //Buttons
            var PlayTexture = _content.Load<Texture2D>("Menu\\Play");
            var mainMenuTexture = _content.Load<Texture2D>("Menu\\MainMenu");
            var buttonFont = _content.Load<SpriteFont>("GUI\\MenuFont");

            var tryAgainButton = new Button(PlayTexture, buttonFont)
            {
                Position = new Vector2(780, 445),
                Text = "",
            };

            tryAgainButton.Click += TryAgainButton_Click;

            var mainMenuButton = new Button(mainMenuTexture, buttonFont)
            {
                Position = new Vector2(780, 500),
                Text = "",
            };

            mainMenuButton.Click += MainMenuButton_Click;

            _components = new List<Component>()
          {
            tryAgainButton,
            mainMenuButton
          };
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            // Main background
            spriteBatch.Draw(mainBackground, new Rectangle(0, 0, 1280, 700), Color.White);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void TryAgainButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new TutorialState(_game, _graphicsDevice, _content));
        }

        private void MainMenuButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        public override void PostUpdate(GameTime gameTime) { }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}
