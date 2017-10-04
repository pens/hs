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
        Player a;
        List<Enemy> enemies = new List<Enemy>();
        int b;

        public void Load(ContentManager Content)
        {
            Bullet.Load(Content);
            Enemy.Load(Content);
            Player.Load(Content);
            a = new Player();
        }

        public void HandleInput(GameTime gameTime)
        {
            if (InputManager.KeyDown(Keys.Escape, true))
                StateManager.Pause(true);
        }

        public void Update(GameTime gameTime)
        {
            a.Update(gameTime);
            if (enemies.Count < 4)
            {
                if (b == 0)
                {
                    Enemy e = new Enemy(0, new Vector2(GraphicsManager.ViewportCenter.X, -GraphicsManager.ViewportCenter.Y));
                    enemies.Add(e);
                    b++;
                }
                else if (b == 1)
                {
                    Enemy e = new Enemy(0, new Vector2(-GraphicsManager.ViewportCenter.X, GraphicsManager.ViewportCenter.Y));
                    enemies.Add(e);
                    b++;
                }
                else if (b == 2)
                {
                    Enemy e = new Enemy(0, new Vector2(3 * GraphicsManager.ViewportCenter.X, GraphicsManager.ViewportCenter.Y));
                    enemies.Add(e);
                    b++;
                }
                else if (b == 3)
                {
                    Enemy e = new Enemy(0, new Vector2(GraphicsManager.ViewportCenter.X, 3 * GraphicsManager.ViewportCenter.Y));
                    enemies.Add(e);
                    b = 0;
                }
            }
            foreach (Enemy e in enemies)
            {
                e.Update(gameTime, a.position);
                for (int i = 0; i < a.active; i++)
                {
                    e.BulletColl(a.bullets[i]);
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (Enemy e in enemies)
            {
                e.Draw();
            }
            a.Draw();
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
