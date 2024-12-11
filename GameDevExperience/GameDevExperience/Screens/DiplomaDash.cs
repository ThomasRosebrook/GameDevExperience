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

        Timer animationsTimer;

        bool gradMovement = false;
        Timer movementTimer;

        List<Graduate> Graduates;

        bool firstFrame = true;

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
            Graduate.NormalGradTexture = _content.Load<Texture2D>("Normal_Grad_1");
            Graduate.TimidGradTexture = _content.Load<Texture2D>("Timid_Grad");
            total = _beatMap.Actions.Count;
            animationsTimer = new Timer(TimerUnit.Seconds, (float)_secondsPerBeat);
            animationsTimer.TimerAlertEvent += OnAnimationTimerUpdate;
            movementTimer = new Timer(TimerUnit.Seconds, (float)_secondsPerBeat * 2);
            movementTimer.TimerAlertEvent += OnMovementTimerUpdate;

            Graduates = new List<Graduate>()
            {
                new Graduate(RandomHelper.NextBool()) { Position = new Vector2(0, 270) },
                new Graduate(RandomHelper.NextBool()) { Position = new Vector2(64, 270) },
                new Graduate(RandomHelper.NextBool()) { Position = new Vector2(128, 270) },
                new Graduate(RandomHelper.NextBool()) { Position = new Vector2(192, 225) },
                new Graduate(RandomHelper.NextBool()) { Position = new Vector2(256, 225) }
            };

            MediaPlayer.Play(Song);
        }

        protected override void UpdateGame(GameTime gameTime)
        {
            animationsTimer.Update(gameTime);
            if (hitWindowActive)
            {
                if (firstFrame)
                {
                    gradMovement = true;
                    firstFrame = false;
                }
            }
            else firstFrame = true;

            if (gradMovement)
            {
                movementTimer.Update(gameTime);
                foreach (Graduate grad in Graduates)
                {
                    grad.Position.X += (float)(20 * _beatsPerSecond * gameTime.ElapsedGameTime.TotalSeconds);

                    int xdiff = (grad.IsTimid) ? 27 : 25;

                    if (grad.Position.X + xdiff > 718) grad.Position.Y = 270;
                    else if (grad.Position.X + xdiff > 696) grad.Position.Y = 258;
                    else if (grad.Position.X + xdiff > 677) grad.Position.Y = 243;
                    else if (grad.Position.X + 64 - xdiff > 203) grad.Position.Y = 225;
                    else if (grad.Position.X + 64 - xdiff > 184) grad.Position.Y = 243;
                    else if (grad.Position.X + 64 - xdiff > 162) grad.Position.Y = 258;
                }
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

        private void OnAnimationTimerUpdate(object obj, EventArgs e)
        {
            foreach (Graduate grad in Graduates)
            {
                if (grad.IsTimid)
                {
                    if (grad.AnimationFrame.Y == 0) grad.AnimationFrame.Y = 1;
                    else grad.AnimationFrame.Y = 0;
                }
                else
                {
                    if (grad.AnimationFrame.X == 0) grad.AnimationFrame.X = 1;
                    else grad.AnimationFrame.X = 0;
                }
            }
        }

        private void OnMovementTimerUpdate(object obj, EventArgs e)
        {
            gradMovement = false;
        }

        protected override void DrawGame(SpriteBatch spriteBatch)
        {
            foreach (Graduate grad in Graduates)
            {
                grad.Draw(spriteBatch);
            }

            FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(10, 500), Color.Yellow, "Press A to hand out diplomas");

            string currentText = $"Accuracy: {(accuracy * 100).ToString("F2")}%";
            Vector2 size = FontText.SizeOf(currentText, "PublicPixel");
            FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(940 - size.X, 10), Color.Green, currentText);

            
        }
    }
}
