﻿using Microsoft.Xna.Framework;

namespace PhantomProjects.Map_
{
    static class RectangleHelper
    {
        #region Methods
        //Methods to check if the sides of this rectangle is colliding with any side of another rectangle
        public static bool TouchTopOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Bottom >= r2.Top - 1
                    && r1.Bottom <= r2.Top + (r2.Height / 2)
                    && r1.Right >= r2.Left + (r2.Width / 5)
                    && r1.Left <= r2.Right - (r2.Width / 5));
        }

        public static bool TouchBottomOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Top <= r2.Bottom + (r2.Height / 5)
                    && r1.Top >= r2.Bottom - 1
                    && r1.Right >= r2.Left + (r2.Width / 5)
                    && r1.Left <= r2.Right - (r2.Width / 5));
        }

        public static bool TouchLeftOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Right <= r2.Right
                    && r1.Right >= r2.Left - 5
                    && r1.Top <= r2.Bottom - (r2.Width / 4)
                    && r1.Bottom >= r2.Top + (r2.Width / 4));
        }

        public static bool TouchRightOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Left >= r2.Left
                    && r1.Left <= r2.Right + 5
                    && r1.Top <= r2.Bottom - (r2.Width / 4)
                    && r1.Bottom >= r2.Top + (r2.Width / 4));
        }
        #endregion
    }
}
