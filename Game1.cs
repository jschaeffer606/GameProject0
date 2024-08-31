using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;




namespace GameProject0
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont spriteFont;
        private Texture2D _backGround;


        private DynamiteSprite[] _dynamiteGroup;

        private MinerSprite _miner;


        private Vector2 _minerPosition;


        private Vector2 _minerVelocity;




        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _miner = new MinerSprite();

            _dynamiteGroup = new DynamiteSprite[]
            {

                new DynamiteSprite( new Vector2(600, 400)){ flipped = false},
                new DynamiteSprite( new Vector2(200, 400)),
                new DynamiteSprite( new Vector2(400, 400))
            };



            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("FONT");
            _miner.LoadContent(Content);
            _backGround = Content.Load<Texture2D>("BackGround");

            foreach (var dyn in _dynamiteGroup)
            {
                dyn.LoadContent(Content);


            }

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _miner.Update(gameTime);
            


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();
            _spriteBatch.Draw(_backGround, new Rectangle(0, 0, 800, 480), Color.White);
    

            // TODO: Add your drawing code here
           
            _spriteBatch.DrawString(spriteFont , $"Welcome to the game! Press Escape to exit", new Vector2(200, 200), Color.Black);
            _spriteBatch.DrawString(spriteFont, $"DOWNWARDS", new Vector2(200,250), Color.Black,0,new Vector2(0,0), 3f, SpriteEffects.None, 1);


            _miner.Draw(gameTime, _spriteBatch);

            foreach(var dyn in _dynamiteGroup)
            {
                dyn.Draw(gameTime, _spriteBatch);

            }

            base.Draw(gameTime);
            _spriteBatch.End();

        }




    }
}