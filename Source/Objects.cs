using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GameJaaj.Source.Data;

namespace GameJaaj.Source {
    public class Objects : IEntity {
        
        public Texture2D _texture;
        public Vector2 _position;
        public float _speed = 150f;
        private int Multiplier, X = 0 , Y = 0;
        private const int W = 31, H = 12;

        public Rectangle _sourceRect;        
        public Rectangle _hitbox; //{ get { return new Rectangle((int)_position.X, 8, 20, 12); } }

        public bool CanSee = true;

        Random Random = new Random();
        
        public int DrawOrder {get;set;}
        public int UpdateOrder {get;set;}

        public Objects(Texture2D texture, Vector2 position) {
            _texture = texture; _position = position;

            Multiplier = Random.Next(1,7);

            _sourceRect = new Rectangle(X + (W * Multiplier), Y, W, H);
            _hitbox = new Rectangle(0, 7, 20, 12); 
        }

        public void Update(GameTime gameTime) {
            _position.X -= _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_position.X <= 0 - _texture.Width) CanSee = false;

            //_hitbox.Offset(_position);
        }

        public void Draw(SpriteBatch _spriteBatch, GameTime gameTime) {
            _spriteBatch.Draw(_texture, _position, _sourceRect, Color.White); 
        }

    }
}