using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhantomProjects.Player_;
using PhantomProjects.GUI_;

namespace PhantomProjects.Interactables_
{
    class Door
    {
        #region Declarations
        public Animation doorAnimation;
        Texture2D doorOpening, closeDoor, doorOpen, currentStatus;
        Vector2 position;

        bool canOpenDoor = true;

        float elapsed;
        float delay = 120f;
        int Frames = 0;
        public Rectangle doorRectangle, sourceRect;

        public bool Active, canChangeScene;

        public int Width
        {
            get { return doorAnimation.FrameWidth; }
        }

        public int Height
        {
            get { return doorAnimation.FrameHeight; }
        }
        #endregion

        #region Constructor
        //Door Constructor
        public void Initialize(ContentManager Content, Vector2 pos)
        {
            //Load textures
            closeDoor = Content.Load<Texture2D>("Map\\CloseDoor");
            doorOpening = Content.Load<Texture2D>("Map\\doorOpening");
            doorOpen = Content.Load<Texture2D>("Map\\doorOpen");

            //Initialize variables
            canChangeScene = false;
            Active = true;
            position = pos;

            //Create animation
            doorAnimation = new Animation();
            currentStatus = closeDoor;

        }
        #endregion

        #region Methods
        //Update Method
        public void Update(GameTime gameTime, Player p, GUI guiInfo, int AmountRequired)
        {
            if (Active == true)
            {
                doorAnimation.Update(gameTime);

                doorRectangle = new Rectangle((int)position.X, (int)position.Y, 300, 300);

                if (guiInfo.KEYS == AmountRequired)
                {
                    if (canOpenDoor == true)
                    {
                        currentStatus = doorOpening;
                        Animate(gameTime);
                    }
                    else
                    {
                        currentStatus = doorOpen;
                        Animate(gameTime);
                    }

                    //Check to see if the scene can be changed
                    if (doorRectangle.Intersects(p.RECTANGLE) && (Keyboard.GetState().IsKeyDown(Keys.F) ||
                        GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed))
                    {
                        canChangeScene = true;
                    }
                }
            }
        }

        //Return Method
        public bool ReturnChangeScene()
        {
            return canChangeScene;
        }

        //Animate Method
        void Animate(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delay)
            {
                if (Frames >= 6)
                {
                    Frames = 0;
                    canOpenDoor = false;
                }
                else
                {
                    Frames++;
                }
                elapsed = 0;
            }

            sourceRect = new Rectangle((Frames * 300), 0, 300, 300);
        }

        //Draw Method
        public void Draw(SpriteBatch spriteBatch)
        {
            if (currentStatus == closeDoor)
            {
                spriteBatch.Draw(currentStatus, doorRectangle, Color.White);

            }
            else
                spriteBatch.Draw(currentStatus, doorRectangle, sourceRect, Color.White);
        }
        #endregion
    }
}
