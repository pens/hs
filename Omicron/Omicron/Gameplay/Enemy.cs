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
    public class Enemy
    {
        public string Name;
        public float MaxHealth;
        public float Health;
        public float Speed;
        public float AttackSpeed;
        public float Damage;

        public Vector2 Normal;

        Sprite Head;
        Sprite Chest;
        Sprite RightArm;
        Sprite LeftArm;
        Sprite Feet;

        Vector2 position;

        static Texture2D[] textures;
        static string[] strings = new string[]
        {
            "ZombieHead",
            "ZombieChest",
            "ZombieArm",
            "ZombieFeet"
        };

        public Enemy(int type, Vector2 spawnLocation)
        {
            Head = new Sprite(textures[0]);
            Chest = new Sprite(textures[1]);
            Feet = new Sprite(textures[3]);
            RightArm = new Sprite(textures[2]);
            RightArm.Origin = new Vector2(0, 6);
            LeftArm = new Sprite(textures[2]);
            LeftArm.Origin = new Vector2(0, 6);
            position = spawnLocation;
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

        public void Update(GameTime gameTime, Vector2 playerLoc)
        {
            Vector2 dirVec = Vector2.Normalize(playerLoc - position);
            position += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds * dirVec;
            RightArm.Position = LeftArm.Position = Head.Position = Chest.Position = Feet.Position = position;
            RightArm.Rotation = LeftArm.Rotation = Head.Rotation = Chest.Rotation = Feet.Rotation =
                (float)Math.Atan2(dirVec.Y, dirVec.X);
            RightArm.Position += Vector2.Transform(new Vector2(0, 24), Matrix.CreateRotationZ(Head.Rotation));
            LeftArm.Position -= Vector2.Transform(new Vector2(0, 24), Matrix.CreateRotationZ(Head.Rotation));
        }

        public void Draw()
        {
            GraphicsManager.DrawSprite(Feet);
            GraphicsManager.DrawSprite(RightArm);
            GraphicsManager.DrawSprite(LeftArm);
            GraphicsManager.DrawSprite(Chest);
            GraphicsManager.DrawSprite(Head);
        }

        public void BulletColl(Bullet bullet)
        {
            if (CollisionManager.PixelCollision(Head, bullet.Sprite))
            {
                Health -= bullet.Damage;
            }
            else if (CollisionManager.PixelCollision(Chest, bullet.Sprite))
            {
                Health -= bullet.Damage;
            }
            else if (CollisionManager.PixelCollision(RightArm, bullet.Sprite))
            {
                Health -= bullet.Damage;
            }
            else if (CollisionManager.PixelCollision(LeftArm, bullet.Sprite))
            {
                Health -= bullet.Damage;
            }
            else if (CollisionManager.PixelCollision(Feet, bullet.Sprite))
            {
                Health -= bullet.Damage;
            }
            if (Health <= 0)
                OnDeath();
        }

        void SetType(int i)
        {
            if (i == 0)
            {
                Name = "Zombie";
                MaxHealth = 100;
                Health = 100;
                Speed = 250;
                AttackSpeed = .5f;
                Damage = 25;
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
