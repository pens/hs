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
    public class Pool<T> where T : Entity, new()
    {
        Entity[] entities;
        int count;

        public Pool()
        {
            entities = new Entity[99];
            for (int i = 0; i < entities.Count(); i++)
            {
                entities[i] = new T();
                entities[i].Death += Remove;
            }
            count = 0;
        }

        public void Add()
        {
            count++;
            entities[count - 1].Reset();
        }

        private void Remove(object sender, EventArgs e)
        {
            entities[Array.IndexOf(entities, (T)sender)] = entities[count];
            count--;
        }
    }
}
