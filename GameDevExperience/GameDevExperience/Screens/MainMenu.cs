using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
namespace GameDevExperience.Screens
{
    public class MainMenu : GameScreen
    {


        private ContentManager _content;

        private int width;
        private int height;

        public MainMenu()
        {

        }

        public override void Activate()
        {
            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            if (width <= 0) width = ScreenManager.GraphicsDevice.Viewport.Width;
            if (height <= 0) height = ScreenManager.GraphicsDevice.Viewport.Height;

            base.Activate();
        }

        public override void Unload()
        {
            _content.Unload();
        }

        public override void Update(GameTime gameTime, bool unfocused, bool covered)
        {
            base.Update(gameTime, unfocused, covered);

            if (IsActive)
            {

            }
        }

        public override void HandleInput(GameTime gameTime, InputManager input)
        {
            base.HandleInput(gameTime, input);

            if (input.Escape)
            {
                ScreenManager.Game.Exit();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 0, 0);

            var spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            string currentText = "The Game Dev Experience";
            Vector2 size = FontText.SizeOf(currentText, "PublicPixel");
            FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(width / 2 - size.X / 2, 20), Color.LimeGreen, currentText);

            currentText = "Exit: ESC or Back";
            size = FontText.SizeOf(currentText, "PublicPixel");
            FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(width / 2 - size.X / 2, height - size.Y - 20), Color.LimeGreen, currentText);

            spriteBatch.End();
        }
    }
}
