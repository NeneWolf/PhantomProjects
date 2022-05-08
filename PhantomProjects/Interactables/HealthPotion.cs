using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhantomProjects
{
    class HealthPotion
    {
        Texture2D healthPotionTexture;
        Vector2 position;
        public bool Active;

        public int Width
        {
            get { return healthPotionTexture.Width; }
        }

        public int Height
        {
            get { return healthPotionTexture.Height; }
        }

        public void Initialize(ContentManager Content, Vector2 pos)
        {
            Active = true;
            position = pos;
            healthPotionTexture = Content.Load<Texture2D>("HealthPotion");
        }

        public void Update(GameTime gameTime, Player p)
        {
            Rectangle playerRectangle = new Rectangle(
                                        (int)p.Position.X,
                                        (int)p.Position.Y,
                                        50,
                                        50);

            Rectangle potionRectangle = new Rectangle(
                                      (int)position.X,
                                      (int)position.Y,
                                      Width,
                                      Height);

            if (potionRectangle.Intersects(playerRectangle) && Keyboard.GetState().IsKeyDown(Keys.E))
            {
                Active = false;
                
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active == true)
            {
                spriteBatch.Draw(healthPotionTexture, position, Color.White);
            }
        }
    }
}
