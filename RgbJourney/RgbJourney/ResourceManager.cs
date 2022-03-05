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

        private GraphicsDevice _graphicsDevice;
        private ContentManager _contentManager;

        public ResourceManager(GraphicsDevice graphicsDevice, ContentManager content)
        {
            _graphicsDevice = graphicsDevice;
            _contentManager = content;

            RedTexture = new Texture2D(_graphicsDevice, 1, 1);
            RedTexture.SetData(new Color[] { Color.LightCoral });

            BlueTexture = new Texture2D(_graphicsDevice, 1, 1);
            BlueTexture.SetData(new Color[] { Color.DodgerBlue });

            GreenTexture = new Texture2D(_graphicsDevice, 1, 1);
            GreenTexture.SetData(new Color[] { Color.LightGreen });

            WhiteTexture = new Texture2D(_graphicsDevice, 1, 1);
            WhiteTexture.SetData(new Color[] { Color.White });

            PlayerTexture = new Texture2D(graphicsDevice, 1, 1);
            PlayerTexture.SetData(new Color[] { Color.CadetBlue });

            BackTexture = new Texture2D(_graphicsDevice, 1, 1);
            BackTexture.SetData(new Color[] { Color.Black });

            Font = _contentManager.Load<SpriteFont>("Main");
        }
    }
}
