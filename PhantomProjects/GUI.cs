using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace PhantomProjects
{
    class GUI
    {
        private int keys, points, shieldTimer;

        public void Initialize(int Keys, int UpgradePoints, int ShieldTimer)
        {
            keys = Keys;
            points = UpgradePoints;
            shieldTimer = ShieldTimer;
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

        public int SHIELDTIMER
        {
            get { return shieldTimer; }
            set { this.shieldTimer = value; }
        }
    }
}
