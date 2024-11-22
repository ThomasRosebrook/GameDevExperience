using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using System;

namespace GameDevExperience
{
    public class BinaryBeats
    {
        private Song _song;
        private Beatmap _beatMap;
        private double _secondsPerBeat;
        //private double _currentSongTime;
        private Texture2D test;

        private bool drawTest;
        private double drawTime;

        public BinaryBeats(Song song, Beatmap beatMap)
        {
            _song = song;
            _beatMap = beatMap;
            _secondsPerBeat = 60.0 / _beatMap.Bpm;
            MediaPlayer.Play(_song);
            drawTest = false;
            drawTime = 0.0;
        }

        public void Update(GameTime gameTime)
        {
            double SongTime = MediaPlayer.PlayPosition.TotalSeconds;
            foreach (var action in _beatMap.Actions)
            {
                double actionTime = (action.Measure) * 4 * _secondsPerBeat + (action.Beat - 1) * _secondsPerBeat;
                if (Math.Abs(SongTime - actionTime) < 0.005)
                {
                    TriggerAction(action.ActionId);
                }
            }

            if (drawTest)
            {
                drawTime -= gameTime.ElapsedGameTime.TotalSeconds;
                if (drawTime <= 0)
                {
                    drawTest = false;
                }
            }

            if (MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(_song);
            }
        }


        public void LoadContent(ContentManager content)
        {
            test = content.Load<Texture2D>("test");
        }
        /// <summary>
        /// would be changes or edited or made public to trigger what ever thingy we need to do
        /// </summary>
        /// <param name="actionId"></param>
        private void TriggerAction(int actionId)
        {
            drawTest = true;
            drawTime = _secondsPerBeat;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (drawTest)
            {
                spriteBatch.Draw(test, new Vector2(100, 100), Color.White);
            }
        }
    }
}
