using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevExperience.Screens
{
    public class MenuOption
    {
        public string Name = "";

        public GameScreen Screen;

        public Keys Key;

        public bool IsScreenSelection = false;

        public bool IsControlSelection = false;

        public bool IsSelected = false;

        public MenuOption(string name, GameScreen screen)
        {
            Name = name;
            Screen = screen;
            IsScreenSelection = true;
        }

        public MenuOption (string name, Keys key)
        {
            Name = name;
            Key = key;
            IsControlSelection = true;
        }

        public MenuOption(string name)
        {
            Name = name;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Color color = IsSelected ? Color.White : Color.LimeGreen;
            FontText.DrawString(spriteBatch, "PublicPixelMedium", position, color, Name);
        }
    }
}
