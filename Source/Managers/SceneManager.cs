using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameJaaj.Source.Managers {
    public abstract class SceneManager {
        protected Game1 _game;
        protected ContentManager Content;
        protected GraphicsDevice _graphics;

        public SceneManager(Game1 game, ContentManager content, GraphicsDevice graphics)
        {
            _game = game;
            Content = content;
            _graphics = graphics;

        }

        public abstract void Update(GameTime gameTime);
        //public abstract void PostUpdate(GameTime gameTime);
        public abstract void Draw(SpriteBatch _spriteBatch, GameTime gameTime);
    }
}