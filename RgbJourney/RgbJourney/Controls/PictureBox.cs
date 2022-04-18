using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgbJourney.Controls
{
    public class PictureBox : Control
    {
        Texture2D Image { get; set; }
        Rectangle SourceRect { get; set; }
        Rectangle DestinationRect { get; set; }

        public PictureBox(Texture2D image, Rectangle destination)
        {
            Image = image;
            DestinationRect = destination;
            SourceRect = new Rectangle(0, 0, image.Width, image.Height);
            Color = Color.White;
        }

        public PictureBox(Texture2D image, Rectangle destination, Rectangle source)
        {
            Image = image;
            DestinationRect = destination;
            SourceRect = source;
            Color = Color.White;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, DestinationRect, SourceRect, Color);
        }

        public override void HandleInput(PlayerIndex playerIndex)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        public void SetPosition(Vector2 newPosition)
        {
            DestinationRect = new Rectangle(
                (int)newPosition.X,
                (int)newPosition.Y,
                SourceRect.Width,
                SourceRect.Height);
        }
    }
}
