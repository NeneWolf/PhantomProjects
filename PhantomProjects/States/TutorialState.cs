﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using PhantomProjects.Player_;
using PhantomProjects.Interactables_;
using PhantomProjects.Enemy_;
using PhantomProjects.Explosion_;
using PhantomProjects.Map_;
using PhantomProjects.Menus_;

namespace PhantomProjects.States
{
    public class TutorialState : State
    {
        #region Tutorial- Declarations

        private SpriteBatch _spriteBatch;

        //-----------------------------------------
        //Camera & Map
        Camera camera;
        Map map;

        //-----------------------------------------
        // Moving Platforms
        PlatformManager platformManage = new PlatformManager();


        //Static background
        Texture2D mainBackground;

        //----------------------------------------
        // Player
        Player player = new Player();
        Texture2D profileF, profileM;

        //Shield
        Shield shield = new Shield();

        //-----------------------------------------
        //Interactables
        ItemManager itemManager = new ItemManager();
        Door door;

        //-----------------------------------------
        //Player Bullets
        Texture2D pBulletTexture;
        BulletManager pBullets = new BulletManager();

        //-----------------------------------------
        //Basic Enemy
        EnemyManager EnemyA = new EnemyManager();
        GraphicsDevice details;

        //-----------------------------------------
        //Basic Enemy Bullets
        Texture2D bulletETexture;
        BulletEManager BulletBeams = new BulletEManager();

        //-----------------------------------------
        // Explosion(Blood) / GUI 
        Texture2D vfx;
        //Controls all the explosion
        ExplosionManager VFX = new ExplosionManager();

        // G.U.I Details
        SpriteFont guiFont, guiFont2;
        Texture2D legand, shieldTimer;
        Texture2D keysGUI, pointsGUI;
        GUI guiInfo = new GUI();

        // Health bar
        Texture2D healthBarGUI;
        Rectangle healthRectangle;

        //------------ sounds ------------

        //Our Laser Sound and Instance
        private SoundEffect bulletSound;

        //Our Explosion Sound.
        private SoundEffect bloodSound;

        // Game Music.
        private Song gameMusic;
        Sounds SND = new Sounds();

        //Pause & Upgrade Menu 
        PauseMenu pauseMenu = new PauseMenu();
        UpgradeMenu upgradeMenu = new UpgradeMenu();
        private List<Component> _components;

        #endregion

        public TutorialState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            _spriteBatch = new SpriteBatch(graphicsDevice);
            details = graphicsDevice;

            CleanScene();

            // Set Map & Player
            map = new Map();
            pauseMenu.Initialize(graphicsDevice, content, _game);
            upgradeMenu.Initialize(graphicsDevice, content, _game, shield, pBullets, true, true, true);

            //Tiles / Map / Camera
            Tiles.Content = content;
            camera = new Camera(graphicsDevice.Viewport);

            #region Map1_Generator 
            
            map.Generate(new int[,]
            {
                //  64x30 Width // 64x20 Height
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 1, 2, 2, 2, 2, 2, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 5},
                { 2, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 5},
                { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2},
                { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2},
                { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2, 2},
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2}
                }, 64);

            mainBackground = content.Load<Texture2D>("background");

            // platforms 
            platformManage.CreatePlatforms(new Vector2(850, 880), content, true, 650, true);

            #endregion

            #region Player
            profileF = content.Load<Texture2D>("Player\\FemaleProfile");
            profileM = content.Load<Texture2D>("Player\\MaleProfile");
            player.Initialize(content, new Vector2(130, 1100),_game.ReturnPlayerSelected()); //130, 1100

            //Player Bullets
            pBulletTexture = content.Load<Texture2D>("EnemyA\\EnemyBullet");
            pBullets.Initialize(pBulletTexture);

            //Shield
            shield.Initialize(content);
            

            #endregion

            #region Basic Enemy

            //Enemy
            EnemyA.Initialize(details);
            EnemyA.CreateEnemy(new Vector2(960, 1100), content);

            #region Enemy Bullet
            bulletETexture = content.Load<Texture2D>("EnemyA\\EnemyBullet");
            BulletBeams.Initialize(bulletETexture, details);
            #endregion

            #region Explosives
            // EXPLOSSIONS
            vfx = content.Load<Texture2D>("GUI\\bloodEffect");
            VFX.Initialize(vfx, details);
            #endregion
            #endregion

            #region GUI 
            //FONTS
            guiFont = content.Load<SpriteFont>("GUI\\PixType");
            guiFont2 = content.Load<SpriteFont>("GUI\\GUIFont");

            // GUI
            legand = content.Load<Texture2D>("GUI\\legand");
            shieldTimer = content.Load<Texture2D>("Menu\\Button");
            keysGUI = content.Load<Texture2D>("GUI\\key");
            pointsGUI = content.Load<Texture2D>("GUI\\UpgradeCoin");
            healthBarGUI = content.Load<Texture2D>("GUI\\PlayerHealthBar");

            guiInfo.Initialize(0, 0, 0 ,0); // Set GUI with keys, upgrade points, shieldTimer


            #region UpgradePoint
            var buttonTexture = content.Load<Texture2D>("GUI\\UpgradeArrowTest");
            var buttonFont = content.Load<SpriteFont>("GUI\\MenuFont");

            var upgradebutton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, 580),
                Text = "",
            };

            upgradebutton.Click += UpgradePauseMenu_Click;

            _components = new List<Component>()
                {
                upgradebutton,
            };

            #endregion

            #endregion

            #region Interactables
            itemManager.SpawnKeyCard(content, new Vector2(1728, 500));
            itemManager.SpawnKeyCard(content, new Vector2(1550, 190));

            itemManager.SpawnPotion(content, new Vector2(1472, 1165));
            itemManager.SpawnPotion(content, new Vector2(1300, 835));

            door = new Door();
            door.Initialize(content, new Vector2(1600, -40));
            #endregion

            #region Game Sounds
            ////Sounds
            // Load the laserSound Effect and create the effect Instance
            bulletSound = content.Load<SoundEffect>("Sounds\\GunShot");

            // Load the laserSound Effect and create the effect Instance
            bloodSound = content.Load<SoundEffect>("Sounds\\BloodSound");

            // Load the game music
            gameMusic = content.Load<Song>("Sounds\\INGAMEMUSIC");
            SND.Initialize(bulletSound, bloodSound, null);
            MediaPlayer.Play(gameMusic);

            #endregion

        }
        
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            #region Draw the map 


            _spriteBatch.Begin(SpriteSortMode.Deferred,
                   BlendState.AlphaBlend,
                   null, null, null, null,
                   camera.Transform);

            // Main background
            _spriteBatch.Draw(mainBackground, new Rectangle(0, 0, 1920, 1280), Color.White);

            //Map 
            map.Draw(_spriteBatch);

            platformManage.DrawPlatfroms(_spriteBatch);

            #endregion

            //Interactable
            itemManager.DrawCollectibles(_spriteBatch);

            door.Draw(_spriteBatch);

            //Player Shield
            shield.Draw(_spriteBatch);

            //Player
            player.Draw(_spriteBatch);

            //Player Bullet
            pBullets.DrawBullets(_spriteBatch);

            //Enemy
            EnemyA.DrawEnemies(_spriteBatch);

            //Enemy Bullet
            BulletBeams.DrawBullet(_spriteBatch);

            //Explosions
            VFX.DrawExplosions(_spriteBatch);

            _spriteBatch.End();






            // Static GUI
            _spriteBatch.Begin();
            _spriteBatch.Draw(legand, new Rectangle(0, 0, 320, 100), Color.White);

            //
            if (_game.ReturnPlayerSelected() == 0)
            {
                _spriteBatch.Draw(profileF, new Vector2(0,0), Color.White);
                
            }
            else
                _spriteBatch.Draw(profileM, new Vector2(0, 0), Color.White);

            ////HealthGUI
            _spriteBatch.Draw(healthBarGUI, new Vector2(100, 10), healthRectangle, Color.White);

            /////Upgrade points
            _spriteBatch.Draw(pointsGUI, new Vector2(100, 40), Color.White);
            _spriteBatch.DrawString(guiFont, "" + guiInfo.UPGRADEPOINTS, new Vector2(150, 60), Color.White);

            /////keysGUI
            _spriteBatch.Draw(keysGUI, new Vector2(200, 40), Color.White);
            _spriteBatch.DrawString(guiFont, "" + guiInfo.KEYS, new Vector2(250, 60), Color.White);


            foreach (var component in _components)
                component.Draw(gameTime, _spriteBatch);

            //Shield Timer
            _spriteBatch.Draw(shieldTimer, new Vector2(1155, 600), Color.White);


            if (shield.Active == true)
            {
                _spriteBatch.DrawString(guiFont2, "Duration:" + guiInfo.SHIELDTIMER, new Vector2(1165, 610), Color.White);
            }
            else
            {
                _spriteBatch.DrawString(guiFont2, "Cooldown:" + guiInfo.SHIELDCOOLDOWN, new Vector2(1165, 610), Color.White);
            }



            upgradeMenu.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();






            _spriteBatch.Begin();

            pauseMenu.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            GameManager();

            pauseMenu.Update(gameTime);
            upgradeMenu.Update(gameTime, guiInfo);

            foreach (var component in _components)
                component.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                pauseMenu.setPauseMenu(true);
            }

            if (pauseMenu.IsPaused() == false && upgradeMenu.IsUpgradePause() == false)
            {
                // Remove at the end
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    _game.Exit();

                //Map
                #region MapCollision
                foreach (CollisionTiles tile in map.CollisionTiles)
                {
                    player.Collision(tile.Rectangle, map.Width, map.Height);
                    pBullets.Collision(tile.Rectangle, map.Width, map.Height);

                    camera.Update(player.Position, map.Width, map.Height);

                    foreach (EnemyA enemy in EnemyManager.enemyType1)
                    {
                        enemy.Collision(tile.Rectangle, map.Width, map.Height);
                    }
                    BulletBeams.Collision(tile.Rectangle, map.Width, map.Height);
                }
                #endregion

                //Platforms
                platformManage.UpdatePlatforms(gameTime, player, true);

                //Player
                player.Update(gameTime);
                pBullets.UpdateManagerBullet(gameTime, player, VFX, SND,upgradeMenu);
                shield.Update(gameTime, player, false, guiInfo);

                // Enemies & their bullets
                EnemyA.UpdateEnemy(gameTime, player, VFX, guiInfo, SND);
                BulletBeams.UpdateManagerBulletE(gameTime, player, VFX, SND);


                //Interactables
                itemManager.UpdateKey(gameTime, player, guiInfo);
                itemManager.UpdatePotion(gameTime, player, guiInfo);
                door.Update(gameTime, player, guiInfo, 2);

                //Explotions
                VFX.UpdateExplosions(gameTime);

                //GUI
                healthRectangle = new Rectangle(0, 0, player.BarHealth, 16);

                
            }
        }

        private void UpgradePauseMenu_Click(object sender, EventArgs e)
        {
            upgradeMenu.setUpgradePauseMenu(true);
        }

        void GameManager()
        {
            // Clean Level and change to Game Over
            if (player.Active == false)
            {
                CleanScene();
                _game.GoToGameOver(true);
            }

            if(door.canChangeScene == true)
            {
                var SRC = shield.ReturnC();
                var SRD = shield.ReturnD();
                var UMSRC = upgradeMenu.ReturnSC();
                var UMSRD = upgradeMenu.ReturnSD();
                var UMDamage = upgradeMenu.ReturnDMG();

                CleanScene();
                _game.SaveHealthAndUpgradePoints(player.Health, player.BarHealth, guiInfo.UPGRADEPOINTS, SRC, SRD, UMSRC, UMSRD, UMDamage);
                _game.GoToLevelOne(door.canChangeScene);
            }
        }

        public void CleanScene()
        {
            if(EnemyManager.enemyType1.Count > 0)
            {
                EnemyA.CleanEnemies();
            }

            if(ItemManager.Keycard.Count > 0 && ItemManager.Potions.Count > 0)
            {
                itemManager.RemoveCollectibles();
            }

            if(PlatformManager.platform.Count > 0)
            {
                platformManage.CleanPlatforms();
            }
        }
    }
}
