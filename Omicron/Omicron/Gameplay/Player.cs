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
    public class Player
    {
        public string Name;
        public float MaxHealth;
        public float Health;
        public float Speed;

        Text health;

        Sprite Head;
        Sprite Chest;
        Sprite Feet;
        Sprite RightArm;
        Sprite LeftArm;
        Sprite Gun;
        public Vector2 position;

        public Bullet[] bullets = new Bullet[99];
        public int active;

        static SpriteFont ocra;

        static Texture2D[] textures;
        static string[] strings = new string[]
        {
            "Head",
            "Chest",
            "Arm",
            "Feet",
            "LaserGun"
        };

        public Player()
        {
            health = new Text(ocra, "", Color.White, Vector2.Zero, Vector2.Zero, .25f);
            Head = new Sprite(textures[0]);
            Chest = new Sprite(textures[1]);
            Feet = new Sprite(textures[3]);
            RightArm = new Sprite(textures[2]);
            RightArm.Origin = new Vector2(0, 6);
            LeftArm = new Sprite(textures[2]);
            LeftArm.Origin = new Vector2(0, 6);
            Gun = new Sprite(textures[4]);
            Gun.Origin = new Vector2(0, 6);
            position = GraphicsManager.ViewportCenter;
            for (int i = 0; i < 99; i++)
            {
                bullets[i] = new Bullet();
                bullets[i].Death += KillBullet;
            }
            MaxHealth = Health = 100;
            Speed = 350;
        }

        public static void Load(ContentManager Content)
        {
            textures = new Texture2D[strings.Count()];
            for (int i = 0; i < strings.Count(); i++)
            {
                textures[i] = Content.Load<Texture2D>(strings[i]);
            }
            ocra = Content.Load<SpriteFont>("ocra72");
        }
        public void Update(GameTime gameTime)
        {
            if (InputManager.KeyDown(Keys.W, false))
            {
                position.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (InputManager.KeyDown(Keys.S, false))
            {
                position.Y += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (InputManager.KeyDown(Keys.A, false))
            {
                position.X -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (InputManager.KeyDown(Keys.D, false))
            {
                position.X += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            Gun.Position = RightArm.Position = LeftArm.Position = Head.Position = Chest.Position = Feet.Position = position;
            Gun.Rotation = RightArm.Rotation = LeftArm.Rotation = Head.Rotation = Chest.Rotation = Feet.Rotation =
                (float)Math.Atan2(-Head.Position.Y + InputManager.MousePos().Y, -Head.Position.X + InputManager.MousePos().X);
            Gun.Position += Vector2.Transform(new Vector2(16, 0), Matrix.CreateRotationZ((float)Math.Atan2(-Head.Position.Y + InputManager.MousePos().Y, -Head.Position.X + InputManager.MousePos().X)));
            RightArm.Position += Vector2.Transform(new Vector2(0, 24), Matrix.CreateRotationZ(Head.Rotation));
            LeftArm.Position -= Vector2.Transform(new Vector2(0, 24), Matrix.CreateRotationZ(Head.Rotation));
            RightArm.Rotation -= MathHelper.ToRadians(45f);
            LeftArm.Rotation += MathHelper.ToRadians(45f);
            if (InputManager.MouseLClicked(true))
            {
                AddBullet(Gun.Position, Gun.Rotation);
            }
            for (int i = 0; i < active; i++)
            {
                bullets[i].Update(gameTime);
            }
            health.String = "Health: " + Health.ToString();
        }
        public void Draw()
        {
            GraphicsManager.DrawSprite(Feet);
            GraphicsManager.DrawSprite(RightArm);
            GraphicsManager.DrawSprite(LeftArm);
            GraphicsManager.DrawSprite(Gun);
            GraphicsManager.DrawSprite(Chest);
            GraphicsManager.DrawSprite(Head);
            for (int i = 0; i < active; i++)
            {
                bullets[i].Draw();
            }
            GraphicsManager.DrawText(health);
            GraphicsManager.EndSpriteBatch();
        }

        void AddBullet(Vector2 position, float rotation)
        {
            active++;
            bullets[active - 1].Reset(0, position, rotation);
        }
        void KillBullet(object sender, EventArgs e)
        {
            int a = Array.IndexOf(bullets, (Bullet)sender);
            bullets[a] = bullets[active - 1];
            active--;
        }
    }
}
