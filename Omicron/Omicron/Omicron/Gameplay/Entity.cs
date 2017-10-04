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
    public class Entity : GameObject
    {
        Sprite sprite;

        static Texture2D[] textures;
        static string[] Strings;

        public Entity()
        {
            sprite = new Sprite();
            Strings = new string[] { };
        }

        public override void Load(ContentManager content)
        {
        }
        public override void Update(GameTime gameTime)
        {
        }
        public override void Draw()
        {
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
