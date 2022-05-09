using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhantomProjects.PlayerBullets;

namespace PhantomProjects
{
    class EnemyManager
    {
        #region Definitions 
        static public List<EnemyA> enemyType1 = new List<EnemyA>();

        //Handle the graphics info
        Vector2 graphicsInfo;
        #endregion
        public void Initialize(GraphicsDevice Graphics)
        {
            graphicsInfo.X = Graphics.Viewport.Width;
            graphicsInfo.Y = Graphics.Viewport.Height;
        }

        public static void UpdateColission(Player player, ExplosionManager VFX, GUI guiInfo, Sounds SND)
        {
            //use the Rectangle's build-in interscect function to determine if
            //two objects are overlapping
            Rectangle rect1, rect2;

            //Only create the rectangle once for the player
            rect1 = new Rectangle(
                (int)player.Position.X,
                (int)player.Position.Y,
                player.Width, player.Height);

            //Do the collision between the player and the enemies
            for (int i = 0; i < enemyType1.Count; i++)
            {
                rect2 = new Rectangle(
                    (int)enemyType1[i].position.X,
                    (int)enemyType1[i].position.Y,
                    100, 93);

                //Now determine if the two objects collide with each other
                if (rect1.Intersects(rect2))
                {
                    //Subtract the health from the player based on the enemy damage
                    player.Health -= enemyType1[i].Damage;
                    player.BarHealth -= 15;

                    //Since the enemy collided with the player destroy it
                    enemyType1[i].Health -= 10;

                    //Add the Explossion in the enemy location
                    VFX.AddExplosion(enemyType1[i].LocationEnemy, SND);

                    //if the player health is less than zero then player must be destroyed
                    if (player.Health <= 0)
                    {
                        player.Active = false;
                    }
                }

            }

        }

        public void CreateEnemy(Vector2 position, ContentManager content)
        {
            // create the animation object
            Animation enemyAnimation = new Animation();

            // create an enemy
            EnemyA enemyA = new EnemyA();
            enemyA.Initialize(enemyAnimation, position, content);

            enemyType1.Add(enemyA);
        }

        public void UpdateEnemy(GameTime gameTime, Player player, ExplosionManager VFX, GUI guiInfo, Sounds SND)
        {
            UpdateColission(player, VFX, guiInfo, SND); //Update Collision

            //Update enemies
            for (int i = (enemyType1.Count - 1); i >= 0; i--)
            {
                enemyType1[i].Update(gameTime, player, SND);

                if (enemyType1[i].Active == false)
                { 
                    enemyType1.RemoveAt(i);
                    guiInfo.UPGRADEPOINTS += 50;
                }
            }
        }

        public void CleanEnemies()
        {
            for(int i = (enemyType1.Count - 1); i >= 0; i--)
            {
                enemyType1[i].Active = false;
                enemyType1.RemoveAt(i);
            }
        }

        public void DrawEnemies(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < enemyType1.Count; i++)
            {
                enemyType1[i].Draw(spriteBatch);
            }
        }
    }
}
