using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhantomProjects.PlayerBullets
{
    class Shield
    {

        Texture2D shieldTexture;
        Vector2 position;
        public bool Active;
        int shieldTimer;
        int cooldown;

        public int Width
        {
            get { return shieldTexture.Width; }
        }

        public int Height
        {
            get { return shieldTexture.Height; }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public void Initialize(ContentManager Content)
        {
            shieldTexture = Content.Load<Texture2D>("PlayerContent\\Shield");
            Active = false;
            shieldTimer = 60 * 5;
            cooldown = 0;
        }

        public void Update(GameTime gameTime, Player p, GUI guiInfo)
        {
            position.X = p.Position.X - 50;
            position.Y = p.Position.Y - 50;


            cooldown--;

            if ((Keyboard.GetState().IsKeyDown(Keys.E) || GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed) && cooldown <= 0)
            {
                Active = true;
                cooldown = 60 * 15;
            }

            if (Active == true)
            {
                guiInfo.SHIELDTIMER = shieldTimer;
                shieldTimer--;

                Rectangle shieldRectangle = new Rectangle(
                    (int)position.X,
                    (int)position.Y,
                    Width,
                    Height);

                foreach (BulletE B in BulletEManager.bulletEBeams)
                {
                    Rectangle bulletRectangle = new Rectangle(
                        (int)B.Position.X,
                        (int)B.Position.Y,
                        B.Width,
                        B.Height);

                    if (bulletRectangle.Intersects(shieldRectangle))
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

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active == true)
            {
                spriteBatch.Draw(shieldTexture, position, Color.White);
            }
        }

    }
}
