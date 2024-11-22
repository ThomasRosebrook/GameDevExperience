using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Text.Json;
using Microsoft.Xna.Framework.Content;

namespace GameDevExperience.Screens
{
    public class BinaryBeats : GameScreen
    {
        private ContentManager _content;
        private Song _song;
        private Beatmap _beatMap;
        private double _secondsPerBeat;
        private Texture2D test;
        private Texture2D hitBoxTexture;
        private Color hitBoxColor;
        private double delayTimer;
        private bool hitWindowActive;
        private double actionTime;
        bool drawtest;
        double drawtime;
        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;

        public BinaryBeats()
        {
            hitBoxColor = Color.White;
            drawtest = false;
            drawtime = 0;
        }

        public override void Activate()
        {
            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            test = _content.Load<Texture2D>("test");
            _beatMap = JsonSerializer.Deserialize<Beatmap>(File.ReadAllText("test.json"));
            _secondsPerBeat = 60.0 / _beatMap.Bpm;
            _song = _content.Load<Song>("a-video-game");
            hitBoxTexture = _content.Load<Texture2D>("test2");
            MediaPlayer.Play(_song);
            base.Activate();
        }

        public override void Update(GameTime gameTime, bool unfocused, bool covered)
        {
            if (IsActive)
            {
                currentKeyboardState = Keyboard.GetState();
                double songTime = MediaPlayer.PlayPosition.TotalSeconds;

                // Check for actions in the beatmap
                foreach (var action in _beatMap.Actions)
                {
                    actionTime = action.Measure * 4 * _secondsPerBeat + (action.Beat - 1) * _secondsPerBeat;
                    if (Math.Abs(songTime - actionTime) < 0.005)
                    {
                        TriggerAction();
                        break;
                    }
                }

                // Handle the hit window logic
                if (hitWindowActive)
                {
                    double timeDifference = songTime - actionTime;

                    // Check if input is within the hit window
                    if (currentKeyboardState.IsKeyDown(Keys.A) || currentKeyboardState.IsKeyDown(Keys.B))
                    {
                        if (Math.Abs(timeDifference) <= _secondsPerBeat / 3)
                            hitBoxColor = Color.Green;
                        else if (Math.Abs(timeDifference) <= _secondsPerBeat / 2)
                            hitBoxColor = Color.Yellow;
                        else if (Math.Abs(timeDifference) <= _secondsPerBeat)
                            hitBoxColor = Color.Orange;
                        else
                            hitBoxColor = Color.Red;
                    }
                    else
                    {
                        hitBoxColor = Color.White;
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
                    }
                }

                previousKeyboardState = currentKeyboardState;
            }
        }

        public override void HandleInput(GameTime gameTime, InputManager input)
        {
            if (input.Escape)
            {
                ScreenManager.AddScreen(new MainMenu());
                ScreenManager.RemoveScreen(this);
            }
        }

        public void TriggerAction()
        {
            delayTimer = _secondsPerBeat * 2;
            hitWindowActive = false;
            drawtest = true;
            drawtime = _secondsPerBeat;
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.CornflowerBlue);

            var spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();

            if (drawtest)
            {
                spriteBatch.Draw(test, new Rectangle(960 / 4, 540 / 4, 960/4, 540/4), Color.White);
            }
            spriteBatch.Draw(hitBoxTexture, new Rectangle(0, 0, 960 / 4, 540 / 4), hitBoxColor);

            spriteBatch.End();
        }
    }
}
