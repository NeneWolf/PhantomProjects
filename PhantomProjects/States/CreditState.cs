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
        Texture2D mainBackground, background, gameLogo, buttonTexture;

        //Button list
        private List<Component> _components;
        Button mainMenuButton;

        //Fonts
        SpriteFont buttonFont;
        #endregion

        public CreditState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            background = content.Load<Texture2D>("Backgrounds\\PauseBackground");
            mainBackground = content.Load<Texture2D>("Backgrounds\\smallBackground");
            gameLogo = content.Load<Texture2D>("Logos\\Phantom Projects-logos_white");

            buttonTexture = _content.Load<Texture2D>("Menu\\MainMenu");
            buttonFont = _content.Load<SpriteFont>("GUI\\MenuFont");

            mainMenuButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(570, 570),
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
            spriteBatch.Draw(background, new Rectangle(0, 0, 1280, 700), Color.White);
            spriteBatch.Draw(gameLogo, new Rectangle(0, 450, 300, 300), Color.White);

            spriteBatch.DrawString(buttonFont, "Credits", new Vector2(600, 30), Color.White);
            spriteBatch.DrawString(buttonFont, "Project Manager, Programmer & Level Design", new Vector2(100, 130), Color.White);
            spriteBatch.DrawString(buttonFont, "Lead Programmer/Tester & UI Design", new Vector2(100, 180), Color.White);
            spriteBatch.DrawString(buttonFont, "Software engineer, Art Designer & Sound Engineer", new Vector2(100, 230), Color.White);
            spriteBatch.DrawString(buttonFont, "Special Thanks To", new Vector2(540, 330), Color.White);

            spriteBatch.DrawString(buttonFont, "Jatinderbir Dole", new Vector2(800, 130), Color.White);
            spriteBatch.DrawString(buttonFont, "Ines Mateus Lobo", new Vector2(800, 180), Color.White);
            spriteBatch.DrawString(buttonFont, "Qin Huang", new Vector2(800, 230), Color.White);

            spriteBatch.DrawString(buttonFont, "Gazelle", new Vector2(600, 380), Color.White);
            spriteBatch.DrawString(buttonFont, "Markos", new Vector2(600, 430), Color.White);
            spriteBatch.DrawString(buttonFont, "Our Game Testers", new Vector2(540, 480), Color.White);

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
