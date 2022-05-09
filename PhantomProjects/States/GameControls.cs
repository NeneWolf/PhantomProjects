using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace PhantomProjects.States
{
    public class GameControls : State
    {
        //Static background
        Texture2D mainBackground, gameLogo;
        private List<Component> _components;

        public GameControls(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            mainBackground = content.Load<Texture2D>("background");
            gameLogo = content.Load<Texture2D>("Logos\\Phantom Projects-logos_white");

            var buttonTexture = _content.Load<Texture2D>("Menu\\button");
            var buttonFont = _content.Load<SpriteFont>("GUI\\MenuFont");

            var mainMenuButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(540, 585),
                Text = "Main Menu",
            };

            mainMenuButton.Click += MainMenuButton_Click;

            _components = new List<Component>()
          {
            mainMenuButton,
          };

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            // Main background
            spriteBatch.Draw(mainBackground, new Rectangle(0, 0, 1280, 700), Color.White);
            spriteBatch.Draw(gameLogo, new Rectangle(0, 450, 300, 300), Color.White);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
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
