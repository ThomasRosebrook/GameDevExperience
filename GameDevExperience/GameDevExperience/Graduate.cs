using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevExperience
{
    public class Graduate
    {
        public static Texture2D NormalGradTexture;

        public static Texture2D TimidGradTexture;

        Texture2D Texture;

        public bool IsTimid = false;

        public Vector2 Position = new Vector2();

        public int AnimationFrame = 0;

        public Graduate (bool isTimid)
        {
            if (isTimid) Texture = TimidGradTexture;
            else Texture = NormalGradTexture;
            IsTimid = isTimid;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsTimid) spriteBatch.Draw(Texture, Position, new Rectangle(0, AnimationFrame * 64, 64, 64), Color.White);
            else spriteBatch.Draw(Texture, Position, new Rectangle(AnimationFrame * 64, 0, 64, 64), Color.White);
        }

    }
}
