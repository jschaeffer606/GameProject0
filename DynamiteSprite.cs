using GameProject0.Collisions;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace GameProject0
{
    public class DynamiteSprite
    {
        private const float ANIMATION_SPEED = 0.1f;

        private double animationTimer;

        private int animationFrame;

        private Vector2 position;

        private Texture2D texture;

        private BoundingCircle bounds;

        public bool flipped { get; set; } = false;


        /// <summary>
        /// Creates a new coin sprite
        /// </summary>
        /// <param name="position">The position of the sprite in the game</param>
        public DynamiteSprite(Vector2 position)
        {
            this.position = position;
            this.bounds = new BoundingCircle(position - new Vector2(-8, -8), 8);
        }

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Dynamite");
        }

        /// <summary>
        /// Draws the animated sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            

            var source = new Rectangle(0, 0, 32, 32);

        

            SpriteEffects spriteEffects = (flipped) ? SpriteEffects.FlipVertically : SpriteEffects.None;

            spriteBatch.Draw(texture, position, source, Color.White, 0, new Vector2(64, 64), 1f, spriteEffects, 0);

        
        }











    }
}
