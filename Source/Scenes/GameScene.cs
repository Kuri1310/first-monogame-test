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
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace GameJaaj.Source.Scenes {
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class GameScene : SceneManager {
        Song _song;
        Random Random = new Random();
        InputControl _playerControl;

        private readonly List<Objects> _objects = new List<Objects>();
        private float Spawn = 0;
        private bool CanCollide {get;set;} = false;

        public float _timerCountdown = 120;
        public float cooldownHit = 0;

        public GameScene(Game1 game, ContentManager content, GraphicsDevice graphics) : base(game, content, graphics) {
            _game = game;
            Content = content;
            _graphics = graphics;

            // TODO: use this.Content to load your game content here

            _song = Content.Load<Song>("Assets/Sounds/surf");
            MediaPlayer.Play(_song);
            MediaPlayer.IsRepeating = false;


            _game._playerSprite = Content.Load<Texture2D>("Assets/cortatorresmo");
            _game._enemySprite = Content.Load<Texture2D>("Assets/fritatorresmo");

            _game._playerHead = _game._playerSprite;
            _game._enemyHead = _game._enemySprite;

            
            _game._player = new Player(_game._playerSprite, new Vector2(80,30), 0, 0, LoadTrickSound());
            _game._enemy = new Enemy(_game._enemySprite, new Vector2(20,30), 0, 0);
            

            _game._player._score = new Score(){ _font = _game._defFont };
            _game._enemy._score = new Score(){ _font = _game._defFont };

            _playerControl = new InputControl(_game._player, _game._enemy);

            _game._objectSprite = _game._playerSprite;
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game.Exit();

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            // TODO: Add your update logic here
            _timerCountdown -= deltaTime;
            
            if (_timerCountdown != 0) {
            _game._player.Update(gameTime);
            _game._enemy.Update(gameTime);
            _playerControl.Update(gameTime);

            if (_game._player._position.Y > _game._graphics.PreferredBackBufferHeight - _game._player._spriteSheet.Height * 2)
                _game._player._position.Y = _game._graphics.PreferredBackBufferHeight - _game._player._spriteSheet.Height * 2;
            else if (_game._player._position.Y < _game._player._spriteSheet.Height / 4)
                _game._player._position.Y = _game._player._spriteSheet.Height / 4;

            if (_game._enemy._position.Y > _game._graphics.PreferredBackBufferHeight - _game._enemy._spriteSheet.Height * 2)
                _game._enemy._position.Y = _game._graphics.PreferredBackBufferHeight - _game._enemy._spriteSheet.Height * 2;
            else if (_game._enemy._position.Y < _game._enemy._spriteSheet.Height / 4)
                _game._enemy._position.Y = _game._enemy._spriteSheet.Height / 4;

            Spawn += deltaTime;

            foreach (Objects objects in _objects) {
                objects.Update(gameTime);
                
                if (objects._hitbox.Intersects(_game._player._hitbox)) {  }
                if (objects._hitbox.Intersects(_game._enemy._hitbox) && _game._enemy.CanCollide == true) { _game._enemy.LosePoints(); }
            }
            LoadObjects();
            }

            if (_timerCountdown <= 0) { 
                _timerCountdown = 0; 
                _game._player._position.X += _game._player._speed * deltaTime;
                _game._enemy._position.X += _game._player._speed * deltaTime;
                _game._player.CanTrick = false;
                _game._player._cooldown = 0;

                CanCollide = false;

                if (Spawn >= 0) {
                    Spawn = 0;
                    if (_objects.Count < 5)
                        _objects.Remove(new Objects(_game._objectSprite, new Vector2(0,0)));
                }
            }

            if (MediaPlayer.PlayPosition == TimeSpan.FromSeconds(125.622857100) || Keyboard.GetState().IsKeyDown(Keys.F2)) { _game.SetScene(new ScoreScene(_game, Content, _graphics)); }

        }

        public override void Draw(SpriteBatch _spriteBatch, GameTime gameTime)
        {
            var clamp = SamplerState.PointClamp;

            _graphics.Clear(Color.CornflowerBlue);
            


            // TODO: Add your drawing code here
            
            _spriteBatch.Begin(samplerState: clamp);
            _spriteBatch.Draw(_game._enemy._spriteSheet, _game._enemy._position + new Vector2(5,13), _game._enemy._hitbox, Color.Brown);
            _spriteBatch.Draw(_game._player._spriteSheet, _game._player._position + new Vector2(5,13), _game._player._hitbox, Color.BlueViolet);
            foreach (Objects objects in _objects) { _spriteBatch.Draw(objects._texture, objects._position + new Vector2(8,8), objects._hitbox, Color.HotPink); }
            _spriteBatch.End();

            _spriteBatch.Begin(samplerState: clamp);
            foreach (Objects objects in _objects) { objects.Draw(_spriteBatch, gameTime); }
            _game._enemy.Draw(_spriteBatch, gameTime);
            _game._player.Draw(_spriteBatch, gameTime);
            _spriteBatch.End();

            _spriteBatch.Begin(samplerState: clamp);

            _spriteBatch.Draw(_game._enemyHead, new Vector2((_game._graphics.PreferredBackBufferWidth / 2) / 5, -20) - new Vector2(-190,-30), new Rectangle(0,0,28,8), Color.White, 0, Vector2.Zero, 2f, SpriteEffects.FlipHorizontally,0);
            _game._enemy._score.Draw(_spriteBatch, gameTime, new Vector2((_game._graphics.PreferredBackBufferWidth / 3), -20), _game._fontColor);

            _spriteBatch.Draw(_game._playerHead, new Vector2((_game._graphics.PreferredBackBufferWidth / 2) / 5, -20) - new Vector2(50,-30), new Rectangle(0,0,32,10),Color.White,0,Vector2.Zero,2f,SpriteEffects.None,0);
            _game._player._score.Draw(_spriteBatch, gameTime, new Vector2((_game._graphics.PreferredBackBufferWidth / 2) / 5, -20), _game._fontColor);

            ////////// Shadow hehe
            _spriteBatch.DrawString(_game._defFont, string.Format("{0:0}", _timerCountdown), new Vector2(_game._graphics.PreferredBackBufferWidth / 4.3f, 10), _game._shadowColor, 0, Vector2.Zero,1.07f,SpriteEffects.None,0);
            _spriteBatch.DrawString(_game._defFont, string.Format("{0:0}", _timerCountdown), new Vector2(_game._graphics.PreferredBackBufferWidth / 4.3f, 10), _game._fontColor);

            if (_timerCountdown <= 30) { _spriteBatch.DrawString(_game._defFont, string.Format("{0:0}", _timerCountdown), new Vector2(_game._graphics.PreferredBackBufferWidth / 4.3f, 10), new Color(230, 72, 46)); }
            else { _spriteBatch.DrawString(_game._defFont, string.Format("{0:0}", _timerCountdown), new Vector2(_game._graphics.PreferredBackBufferWidth / 4.3f, 10), _game._fontColor); }

            _spriteBatch.End();
        }

        private SFX LoadTrickSound() {
            SFX soundEffects = new SFX();

            soundEffects.Add(Content.Load<SoundEffect>("Assets/Sounds/efeitoum"));
            soundEffects.Add(Content.Load<SoundEffect>("Assets/Sounds/efeitodois"));
            soundEffects.Add(Content.Load<SoundEffect>("Assets/Sounds/efeitotres"));
            soundEffects.Add(Content.Load<SoundEffect>("Assets/Sounds/efeitoquatro"));

            return soundEffects;
        }

        private void LoadObjects() {
            int randY = Random.Next(30, _game._graphics.PreferredBackBufferHeight);

            if (_timerCountdown <= 120) {
                if (Spawn >= 0.5f) {
                    Spawn = 0;
                    if (_objects.Count < 5) _objects.Add(new Objects(_game._objectSprite, new Vector2(_game._graphics.PreferredBackBufferWidth / 2, randY)));
                }
            }
            else if (_timerCountdown <= 60) {
                if (Spawn >= 0.2f) { 
                    Spawn = 0; 
                    if (_objects.Count < 3) _objects.Add(new Objects(_game._objectSprite, new Vector2(_game._graphics.PreferredBackBufferWidth / 2, randY)));
                }
            }

            for (int i = 0; i < _objects.Count; i++) {
                if (_objects[i].CanSee == false) {
                    _objects.RemoveAt(i);
                    --i;
                }
            }
        }
        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
