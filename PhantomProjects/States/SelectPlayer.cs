using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PhantomProjects.GUI_;

namespace PhantomProjects.States
{
    public class SelectPlayer : State
    {
        #region Select Player- Declarations

        //Static background
        Texture2D mainBackground, selectCharacterTexture, femaleCharacter, femaleCharacterSelected, maleCharacter, maleCharacterSelected, continueP;
        Button femalePlayerButton, malePlayerButton, newGameButton;
        bool canContinue;
        private List<Component> _components;

        #endregion

        public SelectPlayer(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            canContinue = false;
            selectCharacterTexture = content.Load<Texture2D>("Menu\\SelectPlayer");
            mainBackground = content.Load<Texture2D>("Backgrounds\\SelectCharacterBackground");

            femaleCharacterSelected = content.Load<Texture2D>("Player\\FemalePlayerSelected");
            maleCharacterSelected = content.Load<Texture2D>("Player\\MalePlayerSelected");

            femaleCharacter = content.Load<Texture2D>("Player\\FemalePlayer");
            maleCharacter = content.Load<Texture2D>("Player\\MalePlayer");


            continueP = content.Load<Texture2D>("Menu\\Continue");

            var buttonFont = _content.Load<SpriteFont>("GUI\\MenuFont");


            femalePlayerButton = new Button(femaleCharacter, buttonFont)
            {
                Position = new Vector2(200, 150),
                Text = "",
            };

            femalePlayerButton.Click += FemalePlayerButton_Click;

            malePlayerButton = new Button(maleCharacter, buttonFont)
            {
                Position = new Vector2(800, 150),
                Text = "",
            };

            malePlayerButton.Click += MalePlayerButton_Click;

            newGameButton = new Button(continueP, buttonFont)
            {
                Position = new Vector2(570, 500),
                Text = "",
            };

            newGameButton.Click += NewGameButton_Click;

            _components = new List<Component>()
          {
            femalePlayerButton,
            malePlayerButton,
            newGameButton
          };

        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            if(canContinue == true)
            {
                _game.ChangeState(new TutorialState(_game, _graphicsDevice, _content));
            }
        }

        private void FemalePlayerButton_Click(object sender, EventArgs e)
        {
            femalePlayerButton._texture = femaleCharacterSelected;
            malePlayerButton._texture = maleCharacter;

            _game.SaveCharacterSelected(0);
            canContinue = true;
        }


        private void MalePlayerButton_Click(object sender, EventArgs e)
        {
            femalePlayerButton._texture = femaleCharacter;
            malePlayerButton._texture = maleCharacterSelected;

            _game.SaveCharacterSelected(1);
            canContinue = true;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            // Main background
            spriteBatch.Draw(mainBackground, new Rectangle(0, 0, 1280, 700), Color.White);
            spriteBatch.Draw(selectCharacterTexture, new Rectangle(400, -180, 500, 500), Color.White);
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);

        }
    }
}
