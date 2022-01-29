using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJaaj.Source.Data {
    public interface IEntity {
        int DrawOrder {get;}
        int UpdateOrder {get;}

        void Update(GameTime gameTime);
        void Draw(SpriteBatch _spriteBatch, GameTime gameTime);
    }
}