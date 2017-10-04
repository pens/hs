using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Omicron
{
    public interface ILevel
    {
        void Load(ContentManager Content);
        void HandleInput(GameTime gameTime);
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
        void Unload();
        IScreen LoadScreen { get; }
        IScreen PauseScreen { get; }
    }
}
