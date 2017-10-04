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
    public abstract class GameObject
    {
        public Vector2 Position;
        public float Rotation;

        public abstract void Load(ContentManager content);
        public abstract void Update(GameTime gameTime);
        public abstract void Draw();

        public abstract void Reset(string type, Vector2 position, float rotation);

        public abstract OBB GetOBB();

        public event EventHandler Death;
        public void OnDeath()
        {
            if (Death != null)
                Death(this, EventArgs.Empty);
        }
    }
}
