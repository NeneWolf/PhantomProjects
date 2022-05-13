using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PhantomProjects.GUI_;

namespace PhantomProjects.States
{
    public class CreditState : State
    {
        #region Credit State - Declarations
        //Static background
        Texture2D mainBackground, gameLogo;
        private List<Component> _components;
        #endregion

        public CreditState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            mainBackground = content.Load<Texture2D>("Backgrounds\\background");
            gameLogo = content.Load<Texture2D>("Logos\\Phantom Projects-logos_white");

            var buttonTexture = _content.Load<Texture2D>("Menu\\MainMenu");
            var buttonFont = _content.Load<SpriteFont>("GUI\\MenuFont");

            var mainMenuButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(540, 570),
                Text = "",
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
            spriteBatch.Draw(gameLogo, new Rectangle(0, 400, 300, 300), Color.White);

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
