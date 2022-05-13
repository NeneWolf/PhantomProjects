using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhantomProjects.GUI_;

namespace PhantomProjects
{
    public class Button : Component
    {
        #region Declarations

        private MouseState _currentMouse; //check the mouse state
        private SpriteFont _font; //set font
        private bool _isHovering; // check if the mouse is overing any button
        private MouseState _previousMouse; // field to compare with previous mouse state
        public Texture2D _texture; // button texture

        //Events
        public event EventHandler Click;
        public bool Clicked { get; private set; }

        public Color PenColour { get; set; } // font colour
        public Vector2 Position { get; set; } //button position

        // button rectangle based on the position and texture width/height
        public Rectangle Rectangle
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height); }
        }

        public string Text { get; set; }

        #endregion

        #region Constructor
        //ini of buttons method
        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;
            _font = font;
            PenColour = Color.White;
        }

        #endregion

        #region Methods
        //button draw method
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var colour = Color.White;

            if (_isHovering)
                colour = Color.Gray;

            spriteBatch.Draw(_texture, Rectangle, colour);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColour);
            }
        }

        public override void Update(GameTime gameTime)
        {
            //compare current and previous statements

            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                // set the hover to true , in order to change button "colour"
                _isHovering = true;
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
        #endregion
    }
}
