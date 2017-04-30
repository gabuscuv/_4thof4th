using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace _4thof4th
{

    abstract class character : DrawableGameComponent
    {
        protected float posbase;
        protected SpriteEffects effects;
        protected Texture2D sprite;
        protected Vector2 pos;
        protected character(String filename, Game e) : base(e) {
            sprite = Game.Content.Load<Texture2D>(filename);
            // Solucionar el tema de posiciones
            posbase = (GraphicsDevice.Viewport.Height / 2) + 100;
            pos = new Vector2(0, posbase);
        }

    }
}
