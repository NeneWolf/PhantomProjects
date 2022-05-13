using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PhantomProjects
{
    class Camera
    {
        #region Declarations
        private Matrix transform;
        private Vector2 centre;
        private Viewport viewport;

        public Matrix Transform
        {
            get { return transform; }
        }

        public Camera(Viewport newViewport)
        {
            viewport = newViewport;
        }
        #endregion

        #region Methods
        //Update Method
        public void Update(Vector2 position, int xOffset, int yOffset)
        {
            //Center the camera based on the player's position of the game's window size
            if (position.X < viewport.Width / 2)
                centre.X = viewport.Width / 2;
            else if (position.X > xOffset - (viewport.Width / 2))
                centre.X = xOffset - (viewport.Width / 2);
            else centre.X = position.X;

            if (position.Y < viewport.Height / 2)
                centre.Y = viewport.Height / 2;
            else if (position.Y > yOffset - (viewport.Height / 2))
                centre.Y = yOffset - (viewport.Height / 2);
            else centre.Y = position.Y;

            transform = Matrix.CreateTranslation(new Vector3(-centre.X + (viewport.Width / 2), -centre.Y + (viewport.Height / 2), 0));
        }
        #endregion
    }
}