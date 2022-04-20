using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace RgbJourney.Controls
{
    public abstract class Control
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Position { get; set; }
        public object Value { get; set; }
        public bool HasFocus { get; set; }
        public bool Enabled { get; set; }
        public bool Visible { get; set; }
        public bool TabStop { get; set; }
        public SpriteFont SpriteFont { get; set; }
        public Color Color { get; set; }
        public string Type { get; set; }

        public event EventHandler Selected;

        public Control()
        {
            Color = Color.White;
            Enabled = true;
            Visible = true;
            SpriteFont = ControlManager.SpriteFont;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void HandleInput(PlayerIndex playerIndex);

        protected virtual void OnSelected(EventArgs e)
        {
            if(Selected != null)
            {
                Selected(this, e);
            }
        }

        public virtual Rectangle GetBounds()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
        }
    }
}
