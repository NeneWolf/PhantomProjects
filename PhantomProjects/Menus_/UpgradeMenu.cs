using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PhantomProjects.Player_;
using PhantomProjects.GUI_;

namespace PhantomProjects.Menus_
{
    class UpgradeMenu
    {
        #region Declarations
        Game1 _game;
        GraphicsDevice _graphicsDevice;
        ContentManager _content;

        //Static background
        Texture2D mainBackground, upgradeLogo;
        Texture2D shieldCooldownUpgrade, shieldDurationUpgrade;
        Texture2D shieldCooldownlocked, shieldDurationlocked, continueButton;
        Texture2D weaponDamageUpgrade, weaponDamagelocked;

        bool canshieldCooldownUpgrade, canshieldDurationUpgrade, canWeaponDamageUpgrade;

        SpriteFont buttonFont, titleFont;
        Button continuegameButton, upgradeShieldDurationButton, upgradeShieldCooldownButton, upgradeWeaponDamageButton;
        GUI guiInfo;
        Shield shield;
        BulletManager Pbullets;
        private List<Component> _components;

        bool upgradePause = false;
        public bool DurationUpgraded = false;
        public bool CooldownUpgraded = false;
        public bool DamageUpgraded = false;
        bool hasBeenUpgrade = false;
        #endregion

        #region Constructor
        public void Initialize(GraphicsDevice graphicsDevice, ContentManager content, Game1 Game, Shield Shield, BulletManager pbullets, bool canUpSC, bool canUpSD, bool canUpDmg)
        {
            //Initialize variables
            _game = Game;
            _graphicsDevice = graphicsDevice;
            _content = content;
            shield = Shield;
            Pbullets = pbullets;

            canshieldCooldownUpgrade = canUpSC;
            canshieldDurationUpgrade = canUpSD;
            canWeaponDamageUpgrade = canUpDmg;

            //Load textures
            mainBackground = content.Load<Texture2D>("Backgrounds\\PauseBackground");
            upgradeLogo = content.Load<Texture2D>("Menu\\CharacterUpgrades"); 

            //Ability Upgrade Icons
            shieldCooldownUpgrade = content.Load<Texture2D>("GUI\\ShieldCooldownUp");
            shieldCooldownlocked = content.Load<Texture2D>("GUI\\ShieldCooldownLocked");

            shieldDurationUpgrade = content.Load<Texture2D>("GUI\\ShieldDurationUp");
            shieldDurationlocked = content.Load<Texture2D>("GUI\\ShieldDurationLocked");

            weaponDamageUpgrade = content.Load<Texture2D>("GUI\\WeaponDMGUpgrade");
            weaponDamagelocked = content.Load<Texture2D>("GUI\\WeaponDMGLocked");

            //Button textures
            continueButton = content.Load<Texture2D>("Menu\\Continue");
            titleFont = content.Load<SpriteFont>("GUI\\MenuFont");
            buttonFont = content.Load<SpriteFont>("GUI\\GUIFont");


            //Create buttons for upgrades
            upgradeWeaponDamageButton = new Button(weaponDamagelocked, buttonFont)
            {
                Position = new Vector2(440, 280),
                Text = "",
            };
            //Carries out a method on button click
            upgradeWeaponDamageButton.Click += UpgradeWeaponDamageGame_Click;

            if (canWeaponDamageUpgrade == false)
                upgradeWeaponDamageButton._texture = weaponDamageUpgrade;


            upgradeShieldDurationButton = new Button(shieldDurationlocked, buttonFont)
            {
                Position = new Vector2(600, 280),
                Text = "",
            };

            if (canshieldDurationUpgrade == false)
                upgradeShieldDurationButton._texture = shieldDurationUpgrade;


            upgradeShieldDurationButton.Click += upgradeShieldDurationButton_Click;


            upgradeShieldCooldownButton = new Button(shieldCooldownlocked, buttonFont)
            {
                Position = new Vector2(760, 280),
                Text = "",
            };

            if (canshieldCooldownUpgrade == false)
                upgradeShieldCooldownButton._texture = shieldCooldownUpgrade;

            upgradeShieldCooldownButton.Click += upgradeShieldCooldownButton_Click;


            continuegameButton = new Button(continueButton, buttonFont)
            {
                Position = new Vector2(570, 535),
                Text = "",
            };
            continuegameButton.Click += UpgradeUnPauseGame_Click;

            _components = new List<Component>()
                {
                continuegameButton,
                upgradeShieldDurationButton,
                upgradeShieldCooldownButton,
                upgradeWeaponDamageButton
                };

        }
        #endregion

        #region Methods
        //Update Method
        public void Update(GameTime gameTime, GUI GuiInfo)
        {
            guiInfo = GuiInfo;
            if (upgradePause == true)
            {
                foreach (var component in _components)
                    component.Update(gameTime);
            }
        }

        //Check if game is paused because upgrade menu is open
        public bool IsUpgradePause() { return upgradePause; }

        public bool setUpgradePauseMenu(bool pauseUpgrade) => upgradePause = pauseUpgrade;

        //Methods for upgrades
        private void UpgradeWeaponDamageGame_Click(object sender, EventArgs e)
        {
            //Check if following conditions are met
            if (guiInfo.UPGRADEPOINTS >= 300 && upgradeWeaponDamageButton._texture == weaponDamagelocked)
            {
                upgradeWeaponDamageButton._texture = weaponDamageUpgrade;
                //Upgrade ability and deduct points
                guiInfo.UPGRADEPOINTS -= 300;
                canWeaponDamageUpgrade = false;
                DamageUpgraded = true;
            }
        }

        private void upgradeShieldDurationButton_Click(object sender, EventArgs e)
        {
            if (guiInfo.UPGRADEPOINTS >= 100 && upgradeShieldDurationButton._texture == shieldDurationlocked)
            {
                upgradeShieldDurationButton._texture = shieldDurationUpgrade;
                guiInfo.UPGRADEPOINTS -= 100;
                shield.UpdateDuration(7);
                canshieldDurationUpgrade = false;
                DurationUpgraded = true;
            }
        }

        private void upgradeShieldCooldownButton_Click(object sender, EventArgs e)
        {
            if (guiInfo.UPGRADEPOINTS >= 200 && upgradeShieldCooldownButton._texture == shieldCooldownlocked)
            {
                upgradeShieldCooldownButton._texture = shieldCooldownUpgrade;
                guiInfo.UPGRADEPOINTS -= 200;
                shield.UpdateCooldown(15);
                canshieldCooldownUpgrade = false;
                CooldownUpgraded = true;
            }
        }

        //Method to resume game
        private void UpgradeUnPauseGame_Click(object sender, EventArgs e)
        {
            setUpgradePauseMenu(false);
        }

        // allows the bullet itself to know if its dmg has been upgraded or not based on the button texture... not the best way but its working...
        public bool ActivateDamage()
        {
            if (upgradeWeaponDamageButton._texture == weaponDamageUpgrade)
            {
                return true;
            }
            else return false;
            
        }

        //Return relevant data to be saved
        public bool ReturnSC() { return canshieldCooldownUpgrade; }
        public bool ReturnSD() { return canshieldDurationUpgrade; }
        public bool ReturnDMG() { return canWeaponDamageUpgrade; }

        //Draw Method
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (upgradePause == true)
            {
                spriteBatch.Draw(mainBackground, new Rectangle(0, 0, 1280, 650), Color.White);
                spriteBatch.Draw(upgradeLogo, new Rectangle(355, -175, 600, 600), Color.White);

                #region Text
                spriteBatch.DrawString(titleFont, "Weapon", new Vector2(415, 220), Color.White);
                spriteBatch.DrawString(titleFont, "Shield", new Vector2(665, 220), Color.White);

                spriteBatch.DrawString(buttonFont, "300", new Vector2(450, 350), Color.White);
                spriteBatch.DrawString(buttonFont, "100", new Vector2(610, 350), Color.White);
                spriteBatch.DrawString(buttonFont, "200", new Vector2(770, 350), Color.White);
                #endregion

                foreach (var component in _components)
                    component.Draw(gameTime, spriteBatch);
            }
        }
        #endregion
    }
}
