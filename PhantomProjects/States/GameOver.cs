using System;
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
    public class GameOver : State
    {
        // Game Music.
        private Song menuMusic;
        private List<Component> _components;

        //Static background
        Texture2D mainBackground, gameOver, companyLogo;

        public GameOver(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
        : base(game, graphicsDevice, content)
        {
            //Background Music
            menuMusic = content.Load<Song>("Sounds\\MENU");
            MediaPlayer.Play(menuMusic);

            //Background
            mainBackground = content.Load<Texture2D>("menuBackground"); // change the background
            gameOver = content.Load<Texture2D>("Menu\\GameOver");
            companyLogo = content.Load<Texture2D>("Logos\\Future App-logos_white");

            //Buttons
            var buttonTexture = _content.Load<Texture2D>("Menu\\button");
            var buttonFont = _content.Load<SpriteFont>("GUI\\MenuFont");

            var tryAgainButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(540, 445),
                Text = "Try Again",
            };

            tryAgainButton.Click += TryAgainButton_Click;

            var mainMenuButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(540, 500),
                Text = "Main Menu",
            };

            mainMenuButton.Click += MainMenuButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(540, 555),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Component>()
          {
            tryAgainButton,
            mainMenuButton,
            quitGameButton,
          };
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            // Main background
            spriteBatch.Draw(mainBackground, new Rectangle(0, 0, 1280, 700), Color.White);
            spriteBatch.Draw(gameOver, new Rectangle(350, 10, 600, 242), Color.White);
            spriteBatch.Draw(companyLogo, new Rectangle(0, 450, 300, 300), Color.White);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void TryAgainButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
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

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}
