﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevExperience.Screens
{
    public class SongSelect : GameScreen
    {
        private ContentManager _content;

        private int width;
        private int height;

        List<RhythmGameScreen> PotentialGames;

        int gameIndex = 0;

        public SongSelect()
        {
            
        }

        public override void Activate()
        {
            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            if (width <= 0) width = ScreenManager.GraphicsDevice.Viewport.Width;
            if (height <= 0) height = ScreenManager.GraphicsDevice.Viewport.Height;

            PotentialGames = new List<RhythmGameScreen>()
            {
                new BinaryBeats("a-video-game", "test.json", "A Video Game by moodmode")
                //new BinaryBeats("", "Song2.json", "")
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
                ScreenManager.AddScreen(PotentialGames[gameIndex]);
                ScreenManager.RemoveScreen(this);
            }
            if (input.Right)
            {
                if (gameIndex < PotentialGames.Count - 1) gameIndex++;
                else gameIndex = 0;
            }
            else if (input.Left)
            {
                if (gameIndex > 0) gameIndex--;
                else gameIndex = PotentialGames.Count - 1;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.Black);

            var spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            string currentText = "Select Song";
            Vector2 size = FontText.SizeOf(currentText, "PublicPixelLarge");
            FontText.DrawString(spriteBatch, "PublicPixelLarge", new Vector2(width / 2 - size.X / 2, 20), Color.LimeGreen, currentText);

            currentText = PotentialGames[gameIndex].GameName;
            size = FontText.SizeOf(currentText, "PublicPixelMedium");
            FontText.DrawString(spriteBatch, "PublicPixelMedium", new Vector2(width / 2 - size.X / 2, 125 + size.Y / 2), Color.LimeGreen, currentText);

            currentText = PotentialGames[gameIndex].DisplaySong;
            size = FontText.SizeOf(currentText, "PublicPixelMedium");
            FontText.DrawString(spriteBatch, "PublicPixelMedium", new Vector2(width / 2 - size.X / 2, 175 + size.Y / 2), Color.LimeGreen, currentText);

            currentText = "Use Arrow Keys to Cycle Songs\nA to Select";
            size = FontText.SizeOf(currentText, "PublicPixel");
            FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(width / 2 - size.X / 2, height - size.Y - 20), Color.LimeGreen, currentText);

            spriteBatch.End();
        }
    }
}
