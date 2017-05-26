using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace _4thof4th.CharacterStructure
{
    class Enemy : character
    {
        public Enemy(String filename, ContentManager e, Rectangle s) : base(filename,e,s){}

        public override void Update(GameTime gameTime,bool debug){

        }
    }
}
