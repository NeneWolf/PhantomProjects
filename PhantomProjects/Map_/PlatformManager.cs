﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using PhantomProjects.States;
using PhantomProjects.Player_;
using PhantomProjects.Map_;


namespace PhantomProjects.Map_
{
    class PlatformManager
    {
        static public List<Platforms> platform = new List<Platforms>();

        public static void UpdateCollision(Player player)
        {
            for(int i = 0; i < platform.Count; i++)
            {
                if (platform[i].rectangle.Intersects(player.RECTANGLE))
                {
                    if (platform[i].VERTICAL() == true)
                    {
                        player.ChangePositionOnPlatforms(player.Position.X, platform[i].rectangle.Y - 100,false);
                    }
                    else
                    {
                        player.ChangePositionOnPlatforms(platform[i].rectangle.X+50, platform[i].rectangle.Y - 100,false);
                    }
                }
            }
        }

        public void CreatePlatforms(Vector2 position, ContentManager content, bool Horizontal, int MovingDistance)
        {
            Platforms platforms = new Platforms();
            platforms.Initialize(position, content, Horizontal, MovingDistance);
            platform.Add(platforms);
        }

        public void UpdatePlatforms(GameTime gameTime, Player player)
        {
            UpdateCollision(player);

            for (int i = (platform.Count - 1); i >= 0; i--)
            {
                platform[i].Update(gameTime);
            }

        }

        public void CleanPlatforms()
        {
            for (int i = (platform.Count - 1); i >= 0; i--)
            {
                platform.RemoveAt(i);
            }
        }

        public void DrawPlatfroms(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < platform.Count; i++)
            {
                platform[i].Draw(spriteBatch);
            }
        }
    }
}