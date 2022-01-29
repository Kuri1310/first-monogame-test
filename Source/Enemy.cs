using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GameJaaj.Source.Data;
using GameJaaj.Source.Managers;

namespace GameJaaj.Source {
    public class Enemy : IEntity {
        public Texture2D _spriteSheet;
        public Vector2 _position;
        public float _speed = 1.5f;
        public int _x = 0, _y = 0;
        public int _w = 31, _h = 24;

        public Score _score;
        public bool CanTrick = false;
        public float _cooldown;
        public float cooldownHit;
        public bool CanCollide = false;

        public Rectangle _hitbox;
                    
        public int DrawOrder {get;set;} 
        public int UpdateOrder {get;set;}

        private readonly StateManager _stateManager = new StateManager();
        public StateManager State => _stateManager;

        public Enemy(Texture2D spriteSheet, Vector2 position, int x, int y) {
            _spriteSheet = spriteSheet; _position = position; _x = x; _y = y;
            
            EnemyStates();
            _stateManager.SetState("Idle");
            _stateManager._currentState?.Render?.Play();

            _hitbox = new Rectangle(_x + 5, _y + 13, 21, 10);
        }

        public void Update(GameTime gameTime) {
            _stateManager.Update(gameTime);

            _cooldown += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_cooldown > 1.5f) { CanTrick = true; _cooldown = 1.5f; }

            cooldownHit += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (cooldownHit >= 1.5f) { cooldownHit = 1.5f; CanCollide = true; }

        }

        public void Draw(SpriteBatch _spriteBatch, GameTime gameTime) {
            _stateManager.Draw(_spriteBatch, _position);
        }

/////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public string[] trickNum = new string[] { "UpRight", "UpLeft", "UpDown", "DownLeft", "DownRight", "LeftRight"};

        private void EnemyStates() {
            _stateManager.AddState("Idle", new AnimatedSprite(_spriteSheet, 2, _x, _y, _w, _h){_fps = 4});
            _stateManager.AddState("SurfUp", new AnimatedSprite(_spriteSheet, 1, _x + (_w * 5), _y, _w, _h));
            _stateManager.AddState("SurfDown", new AnimatedSprite(_spriteSheet, 1, _x + (_w * 2), _y, _w, _h));
            _stateManager.AddState("Win", new AnimatedSprite(_spriteSheet, 2, _x, _y, _w, _h));
            _stateManager.AddState("Lose", new AnimatedSprite(_spriteSheet, 2, _x, _y, _w, _h));
            _stateManager.AddState("Waiting", new AnimatedSprite(_spriteSheet, 2, _x, _y, _w, _h));

            _stateManager.AddState(trickNum[0], new AnimatedSprite(_spriteSheet, 1, _x, _y + (_h * 3), _w, _h));
            _stateManager.AddState(trickNum[1], new AnimatedSprite(_spriteSheet, 1, _x, _y + (_h * 3), _w, _h));
            _stateManager.AddState(trickNum[2], new AnimatedSprite(_spriteSheet, 1, _x, _y + (_h * 3), _w, _h){_fps=1});
            _stateManager.AddState(trickNum[3], new AnimatedSprite(_spriteSheet, 1, _x, _y + (_h * 2), _w, _h));
            _stateManager.AddState(trickNum[4], new AnimatedSprite(_spriteSheet, 1, _x, _y + (_h * 2), _w, _h));
            _stateManager.AddState(trickNum[5], new AnimatedSprite(_spriteSheet, 1, _x, _y + (_h * 2), _w, _h));
        }

        public void MoveUp(GameTime gameTime) {
            _stateManager.SetState("SurfUp");
            _stateManager._currentState.Render.Play();
        }
        public void MoveDown(GameTime gameTime) {
            _stateManager.SetState("SurfDown");
            _stateManager._currentState.Render.Play();
        }

        public void StopMove(GameTime gameTime) {
            _stateManager.SetState("Idle");
            _stateManager._currentState.Render.Play();
        }

         public void DoTrick() {
            _score._initScore += _score._points;

            CanTrick = false;
            _cooldown = 0;
        }
        public void LosePoints() {
            _score._initScore -= _score._losePoints;
            CanCollide = false;
            cooldownHit = 0;

            CanTrick = false;
            _cooldown = 0;
        }

    }
}