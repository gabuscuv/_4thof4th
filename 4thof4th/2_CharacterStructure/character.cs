using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace _4thof4th.CharacterStructure{
    abstract class character{
        protected Texture2D sprite;
        protected Color[] TextureData;
        protected float scaling;
        protected float posbase;
        protected SpriteEffects effects;
        protected Vector2 pos;
        protected character(String filename, ContentManager e,Rectangle s) {
            sprite = e.Load<Texture2D>(filename);
            posbase = s.Bottom-255;
            pos = new Vector2(0, posbase);
            TextureData = new Color[sprite.Width * sprite.Height];
            sprite.GetData(TextureData);
             effects = SpriteEffects.None;
            scaling = 1;
        }
        protected character(String filename, Vector2 s, ContentManager content, Rectangle g ){
            sprite = content.Load<Texture2D>(filename);
            posbase = g.Bottom-255;
            pos = s;
            TextureData=new Color[sprite.Width * sprite.Height];
            sprite.GetData(TextureData);
            effects = SpriteEffects.None;
            scaling = 1;
        }
        protected character(Vector2 s, Rectangle g) {
            posbase = g.Bottom - 255;
            pos = s;
            effects = SpriteEffects.None;
            scaling = 1;
        }
        /// <summary>
        /// Metodo que devuelve la posicion X del personaje en la pantalla
        /// </summary>
        /// <returns></returns>
        public Vector2 getPos() {
            return pos;
        }
        /// <summary>
        /// Devuelve anchura del sprite del sprite
        /// </summary>
        /// <returns></returns>
        public int width()
        {
            return sprite.Width;
        }
        public int height()
        {
            return sprite.Height;
        }
        public Texture2D getBoxZone() {
            return sprite;
        }
        public Color[] getTextureData() {
            return TextureData;
        }
        public abstract void Update(GameTime gameTime,bool debug);
        /// <summary>
        /// El Metodo de dibujado de pantalla del mismo Personaje
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch){
            // Solucionar tema de Scaling respecto la resolucion
            spriteBatch.Draw(sprite, pos, sprite.Bounds, Color.White, 0f, Vector2.Zero, scaling, effects, 0f);
        }
    }
}
