using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace _4thof4th.CharacterStructure
{
    class Player : character{
        public int life = 4;
        public List<BulletStructure.PlayerBullet> bullets;
        private Texture2D bullet;
        private float playerMoveSpeed=5;
        private int screen_width;
        private int screen_height;
        private bool x;
        private bool locks;
        public Player(String spritefilename,Texture2D f,ContentManager s,Rectangle g) : base(spritefilename,s,g){
            screen_width = g.Width;
            screen_height = g.Height-200;
            bullet = f;
            bullets = new List<BulletStructure.PlayerBullet>();
        }
        public Player(String spritefilename, Texture2D f,Vector2 pos_custom, ContentManager s, Rectangle g) : base(spritefilename, pos_custom, s, g){
            screen_height = g.Height-200;
            bullet = f;
            screen_width = g.Width;
            x = true;
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
            if (Keyboard.GetState().IsKeyUp(Keys.Z) && pos.Y<posbase) x = true;

            for (int i = 0; i < bullets.Count; i++){
                bullets[i].Update();
            }
            // Sistema de Gravedad
            if (x) {pos.Y += 10;if (pos.Y > posbase) x = false;
            }

            }
        public new void Draw(SpriteBatch spriteBatch) {
            for (int i = 0; i < bullets.Count; i++) {
                bullets[i].Draw(spriteBatch);
            }
            base.Draw(spriteBatch);
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
            if (Keyboard.GetState().IsKeyDown(Keys.X)&&bullets.Count<4) {
                bullets.Add(new BulletStructure.PlayerBullet(bullet,pos)); }
            if(GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Released || Keyboard.GetState().IsKeyUp(Keys.Z)){
                locks = false;
            }
        }

    }
}
