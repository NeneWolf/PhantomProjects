using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using PhantomProjects.Player_;
using PhantomProjects.GUI_;

namespace PhantomProjects.Interactables_
{
    class ItemManager
    {
        #region Declarations
        //Create lists to hold collectibles
        static public List<HealthPotion> Potions = new List<HealthPotion>();
        static public List<Keycard> Keycard = new List<Keycard>();
        #endregion

        #region Constructors
        //Health Potion Constructor
        public void SpawnPotion(ContentManager Content, Vector2 Position)
        {
            //Create health potion object
            HealthPotion healthPotion = new HealthPotion();
            healthPotion.Initialize(Content, Position);

            //Add object to the appropriate list
            Potions.Add(healthPotion);
        }

        //Key Card Constructor
        public void SpawnKeyCard(ContentManager Content, Vector2 Position)
        {
            //Create key card object
            Keycard keycard = new Keycard();
            keycard.Initialize(Content, Position);

            //Add object to the appropriate list
            Keycard.Add(keycard);
        }
        #endregion

        #region Methods
        //Health Potion Update Method
        public void UpdatePotion(GameTime gameTime, Player p, GUI guiInfo)
        {
            for (int i = (Potions.Count - 1); i >= 0; i--)
            {
                Potions[i].Update(gameTime, p);
            }

        }

        //Key Card Update Method
        public void UpdateKey(GameTime gameTime, Player p, GUI guiInfo)
        {
            for (int i = (Keycard.Count - 1); i >= 0; i--)
            {
                Keycard[i].Update(gameTime, p, guiInfo);
            }
        }

        //Remove any collectibles left behind so they dont appear on other levels or when game is restarted
        public void RemoveCollectibles()
        {
            for (int i = (Potions.Count - 1); i >= 0; i--)
            {
                Potions[i].Active = false;
                Potions.RemoveAt(i);
            }

            for (int i = (Keycard.Count - 1); i >= 0; i--)
            {
                Keycard[i].Active = false;
                Keycard.RemoveAt(i);
            }
        }

        //Draw Method
        public void DrawCollectibles(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Potions.Count; i++)
            {
                Potions[i].Draw(spriteBatch);
            }

            for (int i = 0; i < Keycard.Count; i++)
            {
                Keycard[i].Draw(spriteBatch);
            }
        }
        #endregion
    }
}
