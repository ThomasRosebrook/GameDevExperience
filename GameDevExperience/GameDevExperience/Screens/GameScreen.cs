using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevExperience.Screens
{
    public class GameScreen
    {
        public ScreenManager ScreenManager { get; internal set; }

        public ScreenState ScreenState { get; set; } = ScreenState.Active;

        public bool IsExiting { get; internal set; }
        public bool IsPopup { get; protected set; }

        protected bool IsActive = false;
        protected bool _unfocused = false;
        protected bool _covered = false;

        public virtual void Activate()
        {
            IsActive = true;
        }

        public virtual void Deactivate()
        {

        }

        public virtual void Unload()
        {

        }

        public virtual void Update(GameTime gameTime, bool unfocused, bool covered)
        {
            _unfocused = unfocused;

            if (IsExiting) ScreenManager.RemoveScreen(this);
            else if (covered)
            {
                ScreenState = ScreenState.Hidden;
            }
        }

        public virtual void HandleInput(GameTime gameTime, InputManager input)
        {

        }

        public virtual void Draw(GameTime gameTime)
        {

        }

        public void ExitScreen()
        {
            ScreenManager.RemoveScreen(this);
        }


    }
}
