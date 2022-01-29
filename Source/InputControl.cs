using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameJaaj.Source {
    public class InputControl {
        private Player _player;
        private Enemy _enemy;

        public InputControl(Player player, Enemy enemy) { _player = player; _enemy = enemy; }

        public void Update(GameTime gameTime) { Controls(gameTime); }

        public void Controls(GameTime gameTime) {
            KeyboardState key = Keyboard.GetState();

            if (key.IsKeyDown(Keys.Down) || key.IsKeyDown(Keys.S)) {
                _player.MoveDown(gameTime); _enemy.MoveDown(gameTime);
                if (_enemy._position.Y < _player._position.Y)
                    _enemy._position.Y += _player._position.Y  * (float)gameTime.ElapsedGameTime.TotalSeconds / _enemy._speed;
            }

            else if (key.IsKeyDown(Keys.Up) || key.IsKeyDown(Keys.W)) {
                _player.MoveUp(gameTime); _enemy.MoveUp(gameTime);
                if (_enemy._position.Y > _player._position.Y)
                    _enemy._position.Y -= _player._position.Y * (float)gameTime.ElapsedGameTime.TotalSeconds / _enemy._speed;
            }

            else { _player.StopMove(gameTime); _enemy.StopMove(gameTime);
                if (_enemy._position.Y <= _player._position.Y)
                    _enemy._position.Y += _player._position.Y  * (float)gameTime.ElapsedGameTime.TotalSeconds / _enemy._speed;
                if (_enemy._position.Y >= _player._position.Y)
                    _enemy._position.Y -= _player._position.Y  * (float)gameTime.ElapsedGameTime.TotalSeconds / _enemy._speed;
                }



            if (key.IsKeyDown(Keys.Up) && key.IsKeyDown(Keys.Right) ||
                key.IsKeyDown(Keys.W) && key.IsKeyDown(Keys.D)) {
                    _player.State.SetState(_player.trickNum[0]); _enemy.State.SetState(_enemy.trickNum[0]);
                    _player.State._currentState.Render.Play(); _enemy.State._currentState.Render.Play();

                    if (_player.CanTrick == true) { _player.DoTrick(); _enemy.DoTrick();}
                    }

            else if (key.IsKeyDown(Keys.Up) && key.IsKeyDown(Keys.Left) ||
                key.IsKeyDown(Keys.W) && key.IsKeyDown(Keys.A)) {
                    _player.State.SetState(_player.trickNum[1]);
                    _player.State._currentState.Render.Play();

                    if (_player.CanTrick == true) { _player.DoTrick(); _enemy.DoTrick(); }
                    }
            
            else if (key.IsKeyDown(Keys.Up) && key.IsKeyDown(Keys.Down) ||
                key.IsKeyDown(Keys.W) && key.IsKeyDown(Keys.S)) {
                    _player.State.SetState(_player.trickNum[2]);
                    _player.State._currentState.Render.Play();
                    
                    if (_player.CanTrick == true) { _player.DoTrick(); _enemy.DoTrick(); }
                    }

            else if (key.IsKeyDown(Keys.Down) && key.IsKeyDown(Keys.Right) ||
                key.IsKeyDown(Keys.S) && key.IsKeyDown(Keys.D)) {
                    _player.State.SetState(_player.trickNum[3]);
                    _player.State._currentState.Render.Play();

                    if (_player.CanTrick == true) { _player.DoTrick(); _enemy.DoTrick(); }
                    }

            else if (key.IsKeyDown(Keys.Down) && key.IsKeyDown(Keys.Left) ||
                key.IsKeyDown(Keys.S) && key.IsKeyDown(Keys.A)) {
                    _player.State.SetState(_player.trickNum[4]);
                    _player.State._currentState.Render.Play();

                    if (_player.CanTrick == true) { _player.DoTrick(); _enemy.DoTrick(); }
                    }

            else if (key.IsKeyDown(Keys.Left) && key.IsKeyDown(Keys.Right) ||
                key.IsKeyDown(Keys.A) && key.IsKeyDown(Keys.D)) {
                    _player.State.SetState(_player.trickNum[5]);
                    _player.State._currentState.Render.Play();

                    if (_player.CanTrick == true) { _player.DoTrick(); _enemy.DoTrick(); }
                    }
        }
    }
}