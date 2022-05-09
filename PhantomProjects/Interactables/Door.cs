using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhantomProjects.PlayerBullets;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhantomProjects.Interactables
{
    class Door
    {
        Texture2D doorOpen, doorClose, currentStatus;
        Vector2 position;
        public bool Active, canChangeScene;

        public int Width
        {
            get { return doorOpen.Width; }
        }

        public int Height
        {
            get { return doorOpen.Height; }
        }

        public void Initialize(ContentManager Content, Vector2 pos)
        {
            doorOpen = Content.Load<Texture2D>("Map\\OpenDoor");
            doorClose = Content.Load<Texture2D>("Map\\ClosedDoor");
            canChangeScene = false;
            Active = true;
            position = pos;


            currentStatus = doorClose;
        }

        public void Update(GameTime gameTime, Player p, GUI guiInfo)
        {
            if (Active == true)
            {
                Rectangle playerRectangle = new Rectangle(
                                        (int)p.Position.X,
                                        (int)p.Position.Y,
                                        50,
                                        50);

                Rectangle potionRectangle = new Rectangle(
                                          (int)position.X,
                                          (int)position.Y,
                                          Width,
                                          Height);

                if (guiInfo.KEYS >=1)
                {
                    currentStatus = doorOpen;

                    if (potionRectangle.Intersects(playerRectangle) && (Keyboard.GetState().IsKeyDown(Keys.F) ||
                        GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed))
                    {
                        canChangeScene = true;
                    }
                }
            }
        }

        public bool ReturnChangeSCene()
        {
            return canChangeScene;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currentStatus, position, Color.White);
        }
    }
}
