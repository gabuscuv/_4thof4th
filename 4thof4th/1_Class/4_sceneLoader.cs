using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using _4thof4th.GameStates;
using _4thof4th.Utils;
using _4thof4th.CharacterStructure;
using _4thof4th.BulletStructure;

namespace _4thof4th{
    class sceneLoader : GameState {
        private scene Scene = null;
        private List<Enemy> enemyList = null;
        private List<BulletEmisor> bulletList =null;
        private Player player = null;
        private Stream FileStream = null;
        private SpriteFont font = null;
        private SpriteFont fontDOS = null;
        private Texture2D background = null;
        private Dialog s = null;
        private string map = null;
        private bool debug;
        private bool fail;
        public sceneLoader(String map, bool debug, GraphicsDevice graphicsdevice, GraphicsDeviceManager graphics) : base(graphicsdevice, graphics) {
            this.map = map;
            this.debug = debug;
        }
        private enum inputmodes { Battle, Talking, Menu, GameOver }
        private inputmodes inputmode;
        public bool load(string map) {
            try {
                FileStream = File.OpenRead(map);
                BinaryFormatter deserializer = new BinaryFormatter();
                Scene = deserializer.Deserialize(FileStream) as scene;
                FileStream.Close();
                FileStream = null;

            }
            catch (SerializationException) { return false; }
            catch (FileNotFoundException) { return false; }
            catch (DirectoryNotFoundException) { return false; }
            catch (Exception)
            {return false;}
            return true;
        }

        public override void Initialize() {

            bulletList = new List<BulletEmisor>();
            inputmode = inputmodes.Talking;
        }

        public override void LoadContent(ContentManager content){
            try{
                if (load(map)){
                    font = content.Load<SpriteFont>("Fonts/debug");
                    fontDOS = content.Load<SpriteFont>("Fonts/FontDOS");
                    if (Scene.Player) player = new Player(@"Sprites/characters/player", content.Load<Texture2D>(@"Sprites/misc/scircle"), content, mainFrame);
                    if (Scene.Enemy) { }
                    if (Scene.Text != null) { s = new Dialog(graphicsDevice, Scene.Text); }
                    bulletList.Add(new BulletEmisor(new Vector2(128, 32), content, mainFrame));
                    bulletList.Add(new BulletEmisor(new Vector2(1120, 32), content, mainFrame));
                    if (Scene.Bgpath != "") background = content.Load<Texture2D>(@Scene.Bgpath);//mainFrame.Y += 100;
                }
                else { s = new Dialog(graphicsDevice, "Ha ocurrido un error a la hora de cargar el mapa, pulsa Z para volver"); }
            }
            catch {
                throw;
            }


        }

        public override void UnloadContent(){
            bulletList.Clear();
        }

        public override void Update(GameTime gameTime)
        {
            if (player != null) { player.Update(gameTime, debug); }

            if (enemyList != null && enemyList.Count != 0){
                enemyList.ForEach(delegate (Enemy s){
                    //utils.IntersectPixels(player.getBoxZone(), player.getTextureData(), s.getBoxZone(), s.getTextureData());
                });
            }
            switch (inputmode)
            {
                case inputmodes.Battle:
                    for (int i = 0; i < bulletList.Count; i++){
                        bulletList[i].Update(gameTime, debug);
                        if (utils.Isoutofscreen(bulletList[i].getPos(), mainFrame)) bulletList.Remove(bulletList[i]);
                        for (int j = 0; j < bulletList[i].bulletsonDisplay.Count; j++){
                            bulletList[i].bulletsonDisplay[j].Update(player.getPos(), bulletList[i].getPos(), gameTime);
                            if (utils.charactervsbullets(player,bulletList[i].bulletsonDisplay[j])) {
                                player.life--; bulletList[i].bulletsonDisplay.Remove(bulletList[i].bulletsonDisplay[j]);
                            }
                            if (utils.Isoutofscreen(bulletList[i].bulletsonDisplay[j].getpos(), mainFrame)){
                                bulletList[i].bulletsonDisplay.Remove(bulletList[j].bulletsonDisplay[j]);
                            }
                        }
                    };
                    for (int i = 0; i < player.bullets.Count;i++) {
                        for (int j = 0; j < bulletList.Count; j++){
                            if (utils.charactervsbullets(bulletList[j],player.bullets[i])){
                                bulletList.Remove(bulletList[j]);break; }
                            }
                        if (utils.Isoutofscreen(player.bullets[i].getpos(), mainFrame)){
                            player.bullets.Remove(player.bullets[i]); break;
                        }
                    }
                    // if (Keyboard.GetState().IsKeyDown(Keys.Enter)) inputmode = inputmodes.Menu; 
                    break;
                case inputmodes.Talking: if (s.Update(gameTime, Keyboard.GetState())) inputmode = inputmodes.Battle; break;
                case inputmodes.GameOver:if (Keyboard.GetState().IsKeyDown(Keys.Z)) { player.life = 4; inputmode = inputmodes.Battle; };break;
            }
            if (player.life < 0) inputmode = inputmodes.GameOver;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            if (inputmode != inputmodes.GameOver)
            {
                graphicsDevice.Clear(Color.CornflowerBlue);

                spriteBatch.Begin();
                // FONDO; NO DEBE HABER NADA ANTES
                if (background != null) { spriteBatch.Draw(background, mainFrame, Color.White); }
                // AQUI EMPIEZAS A DIBUJAR COSAS
                if (player != null) { player.Draw(spriteBatch); }

                if (inputmode == inputmodes.Talking) s.Draw(new Vector2(400, 400), spriteBatch, font);

                bulletList.ForEach(delegate (BulletEmisor s)
                {
                    s.Draw(spriteBatch);
                    if (debug) spriteBatch.DrawString(font, s.bulletsonDisplay.Count.ToString(), new Vector2(s.getPos().X + 10, 120), Color.White);
                    s.bulletsonDisplay.ForEach(delegate (BulletGeneric bulletg)
                    {
                        bulletg.Draw(spriteBatch);
                    });
                });

                if (debug){
                    spriteBatch.DrawString(font,
                      "Life: "+player.life+
                     "X: " + player.getPos().X +
                    "\nY: " + player.getPos().Y +
                    "\nWidth: " + graphicsDevice.Viewport.Width +
                    "\nHeight:" + graphicsDevice.Viewport.Height +
                    "\nGamestate:" + inputmode.ToString(), new Vector2(50, 50), Color.White);
                }
                // TODO: Add your drawing code here
                spriteBatch.End();
            }
            else {
                graphicsDevice.Clear(Color.Black);
                spriteBatch.Begin();
                spriteBatch.DrawString(fontDOS,"",new Vector2(10,10),Color.White);
                spriteBatch.End();
            }
        }
    }
}