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
        static public List<HealthPotion> Potions = new List<HealthPotion>();
        static public List<Keycard> Keycard = new List<Keycard>();

        public void SpawnPotion(ContentManager Content, Vector2 Position)
        {
            HealthPotion healthPotion = new HealthPotion();
            healthPotion.Initialize(Content, Position);
            Potions.Add(healthPotion);
        }

        public void SpawnKeyCard(ContentManager Content, Vector2 Position)
        {
            Keycard keycard = new Keycard();
            keycard.Initialize(Content, Position);
            Keycard.Add(keycard);
        }

        public void UpdatePotion(GameTime gameTime, Player p, GUI guiInfo)
        {
            for (int i = (Potions.Count - 1); i >= 0; i--)
            {
                Potions[i].Update(gameTime, p);
            }

        }

        public void UpdateKey(GameTime gameTime, Player p, GUI guiInfo)
        {
            for (int i = (Keycard.Count - 1); i >= 0; i--)
            {
                Keycard[i].Update(gameTime, p, guiInfo);
            }
        }

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
    }
}
