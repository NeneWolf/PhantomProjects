using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhantomProjects.Player_;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhantomProjects.Interactables_
{
    class Door
    {
        public Animation doorAnimation;
        Texture2D OpenDoor, CloseDoor, OpenPDoor,  currentStatus;
        Vector2 position;

        bool canOpenDoor = true;

        float elapsed;
        float delay = 120f;
        int Frames = 0;
        public Rectangle doorRectangle, sourceRect;

        public bool Active, canChangeScene;

        public int Width
        {
            get { return CloseDoor.Width; }
        }

        public int Height
        {
            get { return CloseDoor.Height; }
        }

        public void Initialize(ContentManager Content, Vector2 pos)
        {
            CloseDoor = Content.Load<Texture2D>("Map\\CloseDoor");
            OpenDoor = Content.Load<Texture2D>("Map\\DoorAnim");
            OpenPDoor = Content.Load<Texture2D>("Map\\OpenDoor");

            canChangeScene = false;
            Active = true;
            position = pos;

            doorAnimation = new Animation();
            currentStatus = CloseDoor;

        }

        public void Update(GameTime gameTime, Player p, GUI guiInfo, int AmountRequired)
        {
            if (Active == true)
            {
                doorAnimation.Update(gameTime);

                doorRectangle = new Rectangle((int)position.X, (int)position.Y, Width, Height);

                if (guiInfo.KEYS == AmountRequired)
                {
                     if (canOpenDoor == true)
                    {
                        currentStatus = OpenDoor;
                        Animate(gameTime);
                    }
                    else
                    {
                        currentStatus = OpenPDoor;
                    }


                    if (doorRectangle.Intersects(p.RECTANGLE) && (Keyboard.GetState().IsKeyDown(Keys.F) ||
                        GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed))
                    {
                        canChangeScene = true;
                    }
                }
            }
        }

        public bool ReturnChangeScene()
        {
            return canChangeScene;
        }

        void Animate(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delay)
            {
                if (Frames >= 7)
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

        public void Draw(SpriteBatch spriteBatch)
        {
            if (currentStatus == OpenPDoor || currentStatus == CloseDoor)
            {
                spriteBatch.Draw(currentStatus, doorRectangle, Color.White);

            }else 
                spriteBatch.Draw(currentStatus, doorRectangle, sourceRect, Color.White);
        }
    }
}
