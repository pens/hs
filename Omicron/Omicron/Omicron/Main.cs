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
    public class Main : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.IsFullScreen = SaveManager.Load<Settings>("settings").Fullscreen;
            graphics.PreferredBackBufferWidth = (int)SaveManager.Load<Settings>("settings").Resolution.X;
            graphics.PreferredBackBufferHeight = (int)SaveManager.Load<Settings>("settings").Resolution.Y;
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            GraphicsManager.Load(this, graphics);
            StateManager.AddMenu("Main", new MainMenu());
            StateManager.AddMenu("Settings", new SettingsMenu());
            StateManager.AddLevel("TestLevel", new TestLevel());
            StateManager.Load(this, "Main", new LoadScreen());
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            InputManager.Update();
            StateManager.Update(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);
            StateManager.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
