using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using PhantomProjects.Player_;

namespace PhantomProjects.Map_
{
    class PlatformManager
    {
        #region Declarations
        //Create list to hold all platforms
        static public List<Platforms> platform = new List<Platforms>();
        #endregion

        #region Constructor
        public void CreatePlatforms(Vector2 position, ContentManager content, bool Horizontal, int MovingDistance, bool turnOn)
        {
            //Create object
            Platforms platforms = new Platforms();
            platforms.Initialize(position, content, Horizontal, MovingDistance, turnOn);

            //Add to list
            platform.Add(platforms);
        }
        #endregion

        #region Methods
        //Check player collision with platform
        public static void UpdateCollision(Player player)
        {
            for (int i = 0; i < platform.Count; i++)
            {
                if (platform[i].rectangle.Intersects(player.RECTANGLE))
                {
                    if (platform[i].VERTICAL() == true)
                    {
                        player.ChangePositionOnPlatforms(player.Position.X, platform[i].rectangle.Y - 100, false);
                    }
                    else
                    {
                        player.ChangePositionOnPlatforms(player.Position.X, platform[i].rectangle.Y - 100, false);
                    }
                }
            }
        }

        //Update Method
        public void UpdatePlatforms(GameTime gameTime, Player player, bool activate)
        {
            for (int i = (platform.Count - 1); i >= 0; i--)
            {
                if (platform[i].IsActive() == true)
                {
                    UpdateCollision(player);
                    platform[i].UpdateStatus(true);
                    platform[i].Update(gameTime);
                }

                if (platform[i].IsActive() == false && activate == true)
                {
                    UpdateCollision(player);
                    platform[i].UpdateStatus(true);
                    platform[i].Update(gameTime);
                }
            }
        }

        //Remove platforms when scene is changed or game is restarted
        public void CleanPlatforms()
        {
            for (int i = (platform.Count - 1); i >= 0; i--)
            {
                platform.RemoveAt(i);
            }
        }

        //Draw Method
        public void DrawPlatfroms(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < platform.Count; i++)
            {
                platform[i].Draw(spriteBatch);
            }
        }
        #endregion
    }
}
