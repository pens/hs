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
    public class Player : Actor
    {
        public new Vector2 Position;

        public List<Projectile> Bullets = new List<Projectile>();
        public Player(Texture2D texture, Vector2 position, float moveSpeed)
            : base(texture, GraphicsManager.ViewportCenter, moveSpeed)
        {
            Position = position; 
        }

        public void Move()
        {
            if (InputManager.KeyDown(Keys.W, false))
            {
                Position.Y -= MoveSpeed;
            }
            if (InputManager.KeyDown(Keys.S, false))
            {
                Position.Y += MoveSpeed;
            }
            if (InputManager.KeyDown(Keys.A, false))
            {
                Position.X -= MoveSpeed;
            }
            if (InputManager.KeyDown(Keys.D, false))
            {
                Position.X += MoveSpeed;
            }
            Rotation = (float)Math.Atan2((Mouse.GetState().Y - base.Position.Y), (Mouse.GetState().X - base.Position.X));
            if (InputManager.MouseLClicked(true))
            {
                Bullets.Add(new Projectile(Texture, Position + base.Position, 1000f, Rotation, 20f));
            }
        }
    }
}
