using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhantomProjects.GUI_;
using PhantomProjects.Enemy_;
using PhantomProjects.Boss_;

namespace PhantomProjects.Player_
{
    class Shield
    {
        #region Declarations

        public Animation shieldAnimation;
        Texture2D shieldTexture, currentAnim;
        Vector2 position;
        public bool Active;
        int shieldUptime, cooldown;
        public int shieldDuration = 5, cdDelay = 20;
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
        #endregion

        #region Constructor
        public void Initialize(ContentManager Content)
        {
            //Load texture
            shieldTexture = Content.Load<Texture2D>("Player\\PlayerShield");

            //Deactivate the object as we don't want the shield always active
            Active = false;

            //Create animation
            shieldAnimation = new Animation();
        }
        #endregion

        #region Methods
        //Update Method
        public void Update(GameTime gameTime, Player p, bool bossActive, GUI guiInfo)
        {
            //Shield Position based on the player
            position.X = p.Position.X - 50;
            position.Y = p.Position.Y - 50;

            //Activate the shield if the following conditions are met
            if ((Keyboard.GetState().IsKeyDown(Keys.E) || GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed) && cooldown <= 0)
            {
                Active = true;

                //Set how long the shield will be active for
                shieldUptime = 60 * shieldDuration;

                //Increase the cooldown timer to prevent shield from being used instantly
                cooldown = 0;
                cooldown = 60 * cdDelay;
            }

            //Prevent taking damage when the shield is active and collides with other projectiles
            if (Active == true)
            {
                rectangle = new Rectangle((int)position.X -20, (int)position.Y-25, 200, 180);
                shieldAnimation.Position = position;
                shieldAnimation.Update(gameTime);

                //Display how long the shield will be up for
                guiInfo.SHIELDTIMER = (shieldUptime + 30) / 60;
                shieldUptime--;

                currentAnim = shieldTexture;
                Animate(gameTime);

                CancelIncomingDamage(guiInfo, bossActive, shieldDuration);
            }
            else
            {
                //Display shield cooldown info
                if (cooldown <= 0)
                {
                    guiInfo.SHIELDCOOLDOWN = 0;
                }
                else guiInfo.SHIELDCOOLDOWN = (cooldown + 30) / 60;

                cooldown--;
            }
        }

        //Animate Method
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

        //Method to set damage to 0 from all projectiles
        void CancelIncomingDamage(GUI guiInfo, bool bossActive, int duration)
        {
            //Check for enemy bullets
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

            //Check for boss projectiles
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
                shieldUptime = 60 * duration;
                guiInfo.SHIELDTIMER = 0;
            }
        }

        //Update Duration & Cooldown
        public void UpdateDuration(int duration)
        {
            shieldDuration = duration;
        }

        public void UpdateCooldown(int cooldown)
        {
            cdDelay = cooldown;
        }

        //Send data to save in scene
        public int ReturnD() { return shieldDuration; }
        public int ReturnC() { return cdDelay; }

        //Draw Method
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active == true)
            {
                spriteBatch.Draw(currentAnim, rectangle, sourceRect, Color.White);
            }
        }
        #endregion
    }
}
