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
    public class LoadScreen : IScreen
    {
        Text loading;

        public void Load(ContentManager Content)
        {
            loading = new Text(Content.Load<SpriteFont>("OCRA72"), "loading", Color.White, GraphicsManager.ViewportCenter, new Vector2(.5f, .5f), 1);
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime)
        {
            GraphicsManager.DrawText(loading);
            GraphicsManager.EndSpriteBatch();
        }

        public void Unload()
        {
        }
    }
}
