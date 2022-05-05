using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PhantomProjects
{
    class Animation
    {
        #region Declarations
        //image represeting the collection of images used for animation
        public Texture2D spriteStrip;
        //the scale used to display the sprite strip
        float scale;
        //the time since we last updated the frame
        int elapsedTime;
        //the time we display a frame until the next one 
        int frameTime;
        //the number of frames that the animation contains
        int frameCount;
        //the index of the current frame we are displaying
        int currentFrame;
        //thecolor of the frame we will be displaying
        Color color;
        //The area of the image strip we want to display
        Rectangle sourceRect = new Rectangle();
        //the area where we want to display the image strip in the game
        Rectangle destinationRect = new Rectangle();
        //width of a given frame
        public int FrameWidth;
        //Heigh of a given frame
        public int FrameHeight;
        //the state of the animation
        public bool Active;
        //Determines if the animation will keep playing or deactivate after one run
        public bool Looping;
        //Width of a given frame
        public Vector2 Position;

        private List<Rectangle> frames = new List<Rectangle>();
        #endregion

        public void Initialize(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int frameCount, int frameTime, Color color, float scale, bool looping)
        {
            this.color = color;
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;
            this.frameCount = frameCount;
            this.frameTime = frameTime;
            this.scale = scale;

            Looping = looping;
            Position = position;
            spriteStrip = texture;

            //Set the time to zero
            elapsedTime = 0;
            currentFrame = 0;

            //Set the Animation to active by default
            Active = true;

            //Grab the correct frame in the image strip by multiplying the currentFrame index by the frame width
            //This is the animation rectangle to be picked from the actual spritesheet

            sourceRect = new Rectangle(currentFrame * FrameWidth, 0, FrameWidth, FrameHeight);

            //This is for the rectangle animation to be played in the game world
            destinationRect = new Rectangle(
                (int)Position.X - (int)(FrameWidth * scale) / 2,
                (int)Position.Y - (int)(frameHeight * scale) / 2,
                (int)(FrameWidth * scale),
                (int)(frameHeight * scale)
                );

            //Adding Animation Sequence
            for (int x = 0; x < frameCount; x++)
            {
                frames.Add(new Rectangle(
                    (FrameWidth * x),
                    0,
                    frameWidth,
                    frameHeight));
            }
        }

        public void Update(GameTime gameTime)
        {
            //Do not update the game if we are not active
            if (Active == false) return;

            //Update the elapsed time
            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            //if the elapsed time is larger than the frame we need to switch frames
            if (elapsedTime > frameTime)
            {
                currentFrame++; // move to the next frame
                if (currentFrame == frameCount) // if the currentFrame is equal to frameCount reset currentFrame to zero
                {
                    currentFrame = 0;
                    if (Looping == false) //if we are not looping diactivate the animation
                        Active = false;
                }
                elapsedTime = 0;// reset the elapsed time to zero
            }

            sourceRect = frames[currentFrame];
            //this is for the Rectangle animation to be played in the game World
            destinationRect = new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                FrameWidth,
                FrameHeight);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                spriteBatch.Draw(spriteStrip, destinationRect, sourceRect, color);
            }
        }
    }
}
