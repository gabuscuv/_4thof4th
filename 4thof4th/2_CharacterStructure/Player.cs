using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace _4thof4th.CharacterStructure
{
    class Player : character{
        private float playerMoveSpeed=5;
        private int screen_width;
        private int screen_height;
        private bool x;
        private bool locks;
        public Player(String spritefilename,ContentManager s,Rectangle g) : base(spritefilename,s,g){
            screen_width = g.Width;
            screen_height = g.Height-200;
        }
        public Player(String spritefilename, Vector2 pos_custom, ContentManager s, Rectangle g) : base(spritefilename, pos_custom, s, g)
        {
            screen_height = g.Height-200;
            screen_width = g.Width;
        }
        public override void Update(GameTime gameTime,bool debug){
            userinput();

            // Esto es solo para debugging 
           // if (Keyboard.GetState().IsKeyDown(Keys.Down)&&debug) pos.Y += playerMoveSpeed;
            
            // Esto hace que cuando se vaya del campo de vision cruce hacia el otro
            // punto de la pantalla.
            // Estado: OK, comprobado funcionamiento en 1080p y 720p, FUNCIONA PERFECTAMENTE EN TODOS LOS ASPECT RATIOS
            if (pos.X > (screen_width-50)) pos.X = -200;else if(pos.X < -200)pos.X = (screen_width - 50);

            // Esto es para cuando no se realiza un salto de maxima altitud, activa la gravedad
            if (Keyboard.GetState().IsKeyUp(Keys.Z) && pos.Y!=posbase) x = true;

            // Sistema de Gravedad
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
            if (!locks) { 
            if (!x && (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed 
                || Keyboard.GetState().IsKeyDown(Keys.Z)))
                if (!x && pos.Y > screen_height/2){
                    pos.Y -= 10;
                    // En caso que llege a la maxima altitud se activa el sistema de gravedad
                    if (pos.Y <= screen_height/2 ) x = true;

                    }
            }
            if(GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Released || Keyboard.GetState().IsKeyUp(Keys.Z)){
                locks = false;
            }
        }

    }
}
