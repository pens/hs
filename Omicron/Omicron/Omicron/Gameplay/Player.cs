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
    public class Player : GameObject
    {
        public float MaxHealth;
        public float Health;
        public float Speed;

        Sprite head;
        Sprite chest;
        Sprite armR;
        Sprite armL;
        Sprite feet;
        Sprite gun;

        public override void Load(ContentManager content)
        {
            head = new Sprite();
            chest = new Sprite();
            armR = new Sprite();
            armL = new Sprite();
            feet = new Sprite();
            gun = new Sprite();
            head.Texture = content.Load<Texture2D>("Head");
            chest.Texture = content.Load<Texture2D>("Chest");
            armR.Texture = content.Load<Texture2D>("Arm");
            armL.Texture = content.Load<Texture2D>("Arm");
            feet.Texture = content.Load<Texture2D>("Feet");
            gun.Texture = content.Load<Texture2D>("LaserGun");
        }

        public void HandleInput(GameTime gameTime)
        {
            if (InputManager.KeyDown(Keys.W, false))
            {
                Position.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (InputManager.KeyDown(Keys.S, false))
            {
                Position.Y += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (InputManager.KeyDown(Keys.A, false))
            {
                Position.X -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (InputManager.KeyDown(Keys.D, false))
            {
                Position.X += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public override void Update(GameTime gameTime)
        {
            gun.Position = armR.Position = armL.Position = head.Position = chest.Position = feet.Position = Position;
            Rotation = gun.Rotation = armR.Rotation = armL.Rotation = head.Rotation = chest.Rotation = feet.Rotation =
                (float)Math.Atan2(-head.Position.Y + InputManager.MousePos().Y, -head.Position.X + InputManager.MousePos().X);
            gun.Position += Vector2.Transform(new Vector2(16, 0), Matrix.CreateRotationZ((float)Math.Atan2(-head.Position.Y + InputManager.MousePos().Y, -head.Position.X + InputManager.MousePos().X)));
            armR.Position += Vector2.Transform(new Vector2(0, 24), Matrix.CreateRotationZ(head.Rotation));
            armL.Position -= Vector2.Transform(new Vector2(0, 24), Matrix.CreateRotationZ(head.Rotation));
            armR.Rotation -= MathHelper.ToRadians(45f);
            armL.Rotation += MathHelper.ToRadians(45f);
        }
        public override void Draw()
        {
            GraphicsManager.DrawSprite(feet);
            GraphicsManager.DrawSprite(armR);
            GraphicsManager.DrawSprite(armL);
            GraphicsManager.DrawSprite(gun);
            GraphicsManager.DrawSprite(chest);
            GraphicsManager.DrawSprite(head);
        }

        public override OBB GetOBB()
        {
            return null;
        }

        public override void Reset(string type, Vector2 position, float rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}
