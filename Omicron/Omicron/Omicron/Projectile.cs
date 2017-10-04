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
    public class Projectile : Sprite
    {
        public float Speed;
        public float Distance;

        Vector2 rotatedVector;

        public Projectile(Texture2D texture, Vector2 position, float speed, float rotation, float distance)
            : base(texture, position)
        {
            SetRelativeOrigin(new Vector2(.5f, .5f));
            Scale = new Vector2(.25f, .25f);
            Speed = speed;
            Rotation = rotation;
            Distance = distance;
            rotatedVector = Vector2.Transform(Vector2.UnitX, Matrix.CreateRotationZ(rotation));
        }

        public void Update(GameTime gameTime)
        {
            Position.X += (float)(rotatedVector.X * gameTime.ElapsedGameTime.TotalSeconds * Speed);
            Position.Y += (float)(rotatedVector.Y * gameTime.ElapsedGameTime.TotalSeconds * Speed);
            if (Position.Length() >= Distance)
            {
            }
        }
    }
}
