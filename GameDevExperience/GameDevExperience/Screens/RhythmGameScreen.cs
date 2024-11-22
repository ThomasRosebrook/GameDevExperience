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
    public class RhythmGameScreen : GameScreen
    {
        private ContentManager _content;

        private double _secondsPerBeat;
        private Texture2D test;
        private Texture2D hitBoxTexture;
        private Color hitBoxColor;
        private double delayTimer;
        private bool hitWindowActive;
        private double actionTime;
        private double songTime;
        bool drawtest;
        double drawtime;
        SoundEffect correct;
        SoundEffect wrong;
        SoundEffect indicator;
        double total = 0;
        double score = 0;
        double count = 0;

        protected Song _song;
        protected Texture2D _background;
        protected Beatmap _beatMap;
        protected Color _backgroundColor = Color.CornflowerBlue;

        protected int beatDelay = 2;

        //Due to mirroring, these values should be half the total amount of time for each zone
        protected double greenZoneSize = 0.25;
        protected double yellowZoneSize = 0.125;

        public RhythmGameScreen()
        {
            hitBoxColor = Color.White;
            drawtest = false;
            drawtime = 0;
        }

        public override void Activate()
        {
            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            test = _content.Load<Texture2D>("test");
            correct = _content.Load<SoundEffect>("correct");
            wrong = _content.Load<SoundEffect>("wrong");

            indicator = _content.Load<SoundEffect>("indicator");
            hitBoxTexture = _content.Load<Texture2D>("test2");
            _background = _content.Load<Texture2D>("Programming");

            //MediaPlayer.Play(_song);

            songTime = 0;
            score = 0;
            //total = _beatMap.Actions.Count;
            base.Activate();
        }

        public override void Update(GameTime gameTime, bool unfocused, bool covered)
        {
            if (IsActive)
            {
                songTime += gameTime.ElapsedGameTime.TotalSeconds;
                //songTime = MediaPlayer.PlayPosition.TotalSeconds;

                // Check for actions in the beatmap
                foreach (var action in _beatMap.Actions)
                {
                    double currActionTime = action.Measure * 4 * _secondsPerBeat + (action.Beat - 1) * _secondsPerBeat;
                    if (Math.Abs(songTime - currActionTime) <= 0.005)
                    {
                        actionTime = currActionTime;
                        count++;
                        TriggerAction();
                        break;
                    }
                }

                if (delayTimer > 0)
                {
                    delayTimer -= gameTime.ElapsedGameTime.TotalSeconds;
                    if (delayTimer <= 0)
                    {
                        hitWindowActive = true;
                    }
                }
                if (drawtest)
                {
                    drawtime -= gameTime.ElapsedGameTime.TotalSeconds;
                    if (drawtime <= 0)
                    {
                        drawtest = false;
                        hitWindowActive = true;
                        delayTimer = _secondsPerBeat * beatDelay;
                        indicator.Play();
                    }
                }


                if (MediaPlayer.State != MediaState.Playing)
                {
                    MediaPlayer.Play(_song);
                    songTime = 0;
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

            if (hitWindowActive && (input.A || input.B))
            {
                double timeDelay = Math.Abs(songTime - actionTime - _secondsPerBeat * beatDelay);
                if (timeDelay <= greenZoneSize)
                {
                    hitBoxColor = Color.Green;
                    correct.Play();
                    score += 1;
                }
                else if (timeDelay > greenZoneSize && timeDelay <= greenZoneSize + yellowZoneSize)
                {
                    hitBoxColor = Color.Yellow;
                    correct.Play();
                    score += .5;
                }
                else
                {
                    hitBoxColor = Color.Red;
                    wrong.Play();
                }
                hitWindowActive = false;
            }
            else
            {
                hitBoxColor = Color.White;
            }
        }

        protected void LoadBeatmap(string path)
        {
            _beatMap = JsonSerializer.Deserialize<Beatmap>(File.ReadAllText(path));
            _secondsPerBeat = 60.0 / _beatMap.Bpm;
        }


        public void TriggerAction()
        {
            delayTimer = _secondsPerBeat * beatDelay;
            hitWindowActive = false;
            drawtest = true;
            drawtime = _secondsPerBeat;
            indicator.Play();
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(_backgroundColor);

            var spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();

            spriteBatch.Draw(_background, new Vector2(0, 0), Color.White);

            if (drawtest)
            {
                spriteBatch.Draw(test, new Rectangle(960 / 4, 540 / 4, 960 / 4, 540 / 4), Color.White);
            }
            spriteBatch.Draw(hitBoxTexture, new Rectangle(0, 0, 960 / 4, 540 / 4), hitBoxColor);

            //FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(150,300), Color.Yellow, $"Frames: {frame}");
            FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(10, 350), Color.Yellow, $"ActionTime: {actionTime}");
            FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(10, 400), Color.Yellow, $"SongTime: {songTime}");

            //FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(10, 450), Color.Yellow, $"Accuracy: {total / ((total - count) + score) * 100}%");

            spriteBatch.End();
        }
    }
}
