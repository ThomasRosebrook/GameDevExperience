using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;

namespace GameDevExperience.Screens
{
    public class SongSelect : GameScreen
    {
        private ContentManager _content;

        private int width;
        private int height;

        List<RhythmGameScreen> PotentialGames;

        int prevGameIndex = -1;
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
                new BinaryBeats("a-video-game", "test.json", "A Video Game by moodmode") { ExitGameOnEnd = true },
                new BinaryBeats("funny-bgm", "Song2.json", "BGM Videogame Song by Sekuora") { ExitGameOnEnd = true },
                new DiplomaDash("a-video-game", "test.json", "A Video Game by moodmode") { ExitGameOnEnd = true }
            };

            foreach (RhythmGameScreen screen in PotentialGames)
            {
                screen.LoadSong(_content);
            }

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
                if (gameIndex != prevGameIndex)
                {
                    prevGameIndex = gameIndex;
                    if (gameIndex != PotentialGames.Count) MediaPlayer.Play(PotentialGames[gameIndex].Song);
                    else MediaPlayer.Stop();
                }
                
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
                if (gameIndex == PotentialGames.Count)
                {
                    ScreenManager.AddScreen(PotentialGames[RandomHelper.Next(PotentialGames.Count)]);
                }
                else
                {
                    ScreenManager.AddScreen(PotentialGames[gameIndex]);
                    ScreenManager.RemoveScreen(this);
                }
                
            }
            if (input.Right)
            {
                if (gameIndex < PotentialGames.Count)
                {
                    prevGameIndex = gameIndex;
                    gameIndex++;
                }
                else
                {
                    prevGameIndex = gameIndex;
                    gameIndex = 0;
                }
            }
            else if (input.Left)
            {
                if (gameIndex > 0)
                {
                    prevGameIndex = gameIndex;
                    gameIndex--;
                }
                else
                {
                    prevGameIndex = gameIndex;
                    gameIndex = PotentialGames.Count;
                }
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

            currentText = (gameIndex == PotentialGames.Count) ? "Random Game" : PotentialGames[gameIndex].GameName;
            size = FontText.SizeOf(currentText, "PublicPixelMedium");
            FontText.DrawString(spriteBatch, "PublicPixelMedium", new Vector2(width / 2 - size.X / 2, 125 + size.Y / 2), Color.LimeGreen, currentText);

            currentText = (gameIndex == PotentialGames.Count) ? "Random Song" : PotentialGames[gameIndex].DisplaySong;
            size = FontText.SizeOf(currentText, "PublicPixelMedium");
            FontText.DrawString(spriteBatch, "PublicPixelMedium", new Vector2(width / 2 - size.X / 2, 175 + size.Y / 2), Color.LimeGreen, currentText);

            currentText = "Use Arrow Keys to Cycle Songs\nA to Select";
            size = FontText.SizeOf(currentText, "PublicPixel");
            FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(width / 2 - size.X / 2, height - size.Y - 20), Color.LimeGreen, currentText);

            spriteBatch.End();
        }
    }
}
