using Microsoft.Xna.Framework;
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
        public Animation healthPotionAnimation;
        Texture2D healthPotionTexture;
        public Rectangle sourceRect;
        Vector2 position;
        public bool Active;

        float elapsed;
        float delay = 120f;
        int Frames = 0;

        public int Width
        {
            get { return healthPotionAnimation.FrameWidth; }
        }

        public int Height
        {
            get { return healthPotionAnimation.FrameHeight; }
        }

        public void Initialize(ContentManager Content, Vector2 pos)
        {
            healthPotionTexture = Content.Load<Texture2D>("GUI\\HealthPotion");
            Active = true;
            position = pos;

            healthPotionAnimation = new Animation();
        }

        public void Update(GameTime gameTime, Player p)
        {
            if (Active == true)
            {
                healthPotionAnimation.Update(gameTime);
                Animate(gameTime);

                Rectangle potionRectangle = new Rectangle(
                                          (int)position.X,
                                          (int)position.Y,
                                          Width + 50,
                                          Height + 50);

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

        void Animate(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delay)
            {
                if (Frames >= 5)
                {
                    Frames = 0;
                }
                else
                {
                    Frames++;
                }
                elapsed = 0;
            }

            sourceRect = new Rectangle((Frames * 70), 0, 70, 70);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active == true)
            {
                spriteBatch.Draw(healthPotionTexture, position, sourceRect, Color.White);
            }
        }
    }
}
