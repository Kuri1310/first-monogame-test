using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GameJaaj.Source.Data;


namespace GameJaaj.Source.Managers {
    public class RenderState {
        public string Name {get; private set;}
        public IRender Render {get;}

        public RenderState(string name, IRender render) {
            Name = name; Render = render;
        }

        public void Draw(SpriteBatch _spriteBatch, Vector2 position) {
            Render?.Draw(_spriteBatch, position);
        }
    }

    public class StateManager {
        private readonly Dictionary<string, RenderState> _states = new Dictionary<string, RenderState>();
        public RenderState _currentState {get; private set;} = null;

        public StateManager(){}

        public RenderState AddState(string name, IRender render){
            var state = new RenderState(name, render);
            _states.Add(name, state);
            return state;
        }
        
        public RenderState GetState(string name) => _states[name];

        public void SetState(string name) {
            _currentState = GetState(name);
        }

        public void RemoveState(string stateName) {
            var state = GetState(stateName);

            if (state == null) return;

            _states.Remove(stateName);
            if (_currentState == state) _currentState = null;
        }

        public void Update(GameTime gameTime) {
            _currentState?.Render?.Update(gameTime);
        }
        public void Draw(SpriteBatch _spriteBatch, Vector2 position) {
            _currentState?.Render?.Draw(_spriteBatch, position);
        }

    }
}