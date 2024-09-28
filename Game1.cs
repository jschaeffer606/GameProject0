using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using GameProject0.StateManagement;
using GameProject0.Screens;



namespace GameProject0
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont spriteFont;
        private Texture2D _backGround;
        private SoundEffect _dynCollect;
        private Song _fire_level;



        private readonly ScreenManager _screenManager;

        private DynamiteSprite[] _dynamiteGroup;

        private MinerSprite _miner;


        private Vector2 _minerPosition;


        private Vector2 _minerVelocity;

        private int _dynamiteCounter;

        private bool _gameOver;
        private bool _gameWon = false;
        private int _maxTime; 

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            var screenFactory = new ScreenFactory();
            Services.AddService(typeof(IScreenFactory), screenFactory);

            _screenManager = new ScreenManager(this);
            _screenManager.Visible = true;
            Components.Add(_screenManager);

            AddInitialScreens();
        }

        private void AddInitialScreens()
        {
            _screenManager.AddScreen(new BackgroundScreen(), null);
            _screenManager.AddScreen(new MainMenuScreen(), null);
            

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            /*

            foreach (var dyn in _dynamiteGroup)
            {
                dyn.LoadContent(Content);
                

            }
            */
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            /*
          


            // TODO: Add your update logic here
            _miner.Update(gameTime);

            foreach (var dynamite in _dynamiteGroup)
            {
                if (!dynamite.Collected && dynamite.Bounds.CollidesWith(_miner.Bounds))
                {
                    dynamite.Collected = true;
                    _dynamiteCounter--;
                    _dynCollect.Play();

                }
            }

            if(_dynamiteCounter == 0)
            {
                _gameWon = true;
            }

            if(gameTime.TotalGameTime.TotalSeconds > _maxTime || _dynamiteCounter == 0)
            {
                _gameOver = true;
            };

            */

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);    // The real drawing happens inside the ScreenManager component
            

        }




        }




    }
