using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RgbJourney.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgbJourney.Controls
{
    public class LinkLabel : Control
    {
        public Color SelectedColor { get; set; } = Color.Red;

        public LinkLabel()
        {
            TabStop = true;
            HasFocus = false;
            Position = Vector2.Zero;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (HasFocus)
                spriteBatch.DrawString(SpriteFont, Text, Position, SelectedColor);
            else
                spriteBatch.DrawString(SpriteFont, Text, Position, Color);
        }

        public override void HandleInput(PlayerIndex playerIndex)
        {
            if (!HasFocus)
                return;

            if (InputHandler.KeyReleased(Keys.Enter))
                base.OnSelected(null);

            if (InputHandler.CheckMouseReleased(MouseButton.Left))
            {
                Size = SpriteFont.MeasureString(Text);

                var r = new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    (int)Size.X,
                    (int)Size.Y);

                if (r.Contains(InputHandler.MouseAsPoint()))
                    base.OnSelected(null);
            }
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
