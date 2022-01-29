using Microsoft.Xna.Framework;

using GameJaaj.Source.Managers;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameJaaj.Source.Scenes {
    public class ScoreScene : SceneManager
    {
        private string[] _scoreResult = { "YOU WON!", "TIE!", "YOU LOST!" };
        private float tensionTimer = 0;
        public ScoreScene(Game1 game, ContentManager content, GraphicsDevice graphics) : base(game, content, graphics) {
            _game = game;
            Content = content;
            _graphics = graphics;
        }

        public override void Update(GameTime gameTime) {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            tensionTimer += deltaTime;
            if (tensionTimer >= 2f) tensionTimer = 2f;

            _game._player.State.SetState("Waiting");
            _game._enemy.State.SetState("Waiting");

            if (_game._player._score._initScore > _game._enemy._score._initScore) {
                _game._player.State.SetState("Win");
                _game._enemy.State.SetState("Lose");
            }
            else if (_game._player._score._initScore == _game._enemy._score._initScore) {
                _game._player.State.SetState("Lose");
                _game._enemy.State.SetState("Lose");
            }
            else if (_game._player._score._initScore < _game._enemy._score._initScore) {
                _game._player.State.SetState("Lose");
                _game._enemy.State.SetState("Win");
            }
        }

        public override void Draw(SpriteBatch _spriteBatch, GameTime gameTime) {
            var clamp = SamplerState.PointClamp;
            _graphics.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: clamp);
                _spriteBatch.DrawString(_game._defFont, _game._enemy._score._initScore.ToString(), new Vector2(80,120), _game._fontColor);
                _spriteBatch.DrawString(_game._defFont, _game._player._score._initScore.ToString(), new Vector2(60,80), _game._fontColor);
            
            if (tensionTimer == 2f) {
            if (_game._player._score._initScore > _game._enemy._score._initScore) {
                _spriteBatch.DrawString(_game._defFont, _scoreResult[0],  new Vector2(_game._graphics.PreferredBackBufferWidth / 4.8f, 30), _game._shadowColor, 0, Vector2.Zero, 2.07f, SpriteEffects.None, 0);
                _spriteBatch.DrawString(_game._defFont, _scoreResult[0], new Vector2(_game._graphics.PreferredBackBufferWidth / 4.8f, 30), _game._fontColor, 0, Vector2.Zero, 2f, SpriteEffects.None, 0);
                //_game._player.State.SetState("Win");
                //_game._enemy.State.SetState("Lose");
            }
            else if (_game._player._score._initScore == _game._enemy._score._initScore) {
                _spriteBatch.DrawString(_game._defFont, _scoreResult[1],  new Vector2(_game._graphics.PreferredBackBufferWidth / 4.8f, 30), _game._shadowColor, 0, Vector2.Zero, 2.07f, SpriteEffects.None, 0);
                _spriteBatch.DrawString(_game._defFont, _scoreResult[1], new Vector2(_game._graphics.PreferredBackBufferWidth / 4.8f, 30), _game._fontColor, 0, Vector2.Zero, 2f, SpriteEffects.None, 0);
                //_game._player.State.SetState("Lose");
                //_game._enemy.State.SetState("Lose");
            }
            else if (_game._player._score._initScore < _game._enemy._score._initScore) {
                _spriteBatch.DrawString(_game._defFont, _scoreResult[2],  new Vector2(_game._graphics.PreferredBackBufferWidth / 4.8f, 30), _game._shadowColor, 0, Vector2.Zero, 2.07f, SpriteEffects.None, 0);
                _spriteBatch.DrawString(_game._defFont, _scoreResult[2], new Vector2(_game._graphics.PreferredBackBufferWidth / 4.8f, 30), _game._fontColor, 0, Vector2.Zero, 2f, SpriteEffects.None, 0);
                //_game._player.State.SetState("Lose");
                //_game._enemy.State.SetState("Win");
            }
        }

            _spriteBatch.End();
        }
    }
}