using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Omicron
{
    public static class GraphicsManager
    {
        public static Viewport Viewport
        {
            get { return game.GraphicsDevice.Viewport; }
            set { game.GraphicsDevice.Viewport = value; }
        }
        public static Vector2 ViewportCenter
        {
            get { return new Vector2(Viewport.Width / 2, Viewport.Height / 2); }
        }

        public static Vector2 Resolution
        {
            get { return new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight); }
            set
            {
                _graphics.PreferredBackBufferWidth = (int)value.X;
                _graphics.PreferredBackBufferHeight = (int)value.Y;
            }
        }
        public static bool IsFullscreen
        {
            get { return _graphics.IsFullScreen; }
            set { _graphics.IsFullScreen = value; }
        }

        static SpriteBatch spriteBatch;
        static bool batchActive;

        static GraphicsDeviceManager _graphics;
        static Game game;

        public static void Load(Game game, GraphicsDeviceManager graphics)
        {
            GraphicsManager.game = game;
            _graphics = graphics;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        public static void DrawFPS(SpriteFont font, GameTime gameTime)
        {
            if (!batchActive)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
                batchActive = true;
            }
            spriteBatch.DrawString(font, (1 / gameTime.ElapsedGameTime.TotalSeconds).ToString("#"), Vector2.Zero, Color.White);
        }
        public static void DrawText(Text text)
        {
            if (!batchActive)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
                batchActive = true;
            }
            spriteBatch.DrawString(text.Font, text.String, text.Position, text.Color, 0f, text.Origin, text.Scale, SpriteEffects.None, 0);
        }

        public static Vector2[] GetSupportedResolutions(Vector2 min, Vector2 max)
        {
            List<Vector2> resolutions = new List<Vector2>();
            foreach (DisplayMode mode in _graphics.GraphicsDevice.Adapter.SupportedDisplayModes)
            {
                if (mode.Width >= min.X && mode.Height >= min.Y && mode.Width <= max.X && mode.Height <= max.Y)
                {
                    if (!resolutions.Contains(new Vector2(mode.Width, mode.Height)))
                    {
                        resolutions.Add(new Vector2(mode.Width, mode.Height));
                    }
                }
            }
            return resolutions.ToArray();
        }
        public static void ApplyChanges()
        {
            _graphics.ApplyChanges();
        }

        public static void DrawSprite(Sprite sprite)
        {
            DrawSprite(sprite, Vector2.Zero);
        }
        public static void DrawSprite(Sprite sprite, Vector2 offset)
        {
            if (!batchActive)
            {
                spriteBatch.Begin();
                batchActive = true;
            }
            spriteBatch.Draw(sprite.Texture, sprite.Position - offset, sprite.SourceRectangle, sprite.Color, sprite.Rotation, sprite.Origin, sprite.Scale, SpriteEffects.None, 0);
        }
        public static void EndSpriteBatch()
        {
            if (batchActive)
            {
                spriteBatch.End();
                batchActive = false;
            }
        }
    }
}
