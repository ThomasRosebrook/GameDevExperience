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

        public Vector2 AnimationFrame = new Vector2();

        public Graduate (bool isTimid)
        {
            if (isTimid) Texture = TimidGradTexture;
            else Texture = NormalGradTexture;
            IsTimid = isTimid;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, new Rectangle((int)AnimationFrame.X * 64, (int)AnimationFrame.Y * 64, 64, 64), Color.White);
        }

    }
}
