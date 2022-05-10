using System;
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
using PhantomProjects.Boss_;
using PhantomProjects.Enemy_;
using PhantomProjects.Explosion_;
using PhantomProjects.Map_;

namespace PhantomProjects.States
{
    public class GameLevel2 : State
    {
        #region Tutorial- Declarations

        private SpriteBatch _spriteBatch;

        //-----------------------------------------
        //Camera & Map
        Camera camera;
        Map map;

        //Static background
        Texture2D mainBackground;

        //----------------------------------------
        // Player
        Player player;
        int playerHealth, playerBarHealth, playerUpgradePoints;

        //Shield
        Shield shield = new Shield();

        //-----------------------------------------
        //Interactables
        Keycard keycard;
        HealthPotion healthPotion1, healthPotion2, healthPotion3, healthPotion4;
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
        //Boss
        Boss boss;

        //-----------------------------------------
        //Fireball
        Texture2D fireballTexture;
        FireballManager Fireball = new FireballManager();

        //-----------------------------------------
        // Explosion(Blood) / GUI 
        Texture2D vfx;
        //Controls all the explosion
        ExplosionManager VFX = new ExplosionManager();

        // G.U.I Details
        SpriteFont guiFont, MenuFont;
        Texture2D legand, shieldTimer;
        Texture2D keysGUI, pointsGUI;
        GUI guiInfo = new GUI();

        // Health bar
        Texture2D healthBarGUI;
        Rectangle healthRectangle;

        //------------ sounds ------------

        //Bullet, Blood, Fireball sound
        private SoundEffect bulletSound, bloodSound, fireballSound;

        // Game Music.
        private Song gameMusic;
        Sounds SND = new Sounds();

        #endregion

        public GameLevel2(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            _spriteBatch = new SpriteBatch(graphicsDevice);
            details = graphicsDevice;

            // Set Map & Player
            map = new Map();

            //Tiles / Map / Camera
            Tiles.Content = content;
            camera = new Camera(graphicsDevice.Viewport);

            #region Map1_Generator 
            //  64x30 Width // 20x64 Height
            map.Generate(new int[,]
            {
                //  64x50 Width // 64x40 Height
                { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4},
                { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 3, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 3, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 3, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 5},
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 1, 0, 0, 0, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 1, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 2},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 1, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 1, 0, 0, 0, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 1, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 5},
                { 2, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 1, 0, 0, 0, 5},
                { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 5},
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
            }, 64);


            mainBackground = content.Load<Texture2D>("background");

            #endregion

            #region Player
            ReturnStoreData();

            player = new Player();
            player.Initialize(content, new Vector2(130, 2400)); //130, 2400

            //Reset to the previous level values
            player.Health = playerHealth;
            player.BarHealth = playerBarHealth;

            //Player Bullets
            pBulletTexture = content.Load<Texture2D>("EnemyA\\EnemyBullet");
            pBullets.Initialize(pBulletTexture);

            //Shield
            shield.Initialize(content);
            #endregion

            #region Basic Enemy

            //Enemy
            EnemyA.Initialize(details);
            EnemyA.CreateEnemy(new Vector2(2000, 2400), content);
            EnemyA.CreateEnemy(new Vector2(1250, 2400), content);
            EnemyA.CreateEnemy(new Vector2(2112, 1688), content);

            #region Enemy Bullet
            bulletETexture = content.Load<Texture2D>("EnemyA\\EnemyBullet");
            BulletBeams.Initialize(bulletETexture, details);
            #endregion

            #endregion

            #region Boss
            boss = new Boss();
            boss.Initialize(new Vector2(2250,1150), content);

            #region Fireball
            Fireball.Initialize(details,content);
            #endregion

            #endregion

            #region Explosives
            // EXPLOSSIONS
            vfx = content.Load<Texture2D>("GUI\\bloodEffect");
            VFX.Initialize(vfx, details);
            #endregion
            

            #region GUI 
            //FONTS
            guiFont = content.Load<SpriteFont>("GUI\\GUIFont");
            MenuFont = content.Load<SpriteFont>("GUI\\MenuFont");

            // GUI
            legand = content.Load<Texture2D>("GUI\\legand");
            shieldTimer = content.Load<Texture2D>("Menu\\Button");
            keysGUI = content.Load<Texture2D>("GUI\\key");
            pointsGUI = content.Load<Texture2D>("GUI\\UpgradeCoin");
            healthBarGUI = content.Load<Texture2D>("GUI\\PlayerHealthBar");

            guiInfo.Initialize(0, playerUpgradePoints, 0); // Set GUI with keys, upgrade points, shieldTimer

            #endregion

            #region Interactables

            keycard = new Keycard();
            keycard.Initialize(content, new Vector2(boss.position.X, boss.position.Y + 100));

            healthPotion1 = new HealthPotion();
            healthPotion2 = new HealthPotion();
            healthPotion3 = new HealthPotion();
            healthPotion4 = new HealthPotion();
            healthPotion1.Initialize(content, new Vector2(1900, 1745));
            healthPotion2.Initialize(content, new Vector2(1000, 1295));
            healthPotion3.Initialize(content, new Vector2(2665, 955));
            healthPotion4.Initialize(content, new Vector2(485, 955));

            door = new Door();
            door.Initialize(content, new Vector2(1565, 540));
            #endregion

            #region Game Sounds
            ////Sounds
            // Load the laserSound Effect and create the effect Instance 
            bulletSound = content.Load<SoundEffect>("Sounds\\GunShot");

            // Load the laserSound Effect and create the effect Instance
            bloodSound = content.Load<SoundEffect>("Sounds\\BloodSound");

            fireballSound = content.Load<SoundEffect>("Sounds\\Fire");

            // Load the game music
            gameMusic = content.Load<Song>("Sounds\\BOSS");
            SND.Initialize(bulletSound, bloodSound, fireballSound);
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
            _spriteBatch.Draw(mainBackground, new Rectangle(0, 0, 3200, 3000), Color.White);

            //Fireball
            Fireball.DrawFireball(_spriteBatch);

            //Map 
            map.Draw(_spriteBatch);

            #endregion

            //Interactable
            if (boss.Active == false)
                keycard.Draw(_spriteBatch);

            healthPotion1.Draw(_spriteBatch);
            healthPotion2.Draw(_spriteBatch);
            healthPotion3.Draw(_spriteBatch);
            healthPotion4.Draw(_spriteBatch);

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

            //Boss
            boss.Draw(_spriteBatch);

            //Explosions
            VFX.DrawExplosions(_spriteBatch);

            _spriteBatch.End();

            // Static GUI
            _spriteBatch.Begin();
            _spriteBatch.Draw(legand, new Vector2(0, 0), Color.White);

            /////Upgrade points
            _spriteBatch.Draw(pointsGUI, new Vector2(925, 20), Color.White);
            _spriteBatch.DrawString(guiFont, "" + guiInfo.UPGRADEPOINTS, new Vector2(985, 38), Color.White);

            /////keysGUI
            _spriteBatch.Draw(keysGUI, new Vector2(1095, 20), Color.White);
            _spriteBatch.DrawString(guiFont, "" + guiInfo.KEYS, new Vector2(1155, 38), Color.White);

            if (shield.Active == true)
            {
                /////ShieldTimer
                _spriteBatch.Draw(shieldTimer, new Vector2(1155, 600), Color.White);
                _spriteBatch.DrawString(guiFont, "Timer: " + guiInfo.SHIELDTIMER, new Vector2(1185, 610), Color.White);
            }

            ////HealthGUI
            _spriteBatch.Draw(healthBarGUI, new Vector2(10, 20), healthRectangle, Color.White);

            _spriteBatch.End();

        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game.Exit();

            UpdateMap();

            //Map
            #region MapCollision
            foreach (CollisionTiles tile in map.CollisionTiles)
            {
                player.Collision(tile.Rectangle, map.Width, map.Height);
                pBullets.Collision(tile.Rectangle, map.Width, map.Height);

                boss.Collision(tile.Rectangle, map.Width, map.Height);
                Fireball.Collision(tile.Rectangle, map.Width, map.Height);

                camera.Update(player.Position, map.Width, map.Height);

                foreach (EnemyA enemy in EnemyManager.enemyType1)
                {
                    enemy.Collision(tile.Rectangle, map.Width, map.Height);
                }
            }
            #endregion

            healthRectangle = new Rectangle(0, 0, player.BarHealth, 16);

            //Player
            player.Update(gameTime);
            pBullets.UpdateManagerBullet(gameTime, player, VFX, SND);
            pBullets.UpdateBullet(gameTime, player, boss, VFX, SND);
            shield.Update(gameTime, player, guiInfo);

            // Enemies & their bullets
            EnemyA.UpdateEnemy(gameTime, player, VFX, guiInfo, SND);
            BulletBeams.UpdateManagerBulletE(gameTime, player, VFX, SND);

            //Enemy & Fireball
            boss.Update(gameTime, player, guiInfo, SND);
            Fireball.UpdateManagerFireball(gameTime, player, VFX, SND);

            //Interactables

            if (boss.Active == false)
            { keycard.Update(gameTime, player, guiInfo); }
                
            healthPotion1.Update(gameTime, player);
            healthPotion2.Update(gameTime, player);
            healthPotion3.Update(gameTime, player);
            healthPotion4.Update(gameTime, player);
            door.Update(gameTime, player, guiInfo);

            //Explotions
            VFX.UpdateExplosions(gameTime);

            GameManager();

            
        }

        void ReturnStoreData()
        {
            playerHealth = _game.ReturnHealth();
            playerBarHealth = _game.ReturnHealthBar();
            playerUpgradePoints = _game.ReturnPoints();
        }

        void GameManager()
        {
            // Clean Level and change to Game Over
            if (player.Active == false)
            {
                EnemyA.CleanEnemies();
                boss.CleanBoss();
                _game.GoToGameOver(true);

            }

            if (door.canChangeScene == true)
            {
                EnemyA.CleanEnemies();
                boss.CleanBoss();
                _game.GoToEndGame(door.canChangeScene);
            }
        }

        void UpdateMap()
        {
            if ((player.Position.Y >= 1200 && player.Position.Y <= 1300) && player.Position.X > 1700) 
            {
                
            }
        }
    }
}
