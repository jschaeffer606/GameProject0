using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject0.Collisions;


namespace GameProject0
{


    public class MinerSprite
    {



        private GamePadState gamePadState;

        private KeyboardState keyboardState;

        private Texture2D texture;





        private Vector2 position = new Vector2(300, 200);

        private bool flipped;


        private double directionTimer;
        private double animationTimer;
        private short animationFrame;

 

        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(300 - 8, 200 - 4), 24, 24);

        public BoundingRectangle Bounds => bounds;


        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("miner");
        }

        /// <summary>
        /// Updates the sprite's position based on user input
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        public void Update(GameTime gameTime)
        {
            bool moving = false;
            keyboardState = Keyboard.GetState();


            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;


            if (animationTimer > 0.2)
            {
                animationFrame++;
                if (animationFrame > 1) animationFrame = 0;
                animationTimer -= .2;

            }






            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W)) { position += new Vector2(0, -1); moving = true; }
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S)) { position += new Vector2(0, 1); moving = true; }
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                position += new Vector2(-1, 0);
                flipped = true;
                moving = true;
            }
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                position += new Vector2(1, 0);
                flipped = false;
                moving = true;
            }
            if(!moving)
            {


                animationFrame = 0;
            }




            //autowalk
            /*
            if (flipped)
            {
                position += new Vector2(-1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                position += new Vector2(1, 0 ) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            */
            moving = false;
            bounds.X = position.X - 32;
            bounds.Y = position.Y - 32;
        }

        /// <summary>
        /// Draws the sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            


            SpriteEffects spriteEffects = (flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            


            var source = new Rectangle(0, animationFrame * 32, 32, 32);

            spriteBatch.Draw(texture, position, source, Color.White, 0, new Vector2(64, 64), 2f, spriteEffects, 0);

        }
    }
}
