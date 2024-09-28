using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameProject0.StateManagement;
using GameProject0;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Reflection.Metadata;

namespace GameArchitectureExample.Screens
{
    // This screen implements the actual game logic. It is just a
    // placeholder to get the idea across: you'll probably want to
    // put some more interesting gameplay in here!
    public class GameplayScreen : GameScreen
    {
        private ContentManager _content;
        private SpriteFont _gameFont;

        private Vector2 _playerPosition = new Vector2(100, 100);
        private Vector2 _enemyPosition = new Vector2(100, 100);

        private readonly Random _random = new Random();

        private float _pauseAlpha;
        private readonly InputAction _pauseAction;

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



        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            _pauseAction = new InputAction(
                new[] { Buttons.Start, Buttons.Back },
                new[] { Keys.Back }, true);
        }

        // Load graphics content for the game
        public override void Activate()
        {
            System.Random rand = new System.Random();


            _dynamiteGroup = new DynamiteSprite[]
            {

               
                new DynamiteSprite(new Vector2((float)rand.Next(ScreenManager.Game.GraphicsDevice.Viewport.Width-60), (float)rand.Next( ScreenManager.Game.GraphicsDevice.Viewport.Height-60))),
                new DynamiteSprite(new Vector2((float)rand.Next(ScreenManager.Game.GraphicsDevice.Viewport.Width-60), (float)rand.Next( ScreenManager.Game.GraphicsDevice.Viewport.Height-60))),
                new DynamiteSprite(new Vector2((float)rand.Next(ScreenManager.Game.GraphicsDevice.Viewport.Width-60), (float)rand.Next( ScreenManager.Game.GraphicsDevice.Viewport.Height-60)))
               

            };

            _dynamiteCounter = _dynamiteGroup.Length;
            _maxTime = 30;

            _miner = new MinerSprite();
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _gameFont = _content.Load<SpriteFont>("FONT");
            _spriteBatch = new SpriteBatch(ScreenManager.Game.GraphicsDevice);
            spriteFont = _content.Load<SpriteFont>("FONT");
            _miner.LoadContent(_content);
            _backGround = _content.Load<Texture2D>("BackGround");
            _dynCollect = _content.Load<SoundEffect>("Pickup_Coin2");
            _fire_level = _content.Load<Song>("Danish Mega Pony v. 4 OST - Track 02 (Fire Level)");
            MediaPlayer.Play(_fire_level);
            foreach (var dyn in _dynamiteGroup)
            {
                dyn.LoadContent(_content);


            }



           

           
            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }


        public override void Deactivate()
        {
            base.Deactivate();
        }

        public override void Unload()
        {
            _content.Unload();
        }

        // This method checks the GameScreen.IsActive property, so the game will
        // stop updating when the pause menu is active, or if you tab away to a different application.
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                _pauseAlpha = Math.Min(_pauseAlpha + 1f / 32, 1);
            else
                _pauseAlpha = Math.Max(_pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {


                /*
                // Apply some random jitter to make the enemy move around.
                const float randomization = 10;

                _enemyPosition.X += (float)(_random.NextDouble() - 0.5) * randomization;
                _enemyPosition.Y += (float)(_random.NextDouble() - 0.5) * randomization;

                // Apply a stabilizing force to stop the enemy moving off the screen.
                var targetPosition = new Vector2(
                    ScreenManager.GraphicsDevice.Viewport.Width / 2 - _gameFont.MeasureString("Insert Gameplay Here").X / 2,
                    200);

                _enemyPosition = Vector2.Lerp(_enemyPosition, targetPosition, 0.05f);

                // This game isn't very fun! You could probably improve
                // it by inserting something more interesting in this space :-)*/




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

                if (_dynamiteCounter == 0)
                {
                    _gameWon = true;
                }

                if (gameTime.TotalGameTime.TotalSeconds > _maxTime || _dynamiteCounter == 0)
                {
                    _gameOver = true;
                };


            }
        }

        // Unlike the Update method, this will only be called when the gameplay screen is active.
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            var keyboardState = input.CurrentKeyboardStates[playerIndex];
            var gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected && input.GamePadWasConnected[playerIndex];

            PlayerIndex player;




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

            if (_dynamiteCounter == 0)
            {
                _gameWon = true;
            }

            if (gameTime.TotalGameTime.TotalSeconds > _maxTime || _dynamiteCounter == 0)
            {
                _gameOver = true;
            };



            // Otherwise move the player position.
            var movement = Vector2.Zero;

            if (keyboardState.IsKeyDown(Keys.Left))
                movement.X--;

            if (keyboardState.IsKeyDown(Keys.Right))
                movement.X++;

            if (keyboardState.IsKeyDown(Keys.Up))
                movement.Y--;

            if (keyboardState.IsKeyDown(Keys.Down))
                movement.Y++;

            var thumbstick = gamePadState.ThumbSticks.Left;

            movement.X += thumbstick.X;
            movement.Y -= thumbstick.Y;

            if (movement.Length() > 1)
                movement.Normalize();

            _playerPosition += movement * 8f;

        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.CornflowerBlue, 0, 0);

            if (_gameOver)
            {
                ScreenManager.Game.GraphicsDevice.Clear(Color.White);
               
                _spriteBatch.Draw(_backGround, new Rectangle(0, 0, 800, 480), Color.White);
                _spriteBatch.DrawString(spriteFont, _gameWon ? $"You Won!" : $"You Lost!", new Vector2(200, 200), _gameWon ? Color.NavajoWhite : Color.WhiteSmoke, 0f, new Vector2(0, 0), 1.5f, SpriteEffects.None, 1);
                base.Draw(gameTime);
              
            }
            else
            {
                ScreenManager.Game.GraphicsDevice.Clear(Color.White);
                
                _spriteBatch.Draw(_backGround, new Rectangle(0, 0, 800, 480), Color.White);


                // TODO: Add your drawing code here


                //_spriteBatch.DrawString(spriteFont , $"Welcome to the game! Press Escape to exit", new Vector2(200, 200), Color.Black);
                //_spriteBatch.DrawString(spriteFont, $"DOWNWARDS", new Vector2(200,250), Color.Black,0,new Vector2(0,0), 3f, SpriteEffects.None, 1);


                _spriteBatch.DrawString(spriteFont, $"Collect all the dynamite before it explodes!", new Vector2(200, 50), Color.NavajoWhite, 0f, new Vector2(0, 0), 1.5f, SpriteEffects.None, 1);
                _spriteBatch.DrawString(spriteFont, $"Dynamite Left: {_dynamiteCounter}", new Vector2(50, 50), Color.White);
                _spriteBatch.DrawString(spriteFont, $"Time Left: {(int)(_maxTime - gameTime.TotalGameTime.TotalSeconds)}", new Vector2(50, 100), Color.White);


                _miner.Draw(gameTime, _spriteBatch);

                foreach (var dyn in _dynamiteGroup)
                {
                    if (!dyn.Collected)
                    {
                        dyn.Draw(gameTime, _spriteBatch);

                    }
                }



                
                base.Draw(gameTime);
            }
            _spriteBatch.End();
        }
    }
}