using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace _4thof4th
{
    class Player : character{
        private float playerMoveSpeed=5;
        Boolean x;
        public Player(String filename,Game g) : base(filename,g){}
        
        public void Update(bool debug){
            userinput();

            // Esto es solo para debugging 
           // if (Keyboard.GetState().IsKeyDown(Keys.Down)&&debug) pos.Y += playerMoveSpeed;
            
            // Esto hace que cuando se vaya del campo de vision cruce hacia el otro
            // punto de la pantalla.
            // Estado: Deberia hacer que el valor de "1230" sea dinamico a la resolucion
            if (pos.X > (GraphicsDevice.Viewport.Width-50) ) pos.X = -200;else if(pos.X < -200)pos.X = (GraphicsDevice.Viewport.Width - 50);


            // 
            if (Keyboard.GetState().IsKeyUp(Keys.Z) && pos.Y!=posbase) x = true;

            if (x) {pos.Y += 10;if (pos.Y == posbase) x = false;}

            }

        public void userinput() {
            // Direccionamiento del personaje
            // Estado: No hace falta cambios
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0 || Keyboard.GetState().IsKeyDown(Keys.Right)){
                pos.X += playerMoveSpeed;
                effects = SpriteEffects.None;
            }
            else if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < 0 || Keyboard.GetState().IsKeyDown(Keys.Left)){
                pos.X -= playerMoveSpeed;
                effects = SpriteEffects.FlipHorizontally;
            }

            // Implementar Salto
            // Estado: Deberia hacer que una vez dado el salto se requiera darle al boton de nuevo
            if (!x && (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed 
                || Keyboard.GetState().IsKeyDown(Keys.Z)))
                if (!x && pos.Y > 290){
                    pos.Y -= 10;
                    if (pos.Y <= 290) x = true;
                }
        }
        /// <summary>
        /// El Metodo de dibujado de pantalla del mismo Personaje
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch){
            // Solucionar tema de Scaling respecto la resolucion
            spriteBatch.Draw(sprite, pos, null, Color.White, 0f, Vector2.Zero, 1f, effects, 0f);
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

        /// <summary>
        /// Metodo que devuelve la posicion X del personaje en la pantalla
        /// </summary>
        /// <returns></returns>
        public double getX() {
            return pos.X;
        }
        /// <summary>
        /// Metodo que devuelve la posicion Y del personaje en la pantalla
        /// </summary>
        /// <returns></returns>
        public double getY()
        {
            return pos.Y;
        }
    }
}
