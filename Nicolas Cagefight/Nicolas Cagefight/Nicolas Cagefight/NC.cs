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

namespace Nicolas_Cagefight
{
    public class NC
    {
        public float Health = 100;
        public enum Actions
        {
            Still,
            Hitting,
            Kicking
        }
        public Actions State = Actions.Still;

        bool left;
        int dir = 1;

        KeyboardState keys;
        Keys l;
        Keys r;
        Keys hit;
        Keys kick;

        public Texture2D body;
        public Vector2 position;
        float moveSpeed = 300;

        public Texture2D arm;
        float armRot;
        float armSpeed = 20;
        int armDamage = 10;
        double armTime = .5;
        Vector2 armOrigin;
        Vector2 armOffset;
        Color[] armData;
        double armTimer;

        public Texture2D leg;
        float legRot;
        float legSpeed = 10;
        int legDamage = 20;
        double legTime = 1;
        Vector2 legOrigin;
        Vector2 legOffset;
        Color[] legData;
        double legTimer;

        public float slide;
        float slideSpeed = 600;

        bool hasHit;

        public Rectangle Rect
        {
            get { return new Rectangle((int)position.X, (int)position.Y, body.Width, body.Height); }
        }

        public NC(bool left)
        {
            this.left = left;
        }

        public void LoadContent(ContentManager content)
        {
            if (left)
            {
                arm = content.Load<Texture2D>("arm");
                leg = content.Load<Texture2D>("leg");
                body = content.Load<Texture2D>("nick");
                position = new Vector2(0, 720 - body.Height);
            }
            else
            {
                arm = content.Load<Texture2D>("arm2");
                leg = content.Load<Texture2D>("leg2");
                body = content.Load<Texture2D>("nick2");
                position = new Vector2(1280 - body.Width, 720 - body.Height);
            }
            armData = new Color[arm.Width * arm.Height];
            arm.GetData<Color>(armData);
            legData = new Color[leg.Width * leg.Height];
            leg.GetData<Color>(legData);
            if (left)
            {
                armOrigin = new Vector2(8, 40);
                armOffset = new Vector2(145, 114);
                legOrigin = new Vector2(24, 20);
                legOffset = new Vector2(114, 319);
                l = Keys.A;
                r = Keys.D;
                hit = Keys.W;
                kick = Keys.S;
            }
            else
            {
                dir = -1;
                armOrigin = new Vector2(arm.Width - 8, 40);
                armOffset = new Vector2(body.Width - 145, 114);
                legOrigin = new Vector2(leg.Width - 24, 20);
                legOffset = new Vector2(body.Width - 114, 319);
                l = Keys.Left;
                r = Keys.Right;
                hit = Keys.Up;
                kick = Keys.Down;
            }
        }

        public void Update(GameTime gameTime)
        {
            keys = Keyboard.GetState();
            if (keys.IsKeyDown(l) && slide <= 0)
            {
                position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (keys.IsKeyDown(r) && slide <= 0)
            {
                position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (keys.IsKeyDown(hit) && armRot == 0 && State == Actions.Still && armTimer >= armTime)
            {
                State = Actions.Hitting;
                armTimer = 0;
            }
            else if (keys.IsKeyDown(kick) && legRot == 0 && State == Actions.Still && legTimer >= legTime)
            {
                State = Actions.Kicking;
                legTimer = 0;
            }
            switch (State)
            {
                case Actions.Still:
                    {
                        if (armRot * dir < 0)
                        {
                            armRot += dir * armSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }
                        if (armRot * dir > 0)
                            armRot = 0;
                        if (legRot * dir < 0)
                        {
                            legRot += dir * legSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }
                        if (legRot * dir > 0)
                            legRot = 0;
                        break;
                    }
                case Actions.Hitting:
                    {
                        if (armRot * dir > -MathHelper.PiOver2)
                        {
                            armRot -= dir * armSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }
                        if (armRot * dir <= -MathHelper.PiOver2)
                        {
                            armRot = dir * -MathHelper.PiOver2;
                            State = Actions.Still;
                        }
                        break;
                    }
                case Actions.Kicking:
                    {
                        if (legRot * dir > -MathHelper.PiOver2)
                        {
                            legRot -= dir * legSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }
                        if (legRot * dir <=  -MathHelper.PiOver2)
                        {
                            legRot = dir * -MathHelper.PiOver2;
                            State = Actions.Still;
                        }
                        break;
                    }
            }
            if (State != Actions.Hitting && armTimer < armTime)
                armTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (State != Actions.Hitting && legTimer < legTime)
                legTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (slide > 0)
            {
                if (left)
                    position.X -= slideSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else position.X += slideSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                slide -= slideSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(arm, position + armOffset, null, Color.White, armRot, armOrigin, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(leg, position + legOffset, null, Color.White, legRot, legOrigin, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(body, position, Color.White);
        }
        public void Coll(NC nc)
        {
            if (left && position.X < 0)
            {
                position.X = 0;
                slide = 0;
            }
            else if (position.X > 1280 - body.Width)
            {
                position.X = 1280 - body.Width;
                slide = 0;
            }
            hasHit = false;
            if (Rect.Intersects(nc.Rect))
            {
                if (left)
                {
                    position.X -= position.X + Rect.Width - nc.position.X;
                }
                else
                {
                    position.X += nc.position.X + nc.Rect.Width - position.X;
                }
            }
            switch (State)
            {
                case Actions.Hitting:
                    {
                        if (GetArmBounds().Intersects(nc.GetArmBounds()))
                        {
                            Matrix transform1To2 = GetArmTransform() * Matrix.Invert(nc.GetArmTransform());

                            Vector2 unitX = Vector2.TransformNormal(Vector2.UnitX, transform1To2);
                            Vector2 unitY = Vector2.TransformNormal(Vector2.UnitY, transform1To2);

                            Vector2 pos2Y = Vector2.Transform(Vector2.Zero, transform1To2);

                            for (int y1 = 0; y1 < arm.Height; y1++)
                            {
                                Vector2 pos2 = pos2Y;

                                for (int x1 = 0; x1 < arm.Width; x1++)
                                {
                                    int x2 = (int)Math.Round(pos2.X);
                                    int y2 = (int)Math.Round(pos2.Y);

                                    if (0 <= x2 && x2 < nc.arm.Width &&
                                        0 <= y2 && y2 < nc.arm.Height)
                                    {
                                        Color colorA = armData[x1 + y1 * arm.Width];
                                        Color colorB = nc.armData[x2 + y2 * nc.arm.Width];

                                        if (colorA.A != 0 && colorB.A != 0)
                                        {
                                            if (!hasHit)
                                            {
                                                if (nc.State != Actions.Hitting)
                                                {
                                                    nc.Health -= armDamage;
                                                }
                                                else nc.State = Actions.Still;
                                                State = Actions.Still;
                                                hasHit = true;
                                            }
                                        }
                                    }
                                    pos2 += unitX;
                                }
                                pos2Y += unitY;
                            }
                        }
                        break;
                    }
                case Actions.Kicking:
                    {
                        if (GetLegBounds().Intersects(nc.GetLegBounds()))
                        {
                            Matrix transform1To2 = GetLegTransform() * Matrix.Invert(nc.GetLegTransform());

                            Vector2 unitX = Vector2.TransformNormal(Vector2.UnitX, transform1To2);
                            Vector2 unitY = Vector2.TransformNormal(Vector2.UnitY, transform1To2);

                            Vector2 pos2Y = Vector2.Transform(Vector2.Zero, transform1To2);

                            for (int y1 = 0; y1 < leg.Height; y1++)
                            {
                                Vector2 pos2 = pos2Y;

                                for (int x1 = 0; x1 < leg.Width; x1++)
                                {
                                    int x2 = (int)Math.Round(pos2.X);
                                    int y2 = (int)Math.Round(pos2.Y);

                                    if (0 <= x2 && x2 < nc.leg.Width &&
                                        0 <= y2 && y2 < nc.leg.Height)
                                    {
                                        Color colorA = legData[x1 + y1 * leg.Width];
                                        Color colorB = nc.legData[x2 + y2 * leg.Width];

                                        if (colorA.A != 0 && colorB.A != 0)
                                        {
                                            if (!hasHit)
                                            {
                                                if (nc.State != Actions.Kicking)
                                                {
                                                    nc.Health -= legDamage;
                                                    nc.slide = 300;
                                                }
                                                else nc.State = Actions.Still;
                                                State = Actions.Still;
                                                hasHit = true;
                                            }
                                        }
                                    }
                                    pos2 += unitX;
                                }
                                pos2Y += unitY;
                            }
                        }
                        break;
                    }
            }
        }

        public void Reset()
        {
            if (left)
                position = new Vector2(0, 720 - body.Height);
            else position = new Vector2(1280 - body.Width, 720 - body.Height);
            Health = 100;
            armRot = 0;
            armTimer = 0;
            legRot = 0;
            legTimer = 0;
        }

        public Rectangle GetArmBounds()
        {
            Vector2 leftTop = Vector2.Transform(new Vector2(0), GetArmTransform());
            Vector2 rightTop = Vector2.Transform(new Vector2(arm.Width, 0), GetArmTransform());
            Vector2 leftBottom = Vector2.Transform(new Vector2(0, arm.Height), GetArmTransform());
            Vector2 rightBottom = Vector2.Transform(new Vector2(arm.Width, arm.Height), GetArmTransform());

            Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop), Vector2.Min(leftBottom, rightBottom));
            Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop), Vector2.Max(leftBottom, rightBottom));

            return new Rectangle((int)Math.Floor(min.X), (int)Math.Floor(min.Y), (int)Math.Ceiling(max.X - min.X), (int)Math.Ceiling(max.Y - min.Y));
        }
        public Rectangle GetLegBounds()
        {
            Vector2 leftTop = Vector2.Transform(new Vector2(leg.Bounds.X, leg.Bounds.Y), GetLegTransform());
            Vector2 rightTop = Vector2.Transform(new Vector2(leg.Bounds.X + leg.Bounds.Width, leg.Bounds.Y), GetLegTransform());
            Vector2 leftBottom = Vector2.Transform(new Vector2(leg.Bounds.X, leg.Bounds.Y + leg.Bounds.Height), GetLegTransform());
            Vector2 rightBottom = Vector2.Transform(new Vector2(leg.Bounds.X + leg.Bounds.Width, leg.Bounds.Y + leg.Bounds.Height), GetLegTransform());

            Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop), Vector2.Min(leftBottom, rightBottom));
            Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop), Vector2.Max(leftBottom, rightBottom));

            return new Rectangle((int)Math.Floor(min.X), (int)Math.Floor(min.Y), (int)Math.Ceiling(max.X - min.X), (int)Math.Ceiling(max.Y - min.Y));
        }
        public Matrix GetArmTransform()
        {
            return Matrix.CreateTranslation(new Vector3(-armOrigin, 0)) *
            Matrix.CreateRotationZ(armRot) *
            Matrix.CreateTranslation(new Vector3(position + armOffset, 0));
        }
        public Matrix GetLegTransform()
        {
            return Matrix.CreateTranslation(new Vector3(-legOrigin, 0)) *
            Matrix.CreateRotationZ(legRot) *
            Matrix.CreateTranslation(new Vector3(position + legOffset, 0));
        }
    }
}
