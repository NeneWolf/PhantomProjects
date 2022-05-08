using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PhantomProjects.Player
{
    class BulletManager
    {
        static Texture2D bulletTexture;
        static Rectangle bulletRectangle;
        static public List<Bullet> bullets;
        const float SECONDS_IN_MINUTE = 60f;
        const float RATE_OF_FIRE = 200f;

        static TimeSpan bulletSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / RATE_OF_FIRE);
        static TimeSpan previousBulletSpawnTime;

        GraphicsDeviceManager graphics;

        static Vector2 graphicsInfo;

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        public void Initialize(Texture2D texture, GraphicsDevice Graphics)
        {
            bullets = new List<Bullet>();
            previousBulletSpawnTime = TimeSpan.Zero;
            bulletTexture = texture;
            graphicsInfo.X = Graphics.Viewport.Width;
            graphicsInfo.Y = Graphics.Viewport.Height;
        }

        private static void FireBullet(GameTime gameTime, Player p)
        {
            if (gameTime.TotalGameTime - previousBulletSpawnTime > bulletSpawnTime)
            {
                previousBulletSpawnTime = gameTime.TotalGameTime;
                AddBullet(p);
            }
        }

        private static void AddBullet(Player p)
        {
            Animation bulletAnimation = new Animation();
            bulletAnimation.Initialize(bulletTexture, p.Position, 46, 16, 1, 30, Color.White, 1f, true);
            Bullet bullet = new Bullet();

            var bulletPosition = p.Position;

            if (p.currentAnim == p.playerRight)
            {
                bulletPosition.Y += 15;
                bulletPosition.X += 25;
            }
            else
            {
                bulletPosition.Y += 15;
                bulletPosition.X -= 25;
            }

            bullet.Initialize(bulletAnimation, bulletPosition, p);
            bullets.Add(bullet);
        }

        public void UpdateManagerBullet(GameTime gameTime, Player p)
        {
            previousGamePadState = currentGamePadState;
            previousKeyboardState = currentKeyboardState;

            currentGamePadState = GamePad.GetState(PlayerIndex.One);
            currentKeyboardState = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
            {
                BulletManager.FireBullet(gameTime, p);
            }

            for (var i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update(gameTime);
                if (!bullets[i].Active /*|| bullets[i].Position.X > graphicsInfo.X*/)
                {
                    bullets.Remove(bullets[i]);
                }
            }

            foreach (EnemyA e in EnemyManager.enemyType1)
            {
                Rectangle enemyRectangle = new Rectangle(
                                           (int)e.position.X,
                                           (int)e.position.Y,
                                           e.rectangle.X,
                                           e.rectangle.Y);

                foreach (Bullet B in BulletManager.bullets)
                {
                    bulletRectangle = new Rectangle(
                                      (int)B.Position.X,
                                      (int)B.Position.Y,
                                      B.Width,
                                      B.Height);

                    if (bulletRectangle.Intersects(enemyRectangle))
                    {
                        e.Health -= B.Damage;
                        B.Active = false;
                    }
                }
            }
        }

        public void DrawBullets(SpriteBatch spriteBatch)
        {
            foreach (var b in bullets)
            {
                b.Draw(spriteBatch);
            }
        }
    }
}
