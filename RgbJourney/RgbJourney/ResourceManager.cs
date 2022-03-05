using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgbJourney
{
    public class ResourceManager
    {
        public Texture2D RedTexture { get; set; }
        public Texture2D BlueTexture { get; set; }
        public Texture2D GreenTexture { get; set; }
        public Texture2D WhiteTexture { get; set; }
        public Texture2D PlayerTexture { get; set; }
        public Texture2D BackTexture { get; set; }

        public SpriteFont Font { get; set; }
        public SpriteFont WinFont { get; set; }


        private ContentManager _contentManager;

        public ResourceManager(GraphicsDevice graphicsDevice, ContentManager content)
        {
            _contentManager = content;

            RedTexture = _contentManager.Load<Texture2D>("RedTrap");

            BlueTexture = _contentManager.Load<Texture2D>("BlueTrap");

            GreenTexture = _contentManager.Load<Texture2D>("GreenTrap");

            WhiteTexture = _contentManager.Load<Texture2D>("WinCell");

            PlayerTexture = _contentManager.Load<Texture2D>("Player");

            BackTexture = _contentManager.Load<Texture2D>("BackTexture");

            Font = _contentManager.Load<SpriteFont>("PixelFont");
            WinFont = _contentManager.Load<SpriteFont>("WinFont");
        }
    }
}
