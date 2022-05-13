using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PhantomProjects.Player_;
using PhantomProjects.Explosion_;
using PhantomProjects.GUI_;

namespace PhantomProjects.Enemy_
{
    class EnemyManager
    {
        #region Declarations 
        //Create a list for all enemies 
        static public List<EnemyA> enemyType1 = new List<EnemyA>();

        Vector2 graphicsInfo;//Handle the graphics info
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

                    ////Since the enemy collided with the player destroy it
                    //enemyType1[i].Health -= 10;

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
                    //Add the blood explosion in the enemy location
                    VFX.AddExplosion(enemyType1[i].LocationEnemy, SND);

                    //add upgrade points into the gui 
                    guiInfo.UPGRADEPOINTS += 50;

                    //remove this enemy from the list
                    enemyType1.RemoveAt(i);
                }
            }
        }

        public void CleanEnemies()
        {
            // clean the entire list
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
