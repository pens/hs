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
    public class Bullet
    {
        public Sprite Sprite;
        public float Speed;
        public float Distance;
        public float Damage;

        Vector2 rotatedVector;
        Vector2 startPosition;

        static Texture2D[] textures;
        static string[] strings = new string[]
        {
            "Laser"
        };

        public Bullet()
        {
            Sprite = new Sprite();
        }

        public void Reset(int type, Vector2 startPosition, float rotation)
        {
            this.startPosition = startPosition;
            Sprite.Position = startPosition;
            Sprite.Rotation = rotation;
            rotatedVector = Vector2.Transform(Vector2.UnitX, Matrix.CreateRotationZ(Sprite.Rotation));
            SetType(type);
        }

        public static void Load(ContentManager content)
        {
            textures = new Texture2D[strings.Count()];
            for (int i = 0; i < strings.Count(); i++)
            {
                textures[i] = content.Load<Texture2D>(strings[i]);
            }
        }

        public void Update(GameTime gameTime)
        {
            Sprite.Position += rotatedVector * (float)gameTime.ElapsedGameTime.TotalSeconds * Speed;
            if (Vector2.Distance(startPosition, Sprite.Position) >= Distance)
                OnDeath();
        }

        public void Draw()
        {
            GraphicsManager.DrawSprite(Sprite);
        }

        private void SetType(int i)
        {
            Sprite.Texture = textures[i];
            Sprite.Origin = new Vector2(0, textures[i].Bounds.Height / 2);
            if (i == 0)
            {
                Speed = 1000;
                Distance = 500;
                Damage = 5;
            }
        }

        public EventHandler Death;
        public void OnDeath()
        {
            if (Death != null)
                Death(this, EventArgs.Empty);
        }
    }
}
