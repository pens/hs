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
        pauseScreen pause = new pauseScreen();

        public void Load(ContentManager content)
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
            GraphicsManager.EndSpriteBatch();
        }

        public IScreen LoadScreen
        {
            get { return load; }
        }

        public IScreen PauseScreen
        {
            get { return pause; }
        }

        class loadScreen : IScreen
        {
            Text loading;
            public void Load(ContentManager Content)
            {
                loading = new Text(Content.Load<SpriteFont>("ocra72"), "loading", Color.White, GraphicsManager.ViewportCenter, new Vector2(.5f, .5f), 1);
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

        class pauseScreen : IScreen
        {
            Text unpause;
            Text back;
            public void Load(ContentManager Content)
            {
                unpause = new Text(Content.Load<SpriteFont>("ocra72"), "unpause", Color.White, new Vector2(GraphicsManager.ViewportCenter.X, GraphicsManager.Viewport.Height / 3), new Vector2(.5f, .5f), 1);
                back = new Text(Content.Load<SpriteFont>("ocra72"), "quit", Color.White, new Vector2(GraphicsManager.ViewportCenter.X, GraphicsManager.Viewport.Height * 2 / 3), new Vector2(.5f, .5f), 1);
            }

            public void Update(GameTime gameTime)
            {
                if (InputManager.KeyDown(Keys.Escape, true))
                {
                    unpause.String = "unpause";
                    back.String = "quit";
                    StateManager.Unpause();
                }
                else if (unpause.GetBounds().Contains((int)InputManager.MousePos().X, (int)InputManager.MousePos().Y))
                {
                    unpause.String = "<unpause>";
                    if (InputManager.MouseLClicked(true))
                        StateManager.Unpause();
                }
                else if (back.GetBounds().Contains((int)InputManager.MousePos().X, (int)InputManager.MousePos().Y))
                {
                    back.String = "<quit>";
                    if (InputManager.MouseLClicked(true))
                        StateManager.CloseLevel("Main");
                }
                else
                {
                    unpause.String = "unpause";
                    back.String = "quit";
                }
            }

            public void Draw(GameTime gameTime)
            {
                GraphicsManager.DrawText(unpause);
                GraphicsManager.DrawText(back);
                GraphicsManager.EndSpriteBatch();
            }

            public void Unload()
            {
            }
        }
    }
}
