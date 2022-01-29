using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJaaj.Source.Data {
    public enum AnimationState { Playing, Stopped, Paused }

    public class AnimatedSprite : IRender {
        public int _spriteCount;
        public int _spriteX {get;set;}
        public int _spriteY {get;set;}
        public int _spriteW {get;set;}
        public int _spriteH {get;set;}
        public Texture2D _texture {get;private set;}

        public double _fps {get;set;} = 60.0;
        public float PlayTime {get; private set;} = 0;
        public double _totalDuration => _spriteCount * (1 / _fps);
        public int _currOffset => (int)(PlayTime / (1 / _fps));

        public AnimationState _animState {get;set;} = AnimationState.Stopped;

        public AnimatedSprite(Texture2D texture, int totalSprites, int x, int y, int spriteWidth, int spriteHeight) {
            _texture = texture; _spriteCount = totalSprites; _spriteX = x; _spriteY = y; _spriteW = spriteWidth; _spriteH = spriteHeight;
        }
        
        public void Play() { _animState = AnimationState.Playing; }
        public void Stop() { _animState = AnimationState.Stopped; PlayTime = 0; }

        public void Update(GameTime gameTime) {
            if (_animState == AnimationState.Playing) {
                PlayTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (PlayTime > _totalDuration)
                    PlayTime = 0;
            }
        }
        public void Draw(SpriteBatch _spriteBatch, Vector2 position) {
            var sourceRect = new Rectangle((_currOffset * _spriteW) + _spriteX, _spriteY, _spriteW, _spriteH);
            _spriteBatch.Draw(_texture, position, sourceRect, Color.White);
        }
    }
}