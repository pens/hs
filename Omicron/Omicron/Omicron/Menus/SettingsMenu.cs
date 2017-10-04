using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Omicron
{
    public class SettingsMenu : IMenu
    {
        SpriteFont ocra;

        Text resolution;
        Text fullscreen;
        Text back;

        Vector2[] resolutions;

        int _resolution;
        bool _fullscreen;
        int oldRes;
        bool oldScreen;

        public void Load(ContentManager Content)
        {
            ocra = Content.Load<SpriteFont>("ocra72");
            resolutions = GraphicsManager.GetSupportedResolutions(new Vector2(800, 600), new Vector2(1920, 1080));
            for (int i = 0; i < resolutions.Count(); i++)
            {
                if (GraphicsManager.Resolution == resolutions[i])
                {
                    _resolution = i;
                    break;
                }
                else if (GraphicsManager.Resolution.X <= resolutions[i].X && GraphicsManager.Resolution.Y < resolutions[i].Y)
                {
                    if (i > 0)
                        _resolution = i - 1;
                    else _resolution = i;
                }
            }
            resolution = new Text(ocra, "resolution: " + resolutions[_resolution].X + " x " + resolutions[_resolution].Y, Color.White, new Vector2(GraphicsManager.ViewportCenter.X, GraphicsManager.ViewportCenter.Y - 100), new Vector2(.5f, .5f), .5f);
            fullscreen = new Text(ocra, "fullscreen: " + GraphicsManager.IsFullscreen, Color.White, new Vector2(GraphicsManager.ViewportCenter.X, GraphicsManager.ViewportCenter.Y), new Vector2(.5f, .5f), .5f);
            back = new Text(ocra, "back", Color.White, new Vector2(GraphicsManager.ViewportCenter.X, GraphicsManager.ViewportCenter.Y + 100), new Vector2(.5f, .5f), .5f);
            _fullscreen = GraphicsManager.IsFullscreen;
        }

        public void HandleInput(GameTime gameTime)
        {
            if (resolution.GetBounds().Contains((int)InputManager.MousePos().X, (int)InputManager.MousePos().Y))
            {
                if (InputManager.MouseLClicked(true))
                {
                    if (_resolution < resolutions.Count() - 1)
                        _resolution++;
                    else _resolution = 0;
                    resolution.String = "resolution: " + resolutions[_resolution].X + " x " + resolutions[_resolution].Y;
                }
                if (InputManager.MouseRClicked(true))
                {
                    if (_resolution > 0)
                        _resolution--;
                    else _resolution = resolutions.Count() - 1;
                    resolution.String = "resolution: " + resolutions[_resolution].X + " x " + resolutions[_resolution].Y;
                }
            }
            else if (fullscreen.GetBounds().Contains((int)InputManager.MousePos().X, (int)InputManager.MousePos().Y))
            {
                if (InputManager.MouseLClicked(true) || InputManager.MouseRClicked(true))
                {
                    _fullscreen = !_fullscreen;
                    if (_fullscreen)
                        fullscreen.String = "fullscreen: " + "yes";
                    else fullscreen.String = "fullscreen: " + "no";
                }
            }
            else if (back.GetBounds().Contains((int)InputManager.MousePos().X, (int)InputManager.MousePos().Y))
            {
                if (InputManager.MouseLClicked(true))
                {
                    if (oldRes != _resolution || oldScreen != _fullscreen)
                    {
                        GraphicsManager.Resolution = resolutions[_resolution];
                        GraphicsManager.IsFullscreen = _fullscreen;
                        GraphicsManager.ApplyChanges();
                        SaveManager.Save(new Settings(GraphicsManager.Resolution, GraphicsManager.IsFullscreen), "settings");
                    }
                    StateManager.ReturnToMenu("Main");
                }
            }
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime)
        {
            GraphicsManager.DrawText(resolution);
            GraphicsManager.DrawText(fullscreen);
            GraphicsManager.DrawText(back);
            GraphicsManager.EndSpriteBatch();
        }

        public void Unload()
        {
        }

        public void TransOn(GameTime gameTime, bool Returning)
        {
            resolution.Position = new Vector2(GraphicsManager.ViewportCenter.X, GraphicsManager.ViewportCenter.Y - 100);
            fullscreen.Position = new Vector2(GraphicsManager.ViewportCenter.X, GraphicsManager.ViewportCenter.Y);
            back.Position = new Vector2(GraphicsManager.ViewportCenter.X, GraphicsManager.ViewportCenter.Y + 100);
            oldRes = _resolution;
            oldScreen = _fullscreen;
            if (_fullscreen)
                fullscreen.String = "fullscreen: " + "yes";
            else fullscreen.String = "fullscreen: " + "no";
            StateManager.EndTransition();
        }

        public void TransOff(GameTime gameTime, bool Returning)
        {
            StateManager.EndTransition();
        }
    }
}
