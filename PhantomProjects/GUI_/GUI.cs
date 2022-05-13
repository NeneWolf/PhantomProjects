
namespace PhantomProjects.GUI_
{
    class GUI
    {
        private int keys, points, shieldTimer, shieldCooldown;

        public void Initialize(int Keys, int UpgradePoints, int ShieldTimer, int ShieldCooldown)
        {
            keys = Keys;
            points = UpgradePoints;
            shieldTimer = ShieldTimer;
            shieldCooldown = ShieldCooldown;
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

        //Cooldown
        public int SHIELDCOOLDOWN
        {
            get { return shieldCooldown; }
            set { this.shieldCooldown = value; }
        }

        //Duration
        public int SHIELDTIMER
        {
            get { return shieldTimer; }
            set { this.shieldTimer = value; }
        }
    }
}
