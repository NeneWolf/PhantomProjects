﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhantomProjects.Player_;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhantomProjects.Interactables_
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
            healthPotionTexture = Content.Load<Texture2D>("GUI\\Potion");
            Active = true;
            position = pos;
        }

        public void Update(GameTime gameTime, Player p)
        {
            if(Active == true)
            {
                Rectangle potionRectangle = new Rectangle(
                                          (int)position.X,
                                          (int)position.Y,
                                          Width,
                                          Height);

                if (potionRectangle.Intersects(p.rectangle) && (Keyboard.GetState().IsKeyDown(Keys.F) || 
                    GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed))
                {
                    Active = false;
                    if (p.Health < 100)
                    {
                        p.Health += 10;
                        p.BarHealth += 15;

                        if (p.Health > 100)
                        {
                            p.Health = 100;
                            p.BarHealth = 150;
                        }
                    }
                }
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
