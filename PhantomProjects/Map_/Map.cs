using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace PhantomProjects.Map_
{
    class Map
    {
        #region Declarations
        //Create list to hold tiles
        private List<CollisionTiles> collisionTiles = new List<CollisionTiles>();

        private int width, height;

        //Get collision tiles
        public List<CollisionTiles> CollisionTiles
        {
            get { return collisionTiles; }
        }

        //Get width of map
        public int Width
        {
            get { return width; }
        }

        //Get height of map
        public int Height
        {
            get { return height; }
        }
        #endregion

        #region Constructor
        //Create map object
        public Map()
        {

        }
        #endregion

        #region Methods
        //Method to generate the may layout
        public void Generate(int[,] map, int size)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];

                    if (number > 0)
                    {
                        collisionTiles.Add(new CollisionTiles(number, new Rectangle(x * size, y * size, size, size)));

                        width = (x + 1) * size;
                        height = (y + 1) * size;
                    }
                }
            }
        }

        //Draw Method
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (CollisionTiles tile in collisionTiles)
            {
                tile.Draw(spriteBatch);
            }
        }
        #endregion
    }
}
