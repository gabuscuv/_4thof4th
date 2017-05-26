using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace _4thof4th.BulletStructure
{
    class BulletGeneric{
        private Texture2D sprite;
        private Vector2 pos;
        private bool[] fire = new bool[3];
        private Color[] TextureData;
        float timeout;
        float timeoutmov;
        int speed=10;
        int angle;
        public BulletGeneric(Texture2D e, Vector2 f) {
            sprite = e;
            TextureData = new Color[sprite.Width * sprite.Height];
    }

        public void Update(Vector2 player,Vector2 pos_bulletEmisor,GameTime gameTime){
            if (fire[0]) {
                if (fire[1]){ pos.X += (MathHelper.Distance(pos.X, player.X) / speed); }
                else { pos.X -= (MathHelper.Distance(pos.X, player.X) / speed); }
                if (fire[2]) { pos.Y += (MathHelper.Distance(pos.Y, player.Y) / speed); }
                else { pos.Y -= (MathHelper.Distance(pos.Y, player.Y) / speed); }
            }
            else if (timeout>4f) {
                fire[0] = true;
                if (player.X > pos.X){ fire[1] = true; }
                if (player.Y > pos.Y) { fire[2] = true; }

            }
            else {
                timeout += (float)gameTime.ElapsedGameTime.TotalSeconds;
                timeoutmov += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timeoutmov>0.067f) {
                    pos.X = (pos_bulletEmisor.X +14) + (float)Math.Cos(angle) * 30;
                    pos.Y = (pos_bulletEmisor.Y +14) + (float)Math.Sin(angle) * 30;
                    timeoutmov = 0f;
                }
                if (angle == 360)
                {
                    angle = 0;
                }
                else { angle+=2; }
            }
            
        }
        public void Draw(SpriteBatch spriteBatch){
        spriteBatch.Draw(sprite, pos, sprite.Bounds, Color.AliceBlue, 0f, Vector2.Zero, 0.05f, SpriteEffects.None, 0f);

        }

        public Vector2 getpos() {
            return pos;
        }

        public Rectangle getBoxZone()
        {
            return sprite.Bounds;
        }
        public Color[] getTextureData()
        {
            return TextureData;
        }
    }
}
