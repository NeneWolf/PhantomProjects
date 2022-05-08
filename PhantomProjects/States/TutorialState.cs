using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PhantomProjects.States
{
    public class TutorialState : State
    {
        #region Tutorial- Declarations

        private SpriteBatch _spriteBatch;

        //Camera & Map
        Camera camera;
        Map map;

        //Static background
        Texture2D mainBackground;
        //----------------------------------------
        // Player
        Player player;
        Texture2D playerRWalk, playerLWalk;

        //-----------------------------------------
        //Basic Enemy
        Texture2D rightWalk, leftWalk;
        EnemyManager EnemyA = new EnemyManager();
        GraphicsDevice details;

        //-----------------------------------------
        //Basic Enemy Bullets
        Texture2D bulletETexture;
        BulletEManager BulletBeams = new BulletEManager();

        //-----------------------------------------
        // Blood on hits.
        Texture2D vfx;
        //Controls all the explosion
        ExplosionManager VFX = new ExplosionManager();

        // G.U.I Details
        SpriteFont guiFont, MenuFont;
        Texture2D legand;
        Texture2D keysGUI, potionsGUI;

        // health bar
        Texture2D healthBarGUI;
        Rectangle healthRectangle;


        GUI guiInfo = new GUI();

        // ------------ sounds ------------

        //Our Laser Sound and Instance
        private SoundEffect bulletSound;

        //Our Explosion Sound.
        private SoundEffect bloodSound;

        // Game Music.
        private Song gameMusic;
        Sounds SND = new Sounds();

        #endregion


        public TutorialState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            player = new Player();

            // Set Map & Player
            map = new Map();

            _spriteBatch = new SpriteBatch(graphicsDevice);

            //Tiles / Map / Camera
            Tiles.Content = content;
            camera = new Camera(graphicsDevice.Viewport);

            #region Map1_Generator 
            //  64x30 Width // 20x64 Height
            map.Generate(new int[,]
{
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 5},
                { 2, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 5},
                { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
                { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2},
                { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2, 2},
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2}
                }, 64);

            mainBackground = content.Load<Texture2D>("background");

            #endregion

            //Player
            playerRWalk = content.Load<Texture2D>("Player\\MalePlayerRightWalk");
            playerLWalk = content.Load<Texture2D>("Player\\MalePlayerLefttWalk");
            player.Initialize(playerRWalk, playerLWalk, new Vector2(100,1100));

            //Enemies  -  Basic

            #region Enemy
            //Constructor
            details = graphicsDevice;
            leftWalk = content.Load<Texture2D>("EnemyA\\enemyALeft");
            rightWalk = content.Load<Texture2D>("EnemyA\\enemyARight");
            EnemyA.Initialize(rightWalk, leftWalk, details);

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
            guiFont = content.Load<SpriteFont>("GUI\\GUIFont");
            MenuFont = content.Load<SpriteFont>("GUI\\MenuFont");

            // GUI
            legand = content.Load<Texture2D>("GUI\\legand");
            keysGUI = content.Load<Texture2D>("GUI\\key");
            potionsGUI = content.Load<Texture2D>("GUI\\Potion");

            healthBarGUI = content.Load<Texture2D>("GUI\\PlayerHealthBar");

            guiInfo.Initialize(0, 0); // Set GUI with 0 keys, 0 potions, 100 HP ( taking direct value from the player )

            #endregion

            #region Game Sounds
            ////Sounds
            // Load the laserSound Effect and create the effect Instance
            bulletSound = content.Load<SoundEffect>("Sounds\\laserFire");

            // Load the laserSound Effect and create the effect Instance
            bloodSound = content.Load<SoundEffect>("Sounds\\BloodSound");

            // Load the game music
            gameMusic = content.Load<Song>("Sounds\\INGAMEMUSIC");
            SND.Initialize(bulletSound, bloodSound);
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

            #endregion

            //Enemy
            EnemyA.DrawEnemies(_spriteBatch);

            //Player
            player.Draw(_spriteBatch);

            //Enemy Bullet
            BulletBeams.DrawBullet(_spriteBatch);

            //Explosions
            VFX.DrawExplosions(_spriteBatch);

            _spriteBatch.End();


            _spriteBatch.Begin();
            // Static GUI

            _spriteBatch.Draw(legand, new Vector2(0, 0), Color.White);

            /////PotionGUI
            _spriteBatch.Draw(potionsGUI, new Vector2(925, 20), Color.White);
            _spriteBatch.DrawString(guiFont, "" + guiInfo.POTIONS, new Vector2(985, 38), Color.White);

            /////keysGUI
            _spriteBatch.Draw(keysGUI, new Vector2(1095, 20), Color.White);
            _spriteBatch.DrawString(guiFont, "" + guiInfo.KEYS, new Vector2(1155, 38), Color.Yellow);

            ////HealthGUI
            _spriteBatch.Draw(healthBarGUI, new Vector2(10, 20),healthRectangle, Color.White);

            _spriteBatch.End();

        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game.Exit();

            //Map
            #region MapCollision
            foreach (CollisionTiles tile in map.CollisionTiles)
            {
                player.Collision(tile.Rectangle, map.Width, map.Height);
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

            // Clean Level and change to Game Over
            if(player.Active == false) { 
                _game.GoToGameOver(true);
                EnemyA.CleanEnemies();
            }

            // Enemies & their bullets
            EnemyA.UpdateEnemy(gameTime, player, VFX, guiInfo, SND);
            BulletBeams.UpdateManagerBulletE(gameTime, player, VFX, SND);

            //Explotions
            VFX.UpdateExplosions(gameTime);

        }
    }
}
