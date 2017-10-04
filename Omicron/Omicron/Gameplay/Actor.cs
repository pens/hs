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
    public class Actor : Entity
    {
        public string Name;
        public float MaxHealth;
        public float Health;
        public float Speed;
        public float AttackSpeed;
        public float Damage;

        public Vector2 Normal;

        public Actor(Texture2D texture, Vector2 position, string name, float health, float speed, float attackSpeed, float damage)
            : base(texture, position)
        {
            Name = name;
            MaxHealth = Health = health;
            Speed = speed;
            AttackSpeed = attackSpeed;
            Damage = damage;
        }

        public void Update(GameTime gameTime)
        {
            if (Health <= 100)
                OnDeath();
        }
    }
}
