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

        public bool Escape { get; private set; } = false;

        public bool A { get; private set; } = false;

        public bool B { get; private set; } = false;

        public bool DirectionChanged { get; private set; } = false;
        public Vector2 Direction { get; private set; }

        private Vector2 prevDirection = new Vector2(0, 0);

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
                || currentKeyboardState.IsKeyDown(Keys.Z) && !previousKeyboardState.IsKeyDown(Keys.Z);

            B = currentGamePadState.Buttons.B == ButtonState.Pressed && previousGamePadState.Buttons.B == ButtonState.Released
                || currentKeyboardState.IsKeyDown(Keys.X) && !previousKeyboardState.IsKeyDown(Keys.X);

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

        }
    }
}
