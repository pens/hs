using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Omicron
{
    public interface IMenu
    {
        void Load(ContentManager Content);
        void HandleInput(GameTime gameTime);
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
        void Unload();
        void TransOn(GameTime gameTime, bool Returning);
        void TransOff(GameTime gameTime, bool Returning);
    }
}
