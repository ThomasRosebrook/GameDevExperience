using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.IO;
using System.Text.Json;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace GameDevExperience.Screens
{
    public class BinaryBeats : RhythmGameScreen
    {
        string songName = "a-video-game";
        string beatPath = "test.json";

        

        public BinaryBeats (string songName, string beatmapPath, string display)
        {
            this.songName = songName;
            beatPath = beatmapPath;
            DisplaySong = display;
            GameName = "Binary Beats";
        }

        protected override void ActivateGame()
        {
            LoadBeatmap(beatPath);
            
            Song = _content.Load<Song>(songName);
            _background = _content.Load<Texture2D>("Programming");
            total = _beatMap.Actions.Count;

            MediaPlayer.Play(Song);
        }

        public override void UpdateGame(GameTime gameTime)
        {

        }

        protected override void OnAPress()
        {
            if (hitWindowActive)
            {
                double timeDelay = Math.Abs(songTime - actionTime - _secondsPerBeat * beatDelay);
                if (timeDelay <= greenZoneSize)
                {
                    OnSuccessfulHit();
                }
                else if (timeDelay > greenZoneSize && timeDelay <= greenZoneSize + yellowZoneSize)
                {
                    OnHalfSuccessfulHit();
                }
                else
                {
                    OnFailedHit();
                }
                hitWindowActive = false;
            }
        }

        protected override void OnBPress()
        {
            if (hitWindowActive)
            {
                double timeDelay = Math.Abs(songTime - actionTime - _secondsPerBeat * beatDelay);
                if (timeDelay <= greenZoneSize)
                {
                    OnSuccessfulHit();
                }
                else if (timeDelay > greenZoneSize && timeDelay <= greenZoneSize + yellowZoneSize)
                {
                    OnHalfSuccessfulHit();
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
            hitBoxColor = Color.Green;
            correct.Play();
            score += 1;
        }

        protected override void OnHalfSuccessfulHit()
        {
            hitBoxColor = Color.Yellow;
            correct.Play();
            score += .5;
        }

        protected override void OnFailedHit()
        {
            hitBoxColor = Color.Red;
            wrong.Play();
        }

       protected override void DrawGame(SpriteBatch spriteBatch)
        {
            //FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(150,300), Color.Yellow, $"Frames: {frame}");
            FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(10, 350), Color.Yellow, $"ActionTime: {actionTime}");
            FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(10, 400), Color.Yellow, $"SongTime: {songTime}");

            //FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(10, 450), Color.Yellow, $"Accuracy: {total / ((total - count) + score) * 100}%");
        }
    }
}
