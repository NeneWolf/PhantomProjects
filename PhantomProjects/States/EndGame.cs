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
        private List<Component> _components;

        //Static background
        Texture2D mainBackground;
        #endregion

        public EndGame(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
        : base(game, graphicsDevice, content)
        {
            //Background Music
            menuMusic = content.Load<Song>("Sounds\\MENU");
            MediaPlayer.Play(menuMusic);

            //Background
            mainBackground = content.Load<Texture2D>("Menu\\GameComplete"); 

            //Buttons
            var buttonTexture = _content.Load<Texture2D>("Menu\\MainMenu");
            var buttonFont = _content.Load<SpriteFont>("GUI\\MenuFont");


            var mainMenuButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(540, 585),
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
