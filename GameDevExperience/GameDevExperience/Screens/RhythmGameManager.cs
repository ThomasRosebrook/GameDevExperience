using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GameDevExperience.Screens
{
    public class RhythmGameManager : GameScreen
    {
        public List<RhythmGameScreen> PotentialGames;

        int CurrentGameIndex = -1;

        double score = 0;

        private int width;
        private int height;

        public RhythmGameManager()
        {
            
        }

        public override void Activate()
        {
            base.Activate();
            if (width <= 0) width = ScreenManager.GraphicsDevice.Viewport.Width;
            if (height <= 0) height = ScreenManager.GraphicsDevice.Viewport.Height;

            PotentialGames = new List<RhythmGameScreen>()
            {
                new BinaryBeats("a-video-game", "test.json", "A Video Game by moodmode"),
                new BinaryBeats("funny-bgm", "Song2.json", "BGM Videogame Song by Sekuora"),
                new DiplomaDash("a-video-game", "test.json", "A Video Game by moodmode")
            };
        }

        public override void Update(GameTime gameTime, bool unfocused, bool covered)
        {
            if (CurrentGameIndex == -1)
            {
                CurrentGameIndex = RandomHelper.Next(PotentialGames.Count);
                PotentialGames[CurrentGameIndex].TotalScoreDisplay = "Total Score: 0";
                ScreenManager.AddScreen(PotentialGames[CurrentGameIndex]);
            }
            else if (!PotentialGames[CurrentGameIndex].IsSongRunning)
            {
                score += PotentialGames[CurrentGameIndex].accuracy;
                ScreenManager.RemoveScreen(PotentialGames[CurrentGameIndex]);
                CurrentGameIndex = RandomHelper.Next(PotentialGames.Count);
                PotentialGames[CurrentGameIndex].TotalScoreDisplay = $"Total Score: {(score * 10).ToString("F0")}";
                ScreenManager.AddScreen(PotentialGames[CurrentGameIndex]);
            }

        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.Gray);

            var spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();

            string currentText = "Loading...";
            Vector2 size = FontText.SizeOf(currentText, "PublicPixel");
            FontText.DrawString(spriteBatch, "PublicPixel", new Vector2((width - size.X) / 2, (height - size.Y) / 2), Color.White, currentText);

            spriteBatch.End();
        }
    }
}
