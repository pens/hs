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
    public class Pool<T> where T : GameObject, new()
    {
        T[] objects;
        int count;

        public Pool()
        {
            objects = new T[99];
            for (int i = 0; i < objects.Count(); i++)
            {
                objects[i] = new T();
                objects[i].Death += Remove;
            }
            count = 0;
        }

        public void Add(string type, Vector2 position, float rotation)
        {
            if (count < objects.Length)
            {
                count++;
                objects[count - 1].Reset(type, position, rotation);
            }
        }
        public void Load(ContentManager content)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].Load(content);
            }
        }
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < count; i++)
            {
                objects[i].Update(gameTime);
            }
        }
        public void Draw()
        {
            for (int i = 0; i < count; i++)
            {
                objects[i].Draw();
            }
        }

        private void Remove(object sender, EventArgs e)
        {
            if (count > 0)
            {
                objects[Array.IndexOf(objects, (T)sender)] = objects[count - 1];
                count--;
            }
        }
    }
}
