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
    public class Entity : Sprite
    {
        public string Name;
        public float MaxHealth;
        public float Health;
        public float Speed;
        public float AttackSpeed;
        public int Ammo;

        public Entity(Texture2D texture, Vector2 position)
            : base(texture, position) { }
    }
}
