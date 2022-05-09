using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhantomProjects.PlayerBullets;

namespace PhantomProjects.Boss_
{
    class BossManager
    {
        #region Definitions 
        static public List<Boss> boss_list = new List<Boss>();

        //Handle the graphics info
        Vector2 graphicsInfo;
        #endregion
        public void Initialize(GraphicsDevice Graphics)
        {
            graphicsInfo.X = Graphics.Viewport.Width;
            graphicsInfo.Y = Graphics.Viewport.Height;
        }

        public void CreateBoss(Vector2 position, ContentManager content)
        {
            // create the animation object
            Animation bossAnimation = new Animation();

            // create an enemy
            Boss boss = new Boss();
            boss.Initialize(bossAnimation, position, content);

            boss_list.Add(boss);
        }

        public void UpdateBoss(GameTime gameTime, Player player, GUI guiInfo, Sounds SND)
        {
            //Update enemies
            for (int i = (boss_list.Count - 1); i >= 0; i--)
            {
                boss_list[i].Update(gameTime, player, SND);

                if (boss_list[i].Active == false)
                {
                    boss_list.RemoveAt(i);
                    guiInfo.UPGRADEPOINTS += 1000;
                }
            }
        }

        public void CleanBoss()
        {
            for (int i = (boss_list.Count - 1); i >= 0; i--)
            {
                boss_list[i].Active = false;
                boss_list.RemoveAt(i);
            }
        }

        public void DrawBoss(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < boss_list.Count; i++)
            {
                boss_list[i].Draw(spriteBatch);
            }
        }
    }
}
