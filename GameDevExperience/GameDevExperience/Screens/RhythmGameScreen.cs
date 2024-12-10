using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.IO;
using System.Text.Json;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using System.Runtime.CompilerServices;
using System.Text;

namespace GameDevExperience.Screens
{
    public class RhythmGameScreen : GameScreen
    {
        public Song Song { get; protected set; }

        public string DisplaySong = "A Video Game";

        public string TotalScoreDisplay = "";

        public string GameName { get; protected set; } = "None";

        public bool IsSongRunning = false;
        public bool ExitGameOnEnd = false;

        protected ContentManager _content;
        protected double _secondsPerBeat;
        protected double _beatsPerSecond;
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
        public double accuracy = 1;
        protected double count = 0;

        protected int beatDelay = 2;

        //Due to mirroring, these values should be half the total amount of time for each zone
        protected double greenZoneSize = 0.125;
        protected double yellowZoneSize = 0.125;

        protected double timeDelay = 0;

        protected bool debugMessage = false;

        protected string songName = "a-video-game";

        protected int numBackroundFrames = 1;

        public RhythmGameScreen()
        {
            hitBoxColor = Color.White;
            drawtest = false;
            drawtime = 0;
        }

        /// <summary>
        /// DO NOT OVERRIDE THIS METHOD
        /// </summary>
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
            IsSongRunning = true;
            base.Activate();
        }

        /// <summary>
        /// This is the method to load in the song for the minigame
        /// The song name should be set in the game's creation
        /// </summary>
        /// <param name="content"></param>
        public void LoadSong(ContentManager content)
        {
            Song = content.Load<Song>(songName);
        }

        /// <summary>
        /// This is the method to override instead of the activate method
        /// </summary>
        protected virtual void ActivateGame ()
        {

        }

        /// <summary>
        /// DO NOT OVERRIDE THIS METHOD
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="unfocused"></param>
        /// <param name="covered"></param>
        public override void Update(GameTime gameTime, bool unfocused, bool covered)
        {
            if (IsActive && IsSongRunning)
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
                        if (count > 0) accuracy = score / count;
                        else accuracy = 1;
                        drawtime = _secondsPerBeat * beatDelay;
                        indicator.Play();
                        break;
                    }
                }

                if (drawtest)
                {
                    drawtime -= gameTime.ElapsedGameTime.TotalSeconds;
                    if (drawtime <= 0.505 && drawtime >= 0.495)
                    {
                        hitWindowActive = true;
                        count++;
                    }
                    else if (drawtime <= 0)
                    {
                        drawtest = false;
                        //hitWindowActive = false;
                        indicator.Play();
                    }
                }

                if (hitWindowActive) timeDelay = Math.Abs(songTime - actionTime - _secondsPerBeat * beatDelay);

                UpdateGame(gameTime);
                

                if (MediaPlayer.State != MediaState.Playing)
                {
                    IsSongRunning = false;
                    if (ExitGameOnEnd)
                    {
                        ScreenManager.AddScreen(new EndGameScreen(accuracy));
                        ScreenManager.RemoveScreen(this);
                    }
                    
                    //MediaPlayer.Play(Song);
                    //songTime = 0;
                }
            }
        }

        /// <summary>
        /// This is the method to override instead of the update method
        /// </summary>
        /// <param name="gameTime"></param>
        protected virtual void UpdateGame(GameTime gameTime)
        {

        }

        /// <summary>
        /// DO NOT OVERRIDE, this is the unload method
        /// </summary>
        public override void Unload()
        {
            _content.Unload();
        }

        /// <summary>
        /// This is where the input handlig takes place
        /// DO NOT OVERRIDE
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="input"></param>
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

        /// <summary>
        /// Method to be overrided by an inherited minigame
        /// Runs when the "A" button is pressed
        /// </summary>
        protected virtual void OnAPress()
        {

        }

        /// <summary>
        /// Method to be overrided by an inherited minigame
        /// Runs when the "B" button is pressed
        /// </summary>
        protected virtual void OnBPress()
        {
            
        }

        /// <summary>
        /// Generic method for successfully timed button presses
        /// To be overrided by minigame versions
        /// </summary>
        protected virtual void OnSuccessfulHit ()
        {
            hitBoxColor = Color.Green;
            correct.Play();
            score += 1;
            //count++;
        }

        /// <summary>
        /// Generic method for almost successfully timed button presses
        /// To be overrided by minigame versions
        /// </summary>
        protected virtual void OnHalfSuccessfulHit()
        {
            hitBoxColor = Color.Yellow;
            correct.Play();
            score += .5;
            //count++;
        }

        /// <summary>
        /// Generic method for unsuccessfully timed button presses
        /// To be overrided by minigame versions
        /// </summary>
        protected virtual void OnFailedHit()
        {
            hitBoxColor = Color.Red;
            wrong.Play();
            //count++;
        }

        /// <summary>
        /// Loads in a beatmap from a specific path
        /// </summary>
        /// <param name="path">The path to the beatmap</param>
        protected void LoadBeatmap(string path)
        {
            _beatMap = JsonSerializer.Deserialize<Beatmap>(File.ReadAllText(path));
            _secondsPerBeat = 60.0 / _beatMap.Bpm;
            _beatsPerSecond = _beatMap.Bpm / 60;
        }

        /// <summary>
        /// Method to update the background frame when the background timer hits its defined interval
        /// </summary>
        /// <param name="obj">Just ignore</param>
        /// <param name="e">ignore this, doesn't matter</param>
        private void OnBackgroundTimerUpdate(object obj, EventArgs e)
        {
            if (backgroundFrame < numBackroundFrames - 1) backgroundFrame++;
            else backgroundFrame = 0;
        }

        /// <summary>
        /// This is the method to draw the minigame. DO NOT OVERRIDE THIS, USE THE DrawGame METHOD FOR DRAWING INHERITED MINIGAMES
        /// </summary>
        /// <param name="gameTime">Game time</param>
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

            Vector2 size = FontText.SizeOf(TotalScoreDisplay, "PublicPixel");
            FontText.DrawString(spriteBatch, "PublicPixel", new Vector2((960 - size.X) / 2 - 2, size.Y + 10), Color.Black, TotalScoreDisplay);
            FontText.DrawString(spriteBatch, "PublicPixel", new Vector2((960 - size.X) / 2, size.Y + 12), Color.White, TotalScoreDisplay);

            spriteBatch.End();
        }

        /// <summary>
        /// Override this method in inherited minigames. This will serve as the draw method for inherited classes. This method is called in the draw method of the RhythmGameScreen class.
        /// </summary>
        /// <param name="spriteBatch">the spritebatch from the draw method</param>
        protected virtual void DrawGame(SpriteBatch spriteBatch)
        {

        }
    }
}
