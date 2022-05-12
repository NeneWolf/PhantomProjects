using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using PhantomProjects.Player_;
using PhantomProjects.States;

namespace PhantomProjects.Menus_
{
    class UpgradeMenu
    {
        Game1 _game;
        GraphicsDevice _graphicsDevice;
        ContentManager _content;

        //Static background
        Texture2D mainBackground, upgradeLogo;
        Texture2D shieldCooldownUpgrade, shieldDurationUpgrade, weaponDamageUpgrade, locked;
        bool canshieldCooldownUpgrade, canshieldDurationUpgrade;
        Texture2D buttonTexture;
        SpriteFont buttonFont;

        Button continuegameButton, upgradeShieldDurationButton, upgradeShieldCooldownButton, upgradeWeaponDamageButton;
        GUI guiInfo;
        Shield shield;
        BulletManager Pbullets;
        private List<Component> _components;

        bool upgradePause = false;

        public void Initialize(GraphicsDevice graphicsDevice, ContentManager content, Game1 Game, Shield Shield, BulletManager pbullets, bool canUpSC, bool canUpSD)
        {
            _game = Game;
            _graphicsDevice = graphicsDevice;
            _content = content;
            shield = Shield;
            Pbullets = pbullets;

            canshieldCooldownUpgrade = canUpSC; 
            canshieldDurationUpgrade = canUpSD;

            mainBackground = content.Load<Texture2D>("PauseBackground");
            upgradeLogo = content.Load<Texture2D>("Menu\\CharacterUpgrades"); // To be changed

            //Abilities
            shieldCooldownUpgrade = content.Load<Texture2D>("GUI\\AbilityUnlocked");
            shieldDurationUpgrade = content.Load<Texture2D>("GUI\\AbilityUnlocked");
            weaponDamageUpgrade = content.Load<Texture2D>("GUI\\AbilityUnlocked");
            locked = content.Load<Texture2D>("GUI\\AbilityLocked");


            buttonTexture = content.Load<Texture2D>("Menu\\button");
            buttonFont = content.Load<SpriteFont>("GUI\\MenuFont");

            upgradeShieldDurationButton = new Button(locked, buttonFont)
            {
                Position = new Vector2(540, 380),
                Text = "Duration",
            };

            if (canshieldDurationUpgrade == false)
                upgradeShieldDurationButton._texture = shieldDurationUpgrade;


            upgradeShieldDurationButton.Click += upgradeShieldDurationButton_Click;


            upgradeShieldCooldownButton = new Button(locked, buttonFont)
            {
                Position = new Vector2(700, 380),
                Text = "Cooldown",
            };

            if (canshieldCooldownUpgrade == false)
                upgradeShieldCooldownButton._texture = shieldCooldownUpgrade;

            upgradeShieldCooldownButton.Click += upgradeShieldCooldownButton_Click;

            //upgradeWeaponDamageButton = new Button(locked, buttonFont)
            //{
            //    Position = new Vector2(690, 380),
            //    Text = "",
            //};
            //upgradeWeaponDamageButton.Click += UpgradeWeaponDamageGame_Click;


            //
            continuegameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(540, 435),
                Text = "Continue",
            };
            continuegameButton.Click += UpgradeUnPauseGame_Click;

            _components = new List<Component>()
                {
                continuegameButton,
                upgradeShieldDurationButton,
                upgradeShieldCooldownButton
                //upgradeWeaponDamageButton
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

        private void upgradeShieldDurationButton_Click(object sender, EventArgs e)
        {
            if(canshieldDurationUpgrade == true && guiInfo.UPGRADEPOINTS > 100 && upgradeShieldDurationButton._texture == locked)
            {
                upgradeShieldDurationButton._texture = shieldCooldownUpgrade;
                guiInfo.UPGRADEPOINTS -= 100;
                shield.UpdateDuration(7);
                canshieldDurationUpgrade = false;
            }
        }

        private void upgradeShieldCooldownButton_Click(object sender, EventArgs e)
        {
            if (canshieldCooldownUpgrade == true && guiInfo.UPGRADEPOINTS > 200 && upgradeShieldCooldownButton._texture == locked)
            {
                upgradeShieldCooldownButton._texture = shieldDurationUpgrade;
                guiInfo.UPGRADEPOINTS -= 200;
                shield.UpdateCooldown(17);
                canshieldCooldownUpgrade = false;
            }
        }

        //private void UpgradeWeaponDamageGame_Click(object sender, EventArgs e)
        //{
        //    if (guiInfo.UPGRADEPOINTS > 300 && upgradeWeaponDamageButton._texture == locked)
        //    {
        //        upgradeWeaponDamageButton._texture = weaponDamageUpgrade;
        //        guiInfo.UPGRADEPOINTS -= 300;
        //        Pbullets.ChangeBulletDamage(100);
        //    }
        //}

        private void UpgradeUnPauseGame_Click(object sender, EventArgs e)
        {
            setUpgradePauseMenu(false);
        }

        public bool ReturnSC() { return canshieldCooldownUpgrade; }
        public bool ReturnSD() { return canshieldDurationUpgrade; }


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
