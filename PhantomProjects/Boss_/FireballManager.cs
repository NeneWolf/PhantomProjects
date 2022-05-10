using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhantomProjects.Player_;
using PhantomProjects.Explosion_;
using PhantomProjects.Map_;


namespace PhantomProjects.Boss_
{
    class FireballManager
    {
        static ContentManager fireContent;
        static Rectangle fireballRectangle;
        static public List<Fireball> fireball;
        const float SECONDS_IN_MINUTE = 35f;
        const float RATE_OF_FIRE = 120f;

        static TimeSpan fireballSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / RATE_OF_FIRE);
        static TimeSpan previousBulletSpawnTime;

        static Vector2 graphicsInfo;

        public void Initialize(GraphicsDevice Graphics, ContentManager content)
        {
            fireball = new List<Fireball>();
            previousBulletSpawnTime = TimeSpan.Zero;
            fireContent = content;

            graphicsInfo.X = Graphics.Viewport.Width;
            graphicsInfo.Y = Graphics.Viewport.Height;
        }

        public static void FireBulletE(GameTime gameTime, Boss b, Sounds SND)
        {
            if (gameTime.TotalGameTime - previousBulletSpawnTime > fireballSpawnTime)
            {
                previousBulletSpawnTime = gameTime.TotalGameTime;
                AddBullet(b, fireContent);
                SND.FIREBALL.Play();
            }
        }

        private static void AddBullet(Boss b, ContentManager content)
        {
            Animation fireballAnimation = new Animation();
            Fireball fireball_ = new Fireball();

            var fireballPosition = b.position;
            Random r = new Random();
            int nextValue = r.Next((int)b.position.X - 850, (int)b.position.X + 850);

            fireballPosition.Y = b.position.Y - 380;
            fireballPosition.X = nextValue;

            fireball_.Initialize(fireballAnimation, fireballPosition, content);

            fireball.Add(fireball_);
        }

        public void UpdateManagerFireball(GameTime gameTime, Player p, ExplosionManager VFX, Sounds SND)
        {
            for (var i = 0; i < fireball.Count; i++)
            {
                fireball[i].Update(gameTime);

                if (!fireball[i].Active)
                {
                    fireball.Remove(fireball[i]);
                }
            }


            foreach (Fireball fire in FireballManager.fireball)
            {
                if (fire.rectangle.Intersects(p.RECTANGLE))
                {
                    // Show the explosion where the player was.
                    VFX.AddExplosion(p.Position, SND);

                    // Damage Player
                    p.Health -= fire.damage;
                    p.BarHealth -= 15;

                    fire.Active = false;
                }
            }

        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            foreach (Fireball fire in FireballManager.fireball)
            {
                if (fireballRectangle.TouchTopOf(newRectangle) || fireballRectangle.TouchLeftOf(newRectangle) ||
                fireballRectangle.TouchRightOf(newRectangle) || fireballRectangle.TouchBottomOf(newRectangle))
                {
                    fire.Active = false;
                }
            }
        }


        public void DrawFireball(SpriteBatch spriteBatch)
        {
            foreach (var b in fireball)
            {
                b.Draw(spriteBatch);
            }
        }
    }
}
