using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace _4thof4th.BulletStructure
{
    class BulletEmisor : CharacterStructure.character{
        private float intervale;
        private Texture2D sprite_bullet;
        public List<BulletGeneric> bulletsonDisplay;

        public BulletEmisor(Vector2 s, ContentManager content, Rectangle g) : base(s,g) {
            bulletsonDisplay = new List<BulletGeneric>();
            sprite = content.Load<Texture2D>("Sprites/misc/dcircule");
            sprite_bullet = content.Load<Texture2D>("Sprites/misc/scircle");
            TextureData = new Color[sprite.Width * sprite.Height];
        }

        public override void Update(GameTime gameTime,bool debug){
            intervale += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if ((intervale > 1.5f) && bulletsonDisplay.Count != 4)
            {
                    bulletsonDisplay.Add(new BulletGeneric(sprite_bullet,pos));
                intervale = 0f;
            }
        }
        public Vector2 getpos() {
            return pos;
        }
        public new void Draw(SpriteBatch spriteBatch)
        {
            // Solucionar tema de Scaling respecto la resolucion
            spriteBatch.Draw(sprite, pos, sprite.Bounds, Color.AliceBlue, 0f, Vector2.Zero, 0.15f, effects, 0f);
        }

    }
}
