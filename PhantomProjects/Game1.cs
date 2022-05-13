using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PhantomProjects.States;

namespace PhantomProjects
{
    public class Game1 : Game
    {
        #region Declaration
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //To assign and check the current and next scene
        private State _currentState;
        private State _nexState;

        // bool for scenes
        bool scene1 = false, scene2 = false, gameOver = false, endGame = false;

        // variables forthe player, points, shield and weapon dmg
        int playerSelected, playerHealth, healthBar, upgradePoints, shieldCooldown, shieldDuration;
        bool shieldUpC, shieldUpD, weaponDamage;
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 640;
            graphics.ApplyChanges();


            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _currentState = new CompanyIntro(this, graphics.GraphicsDevice, Content);
        }

        protected override void Update(GameTime gameTime)
        {
            // if next scene is not null then transfer
            if (_nexState != null)
            {
                _currentState = _nexState;
                _nexState = null;
            }

            // Update the scene
            _currentState.Update(gameTime);
            _currentState.PostUpdate(gameTime);

            //Check whats the next scene
            CheckScenes();

            base.Update(gameTime);
        }

        //Method to change to next state
        public void ChangeState(State state)
        {
            _nexState = state;
        }

        public void CheckScenes()
        {
            // change to the next Scenes if the bool becomes true
            if (scene1 == true)
            {
                scene1 = false;
                _nexState = new GameLevel1(this, graphics.GraphicsDevice, Content);
            }

            if (scene2 == true)
            {
                scene2 = false;
                _nexState = new GameLevel2(this, graphics.GraphicsDevice, Content);
            }

            if (endGame == true)
            {
                endGame = false;
                _nexState = new EndGame(this, graphics.GraphicsDevice, Content);
            }

            if (gameOver == true)
            {
                gameOver = false;
                _nexState = new GameOver(this, graphics.GraphicsDevice, Content);
            }
        }

        // Save character selection
        public void SaveCharacterSelected(int player)
        {
            // 0 - Female, 1 - Male
            playerSelected = player;
        }

        // Methods that assign the next scene if they become true
        #region Check Scenes Methods
        public bool GoToLevelOne(bool move) => scene1 = move;
        public bool GoToLevelTwo(bool move) => scene2 = move;
        public bool GoToEndGame(bool move) => endGame = move;
        public bool GoToGameOver(bool move) => gameOver = move;
        #endregion

        //Method that saves all the important information accross all scenes
        public void SaveHealthAndUpgradePoints( int health, int Bar, int points, int shieldC, int shieldD, bool canUpShieldC, bool canUpShieldD, bool WeaponDamage) 
        { 
            playerHealth = health; 
            healthBar = Bar; 
            upgradePoints = points;
            shieldCooldown = shieldC;
            shieldDuration = shieldD;
            shieldUpC = canUpShieldC;
            shieldUpD = canUpShieldD;
            weaponDamage = WeaponDamage;
        }

        //Methods that return all the information stored to the scenes
        #region Return Data Methods
        public int ReturnPlayerSelected() { return playerSelected; }

        public int ReturnHealth() { return playerHealth; }
        public int ReturnHealthBar() { return healthBar; }

        public int ReturnPoints() { return upgradePoints; }
        public int ReturnShieldCooldown() { return shieldCooldown; }
        public int ReturnShieldDuration() { return shieldDuration; }

        public bool ReturnShieldUpC() { return shieldUpC; }
        public bool ReturnShieldUpD() { return shieldUpD; }

        public bool ReturnWeaponDMG() { return weaponDamage; }
        #endregion

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _currentState.Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
        }
    }
}
