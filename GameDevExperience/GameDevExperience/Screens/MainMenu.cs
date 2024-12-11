using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameDevExperience.Screens
{
    public class MainMenu : GameScreen
    {
        private ContentManager _content;

        private int width;
        private int height;

        int screenIndex = 0;

        List<MenuOption> options;

        public MainMenu()
        {

        }

        public override void Activate()
        {
            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            if (width <= 0) width = ScreenManager.GraphicsDevice.Viewport.Width;
            if (height <= 0) height = ScreenManager.GraphicsDevice.Viewport.Height;

            options = new List<MenuOption>()
            {
                new MenuOption("Select Song", new SongSelect()) { IsSelected = true },
                new MenuOption ("Endless Mode", new RhythmGameManager())
                //new MenuOption("Controls", new ControlScreen())
            };

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
            if (input.Escape)
            {
                ScreenManager.Game.Exit();
            }
            if (input.A)
            {
                if (options[screenIndex].IsScreenSelection)
                {
                    ScreenManager.AddScreen(options[screenIndex].Screen);
                    ScreenManager.RemoveScreen(this);
                }
            }
            if ((input.Up || input.Left) && screenIndex > 0)
            {
                options[screenIndex].IsSelected = false;
                screenIndex--;
                options[screenIndex].IsSelected = true;
            }
            if ((input.Down || input.Right) && screenIndex < options.Count - 1)
            {
                options[screenIndex].IsSelected = false;
                screenIndex++;
                options[screenIndex].IsSelected = true;
            }

        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.Black);

            var spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            string currentText = "The Game Dev Experience";
            Vector2 size = FontText.SizeOf(currentText, "PublicPixelLarge");
            FontText.DrawString(spriteBatch, "PublicPixelLarge", new Vector2(width / 2 - size.X / 2, 20), Color.LimeGreen, currentText);

            int i = 0;
            foreach (MenuOption option in options)
            {
                size = FontText.SizeOf(option.Name, "PublicPixelMedium");
                option.Draw(spriteBatch, new Vector2(width / 2 - size.X / 2, height / 2 - (options.Count * (size.Y + 50) / 2) + i * (size.Y + 50)));
                i++;
            }

            /*
            currentText = "Select Song";
            size = FontText.SizeOf(currentText, "PublicPixelMedium");
            FontText.DrawString(spriteBatch, "PublicPixelMedium", new Vector2(width / 2 - size.X / 2, (height - size.Y) / 2), Color.LimeGreen, currentText);
            */

            currentText = "Exit: ESC or Back";
            size = FontText.SizeOf(currentText, "PublicPixel");
            FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(width / 2 - size.X / 2, height - size.Y - 20), Color.LimeGreen, currentText);

            spriteBatch.End();
        }
    }
}
