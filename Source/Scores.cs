using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameJaaj.Source {
    public class Score {
       public int _initScore {get;set;} = 0;
       public int _points {get;set;} = 20;
       public int _losePoints {get;set;} = 40;

       public SpriteFont _font;

       public Score(){}

       public void Draw(SpriteBatch _spriteBatch, GameTime gameTime, Vector2 position, Color color) {
           //_spriteBatch.Draw(_head, position - new Vector2(30,-30), new Rectangle(0,0,32,10), Color.White, 0, Vector2.Zero, 2f, SpriteEffects.None, 0); 
           _spriteBatch.DrawString(_font,"SCORE:\n" + _initScore.ToString(), position + new Vector2(0,30), color);
       }

    }
}