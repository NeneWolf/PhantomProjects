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
        private int points;

        //more to add on your own choice

        public void Initialize(int Keys, int UpgradePoints)
        {
            keys = Keys;
            points = UpgradePoints;
        }
        public int KEYS
        {
            get { return keys; }
            set { this.keys = value; }
        }

        public int UPGRADEPOINTS
        {
            get { return points; }
            set { this.points = value; }
        }

    }
}
