using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace PhantomProjects
{
    class GUI
    {
        private int keys;
        private int potions;

        //more to add on your own choice

        public void Initialize(int Keys, int Potions)
        {
            keys = Keys;
            potions = Potions;
        }
        public int KEYS
        {
            get { return keys; }
            set { this.keys = value; }
        }

        public int POTIONS
        {
            get { return potions; }
            set { this.potions = value; }
        }

    }
}
