using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhantomProjects.Player_;
using PhantomProjects.GUI_;

namespace PhantomProjects.Interactables_
{
    class Keycard
    {
        #region Definitions
        public Animation keycardAnimation;
        Texture2D keycardTexture;
        public Rectangle sourceRect;
        Vector2 position;
        public bool Active;

        float elapsed;
        float delay = 60f;
        int Frames = 0;
        #endregion

        public int Width
        {
            get { return keycardAnimation.FrameWidth; }
        }

        public int Height
        {
            get { return keycardAnimation.FrameHeight; }
        }

        public void Initialize(ContentManager Content, Vector2 pos)
        {
            keycardTexture = Content.Load<Texture2D>("GUI\\KeyCard");
            Active = true;
            position = pos;

            keycardAnimation = new Animation();
        }

        public void Update(GameTime gameTime, Player p, GUI guiInfo)
        {
            if (Active == true)
            {
                keycardAnimation.Update(gameTime);
                Animate(gameTime);

                Rectangle cardRectangle = new Rectangle(
                                          (int)position.X,
                                          (int)position.Y,
                                          Width + 50,
                                          Height + 50);

                if (cardRectangle.Intersects(p.rectangle) && (Keyboard.GetState().IsKeyDown(Keys.F) ||
                    GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed))
                {
                    Active = false;
                    guiInfo.KEYS += 1;
                }
            }
        }

        void Animate(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delay)
            {
                if (Frames >= 17)
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
                spriteBatch.Draw(keycardTexture, position, sourceRect, Color.White);
            }
        }
    }
}
