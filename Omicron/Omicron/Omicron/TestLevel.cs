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
    public class TestLevel : ILevel
    {
        loadScreen load = new loadScreen();
        public void Load(ContentManager Content)
        {
        }

        public void HandleInput(GameTime gameTime)
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime)
        {
        }

        public void Unload()
        {
        }

        public IScreen LoadScreen
        {
            get { return load; }
        }

        public IScreen PauseScreen
        {
            get { return load; }
        }
        class loadScreen : IScreen
        {
            public void Load(ContentManager Content)
            {
            }

            public void HandleInput(GameTime gameTime)
            {
            }

            public void Update(GameTime gameTime)
            {
            }

            public void Draw(GameTime gameTime)
            {
            }

            public void Unload()
            {
            }

            public void TransOn(GameTime gameTime, bool Returning)
            {
            }

            public void TransOff(GameTime gameTime, bool Returning)
            {
            }
        }
    }
}
