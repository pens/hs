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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        NC nickL = new NC(true);
        NC nickR = new NC(false);

        Texture2D stars;
        float rotation;

        int l;
        int r;

        Texture2D head;
        float rot;
        bool inGame = false;

        Texture2D arm;

        string winner = "";

        SpriteFont ocra;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            nickL.LoadContent(Content);
            nickR.LoadContent(Content);
            head = Content.Load<Texture2D>("head");
            stars = Content.Load<Texture2D>("stars");
            ocra = Content.Load<SpriteFont>("ocra");
            arm = Content.Load<Texture2D>("arm");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (inGame)
            {
                rotation += .05f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (rotation > MathHelper.ToRadians(360))
                    rotation -= MathHelper.ToRadians(360);
                nickL.Update(gameTime);
                nickL.Coll(nickR);
                nickR.Update(gameTime);
                nickR.Coll(nickL);
                if (nickL.Health <= 0)
                {
                    winner = "R Wins>";
                    r++;
                    inGame = false;
                }
                else if (nickR.Health <= 0)
                {
                    winner = "<L Wins";
                    l++;
                    inGame = false;
                }
            }
            else
            {
                rot += .25f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    inGame = true;
                    nickL.Reset();
                    nickR.Reset();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    Exit();
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            if (inGame)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(stars, new Vector2(640, 360), null, Color.White, rotation, new Vector2(stars.Width / 2, stars.Height / 2), .75f, SpriteEffects.None, 1);
                spriteBatch.Draw(arm, new Vector2(450, -25), null, Color.White, MathHelper.ToRadians(90), Vector2.Zero, 2.5f, SpriteEffects.None, 1);
                spriteBatch.Draw(arm, new Vector2(830, -25), null, Color.White, MathHelper.ToRadians(90), new Vector2(0, arm.Height), 2.5f, SpriteEffects.FlipVertically, 1);
                spriteBatch.DrawString(ocra, nickL.Health.ToString(), new Vector2(130, 10), Color.White);
                spriteBatch.DrawString(ocra, nickR.Health.ToString(), new Vector2(1150, 10), Color.White, 0, new Vector2(ocra.MeasureString(nickR.Health.ToString()).X, 0), 1, SpriteEffects.None, 0); 
                nickL.Draw(spriteBatch);
                nickR.Draw(spriteBatch);
                spriteBatch.End();
            }
            else
            {
                spriteBatch.Begin();
                spriteBatch.Draw(head, new Vector2(640, 360) + Vector2.Transform(new Vector2(0, 180), Matrix.CreateRotationZ(rot)), null, Color.White, 0, new Vector2(head.Width / 2, head.Height / 2), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(head, new Vector2(640, 360) + Vector2.Transform(new Vector2(0, 180), Matrix.CreateRotationZ(rot + MathHelper.ToRadians(90))), null, Color.White, 0, new Vector2(head.Width / 2, head.Height / 2), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(head, new Vector2(640, 360) + Vector2.Transform(new Vector2(0, 180), Matrix.CreateRotationZ(rot + MathHelper.ToRadians(180))), null, Color.White, 0, new Vector2(head.Width / 2, head.Height / 2), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(head, new Vector2(640, 360) + Vector2.Transform(new Vector2(0, 180), Matrix.CreateRotationZ(rot + MathHelper.ToRadians(270))), null, Color.White, 0, new Vector2(head.Width / 2, head.Height / 2), 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(ocra, "Nicolas", new Vector2(180, 50), Color.White);
                spriteBatch.DrawString(ocra, "Cagefight", new Vector2(1100, 50), Color.White, 0, new Vector2(ocra.MeasureString("Cagefight").X, 0), 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(ocra, "L wins:" + l.ToString(), new Vector2(180, 360), Color.White, 0, ocra.MeasureString("L wins:" + l.ToString()) / 2, 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(ocra, "R wins:" + r.ToString(), new Vector2(1100, 360), Color.White, 0, ocra.MeasureString("R wins:" + r.ToString()) / 2, 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(ocra, winner, new Vector2(640, 360), Color.White, 0, ocra.MeasureString(winner) / 2, 1, SpriteEffects.None, 0); 
                spriteBatch.DrawString(ocra, "Press", new Vector2(180, 670), Color.White, 0, new Vector2(0, ocra.MeasureString("Press").Y), 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(ocra, "Enter", new Vector2(1100, 670), Color.White, 0, ocra.MeasureString("Enter"), 1, SpriteEffects.None, 0);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}
