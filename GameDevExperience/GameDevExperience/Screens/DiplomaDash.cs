using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace GameDevExperience.Screens
{
    public class DiplomaDash : RhythmGameScreen
    {
        string beatPath = "test.json";

        Timer animationsTimer;

        bool gradMovement = false;
        Timer movementTimer;
        double timidTimer = 0;

        Graduate theGraduatedOne;
        Graduate onDeck;
        Graduate almostGrad;

        Texture2D markiplier;
        Vector2 markiplierAnimationFrame;

        bool firstFrame = true;

        Color testColor = Color.LimeGreen;

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
            beatDelay = 2;

            LoadSong(_content);
            _background = _content.Load<Texture2D>("BackgroundDeploma");
            Graduate.NormalGradTexture = _content.Load<Texture2D>("Normal_Grad_1");
            Graduate.TimidGradTexture = _content.Load<Texture2D>("Timid_Grad");
            markiplier = _content.Load<Texture2D>("Guy_2");
            total = _beatMap.Actions.Count;
            animationsTimer = new Timer(TimerUnit.Seconds, (float)_secondsPerBeat);
            animationsTimer.TimerAlertEvent += OnAnimationTimerUpdate;
            movementTimer = new Timer(TimerUnit.Seconds, 1f);
            movementTimer.TimerAlertEvent += OnMovementTimerUpdate;

            onDeck = new Graduate(false) { Position = new Vector2(0, 270) };
            almostGrad = new Graduate(false) { Position = new Vector2(64, 270) };
            theGraduatedOne = new Graduate(false) { Position = new Vector2(1000, 0) };
            markiplierAnimationFrame = new Vector2(0,1);
            
            //ADJUST DIFFICULTY HERE
            greenZoneSize = 0.11;

            MediaPlayer.Play(Song);
        }

        protected override void UpdateGame(GameTime gameTime)
        {
            //if (almostGrad.IsTimid) beatDelay = 1;
            //else beatDelay = 2;
            animationsTimer.Update(gameTime);
            if (hitWindowActive)
            {
                if (firstFrame)
                {
                    gradMovement = true;
                    firstFrame = false;
                    markiplierAnimationFrame.Y = 1;
                }
            }
            else firstFrame = true;

            if (gradMovement)
            {
                movementTimer.Update(gameTime);

                if (almostGrad.IsTimid)
                {
                    if (timidTimer >= _secondsPerBeat)
                    {
                        almostGrad.Position.X += (float)(384 * (2.2 + _secondsPerBeat) * gameTime.ElapsedGameTime.TotalSeconds);
                    }
                    else timidTimer += gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    almostGrad.Position.X += (float)(384 * (1) * gameTime.ElapsedGameTime.TotalSeconds);
                }

                
                onDeck.Position.X += (float)(32 * 1.5 * gameTime.ElapsedGameTime.TotalSeconds);
                int xdiff = (almostGrad.IsTimid) ? 27 : 25;

                if (almostGrad.Position.X + 64 - xdiff > 203) almostGrad.Position.Y = 225;
                else if (almostGrad.Position.X + 64 - xdiff > 184) almostGrad.Position.Y = 243;
                else if (almostGrad.Position.X + 64 - xdiff > 162) almostGrad.Position.Y = 258;

            }

            theGraduatedOne.Position.X += (float)(200 * _beatsPerSecond * gameTime.ElapsedGameTime.TotalSeconds);
            int xgraddiff = (theGraduatedOne.IsTimid) ? 27 : 25;

            if (theGraduatedOne.Position.X + xgraddiff > 718) theGraduatedOne.Position.Y = 270;
            else if (theGraduatedOne.Position.X + xgraddiff > 696) theGraduatedOne.Position.Y = 258;
            else if (theGraduatedOne.Position.X + xgraddiff > 677) theGraduatedOne.Position.Y = 243;
            //base.UpdateGame(gameTime);
            timeDelay = Math.Abs(songTime - actionTime - _secondsPerBeat * (beatDelay + 0.5));
            if (timeDelay <= greenZoneSize)
            {
                testColor = Color.LimeGreen;
            }
            else
            {
                testColor = Color.Red;
            }
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
                timeDelay = Math.Abs(songTime - actionTime - _secondsPerBeat * (beatDelay + 0.5));
                if (timeDelay <= greenZoneSize)
                {
                    OnSuccessfulHit();
                }
                else
                {
                    OnFailedHit();
                }
                hitWindowActive = false;
                markiplierAnimationFrame.Y = 0;
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

        private void OnAnimationTimerUpdate(object obj, EventArgs e)
        {
            if (onDeck.AnimationFrame == 0) onDeck.AnimationFrame = 1;
            else onDeck.AnimationFrame = 0;

            if (almostGrad.AnimationFrame == 0) almostGrad.AnimationFrame = 1;
            else almostGrad.AnimationFrame = 0;

            if (theGraduatedOne.AnimationFrame == 0) theGraduatedOne.AnimationFrame = 1;
            else theGraduatedOne.AnimationFrame = 0;

            if (markiplierAnimationFrame.X == 0) markiplierAnimationFrame.X = 1;
            else markiplierAnimationFrame.X = 0;
        }

        private void OnMovementTimerUpdate(object obj, EventArgs e)
        {
            gradMovement = false;
            theGraduatedOne = almostGrad;
            almostGrad = onDeck;
            if (currAction < _beatMap.Actions.Count - 2) onDeck = new Graduate(_beatMap.Actions[currAction + 1].ActionId == 2) { Position = new Vector2(0, 270) };
            else onDeck.Position.X = 1000;
            markiplierAnimationFrame.Y = 1;
            timidTimer = 0;
        }

        protected override void DrawGame(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(markiplier, new Vector2(416, 225), new Rectangle((int)markiplierAnimationFrame.X * 64, (int)markiplierAnimationFrame.Y * 64, 64, 64), Color.White);

            onDeck.Draw(spriteBatch);
            almostGrad.Draw(spriteBatch);
            theGraduatedOne.Draw(spriteBatch);

            FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(10, 10), Color.Yellow, "Press A to hand out diplomas");

            //FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(320, 20), testColor, "GREEN ZONE");

            string currentText = $"Accuracy: {(accuracy * 100).ToString("F2")}%";
            Vector2 size = FontText.SizeOf(currentText, "PublicPixel");
            FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(940 - size.X, 10), Color.Green, currentText);

            
        }
    }
}
