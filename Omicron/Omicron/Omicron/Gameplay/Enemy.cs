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
    public class Enemy : GameObject
    {
        public float MaxHealth;
        public float Health;
        public float Speed;
        float damage;
        float attackSpeed;

        Texture2D[] Textures;
        string[] Strings =
        {
            "ZombieHead",
            "ZombieChest",
            "ZombieArm",
            "ZombieFeet",
        };

        Sprite head;
        Sprite chest;
        Sprite armR;
        Sprite armL;
        Sprite feet;

        public Enemy()
        {
            Strings = new string[]
            {
                "ZombieHead",
                "ZombieChest",
                "ZombieArm",
                "ZombieFeet"
            };
            head = new Sprite();
            chest = new Sprite();
            armR = new Sprite();
            armL = new Sprite();
            feet = new Sprite();
        }

        public override void Load(ContentManager content)
        {
        }
        public override void Update(GameTime gameTime)
        {
            Vector2 dirVec = Vector2.Normalize(Player.Position - Position);
            Position += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds * dirVec;
            armR.Position = armL.Position = head.Position = chest.Position = feet.Position = 
                Position;
            armR.Rotation = armL.Rotation = head.Rotation = chest.Rotation = feet.Rotation =
                (float)Math.Atan2(dirVec.Y, dirVec.X);
            armR.Position += Vector2.Transform(new Vector2(0, 24), Matrix.CreateRotationZ(head.Rotation));
            armL.Position -= Vector2.Transform(new Vector2(0, 24), Matrix.CreateRotationZ(head.Rotation));
        }
        public override void Draw()
        {
            GraphicsManager.DrawSprite(feet);
            GraphicsManager.DrawSprite(armR);
            GraphicsManager.DrawSprite(armL);
            GraphicsManager.DrawSprite(chest);
            GraphicsManager.DrawSprite(head);
        }

        public override void Reset(string type, Vector2 position, float rotation)
        {
            Position = position;
            Rotation = rotation;
            switch (type)
            {
                case "Zombie":
                default:
                    {
                        MaxHealth = 100;
                        Health = 100;
                        Speed = 250;
                        attackSpeed = .5f;
                        damage = 25;
                        head.Texture = Textures[0];
                        chest.Texture = Textures[1];
                        feet.Texture = Textures[3];
                        armR.Texture = Textures[2];
                        armR.Origin = new Vector2(0, 6);
                        armL.Texture = Textures[2];
                        armL.Origin = new Vector2(0, 6);
                        armR.Position = armL.Position = head.Position = chest.Position = feet.Position =
                            Position;
                        armR.Position += Vector2.Transform(new Vector2(0, 24), Matrix.CreateRotationZ(Rotation));
                        armL.Position -= Vector2.Transform(new Vector2(0, 24), Matrix.CreateRotationZ(Rotation));
                        break;
                    }
            }
        }
    }
}
