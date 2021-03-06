using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using PhantomProjects.Interactables_;
using PhantomProjects.Explosion_;
using PhantomProjects.Player_;
using PhantomProjects.Enemy_;
using PhantomProjects.Map_;
using PhantomProjects.Menus_;
using PhantomProjects.GUI_;
using PhantomProjects.Decoration_;

namespace PhantomProjects.States
{
    public class TutorialState : State
    {
        #region Tutorial- Declarations

        private SpriteBatch _spriteBatch;

        //-----------------------------------------
        //Camera & Map
        Camera camera;
        Map map = new Map();

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

        //Player Bullets
        Texture2D pBulletTexture;
        BulletManager pBullets = new BulletManager();

        //-----------------------------------------
        //Interactables
        ItemManager itemManager = new ItemManager();
        Door door = new Door();

        //-----------------------------------------
        //Basic Enemy
        EnemyManager EnemyA = new EnemyManager();
        GraphicsDevice details;

        //Basic Enemy Bullets
        Texture2D bulletETexture;
        BulletEManager BulletBeams = new BulletEManager();

        //-----------------------------------------
        // Explosion(Blood)
        Texture2D vfx;

        //Controls all the explosion
        ExplosionManager VFX = new ExplosionManager();

        //-----------------------------------------
        // G.U.I. Details
        SpriteFont guiFont;
        Texture2D legand, shieldTimer;
        Texture2D keysGUI, pointsGUI;
        GUI guiInfo = new GUI();

        // Health bar
        Texture2D healthBarGUI;
        Rectangle healthRectangle;

        //-----------------------------------------
        // Sounds - Bullet
        private SoundEffect bulletSound;

        // Sounds - Explosion
        private SoundEffect bloodSound;

        // Sounds - Game Music
        private Song gameMusic;
        Sounds SND = new Sounds();

        //-----------------------------------------
        //Pause & Upgrade Menu 
        PauseMenu pauseMenu = new PauseMenu();
        UpgradeMenu upgradeMenu = new UpgradeMenu();

        //Button
        private List<Component> _components;

        //-----------------------------------------
        //Decoration - Light ( to be improved )
        Lights lightDecoration = new Lights();
        Lights lightDecoration2 = new Lights(); 
        Lights lightDecoration3 = new Lights();

        #endregion

        public TutorialState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            _spriteBatch = new SpriteBatch(graphicsDevice);
            details = graphicsDevice;

            //Clean any list before starting to load the level
            CleanScene();


            #region Pause & Upgrade Menu
            // Set the Payse & Upgrade Menu
            pauseMenu.Initialize(graphicsDevice, content, _game);
            upgradeMenu.Initialize(graphicsDevice, content, _game, shield, pBullets, true, true, true);
            #endregion

            #region Map / Tiles / Camera 

            //Tiles / Map / Camera
            Tiles.Content = content;
            camera = new Camera(graphicsDevice.Viewport);

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

            //Map background image
            mainBackground = content.Load<Texture2D>("Backgrounds\\background");

            // Platforms 
            platformManage.CreatePlatforms(new Vector2(850, 880), content, true, 650, true);

            #endregion

            #region Player
            player.Initialize(content, new Vector2(130, 1100),_game.ReturnPlayerSelected()); // Default spawn : 130, 1100

            //Player Bullets
            pBullets.Initialize(content);

            //Shield
            shield.Initialize(content);
            
            #endregion

            #region Basic Enemy

            //Baisc Enemy(A) 
            EnemyA.Initialize(details);
            EnemyA.CreateEnemy(new Vector2(960, 1100), content);

           // Enemy Bullet
            BulletBeams.Initialize(content, details);

            #endregion

            #region Blood Explosions
            // Blood Explosions
            vfx = content.Load<Texture2D>("GUI\\bloodEffect");
            VFX.Initialize(vfx, details);
            #endregion

            #region G.U.I. 
            //Character Profile
            profileF = content.Load<Texture2D>("Player\\FemaleProfile");
            profileM = content.Load<Texture2D>("Player\\MaleProfile");

            //Fonts
            guiFont = content.Load<SpriteFont>("GUI\\GUIFont");

            // Images
            legand = content.Load<Texture2D>("GUI\\legand");
            shieldTimer = content.Load<Texture2D>("GUI\\GUIBackground");
            keysGUI = content.Load<Texture2D>("GUI\\key");
            pointsGUI = content.Load<Texture2D>("GUI\\UpgradeCoin");
            healthBarGUI = content.Load<Texture2D>("GUI\\PlayerHealthBar");

            // Default initiation of GUI 
            guiInfo.Initialize(0, 0, 0 ,0); // 0 Keys, 0 Points, default shield duration, default shield cooldown

            #region UpgradePoint
            // Bottom right GUI, Upgrade Menu
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

            #region Interactables ( Potions/Keys/Door )
            //Keys
            itemManager.SpawnKeyCard(content, new Vector2(1728, 500));
            itemManager.SpawnKeyCard(content, new Vector2(1525, 190));

            //Potions
            itemManager.SpawnPotion(content, new Vector2(1472, 1165));
            itemManager.SpawnPotion(content, new Vector2(1300, 835));

            //Doors
            door.Initialize(content, new Vector2(1600, -40));
            #endregion

            #region Decoration
            // lights spawning...
            lightDecoration.Initialize(content, new Vector2(130, 1025));
            lightDecoration2.Initialize(content, new Vector2(530, 1025));
            lightDecoration3.Initialize(content, new Vector2(930, 1025));

            #endregion

            #region Game Sounds
            // Player & Basic Enemy bullet sound
            bulletSound = content.Load<SoundEffect>("Sounds\\GunShot");

            // Blood Sound
            bloodSound = content.Load<SoundEffect>("Sounds\\BloodSound");

            // Load the game music
            gameMusic = content.Load<Song>("Sounds\\INGAMEMUSIC");
            SND.Initialize(bulletSound, bloodSound, null);

            //Media ( music )
            MediaPlayer.Play(gameMusic);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.5f;

            #endregion

        }
        
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // First Game layer
            #region First Layer
            _spriteBatch.Begin(SpriteSortMode.Deferred,
                   BlendState.AlphaBlend,
                   null, null, null, null,
                   camera.Transform);

            #region Map / Background / Platforms
            // Main background
            _spriteBatch.Draw(mainBackground, new Rectangle(0, 0, 1920, 1280), Color.White);

            //Map 
            map.Draw(_spriteBatch);

            //platform
            platformManage.DrawPlatfroms(_spriteBatch);

            //lights
            lightDecoration.Draw(_spriteBatch);
            lightDecoration2.Draw(_spriteBatch);
            lightDecoration3.Draw(_spriteBatch);
            #endregion

            #region Interactable
            //Draw keys & potions
            itemManager.DrawCollectibles(_spriteBatch);

            door.Draw(_spriteBatch);
            #endregion

            #region Player
            //Player Shield
            shield.Draw(_spriteBatch);

            //Player
            player.Draw(_spriteBatch);

            //Player Bullet
            pBullets.DrawBullets(_spriteBatch);
            #endregion

            #region Basic Enemy
            //Enemy
            EnemyA.DrawEnemies(_spriteBatch);

            //Enemy Bullet
            BulletBeams.DrawBullet(_spriteBatch);
            #endregion

            //Explosions
            VFX.DrawExplosions(_spriteBatch);

            _spriteBatch.End();

            #endregion

            #region Second Layer -  GUI's
            // Static GUI
            _spriteBatch.Begin();

            //Background Image of the top GUI
            _spriteBatch.Draw(shieldTimer, new Rectangle(100, 35, 120, 30), Color.White);
            _spriteBatch.Draw(shieldTimer, new Rectangle(100, 70, 120, 30), Color.White);

            //Draw profile depending on the player choosen 
            if (_game.ReturnPlayerSelected() == 0)
            {
                _spriteBatch.Draw(profileF, new Vector2(0,0), Color.White);
                
            }
            else
                _spriteBatch.Draw(profileM, new Vector2(0, 0), Color.White);

            //HealthGUI
            _spriteBatch.Draw(healthBarGUI, new Vector2(100, 10), healthRectangle, Color.White);

            //Upgrade points
            _spriteBatch.Draw(pointsGUI, new Vector2(100, 25), Color.White);
            _spriteBatch.DrawString(guiFont, "" + guiInfo.UPGRADEPOINTS, new Vector2(150, 39), Color.White);

            //keys
            _spriteBatch.Draw(keysGUI, new Vector2(100, 60), Color.White);
            _spriteBatch.DrawString(guiFont, "" + guiInfo.KEYS, new Vector2(150, 75), Color.White);

            //Draw all buttons
            foreach (var component in _components)
                component.Draw(gameTime, _spriteBatch);

            //Background Image of the bottom right GUI ( Shield )
            _spriteBatch.Draw(shieldTimer, new Vector2(1145, 600), Color.White);

            //// Draw Duration if the shield is active / Draw Cooldown if the shield is not active
            if (shield.Active == true)
            {
                _spriteBatch.DrawString(guiFont, "Duration:" + guiInfo.SHIELDTIMER, new Vector2(1165, 610), Color.White);
            }
            else
            {
                _spriteBatch.DrawString(guiFont, "Cooldown:" + guiInfo.SHIELDCOOLDOWN, new Vector2(1165, 610), Color.White);
            }
            _spriteBatch.End();

            #endregion

            #region Third Layer - Upgrade & Pause Menu
            _spriteBatch.Begin();

            upgradeMenu.Draw(gameTime, _spriteBatch);
            pauseMenu.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();
            #endregion
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            // Game Manager - Manages the progress/data/scenes and communicates with Game1
            GameManager();

            //Update Pause & Upgrade Menu
            pauseMenu.Update(gameTime);
            upgradeMenu.Update(gameTime, guiInfo);

            if (pauseMenu.IsPaused() == true)
            {
                upgradeMenu.setUpgradePauseMenu(false);
            }

            //Update buttons
            foreach (var component in _components)
                component.Update(gameTime);

            //Check if player has pressed P to call pause Menu
            if (Keyboard.GetState().IsKeyDown(Keys.P) || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                pauseMenu.setPauseMenu(true);
            }

            // If Pause Menu or Upgrade Menu open, no other update will run, "pausing" the game
            if (pauseMenu.IsPaused() == false && upgradeMenu.IsUpgradePause() == false)
            {
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

                //Lights
                lightDecoration.Update(gameTime);
                lightDecoration2.Update(gameTime);
                lightDecoration3.Update(gameTime);

                //GUI - Player Health
                healthRectangle = new Rectangle(0, 0, player.BarHealth, 16);

                //Player
                player.Update(gameTime);
                pBullets.UpdateManagerBullet(gameTime, player, VFX, SND,null, upgradeMenu);
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

            }
        }

        private void UpgradePauseMenu_Click(object sender, EventArgs e)
        {
            if (pauseMenu.IsPaused() == false)
            {
                upgradeMenu.setUpgradePauseMenu(true);
            }
        }

        void GameManager()
        {
            // Clean Level and change to Game Over
            if (player.Active == false)
            {
                CleanScene();
                _game.GoToGameOver(true);
            }

            // Save all data in Game1 and Change State
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
            // Clean every list if they contain any items
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
