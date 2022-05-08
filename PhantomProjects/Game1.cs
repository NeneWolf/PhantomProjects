using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhantomProjects.States;

namespace PhantomProjects
{
    public class Game1 : Game
    {
        
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private State _currentState;

        private State _nexState;

        bool scene2 = false, scene3 = false, gameOver = false, endGame = false;

        public void ChangeState(State state)
        {
            _nexState = state;
        }

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

            _currentState = new MenuState(this, graphics.GraphicsDevice, Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (_nexState != null)
            {
                _currentState = _nexState;
                _nexState = null;
            }
            _currentState.Update(gameTime);
            _currentState.PostUpdate(gameTime);

            CheckScenes();

            base.Update(gameTime);
        }

        public void CheckScenes()
        {
            // change to the other Scenes if the bool becomes true
            if (scene2 == true)
            {
                scene2 = false;
                _nexState = new GameLevel1(this, graphics.GraphicsDevice, Content);
            }

            if (scene3 == true)
            {
                scene3 = false;
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
        public bool GoToLevelSecond(bool move) => scene2 = move;
        public bool GoToLevelThird(bool move) => scene3 = move;
        public bool GoToEndGame(bool move) => endGame = move;
        public bool GoToGameOver(bool move) => gameOver = move;

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _currentState.Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
        }
    }
}
