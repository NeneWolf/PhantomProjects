using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhantomProjects.PlayerBullets;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhantomProjects.Interactables
{
    class Keycard
    {
        #region Definitions
        Texture2D keyCardTexture;
        Vector2 position;
        public bool Active;


        // Gamepad states used to determine button presses
        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        #endregion

        public int Width
        {
            get { return keyCardTexture.Width; }
        }

        public int Height
        {
            get { return keyCardTexture.Height; }
        }

        public void Initialize(ContentManager Content, Vector2 pos)
        {
            keyCardTexture = Content.Load<Texture2D>("GUI\\key");
            Active = true;
            position = pos;
        }

        public void Update(GameTime gameTime, Player p, GUI guiInfo)
        {
            if(Active == true)
            {
                Rectangle playerRectangle = new Rectangle(
                            (int)p.Position.X,
                            (int)p.Position.Y,
                            50,
                            50);

                Rectangle cardRectangle = new Rectangle(
                                          (int)position.X,
                                          (int)position.Y,
                                          Width,
                                          Height);

                if (cardRectangle.Intersects(playerRectangle) && (Keyboard.GetState().IsKeyDown(Keys.F) || 
                    GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed))
                {
                    Active = false;
                    guiInfo.KEYS += 1;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active == true)
            {
                spriteBatch.Draw(keyCardTexture, position, Color.White);
            }
        }
    }
}
