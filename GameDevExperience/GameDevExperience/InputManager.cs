using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevExperience
{
    public class InputManager
    {
        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        MouseState currentMouseState;
        MouseState previousMouseState;

        public Keys AButtonKey = Keys.A;
        public Keys BButtonKey = Keys.B;

        public bool Escape { get; private set; } = false;

        public bool Right { get; private set; } = false;

        public bool Left { get; private set; } = false;

        public bool Up { get; private set; } = false;

        public bool Down { get; private set; } = false;

        /// <summary>
        /// If the A Button is currently pressed
        /// </summary>
        public bool A { get; private set; } = false;

        /// <summary>
        /// If the B Button is currently pressed
        /// </summary>
        public bool B { get; private set; } = false;

        /// <summary>
        /// If the A Button was just released
        /// </summary>
        public bool AButtonJustReleased { get; private set; } = false;

        /// <summary>
        /// If the B Button was just released
        /// </summary>
        public bool BButtonJustReleased { get; private set; } = false;

        /// <summary>
        /// If the A Button was just pressed
        /// </summary>
        public bool AButtonJustPressed { get; private set; } = false;

        /// <summary>
        /// If the B Button was just pressed
        /// </summary>
        public bool BButtonJustPressed { get; private set; } = false;

        /*
        public bool DirectionChanged { get; private set; } = false;
        public Vector2 Direction { get; private set; }

        private Vector2 prevDirection = new Vector2(0, 0);
        */

        public void Update(GameTime gameTime)
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            previousGamePadState = currentGamePadState;
            currentGamePadState = GamePad.GetState(0);

            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            Escape = currentGamePadState.Buttons.Back == ButtonState.Pressed && previousGamePadState.Buttons.Back == ButtonState.Released
                || currentKeyboardState.IsKeyDown(Keys.Escape) && !previousKeyboardState.IsKeyDown(Keys.Escape);

            A = currentGamePadState.Buttons.A == ButtonState.Pressed && previousGamePadState.Buttons.A == ButtonState.Released
                || currentKeyboardState.IsKeyDown(AButtonKey) && !previousKeyboardState.IsKeyDown(AButtonKey);

            B = currentGamePadState.Buttons.B == ButtonState.Pressed && previousGamePadState.Buttons.B == ButtonState.Released
                || currentKeyboardState.IsKeyDown(BButtonKey) && !previousKeyboardState.IsKeyDown(BButtonKey);

            /*
            A = currentGamePadState.Buttons.A == ButtonState.Pressed
                || currentKeyboardState.IsKeyDown(AButtonKey);

            B = currentGamePadState.Buttons.B == ButtonState.Pressed
                || currentKeyboardState.IsKeyDown(BButtonKey);

            
            AButtonJustPressed = currentGamePadState.Buttons.A == ButtonState.Pressed && previousGamePadState.Buttons.A == ButtonState.Released
                || currentKeyboardState.IsKeyDown(AButtonKey) && !previousKeyboardState.IsKeyDown(AButtonKey);

            BButtonJustPressed = currentGamePadState.Buttons.B == ButtonState.Pressed && previousGamePadState.Buttons.B == ButtonState.Released
                || currentKeyboardState.IsKeyDown(BButtonKey) && !previousKeyboardState.IsKeyDown(BButtonKey);

            AButtonJustReleased = currentGamePadState.Buttons.A == ButtonState.Released && previousGamePadState.Buttons.A == ButtonState.Pressed
                || !currentKeyboardState.IsKeyDown(AButtonKey) && previousKeyboardState.IsKeyDown(AButtonKey);

            BButtonJustReleased = currentGamePadState.Buttons.B == ButtonState.Released && previousGamePadState.Buttons.B == ButtonState.Pressed
                || !currentKeyboardState.IsKeyDown(BButtonKey) && previousKeyboardState.IsKeyDown(BButtonKey);
            */

            Right = currentGamePadState.DPad.Right == ButtonState.Pressed && previousGamePadState.DPad.Right == ButtonState.Released
                || currentKeyboardState.IsKeyDown(Keys.Right) && !previousKeyboardState.IsKeyDown(Keys.Right);

            Left = currentGamePadState.DPad.Left == ButtonState.Pressed && previousGamePadState.DPad.Left == ButtonState.Released
                || currentKeyboardState.IsKeyDown(Keys.Left) && !previousKeyboardState.IsKeyDown(Keys.Left);

            Up = currentGamePadState.DPad.Up == ButtonState.Pressed && previousGamePadState.DPad.Up == ButtonState.Released
                || currentKeyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up);

            Down = currentGamePadState.DPad.Down == ButtonState.Pressed && previousGamePadState.DPad.Down == ButtonState.Released
                || currentKeyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down);

            /*
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Direction = new Vector2();

            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.A))
            {
                Direction = new Vector2(-time, 0);
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.D))
            {
                Direction += new Vector2(time, 0);
            }

            if (currentKeyboardState.IsKeyDown(Keys.Up) || currentKeyboardState.IsKeyDown(Keys.W))
            {
                Direction += new Vector2(0, -time);
            }

            if (currentKeyboardState.IsKeyDown(Keys.Down) || currentKeyboardState.IsKeyDown(Keys.S))
            {
                Direction += new Vector2(0, time);
            }

            if (Direction.X == 0 && Direction.Y == 0) Direction = currentGamePadState.ThumbSticks.Left * time * new Vector2(1, -1);

            DirectionChanged = !(Direction.X == prevDirection.X && Direction.Y == prevDirection.Y);

            prevDirection = Direction;
            */
        }

        
    }
}
