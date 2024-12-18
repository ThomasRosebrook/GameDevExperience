﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevExperience.Screens
{
    public class EndGameScreen : GameScreen
    {
        private ContentManager _content;

        private int width;
        private int height;

        double accuracy = 0;


        Texture2D superbScreen;
        Texture2D winScreen;
        Texture2D loseScreen;

        int GameID = 0;

        //bool HasWon = false;

        public EndGameScreen(double accuracy, int gameID)
        {
            this.accuracy = accuracy;
            //HasWon = accuracy >= 0.6;
            GameID = gameID;
        }

        public override void Activate()
        {
            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            if (width <= 0) width = ScreenManager.GraphicsDevice.Viewport.Width;
            if (height <= 0) height = ScreenManager.GraphicsDevice.Viewport.Height;

            if (GameID == 0)
            {
                superbScreen = _content.Load<Texture2D>("Superb_Programming");
                winScreen = _content.Load<Texture2D>("Normal_Programming");
                loseScreen = _content.Load<Texture2D>("Bad_Programming");
            }
            else if (GameID == 1)
            {
                superbScreen = _content.Load<Texture2D>("Superb_Diploma");
                winScreen = _content.Load<Texture2D>("Normal_Diploma");
                loseScreen = _content.Load<Texture2D>("Bad_Diploma");
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

            
        }

        public override void HandleInput(GameTime gameTime, InputManager input)
        {
            if (input.Escape)
            {
                ScreenManager.AddScreen(new SongSelect());
                ScreenManager.RemoveScreen(this);
            }
            
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.Gray);

            var spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            Color textColor = Color.Red;
            string currentText = (GameID == 1) ? "FLUNKED!" : "100 ERRORS!";
            if (accuracy >= 0.8)
            {
                textColor = Color.LightBlue;
                currentText = (GameID == 1) ? "20 JOB OFFERS!" : "SUPERB CODING!";
            }
            else if (accuracy >= 0.6)
            {
                textColor = Color.LimeGreen;
                currentText = (GameID == 1) ? "YOU GRADUATED!" : "IT WORKS!";
            }

            Vector2 size = FontText.SizeOf(currentText, "PublicPixelLarge");
            FontText.DrawString(spriteBatch, "PublicPixelLarge", new Vector2(width / 2 - size.X / 2, 20), textColor, currentText);

            if (accuracy >= 0.8) spriteBatch.Draw(superbScreen, new Vector2(255, 120), Color.White);
            else if (accuracy >= 0.6) spriteBatch.Draw(winScreen, new Vector2(255, 120), Color.White);
            else spriteBatch.Draw(loseScreen, new Vector2(255, 120), Color.White);

            currentText = $"Accuracy: {(accuracy * 100).ToString("F2")}%";
            size = FontText.SizeOf(currentText, "PublicPixelMedium");
            FontText.DrawString(spriteBatch, "PublicPixelMedium", new Vector2(width / 2 - size.X / 2, height - size.Y * 2 - 40), textColor, currentText);

            currentText = "Press ESC to return to main menu";
            size = FontText.SizeOf(currentText, "PublicPixelMedium");
            FontText.DrawString(spriteBatch, "PublicPixelMedium", new Vector2(width / 2 - size.X / 2, height - size.Y - 20), textColor, currentText);

            spriteBatch.End();
        }
    }
}
