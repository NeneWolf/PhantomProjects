using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using PhantomProjects.Enemy_;

namespace PhantomProjects.Player_
{
    class Shield
    {
        public Animation shieldAnimation;
        Texture2D shieldTexture, currentAnim;
        Vector2 position;
        public bool Active;
        int shieldTimer;
        int cooldown;

        float elapsed;
        float delay = 120f;
        int Frames = 0;

        public Rectangle rectangle, sourceRect;

        public int Width
        {
            get { return shieldAnimation.FrameWidth; }
        }

        public int Height
        {
            get { return shieldAnimation.FrameHeight; }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public void Initialize(ContentManager Content)
        {
            shieldTexture = Content.Load<Texture2D>("Player\\PlayerShield");

            Active = false;
            shieldTimer = 60 * 5;
            cooldown = 0;

            shieldAnimation = new Animation();
        }

        public void Update(GameTime gameTime, Player p, GUI guiInfo)
        {
            position.X = p.Position.X -20;
            position.Y = p.Position.Y -60;

            cooldown--;

            if ((Keyboard.GetState().IsKeyDown(Keys.E) || GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed) && cooldown <= 0)
            {
                Active = true;
                cooldown = 60 * 15;
            }

            if (Active == true)
            {
                rectangle = new Rectangle((int)position.X, (int)position.Y, 150,150);
                shieldAnimation.Position = position;
                shieldAnimation.Update(gameTime);

                guiInfo.SHIELDTIMER = shieldTimer;
                shieldTimer--;

                currentAnim = shieldTexture;
                Animate(gameTime);

                foreach (BulletE B in BulletEManager.bulletEBeams)
                {
                    Rectangle bulletRectangle = new Rectangle(
                        (int)B.Position.X,
                        (int)B.Position.Y,
                        B.Width,
                        B.Height);

                    if (bulletRectangle.Intersects(rectangle))
                    {
                        B.damage = 0;
                        B.Active = false;
                    }
                }

                if (shieldTimer == 0)
                {
                    Active = false;
                    shieldTimer = 60 * 5;
                    guiInfo.SHIELDTIMER = 0;
                }
            }
        }

        void Animate(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delay)
            {
                if (Frames >= 3)
                {
                    Frames = 0;
                }
                else
                {
                    Frames++;
                }
                elapsed = 0;
            }

            sourceRect = new Rectangle((Frames * 152), 0, 152, 150);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active == true)
            {
                spriteBatch.Draw(currentAnim, rectangle, sourceRect, Color.White);
            }
        }

    }
}
