using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.IO;
using System.Text.Json;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using System.Runtime.CompilerServices;

namespace GameDevExperience.Screens
{
    public class RhythmGameScreen : GameScreen
    {
        public Song Song { get; protected set; }

        public string DisplaySong = "A Video Game";

        public string GameName { get; protected set; } = "None";

        protected ContentManager _content;
        protected double _secondsPerBeat;
        //protected Texture2D test;
        //protected Texture2D hitBoxTexture;
        protected Color hitBoxColor;
        protected bool hitWindowActive;
        
        bool drawtest;
        double drawtime;
        protected SoundEffect correct;
        protected SoundEffect wrong;
        protected SoundEffect indicator;
        int backgroundFrame = 0;
        Timer BackgroundTimer;

        protected Texture2D _background;
        protected Beatmap _beatMap;
        protected Color _backgroundColor = Color.CornflowerBlue;
        protected double actionTime;
        protected double songTime;
        protected double total = 0;
        protected double score = 0;
        protected double accuracy = 0;
        protected double count = 0;

        protected int beatDelay = 2;

        //Due to mirroring, these values should be half the total amount of time for each zone
        protected double greenZoneSize = 0.125;
        protected double yellowZoneSize = 0.125;

        protected double timeDelay = 0;

        protected bool debugMessage = false;

        public RhythmGameScreen()
        {
            hitBoxColor = Color.White;
            drawtest = false;
            drawtime = 0;
        }

        public override void Activate()
        {
            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            //test = _content.Load<Texture2D>("test");
            correct = _content.Load<SoundEffect>("correct");
            wrong = _content.Load<SoundEffect>("wrong");
            indicator = _content.Load<SoundEffect>("indicator");
            //hitBoxTexture = _content.Load<Texture2D>("test2");

            ActivateGame();

            //MediaPlayer.Play(_song);
            BackgroundTimer = new Timer(TimerUnit.Seconds, (float)_secondsPerBeat);
            BackgroundTimer.TimerAlertEvent += OnBackgroundTimerUpdate;

            songTime = 0;
            score = 0;
            //total = _beatMap.Actions.Count;
            
            base.Activate();
        }

        public virtual void LoadSong(ContentManager content)
        {

        }

        protected virtual void ActivateGame ()
        {

        }

        public override void Update(GameTime gameTime, bool unfocused, bool covered)
        {
            if (IsActive)
            {
                BackgroundTimer.Update(gameTime);
                songTime += gameTime.ElapsedGameTime.TotalSeconds;
                //songTime = MediaPlayer.PlayPosition.TotalSeconds;

                // Check for actions in the beatmap
                foreach (var action in _beatMap.Actions)
                {
                    double currActionTime = action.Measure * 4 * _secondsPerBeat + (action.Beat) * _secondsPerBeat;
                    if (Math.Abs(songTime - currActionTime) <= 0.005)
                    {
                        actionTime = currActionTime;
                        hitWindowActive = false;
                        drawtest = true;
                        drawtime = _secondsPerBeat * beatDelay;
                        indicator.Play();
                        break;
                    }
                }

                if (drawtest)
                {
                    drawtime -= gameTime.ElapsedGameTime.TotalSeconds;
                    if (drawtime <= 0.505 && drawtime >= 0.495) hitWindowActive = true;
                    else if (drawtime <= 0)
                    {
                        drawtest = false;
                        //hitWindowActive = false;
                        indicator.Play();
                    }
                }

                if (hitWindowActive) timeDelay = Math.Abs(songTime - actionTime - _secondsPerBeat * beatDelay);

                UpdateGame(gameTime);
                accuracy = score / count;

                if (MediaPlayer.State != MediaState.Playing)
                {
                    ScreenManager.AddScreen(new EndGameScreen(accuracy));
                    ScreenManager.RemoveScreen(this);
                    //MediaPlayer.Play(Song);
                    //songTime = 0;
                }
            }
        }

        public virtual void UpdateGame(GameTime gameTime)
        {

        }

        public override void Unload()
        {
            _content.Unload();
        }

        public override void HandleInput(GameTime gameTime, InputManager input)
        {
            if (input.Escape)
            {
                ScreenManager.AddScreen(new MainMenu());
                ScreenManager.RemoveScreen(this);
            }

            //hitBoxColor = Color.White;

            
            if (input.A) OnAPress();
            if (input.B) OnBPress();
        }

        protected virtual void OnAPress()
        {

        }

        protected virtual void OnBPress()
        {
            
        }

        protected virtual void OnSuccessfulHit ()
        {
            hitBoxColor = Color.Green;
            correct.Play();
            score += 1;
            count++;
        }

        protected virtual void OnHalfSuccessfulHit()
        {
            hitBoxColor = Color.Yellow;
            correct.Play();
            score += .5;
            count++;
        }

        protected virtual void OnFailedHit()
        {
            hitBoxColor = Color.Red;
            wrong.Play();
            count++;
        }

        protected void LoadBeatmap(string path)
        {
            _beatMap = JsonSerializer.Deserialize<Beatmap>(File.ReadAllText(path));
            _secondsPerBeat = 60.0 / _beatMap.Bpm;
        }

        private void OnBackgroundTimerUpdate(object obj, EventArgs e)
        {
            if (backgroundFrame == 0) backgroundFrame = 1;
            else backgroundFrame = 0;
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(_backgroundColor);

            var spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();

            spriteBatch.Draw(_background, new Vector2(0, 0), new Rectangle(960 * backgroundFrame, 0, 960, 540), Color.White);

            if (drawtest)
            {
                //spriteBatch.Draw(test, new Rectangle(960 / 4, 540 / 4, 960 / 4, 540 / 4), Color.White);
            }
            //spriteBatch.Draw(hitBoxTexture, new Rectangle(0, 0, 960 / 4, 540 / 4), hitBoxColor);
            DrawGame(spriteBatch);

            spriteBatch.End();
        }

        protected virtual void DrawGame(SpriteBatch spriteBatch)
        {

        }
    }
}
