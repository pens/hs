using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Omicron
{
    public class Settings
    {
        public Vector2 Resolution = new Vector2(800, 600);
        public bool Fullscreen = false;

        public Settings() { }
        public Settings(Vector2 resolution, bool fullscreen)
        {
            Resolution = resolution;
            Fullscreen = fullscreen;
        }
    }
}
