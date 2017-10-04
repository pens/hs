using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Omicron
{
    public interface IScreen
    {
        void Load(ContentManager Content);
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}
