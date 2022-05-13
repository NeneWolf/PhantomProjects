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


        SpriteFont buttonFont;

        Button continuegameButton, upgradeShieldDurationButton, upgradeShieldCooldownButton, upgradeWeaponDamageButton;
        GUI guiInfo;
        Shield shield;
        BulletManager Pbullets;
        private List<Component> _components;

        bool upgradePause = false;
        public bool DamageUpgraded = false;
        public bool DurationUpgraded = false;
        public bool CooldownUpgraded = false;
        #endregion

        public void Initialize(GraphicsDevice graphicsDevice, ContentManager content, Game1 Game, Shield Shield, BulletManager pbullets, bool canUpSC, bool canUpSD, bool canUpDmg)
        {
            _game = Game;
            _graphicsDevice = graphicsDevice;
            _content = content;
            shield = Shield;
            Pbullets = pbullets;

            DamageUpgraded = canUpDmg;

            canshieldCooldownUpgrade = canUpSC;
            canshieldDurationUpgrade = canUpSD;
            canWeaponDamageUpgrade = canUpDmg;

            mainBackground = content.Load<Texture2D>("PauseBackground");
            upgradeLogo = content.Load<Texture2D>("Menu\\CharacterUpgrades"); // To be changed

            //Abilities
            shieldCooldownUpgrade = content.Load<Texture2D>("GUI\\ShieldCooldownUp");
            shieldCooldownlocked = content.Load<Texture2D>("GUI\\ShieldCooldownLocked");

            shieldDurationUpgrade = content.Load<Texture2D>("GUI\\ShieldDurationUp");
            shieldDurationlocked = content.Load<Texture2D>("GUI\\ShieldDurationLocked");

            weaponDamageUpgrade = content.Load<Texture2D>("GUI\\WeaponDMGUpgrade");
            weaponDamagelocked = content.Load<Texture2D>("GUI\\WeaponDMGLocked");


            continueButton = content.Load<Texture2D>("Menu\\Continue");
            buttonFont = content.Load<SpriteFont>("GUI\\MenuFont");

            upgradeWeaponDamageButton = new Button(weaponDamagelocked, buttonFont)
            {
                Position = new Vector2(440, 280),
                Text = "",
            };
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

        public void Update(GameTime gameTime, GUI GuiInfo)
        {
            guiInfo = GuiInfo;
            if (upgradePause == true)
            {
                foreach (var component in _components)
                    component.Update(gameTime);
            }
        }

        public bool IsUpgradePause() { return upgradePause; }

        public bool setUpgradePauseMenu(bool pauseUpgrade) => upgradePause = pauseUpgrade;

        private void UpgradeWeaponDamageGame_Click(object sender, EventArgs e)
        {
            if (canWeaponDamageUpgrade = true && guiInfo.UPGRADEPOINTS > 300 && upgradeWeaponDamageButton._texture == weaponDamagelocked)
            {
                upgradeWeaponDamageButton._texture = weaponDamageUpgrade;
                guiInfo.UPGRADEPOINTS -= 300;
                canWeaponDamageUpgrade = false;
                DamageUpgraded = true;
            }
        }
        
        private void upgradeShieldDurationButton_Click(object sender, EventArgs e)
        {
            if (canshieldDurationUpgrade == true && guiInfo.UPGRADEPOINTS > 100 && upgradeShieldDurationButton._texture == shieldDurationlocked)
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
            if (canshieldCooldownUpgrade == true && guiInfo.UPGRADEPOINTS > 200 && upgradeShieldCooldownButton._texture == shieldCooldownlocked)
            {
                upgradeShieldCooldownButton._texture = shieldCooldownUpgrade;
                guiInfo.UPGRADEPOINTS -= 200;
                shield.UpdateCooldown(15);
                canshieldCooldownUpgrade = false;
                CooldownUpgraded = true;
            }
        }



        private void UpgradeUnPauseGame_Click(object sender, EventArgs e)
        {
            setUpgradePauseMenu(false);
        }

        public bool ReturnSC() { return canshieldCooldownUpgrade; }
        public bool ReturnSD() { return canshieldDurationUpgrade; }
        public bool ReturnDMG() { return canWeaponDamageUpgrade; }



        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (upgradePause == true)
            {
                spriteBatch.Draw(mainBackground, new Rectangle(0, 0, 1280, 650), Color.White);
                spriteBatch.Draw(upgradeLogo, new Rectangle(355, -100, 600, 600), Color.White);

                foreach (var component in _components)
                    component.Draw(gameTime, spriteBatch);
            }
        }
    }
}
