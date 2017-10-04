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
    public class Sprite
    {
        public Texture2D Texture
        {
            get { return texture; }
            set
            {
                texture = value;
                SetRelativeOrigin(relativeOrigin);
                ColorData = new Color[Texture.Width * Texture.Height];
                Texture.GetData<Color>(ColorData);
                SourceRectangle = Texture.Bounds;
            }
        }
        public Vector2 Position;
        public Rectangle SourceRectangle;
        public Color Color;
        public float Rotation;
        public Vector2 Origin;
        public Vector2 Scale
        {
            get { return scale; }
            set 
            {
                scale = value;
                SetRelativeOrigin(relativeOrigin);
            }
        }
        public SpriteEffects SpriteEffects;
        public Color[] ColorData;

        private Vector2 relativeOrigin;
        private Vector2 scale;
        private Texture2D texture;

        public Sprite()
        {
            Color = Color.White;
            Scale = Vector2.One;
        }
        public Sprite(Texture2D texture)
        {
            this.texture = texture;
            ColorData = new Color[Texture.Width * Texture.Height];
            Texture.GetData<Color>(ColorData);
            SourceRectangle = Texture.Bounds;
            Color = Color.White;
            Scale = Vector2.One;
            SetRelativeOrigin(.5f, .5f);
        }
        public Sprite(Texture2D texture, Vector2 position)
            : this(texture)
        {
            Position = position;
        }
        public Sprite(Texture2D texture, Vector2 position, Vector2 relativeOrigin)
            : this(texture, position)
        {
            SetRelativeOrigin(relativeOrigin);
        }

        public Rectangle GetBounds()
        {
            Vector2 leftTop = Vector2.Transform(new Vector2(Texture.Bounds.X, Texture.Bounds.Y), GetTransform());
            Vector2 rightTop = Vector2.Transform(new Vector2(Texture.Bounds.X + Texture.Bounds.Width, Texture.Bounds.Y), GetTransform());
            Vector2 leftBottom = Vector2.Transform(new Vector2(Texture.Bounds.X, Texture.Bounds.Y + Texture.Bounds.Height), GetTransform());
            Vector2 rightBottom = Vector2.Transform(new Vector2(Texture.Bounds.X + Texture.Bounds.Width, Texture.Bounds.Y + Texture.Bounds.Height), GetTransform());

            Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop), Vector2.Min(leftBottom, rightBottom));
            Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop), Vector2.Max(leftBottom, rightBottom));

            return new Rectangle((int)Math.Floor(min.X), (int)Math.Floor(min.Y), (int)Math.Ceiling(max.X - min.X), (int)Math.Ceiling(max.Y - min.Y));
        }
        public Matrix GetTransform()
        {
            return Matrix.CreateTranslation(new Vector3(-Origin, 0)) *
            Matrix.CreateScale(new Vector3(Scale, 1)) *
            Matrix.CreateRotationZ(Rotation) *
            Matrix.CreateTranslation(new Vector3(Position, 0));
        }

        public void SetRelativeOrigin(Vector2 relativeOrigin)
        {
            this.relativeOrigin = relativeOrigin;
            Origin.X = relativeOrigin.X * SourceRectangle.Width;
            Origin.Y = relativeOrigin.Y * SourceRectangle.Height;
        }
        public void SetRelativeOrigin(float relativeOriginX, float relativeOriginY)
        {
            SetRelativeOrigin(new Vector2(relativeOriginX, relativeOriginY));
        }
    }
}
