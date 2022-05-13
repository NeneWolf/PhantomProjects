namespace PhantomProjects.GUI_
{
    class GUI
    {
        // Declaration
        private int keys, points, shieldTimer, shieldCooldown;

        #region Constructor
        // initialize with specific fields, in this case , key, upgradepoints, shield duration and shield cooldown
        // these fields will be using in the visual GUI
        public void Initialize(int Keys, int UpgradePoints, int ShieldTimer, int ShieldCooldown)
        {
            keys = Keys;
            points = UpgradePoints;
            shieldTimer = ShieldTimer;
            shieldCooldown = ShieldCooldown;
        }
        #endregion

        #region Methods
        //Keys
        public int KEYS
        {
            get { return keys; }
            set { this.keys = value; }
        }

        //UpgradePoints
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
        #endregion
    }
}
