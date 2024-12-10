using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevExperience.Screens
{
    public class DiplomaDash : RhythmGameScreen
    {
        string beatPath = "test.json";

        public DiplomaDash (string songName, string beatmapPath, string display) : base()
        {
            this.songName = songName;
            beatPath = beatmapPath;
            DisplaySong = display;
            GameName = "Diploma Dash";
            numBackroundFrames = 1;
        }

        protected override void ActivateGame()
        {
            LoadBeatmap(beatPath);

            LoadSong(_content);
            _background = _content.Load<Texture2D>("BackgroundDeploma");
            total = _beatMap.Actions.Count;

            MediaPlayer.Play(Song);
        }

        protected override void UpdateGame(GameTime gameTime)
        {
            if (hitWindowActive)
            {

            }
            //base.UpdateGame(gameTime);
        }


        protected override void OnAPress()
        {
            OnPress();
        }

        protected override void OnBPress()
        {
            OnPress();
        }

        private void OnPress()
        {
            if (hitWindowActive)
            {
                timeDelay = Math.Abs(songTime - actionTime - _secondsPerBeat * beatDelay);
                if (timeDelay <= greenZoneSize)
                {
                    OnSuccessfulHit();
                }
                else
                {
                    OnFailedHit();
                }
                hitWindowActive = false;
            }
        }

        protected override void OnSuccessfulHit()
        {
            base.OnSuccessfulHit();
        }

        protected override void OnFailedHit()
        {
            base.OnFailedHit();
        }

        protected override void DrawGame(SpriteBatch spriteBatch)
        {
            FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(10, 500), Color.Yellow, "Press A to hand out diplomas");

            string currentText = $"Accuracy: {(accuracy * 100).ToString("F2")}%";
            Vector2 size = FontText.SizeOf(currentText, "PublicPixel");
            FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(940 - size.X, 10), Color.Green, currentText);

            
        }
    }
}
