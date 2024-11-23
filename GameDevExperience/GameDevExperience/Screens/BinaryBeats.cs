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
        Texture2D thoughtBubble;

        string response = "";
        int responseIndex = 0;

        string currentCodeString = "";
        

        public BinaryBeats (string songName, string beatmapPath, string display)
        {
            this.songName = songName;
            beatPath = beatmapPath;
            DisplaySong = display;
            GameName = "Binary Beats";

        }

        public override void LoadSong(ContentManager content)
        {
            Song = content.Load<Song>(songName);
        }

        protected override void ActivateGame()
        {
            LoadBeatmap(beatPath);
            
            LoadSong(_content);
            _background = _content.Load<Texture2D>("Programming");
            total = _beatMap.Actions.Count;
            thoughtBubble = _content.Load<Texture2D>("ThoughtBubble");

            MediaPlayer.Play(Song);
        }

        public override void UpdateGame(GameTime gameTime)
        {
            if (hitWindowActive)
            {
                int index = RandomHelper.Next(5);
                if (timeDelay <= greenZoneSize)
                {
                    if (responseIndex == 2) return;
                    responseIndex = 2;
                    switch (index)
                    {
                        case 0:
                            currentCodeString = "if(x == 0)";
                            break;
                        case 1:
                            currentCodeString = "while(x > 0)";
                            break;
                        case 2:
                            currentCodeString = "int x = 0;";
                            break;
                        case 3:
                            currentCodeString = "public void Update()";
                            break;
                        case 4:
                            currentCodeString = "foreach (int n in nums)";
                            break;
                    }
                }
                else if (timeDelay > greenZoneSize && timeDelay <= greenZoneSize + yellowZoneSize)
                {
                    if (responseIndex == 1) return;
                    responseIndex = 1;
                    switch (index)
                    {
                        case 0:
                            currentCodeString = "if(true)";
                            break;
                        case 1:
                            currentCodeString = "while(true)";
                            break;
                        case 2:
                            currentCodeString = "int x = 1000;";
                            break;
                        case 3:
                            currentCodeString = "private void Run()";
                            break;
                        case 4:
                            currentCodeString = "for(int i = 0; i < 1; i++)";
                            break;
                    }
                }
                else
                {
                    if (responseIndex == 0) return;
                    responseIndex = 0;
                    switch (index)
                    {
                        case 0:
                            currentCodeString = "if [";
                            break;
                        case 1:
                            currentCodeString = "lwihe (!true)";
                            break;
                        case 2:
                            currentCodeString = "int x = 0.253;";
                            break;
                        case 3:
                            currentCodeString = "void Run() { return x; }";
                            break;
                        case 4:
                            currentCodeString = "for (0)";
                            break;
                    }
                }
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
                timeDelay = Math.Abs(songTime - actionTime - _secondsPerBeat * beatDelay);
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
            count++;

            switch (RandomHelper.Next(5))
            {
                case 0:
                    response = "Nice Code!";
                    break;
                case 1:
                    response = "Runs Flawlessly!";
                    break;
                case 2:
                    response = "Good Form!";
                    break;
                case 3:
                    response = "Perfect Execution!";
                    break;
                case 4:
                    response = "Clean Code!";
                    break;
            }
        }

        protected override void OnHalfSuccessfulHit()
        {
            hitBoxColor = Color.Yellow;
            correct.Play();
            score += .5;
            count++;

            switch (RandomHelper.Next(5))
            {
                case 0:
                    response = "Good Enough...";
                    break;
                case 1:
                    response = "It works...";
                    break;
                case 2:
                    response = "It runs...";
                    break;
                case 3:
                    response = "Spaghetti Code!";
                    break;
                case 4:
                    response = "We'll Fix it Later!";
                    break;
            }
        }

        protected override void OnFailedHit()
        {
            hitBoxColor = Color.Red;
            wrong.Play();
            count++;

            switch (RandomHelper.Next(5))
            {
                case 0:
                    response = "10 Errors";
                    break;
                case 1:
                    response = "There were build errors!";
                    break;
                case 2:
                    response = "Buggy Code!";
                    break;
                case 3:
                    response = "WRONG!";
                    break;
                case 4:
                    response = "Integer Overflow";
                    break;
            }
        }

       protected override void DrawGame(SpriteBatch spriteBatch)
        {
            //FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(150,300), Color.Yellow, $"Song Time: {songTime.ToString("F4")}");
            //FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(150, 350), Color.Yellow, $"Action Time: {actionTime.ToString("F4")}");
            //FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(150, 400), Color.Yellow, $"HitWindow: {hitWindowActive}");
            FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(10, 500), Color.Yellow, "Press A to write text to the beat");
            //if (debugMessage)FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(150, 450), Color.Yellow, $"DEBUG");
            spriteBatch.Draw(thoughtBubble, new Vector2(0, 0), Color.White);

            string currentText = $"Accuracy: {(accuracy * 100).ToString("F2")}%";
            Vector2 size = FontText.SizeOf(currentText, "PublicPixel");
            FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(940 - size.X, 10), Color.Green, currentText);
            

            size = FontText.SizeOf(response, "PublicPixel");
            FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(900 - size.X, 50), hitBoxColor, response);

            size = FontText.SizeOf(currentCodeString, "PublicPixel");
            FontText.DrawString(spriteBatch, "PublicPixel", new Vector2(920 - size.X, 125), Color.Green, currentCodeString);
        }
    }
}
