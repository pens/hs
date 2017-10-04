using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Omicron
{
    public static class CollisionManager
    {
        public static bool PixelCollision(Sprite sprite1, Sprite sprite2)
        {
            if (sprite1.GetBounds().Intersects(sprite2.GetBounds()))
            {
                Matrix transform1To2 = sprite1.GetTransform() * Matrix.Invert(sprite2.GetTransform());

                Vector2 unitX = Vector2.TransformNormal(Vector2.UnitX, transform1To2);
                Vector2 unitY = Vector2.TransformNormal(Vector2.UnitY, transform1To2);

                Vector2 pos2Y = Vector2.Transform(Vector2.Zero, transform1To2);

                for (int y1 = 0; y1 < sprite1.SourceRectangle.Height; y1++)
                {
                    Vector2 pos2 = pos2Y;

                    for (int x1 = 0; x1 < sprite1.SourceRectangle.Width; x1++)
                    {
                        int x2 = (int)Math.Round(pos2.X);
                        int y2 = (int)Math.Round(pos2.Y);

                        if (0 <= x2 && x2 < sprite2.SourceRectangle.Width &&
                            0 <= y2 && y2 < sprite2.SourceRectangle.Height)
                        {
                            Color colorA = sprite1.ColorData[x1 + y1 * sprite1.SourceRectangle.Width];
                            Color colorB = sprite2.ColorData[x2 + y2 * sprite2.SourceRectangle.Width];

                            if (colorA.A != 0 && colorB.A != 0)
                                return true;
                        }
                        pos2 += unitX;
                    }
                    pos2Y += unitY;
                }
                return false;
            }
            else return false;
        }

        public static bool RectCollision(Sprite sprite1, Sprite sprite2)
        {
            return sprite1.GetBounds().Intersects(sprite2.GetBounds());
        }
        public static Vector2 IntersectionVector(Vector2 velNormal, Sprite sprite1, Sprite sprite2)
        {
            if (sprite1.GetBounds().Intersects(sprite2.GetBounds()))
            {
                Vector2 intersection = new Vector2(Math.Min(sprite1.GetBounds().Right, sprite2.GetBounds().Right) - Math.Max(sprite1.GetBounds().Left, sprite2.GetBounds().Left),
                    Math.Min(sprite1.GetBounds().Bottom, sprite2.GetBounds().Bottom) - Math.Max(sprite1.GetBounds().Top, sprite2.GetBounds().Top));
                float scale = Math.Min(Math.Abs(intersection.X / -velNormal.X), Math.Abs(intersection.Y / -velNormal.Y));
                if (Math.Abs(intersection.X / -velNormal.X) < Math.Abs(intersection.Y / -velNormal.Y))
                {
                    return scale * new Vector2(-velNormal.X, 0);
                }
                else if (Math.Abs(intersection.X / -velNormal.X) > Math.Abs(intersection.Y / -velNormal.Y))
                {
                    return scale * new Vector2(0, -velNormal.Y);
                }
                else if (Math.Abs(intersection.X / -velNormal.X) == Math.Abs(intersection.Y / -velNormal.Y))
                {
                    return scale * -velNormal;
                }
                return Vector2.Zero;
            }
            else return Vector2.Zero;
        }
    }
}
