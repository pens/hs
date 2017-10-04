using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Omicron
{
    public class Text
    {
        public SpriteFont Font
        {
            get { return font; }
            set 
            { 
                font = value;
                SetRelativeOrigin(relativeOrigin);
            }
        }
        public string String
        {
            get { return text; }
            set 
            { 
                text = value;
                SetRelativeOrigin(relativeOrigin);
            }
        }
        public Vector2 Position;
        public Color Color;
        public Vector2 Origin;
        public float Scale
        {
            get { return scale; }
            set 
            {
                scale = value;
                SetRelativeOrigin(relativeOrigin);
            }
        }

        SpriteFont font;
        string text;
        Vector2 relativeOrigin;
        float scale;

        public Text(SpriteFont font, string text, Color color, float scale)
        {
            this.font = font;
            this.text = text;
            Color = color;
            SetRelativeOrigin(new Vector2(.5f, .5f));
            this.scale = scale;
        }
        public Text(SpriteFont font, string text, Color color, Vector2 relativeOrigin, float scale)
        {
            this.font = font;
            this.text = text;
            Color = color;
            SetRelativeOrigin(relativeOrigin);
            this.scale = scale;
        }
        public Text(SpriteFont font, string text, Color color, Vector2 position, Vector2 relativeOrigin, float scale)
        {
            this.font = font;
            this.text = text;
            Position = position;
            Color = color;
            SetRelativeOrigin(relativeOrigin);
            this.scale = scale;
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)(Position.X - Origin.X * scale), (int)(Position.Y - Origin.Y * scale), (int)(font.MeasureString(text).X * scale), (int)(font.MeasureString(text).Y * scale));
        }
        public void SetRelativeOrigin(Vector2 relativeOrigin)
        {
            this.relativeOrigin = relativeOrigin;
            Origin.X = relativeOrigin.X * font.MeasureString(text).X;
            Origin.Y = relativeOrigin.Y * font.MeasureString(text).Y;
        }
    }
}
