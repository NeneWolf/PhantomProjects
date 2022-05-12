﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using PhantomProjects.Enemy_;
using PhantomProjects.Boss_;

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

        int cooldownNumber, durationNumber;
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

        public void Initialize(ContentManager Content, int cooldownN, int durationN)
        {
            shieldTexture = Content.Load<Texture2D>("Player\\PlayerShield");

            Active = false;
            shieldTimer = 60 * 5;
            cooldown = 0;
            cooldownNumber = cooldownN;
            durationNumber = durationN;

            shieldAnimation = new Animation();
        }

        public void Update(GameTime gameTime, Player p, bool bossActive,  GUI guiInfo)
        {
            position.X = p.Position.X -50;
            position.Y = p.Position.Y -50;

            if ((Keyboard.GetState().IsKeyDown(Keys.E) || GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed) && cooldown <= 0)
            {
                Active = true;
                cooldown = 60 * cooldownNumber;
            }

            if (Active == true)
            {
                rectangle = new Rectangle((int)position.X, (int)position.Y, 150,150);
                shieldAnimation.Position = position;
                shieldAnimation.Update(gameTime);

                guiInfo.SHIELDTIMER = ((shieldTimer+100)/100);
                shieldTimer--;

                currentAnim = shieldTexture;
                Animate(gameTime);

                CancelIncomingDamage(guiInfo, bossActive, durationNumber);
            }
            else
            {
                if (cooldown <= 0)
                {
                    guiInfo.SHIELDCOOLDOWN = 0;
                }
                else guiInfo.SHIELDCOOLDOWN = ((cooldown + 100) / 100);

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

            if (shieldTimer == 0)
            {
                Active = false;
                shieldTimer = 60 * duration; // change the 5 to a variable - Duration of the shield
                guiInfo.SHIELDTIMER = 0;
            }
        }

        public void UpdateDuration(int duration) { durationNumber = duration; }
        public void UpdateCooldown(int cooldown) { cooldownNumber = cooldown; }

        public int ReturnD() { return durationNumber; }
        public int ReturnC() { return cooldownNumber; }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active == true)
            {
                spriteBatch.Draw(currentAnim, rectangle, sourceRect, Color.White);
            }
        }

    }
}
