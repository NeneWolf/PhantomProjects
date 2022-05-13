using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PhantomProjects.Map_
{
    class Tiles
    {
        #region Declarations
        //Variables
        protected Texture2D texture;
        private Rectangle rectangle;
        private static ContentManager content;

        //Get the tile's rectangle
        public Rectangle Rectangle
        {
            get { return rectangle; }
            protected set { rectangle = value; }
        }

        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }
        #endregion

        #region Methods
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
        #endregion
    }

    class CollisionTiles : Tiles
    {
        #region Constructor
        //Create object
        public CollisionTiles(int i, Rectangle newRectangle)
        {
            //Load all textures for tiles
            texture = Content.Load<Texture2D>("Map\\Tile" + i);
            this.Rectangle = newRectangle;
        }
        #endregion
    }
}
