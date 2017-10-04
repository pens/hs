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
    public class Bullet : GameObject
    {
        Sprite sprite;

        Vector2 rotatedVector;
        Vector2 startPosition;
        float distance;
        float speed;
        float damage;

        Texture2D[] Textures;
        string[] Strings;

        public Bullet()
        {
            sprite = new Sprite();
            Strings = new string[] {"Laser"};
        }

        public override void Update(GameTime gameTime)
        {
            sprite.Position += rotatedVector * (float)gameTime.ElapsedGameTime.TotalSeconds * speed;
            if (Vector2.Distance(startPosition, sprite.Position) >= distance)
                OnDeath();
        }
        public override void Draw()
        {
            GraphicsManager.DrawSprite(sprite);
        }

        public override void Reset(string type, Vector2 position, float rotation)
        {
            startPosition = position;
            sprite.Position = position;
            sprite.Rotation = rotation;
            rotatedVector = Vector2.Transform(Vector2.UnitX, Matrix.CreateRotationZ(rotation));
            switch (type)
            {
                default:
                    {
                        speed = 1000;
                        distance = 500;
                        damage = 5;
                        sprite.Texture = Textures[0];
                        sprite.Origin = new Vector2(0, Textures[0].Bounds.Height / 2);
                        break;
                    }
            }
        }
    }
}
