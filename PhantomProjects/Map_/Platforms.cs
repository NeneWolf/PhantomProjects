using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace PhantomProjects.Map_
{
    class Platforms
    {
        #region Declarations
        //Variables
        Texture2D platformTexture;
        public Rectangle rectangle;
        public Vector2 position, velocity;
        Vector2 currentPosition, destinePosition;
        float movingDistance;

        bool Active;
        bool vertical;

        //Get the width of the texture
        public int Width
        {
            get { return platformTexture.Width; }
        }

        //Get the height of the texture
        public int Height
        {
            get { return platformTexture.Height; }
        }
        #endregion

        #region Constructor
        public void Initialize(Vector2 Position, ContentManager content, bool Horizontal, int MovingDistance, bool active)
        {
            //Load texture
            platformTexture = content.Load<Texture2D>("Map\\Platform");

            //Initialize variables
            position = Position;
            currentPosition = position;
            vertical = Horizontal;
            movingDistance = MovingDistance;
            Active = active;

            destinePosition.Y = position.Y - movingDistance;
            destinePosition.X = position.X + movingDistance;
        }
        #endregion

        #region Methods
        //Update Methods
        public void Update(GameTime gameTime)
        {
            if (Active == true)
            {
                position += velocity;
                rectangle = new Rectangle((int)position.X, (int)position.Y, Width, Height);
                PlatformMovement(gameTime);
            }
        }

        public bool UpdateStatus(bool activate) => Active = activate;

        void PlatformMovement(GameTime gameTime)
        {
            if (vertical == true)
            {
                if (currentPosition.Y == position.Y)
                {
                    velocity.Y -= 1.5f;
                }
                else if (position.Y <= destinePosition.Y)
                {
                    velocity.Y += 1.5f;
                }
            }
            else
            {
                if (currentPosition.X == position.X)
                {
                    velocity.X += 1.5f;
                }
                else if (position.X >= destinePosition.X)
                {
                    velocity.X -= 1.5f;
                }
            }
        }

        public bool VERTICAL()
        {
            return vertical;
        }

        public bool IsActive() { return Active; }

        //Draw Method
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(platformTexture, rectangle, Color.White);
        }
        #endregion
    }
}
