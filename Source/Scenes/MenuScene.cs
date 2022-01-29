using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using GameJaaj.Source.Managers;

namespace GameJaaj.Source.Scenes {
    public class MenuScene : SceneManager {
        public MenuScene(Game1 game, ContentManager content, GraphicsDevice graphics) : base(game, content, graphics) { 
            _game = game;
            Content = content;
            _graphics = graphics;

            _game._playerSprite = Content.Load<Texture2D>("Assets/cortatorresmo");
        }

        public override void Update(GameTime gameTime) {
            var key = Keyboard.GetState();
            if (key.IsKeyDown(Keys.Space)) { _game.SetScene(new GameScene(_game, Content, _graphics)); }
        }
        
        public override void Draw(SpriteBatch _spriteBatch, GameTime gameTime) {
            _graphics.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_game._playerSprite, new Vector2(40,40), Color.White);

            _spriteBatch.DrawString(_game._defFont, "Press SPACE to Start", new Vector2(_game._graphics.PreferredBackBufferWidth / 6.6f, _game._graphics.PreferredBackBufferHeight / 4), _game._fontColor);
            _spriteBatch.End();
        }
    }
}