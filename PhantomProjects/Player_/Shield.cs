using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using PhantomProjects.Enemy_;
using PhantomProjects.Boss_;
using PhantomProjects.Menus_;

namespace PhantomProjects.Player_
{
    class Shield
    {
        public Animation shieldAnimation;
        Texture2D shieldTexture, currentAnim;
        Vector2 position;
        public bool Active;
        int shieldUptime, cooldown;
        public int shieldDuration = 5, cdDelay = 20;
        UpgradeMenu upgrade;

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

            shieldAnimation = new Animation();
        }

        public void Update(GameTime gameTime, Player p, bool bossActive, GUI guiInfo)
        {
            position.X = p.Position.X - 50;
            position.Y = p.Position.Y - 50;

            if ((Keyboard.GetState().IsKeyDown(Keys.E) || GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed) && cooldown <= 0)
            {
                Active = true;
                shieldUptime = 60 * shieldDuration;
                cooldown = 0;
                cooldown = 60 * cdDelay;
            }

            if (Active == true)
            {
                rectangle = new Rectangle((int)position.X, (int)position.Y, 150, 150);
                shieldAnimation.Position = position;
                shieldAnimation.Update(gameTime);

                guiInfo.SHIELDTIMER = (shieldUptime + 30) / 60;
                shieldUptime--;

                currentAnim = shieldTexture;
                Animate(gameTime);

                CancelIncomingDamage(guiInfo, bossActive, shieldDuration);
            }
            else
            {
                if (cooldown <= 0)
                {
                    guiInfo.SHIELDCOOLDOWN = 0;
                }
                else guiInfo.SHIELDCOOLDOWN = (cooldown + 30) / 60;

                cooldown--;
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

        void CancelIncomingDamage(GUI guiInfo, bool bossActive, int duration)
        {
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

            if (bossActive == true)
            {
                foreach (Fireball F in FireballManager.fireball)
                {
                    Rectangle fireballRectangle = new Rectangle(
                                            (int)F.Position.X,
                                            (int)F.Position.Y,
                                            F.Width,
                                            F.Height);

                    if (fireballRectangle.Intersects(rectangle))
                    {
                        F.damage = 0;
                        F.Active = false;
                    }
                }
            }

            if (shieldUptime == 0)
            {
                Active = false;
                shieldUptime = 60 * duration; // change the 5 to a variable - Duration of the shield
                guiInfo.SHIELDTIMER = 0;
            }
        }

        public void UpdateDuration(int duration)
        {
            shieldDuration = duration;
        }
        public void UpdateCooldown(int cooldown)
        {
            cdDelay = cooldown;
        }

        public int ReturnD() { return shieldDuration; }
        public int ReturnC() { return cdDelay; }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active == true)
            {
                spriteBatch.Draw(currentAnim, rectangle, sourceRect, Color.White);
            }
        }

    }
}
