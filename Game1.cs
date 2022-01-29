using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

using GameJaaj.Source;
using GameJaaj.Source.Data;
using GameJaaj.Source.Managers;
using GameJaaj.Source.Scenes;

namespace GameJaaj
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private RenderTarget2D _renderTarget;

        //public Random Random = new Random();
        public Color _fontColor = new Color(223,246,245), _shadowColor = new Color(48,44,46);

        private SceneManager _currentScene;
        private SceneManager _nextScene;
        public void SetScene(SceneManager scene) { _nextScene = scene; }
        
        public SpriteFont _defFont;

        public Texture2D _playerSprite, _enemySprite, _judgeSprite, _objectSprite;
        public Texture2D _playerHead, _enemyHead;

        public Player _player;
        public Enemy _enemy;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 640;
            _graphics.PreferredBackBufferHeight = 480;
            Window.AllowAltF4 = true;
            Window.AllowUserResizing = true;
            _graphics.ApplyChanges();

            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var screenWidth = _graphics.PreferredBackBufferWidth / 2;
            var screenHeight = _graphics.PreferredBackBufferHeight / 2;

            _renderTarget = new RenderTarget2D(GraphicsDevice, screenWidth, screenHeight);

            _currentScene = new MenuScene(this, Content, _graphics.GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _defFont = Content.Load<SpriteFont>("Assets/Font");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (_nextScene != null) {
                _currentScene = _nextScene;
                _nextScene = null;
            }
            
            _currentScene.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            var clamp = SamplerState.PointClamp;

            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _currentScene.Draw(_spriteBatch, gameTime);
    
            GraphicsDevice.SetRenderTarget(null);

            var viewport = GraphicsDevice.Viewport;
            var screenArea = SetScreenArea(_renderTarget.Bounds.Size, viewport.Bounds.Size);

            _spriteBatch.Begin(samplerState: clamp);
            _spriteBatch.Draw(_renderTarget, screenArea, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }


        private static Rectangle SetScreenArea(Point renderTargetSize,Point viewportSize) {
            var scale = Math.Min(
            viewportSize.X / (float)renderTargetSize.X,
            viewportSize.Y / (float)renderTargetSize.Y
            );

            var size = renderTargetSize.ToVector2() * scale;
            var location = viewportSize.ToVector2() * 0.5f - size * 0.5f;
            
            return new Rectangle(location.ToPoint(), size.ToPoint());
        }
    }
}
