﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using PhantomProjects.States;

namespace PhantomProjects.Menus_
{
    class PauseMenu
    {
        Game1 _game;
        GraphicsDevice _graphicsDevice;
        ContentManager _content;

        //Static background
        Texture2D mainBackground, pauseLogo;
        Texture2D buttonTexture;
        SpriteFont buttonFont;
        private List<Component> _components;

        bool pause = false;

        public void Initialize(GraphicsDevice graphicsDevice, ContentManager content, Game1 Game)
        {
            _game = Game;
            _graphicsDevice = graphicsDevice;
            _content = content;

            mainBackground = content.Load<Texture2D>("PauseBackground");
            pauseLogo = content.Load<Texture2D>("Menu\\GamePaused"); // To be changed
            buttonTexture = content.Load<Texture2D>("Menu\\button");
            buttonFont = content.Load<SpriteFont>("GUI\\MenuFont");

            var continuegameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(540, 335),
                Text = "Continue",
            };
            continuegameButton.Click += UnPauseGame_Click;

            var MainMenuButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(540, 385),
                Text = "Menu",
            };
            MainMenuButton.Click += MainMenuGame_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(540, 435),
                Text = "Quit",
            };
            quitGameButton.Click += QuitGame_Click;

            _components = new List<Component>()
                {
                continuegameButton,
                MainMenuButton,
                quitGameButton,
                };
        }

        public void Update(GameTime gameTime)
        {
            if (pause == true)
            {
                foreach (var component in _components)
                    component.Update(gameTime);
            }           
        }

        public bool IsPaused() { return pause; }

        public bool setPauseMenu(bool pauseMenu) => pause = pauseMenu;

        private void UnPauseGame_Click(object sender, EventArgs e)
        {
            setPauseMenu(false);
        }
        private void MainMenuGame_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }
        private void QuitGame_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (pause == true)
            {
                spriteBatch.Draw(mainBackground, new Rectangle(0, 0, 1280, 650), Color.White);
                spriteBatch.Draw(pauseLogo, new Rectangle(345, -100, 600, 600), Color.White);

                foreach (var component in _components)
                    component.Draw(gameTime, spriteBatch);
            }
        }
    }
}
