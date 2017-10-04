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
    public class MainMenu : IMenu
    {
        SpriteFont ocra;

        Sprite settings;
        Sprite play;
        Sprite quit;
        Sprite selection;

        TimeSpan transTime = TimeSpan.Zero;

        Text title;
        Text selectionText;

        int selectedItem = 0;

        public void Load(ContentManager Content)
        {
            ocra = Content.Load<SpriteFont>("ocra72");
            title = new Text(ocra, "omicron", Color.White, new Vector2(GraphicsManager.ViewportCenter.X, GraphicsManager.ViewportCenter.Y - 180), new Vector2(.5f, .5f), 1);
            settings = new Sprite(Content.Load<Texture2D>("Settings"), new Vector2(GraphicsManager.ViewportCenter.X - 240, GraphicsManager.ViewportCenter.Y), new Vector2(.5f, .5f));
            play = new Sprite(Content.Load<Texture2D>("Play"), new Vector2(GraphicsManager.ViewportCenter.X, GraphicsManager.ViewportCenter.Y), new Vector2(.5f, .5f));
            quit = new Sprite(Content.Load<Texture2D>("Quit"), new Vector2(GraphicsManager.ViewportCenter.X + 240, GraphicsManager.ViewportCenter.Y), new Vector2(.5f, .5f));
            selection = new Sprite(Content.Load<Texture2D>("MenuSelect"), Vector2.Zero, new Vector2(.5f, .5f));
            selectionText = new Text(ocra, "", Color.White, new Vector2(.5f, .5f), .5f);
        }

        public void HandleInput(GameTime gameTime)
        {
            if (settings.GetBounds().Contains((int)InputManager.MousePos().X, (int)InputManager.MousePos().Y))
            {
                selection.Position = new Vector2(GraphicsManager.ViewportCenter.X - 240, GraphicsManager.ViewportCenter.Y);
                selectionText.Position = new Vector2(GraphicsManager.ViewportCenter.X - 240, GraphicsManager.ViewportCenter.Y + 150);
                selectionText.String = "<settings>";
                selectedItem = 1;
                if (InputManager.MouseLClicked(true))
                {
                    selectedItem = 0;
                    StateManager.ChangeMenu("Settings");
                }
            }
            else if (play.GetBounds().Contains((int)InputManager.MousePos().X, (int)InputManager.MousePos().Y))
            {
                selection.Position = new Vector2(GraphicsManager.ViewportCenter.X, GraphicsManager.ViewportCenter.Y);
                selectionText.Position = new Vector2(GraphicsManager.ViewportCenter.X, GraphicsManager.ViewportCenter.Y + 150);
                selectionText.String = "<play>";
                selectedItem = 2;
                if (InputManager.MouseLClicked(true))
                {
                    selectedItem = 0;
                    StateManager.OpenLevel("TestLevel");
                }
            }
            else if (quit.GetBounds().Contains((int)InputManager.MousePos().X, (int)InputManager.MousePos().Y))
            {
                selection.Position = new Vector2(GraphicsManager.ViewportCenter.X + 240, GraphicsManager.ViewportCenter.Y);
                selectionText.Position = new Vector2(GraphicsManager.ViewportCenter.X + 240, GraphicsManager.ViewportCenter.Y + 150);
                selectionText.String = "<quit>";
                selectedItem = 3;
                if (InputManager.MouseLClicked(true))
                {
                    selectedItem = 0;
                    StateManager.ExitGame();
                }
            }
            else selectedItem = 0;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime)
        {
            if (selectedItem != 0)
            {
                GraphicsManager.DrawSprite(selection);
                GraphicsManager.DrawText(selectionText);
            }
            GraphicsManager.DrawSprite(settings);
            GraphicsManager.DrawSprite(play);
            GraphicsManager.DrawSprite(quit);
            GraphicsManager.DrawText(title);
            GraphicsManager.EndSpriteBatch();
        }

        public void Unload()
        {
        }

        public void TransOn(GameTime gameTime, bool Returning)
        {
            title.Position = new Vector2(GraphicsManager.ViewportCenter.X, GraphicsManager.ViewportCenter.Y - 180);
            settings.Position = new Vector2(GraphicsManager.ViewportCenter.X - 240, GraphicsManager.ViewportCenter.Y);
            play.Position = new Vector2(GraphicsManager.ViewportCenter.X, GraphicsManager.ViewportCenter.Y);
            quit.Position = new Vector2(GraphicsManager.ViewportCenter.X + 240, GraphicsManager.ViewportCenter.Y);
            StateManager.EndTransition();
        }

        public void TransOff(GameTime gameTime, bool Returning)
        {
            StateManager.EndTransition();
        }
    }
}
