using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GameJaaj.Source.Data;
using GameJaaj.Source.Managers;

namespace GameJaaj.Source {
    public class Player : IEntity {

        public Texture2D _spriteSheet;
        public Vector2 _position;
        public float _speed = 100f;
        private Vector2 _velocity;
        public int _x = 0, _y = 0;
        public int _w = 31, _h = 24;

        public Rectangle _hitbox;

        private SFX _trickSound;

        public Score _score;
        public bool CanTrick = false;
        public float _cooldown;

        public int DrawOrder {get; set;}
        public int UpdateOrder {get; set;}

        private readonly StateManager _stateManager = new StateManager();
        public StateManager State => _stateManager;
        
        public Player(Texture2D spriteSheet, Vector2 position, int x, int y, SFX trickSound) {
            _spriteSheet = spriteSheet; _position = position; _x = x; _y = y;

            PlayerStates();
            _stateManager.SetState("Idle");
            _stateManager._currentState?.Render?.Play();

            _trickSound = trickSound;

            _hitbox = new Rectangle(_x + 5, _y + 13, 21, 10);
        }

        public void Update(GameTime gameTime) {
            _stateManager.Update(gameTime);

            _cooldown += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_cooldown > 1.5f) { CanTrick = true; _cooldown = 1.5f; }
            
        }
        
        public void Draw(SpriteBatch _spriteBatch, GameTime gameTime) { 
            _stateManager.Draw(_spriteBatch, _position);
        }

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public string[] trickNum = new string[] { "UpRight", "UpLeft", "UpDown", "DownLeft", "DownRight", "LeftRight"};

        private void PlayerStates() {
            _stateManager.AddState("Idle", new AnimatedSprite(_spriteSheet, 2, _x, _y, _w, _h){_fps = 4});
            _stateManager.AddState("SurfUp", new AnimatedSprite(_spriteSheet, 1, _x + (_w * 5), _y, _w, _h));
            _stateManager.AddState("SurfDown", new AnimatedSprite(_spriteSheet, 1, _x + (_w * 2), _y, _w, _h));
            _stateManager.AddState("Win", new AnimatedSprite(_spriteSheet, 2, _x, _y, _w, _h));
            _stateManager.AddState("Lose", new AnimatedSprite(_spriteSheet, 2, _x, _y, _w, _h));
            _stateManager.AddState("Waiting", new AnimatedSprite(_spriteSheet, 2, _x, _y, _w, _h));

            _stateManager.AddState(trickNum[0], new AnimatedSprite(_spriteSheet, 1, _x, _y + (_h * 3), _w, _h));
            _stateManager.AddState(trickNum[1], new AnimatedSprite(_spriteSheet, 1, _x, _y + (_h * 3), _w, _h));
            _stateManager.AddState(trickNum[2], new AnimatedSprite(_spriteSheet, 1, _x, _y + (_h * 3), _w, _h));
            _stateManager.AddState(trickNum[3], new AnimatedSprite(_spriteSheet, 1, _x, _y + (_h * 2), _w, _h));
            _stateManager.AddState(trickNum[4], new AnimatedSprite(_spriteSheet, 1, _x, _y + (_h * 2), _w, _h));
            _stateManager.AddState(trickNum[5], new AnimatedSprite(_spriteSheet, 1, _x, _y + (_h * 2), _w, _h));
        }

        public void MoveUp(GameTime gameTime) {
            _stateManager.SetState("SurfUp");
            _stateManager._currentState.Render.Play();

            _position.Y -= _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public void MoveDown(GameTime gameTime) {
            _stateManager.SetState("SurfDown");
            _stateManager._currentState.Render.Play();

            _position.Y += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    
        public void StopMove(GameTime gameTime) {
            _velocity = Vector2.Zero;
            _stateManager.SetState("Idle");
            _stateManager._currentState.Render.Play();
        }

        public void DoTrick() {
            _score._initScore += _score._points;
            CanTrick = false;
            _cooldown = 0;

            _trickSound.RandomPlay();
        }
        public void LosePoints() {
            _score._initScore -= _score._losePoints;
            CanTrick = false;
            _cooldown = 0;
        }
    }
}