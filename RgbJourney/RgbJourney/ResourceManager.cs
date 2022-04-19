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
        public Texture2D SubHighlightTexture { get; set; }
        public Texture2D SumHighlightTexture { get; set; }
        public Texture2D GoldMedal { get; set; }
        public Texture2D SilverMedal { get; set; }
        public Texture2D BronzeMedal { get; set; }


        public SpriteFont Font { get; set; }
        public SpriteFont WinFont { get; set; }

        public ResourceManager()
        {
        }

        public void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
        {
            RedTexture = content.Load<Texture2D>("Red2");
            BlueTexture = content.Load<Texture2D>("Blue2");
            GreenTexture = content.Load<Texture2D>("Green2");
            WhiteTexture = content.Load<Texture2D>("WinCell");

            PlayerTexture = content.Load<Texture2D>("Player");

            BackTexture = content.Load<Texture2D>("BackTexture");

            SubHighlightTexture = new Texture2D(graphicsDevice, 1, 1);
            SubHighlightTexture.SetData(new Color[] { new Color(Color.CornflowerBlue, 1f) });

            SumHighlightTexture = new Texture2D(graphicsDevice, 1, 1);
            SumHighlightTexture.SetData(new Color[] { new Color(Color.DarkRed, 1f) });

            GoldMedal = new Texture2D(graphicsDevice, 1, 1);
            GoldMedal.SetData(new Color[] { new Color(Color.Gold, 1f) });

            SilverMedal = new Texture2D(graphicsDevice, 1, 1);
            SilverMedal.SetData(new Color[] { new Color(Color.Silver, 1f) });

            BronzeMedal = new Texture2D(graphicsDevice, 1, 1);
            BronzeMedal.SetData(new Color[] { new Color(Color.Brown, 1f) });

            Font = content.Load<SpriteFont>("PixelFont");
            WinFont = content.Load<SpriteFont>("WinFont");
        }
    }
}
