using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PhantomProjects.Enemy_
{
    class BulletE
    {
        #region Declarations

        public Animation BulletAnimation; //bullet animation
        float bulletMoveSpeed; //bullet velocity
        public Vector2 Position; //bullet position

        public int damage; // bullet dmg
        public bool Active;
        bool Right; // direction that is being fired

        // Return dimentions of the bullet based on the bullet animation frame width/height
        public int Width
        {
            get { return BulletAnimation.FrameWidth; }
        }

        public int Height
        {
            get { return BulletAnimation.FrameHeight; }
        }
        #endregion

        #region Constructor
        public void Initialize(Animation animation, Vector2 position, bool Direction)
        {
            Right = Direction; // set the direction that the bullet is being fired
            BulletAnimation = animation; // set animation for the bullet
            Position = position; // set the postion
            Active = true;
            damage = 10;

            // check whats the direction that the bullet is being fired and switch the velocity accordingly 
            if (Right)
                bulletMoveSpeed = 10f;
            else
                bulletMoveSpeed = -10f;
        }
        #endregion

        #region Methods
        public void Update(GameTime gameTime)
        {
            //Update bullet position X based on the bullet velocity
            Position.X += bulletMoveSpeed;

            //Update bullet animation & position
            BulletAnimation.Position = Position;
            BulletAnimation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            BulletAnimation.Draw(spriteBatch);
        }
        #endregion
    }
}
