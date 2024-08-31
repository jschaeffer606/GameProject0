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

 

        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(200 - 16, 200 - 16), 32, 32);

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


            directionTimer += gameTime.ElapsedGameTime.TotalSeconds;

            //Switch directions every 2 seconds
            if (directionTimer > 3.5)
            {
                flipped = !flipped;

                directionTimer -= 3.5;
            }

            if(flipped)
            {
                position += new Vector2(-1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                position += new Vector2(1, 0 ) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

        }

        /// <summary>
        /// Draws the sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;


            SpriteEffects spriteEffects = (flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            

            if (animationTimer > 0.2)
            {
                animationFrame++;
                if (animationFrame > 1) animationFrame = 0;
                animationTimer -= .2;

            }

            var source = new Rectangle(0, animationFrame * 32, 32, 32);

            spriteBatch.Draw(texture, position, source, Color.White, 0, new Vector2(64, 64), 2f, spriteEffects, 0);

        }
    }
}
