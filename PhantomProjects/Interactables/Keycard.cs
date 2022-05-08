using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhantomProjects.Interactables
{
    class Keycard
    {
        Texture2D keyCardTexture;
        Vector2 position;
        public bool Active;

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
            Active = true;
            position = pos;
            keyCardTexture = Content.Load<Texture2D>("Keycard");
        }

        public void Update(GameTime gameTime, Player p)
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

            if (cardRectangle.Intersects(playerRectangle) && Keyboard.GetState().IsKeyDown(Keys.E))
            {
                Active = false;
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
