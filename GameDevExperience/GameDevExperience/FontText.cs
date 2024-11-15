using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameDevExperience
{
    public static class FontText
    {
        static Dictionary<string, SpriteFont> Fonts = new Dictionary<string, SpriteFont>();

        public static void AddFont(string fontName, SpriteFont font)
        {
            Fonts.Add(fontName, font);
        }

        public static Vector2 SizeOf(string text, string fontName)
        {
            return Fonts[fontName].MeasureString(text);
        }

        public static void DrawString(SpriteBatch spriteBatch, string fontName, Vector2 position, Color color, string text)
        {
            spriteBatch.DrawString(Fonts[fontName], text, position, color);
        }
    }
}
