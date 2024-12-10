using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevExperience
{
    public class Graduate
    {
        public Texture2D Texture { get; private set; }

        public bool IsTimid { get; private set; } = false;


        public Vector2 Position = new Vector2();

        public Vector2 AnimationFrame = new Vector2();

        public Graduate (Texture2D texture, bool isTimid)
        {
            Texture = texture;
            IsTimid = isTimid;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, new Rectangle((int)AnimationFrame.X * 64, (int)AnimationFrame.Y * 64, 64, 64), Color.White);
        }

    }
}
