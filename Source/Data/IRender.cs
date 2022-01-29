using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJaaj.Source.Data {
    public interface IRender {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch _spriteBatch, Vector2 position);

        void Play();
        void Stop();
        //void Pause();
    }
}