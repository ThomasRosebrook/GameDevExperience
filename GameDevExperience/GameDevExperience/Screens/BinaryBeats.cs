using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Reflection.Metadata;
using System.IO;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;


namespace GameDevExperience.Screens
{
    public class BinaryBeats : GameScreen
    {
        private ContentManager _content;

        private Song _song;
        private Beatmap _beatMap;
        private double _secondsPerBeat;
        //private double _currentSongTime;
        private Texture2D test;

        private bool drawTest;
        private double drawTime;

        private Texture2D test2;
        private Color test2Color;

        public BinaryBeats()
        {
            drawTest = false;
            drawTime = 0.0;
        }

        public override void Activate()
        {
            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            test = _content.Load<Texture2D>("test");
            _beatMap = JsonSerializer.Deserialize<Beatmap>(File.ReadAllText("test.json"));
            _secondsPerBeat = 60.0 / _beatMap.Bpm;
            _song = _content.Load<Song>("a-video-game");
            test2 = _content.Load<Texture2D>("test2");
            MediaPlayer.Play(_song);
            test2Color = Color.White;
            base.Activate();
        }

        public override void Update(GameTime gameTime, bool unfocused, bool covered)
        {
            if (IsActive)
            {
                double SongTime = MediaPlayer.PlayPosition.TotalSeconds;
                foreach (var action in _beatMap.Actions)
                {
                    double actionTime = action.Measure * 4 * _secondsPerBeat + (action.Beat - 1) * _secondsPerBeat;
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
        }

        public override void HandleInput(GameTime gameTime, InputManager input)
        {
            if (input.Escape)
            {
                ScreenManager.AddScreen(new MainMenu());
                ScreenManager.RemoveScreen(this);
            }

            if (input.A)
            {
                if (drawTest)
                {
                    test2Color = Color.Green;
                }
                else
                {
                    test2Color = Color.Red;
                }
            }

            if (input.B)
            {
                if (drawTest)
                {
                    test2Color = Color.Blue;
                }
                else
                {
                    test2Color = Color.Red;
                }
            }

            if (!input.A && !input.B)
            {
                test2Color = Color.White;
            }
           
        }

        /// <summary>
        /// would be changes or edited or made public to trigger what ever thingy we need to do
        /// </summary>
        /// <param name="actionId"></param>
        public void TriggerAction(int actionId)
        {
            drawTest = true;
            drawTime = _secondsPerBeat;
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.CornflowerBlue);

            var spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();
            if (drawTest)
            {
                spriteBatch.Draw(test, new Rectangle(0, 0, 960, 540), Color.White);
            }
            spriteBatch.Draw(test2, new Rectangle(0, 0, 960 /2, 540 / 2), test2Color);
            spriteBatch.End();
        }
    }
}
