using System;
using System.IO;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using _4thof4th.GameStates;
using _4thof4th.Utils;
using _4thof4th.CharacterStructure;
using _4thof4th.BulletStructure;

namespace _4thof4th{
	class sceneLoader : GameState{
		private scene Scene=null;
        private List<Enemy> enemyList = null;
        private List<BulletEmisor> bulletList;
        private BulletEmisor test;
        private Player player=null;
		private Stream FileStream=null;
        private SpriteFont font=null;
        private Texture2D background=null;

        private bool debug;
        public sceneLoader(String map,bool debug,GraphicsDevice graphicsdevice, GraphicsDeviceManager graphics) : base(graphicsdevice, graphics){
            load(map);
            this.debug = debug;
            bulletList = new List<BulletEmisor>();

        }
        enum inputmodes { Battle,Talking,Menu }
        inputmodes inputmode;
	public bool load(string map){
            try{
                FileStream = File.OpenRead(map);
                BinaryFormatter deserializer = new BinaryFormatter();
                Scene = deserializer.Deserialize(FileStream) as scene;
                FileStream.Close();
                FileStream = null;
               
            }
            catch (SerializationException) { throw; }
            catch (FileNotFoundException) { throw; }
            catch (DirectoryNotFoundException){throw;}
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public override void Initialize(){
        }

        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Fonts/debug");
            if (Scene.Player)player = new Player("Sprites/characters/sprite",content,mainFrame);
            if (Scene.Enemy) {
            }
            bulletList.Add (new BulletEmisor(new Vector2(128, 32), content, mainFrame));
            if (Scene.Bgpath != "")background = content.Load<Texture2D>(@Scene.Bgpath);
            
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (player != null) { player.Update(gameTime, debug); }
            if (enemyList != null && enemyList.Count != 0) {
                enemyList.ForEach(delegate (Enemy s) {
                    utils.IntersectPixels(player.getBoxZone(), player.getTextureData(), s.getBoxZone(), s.getTextureData());
                });
            }

            bulletList.ForEach(delegate (BulletEmisor s) {
                s.Update(gameTime, debug);
                for (int i = 0; i < s.bulletsonDisplay.Count; i++) {

                    if ((s.bulletsonDisplay[i].getpos().X > mainFrame.Width + 50 || s.bulletsonDisplay[i].getpos().X < -400) ||
                    (s.bulletsonDisplay[i].getpos().Y > mainFrame.Height + 50 || s.bulletsonDisplay[i].getpos().Y < -400))
                    {
                        s.bulletsonDisplay.Remove(s.bulletsonDisplay[i]);
                    }
                    else { s.bulletsonDisplay[i].Update(player.getPos(), s.getPos(), gameTime); }
                };
            });
            }

        



        public override void Draw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            // FONDO; NO DEBE HABER NADA ANTES
            if (background != null) { spriteBatch.Draw(background, mainFrame, Color.White); }
            // AQUI EMPIEZAS A DIBUJAR COSAS
            if (player != null) { player.Draw(spriteBatch); }

            bulletList.ForEach(delegate (BulletEmisor s) {
                s.Draw(spriteBatch);
                spriteBatch.DrawString(font, s.bulletsonDisplay.Count.ToString(), new Vector2(120, 120), Color.White);

                s.bulletsonDisplay.ForEach(delegate (BulletGeneric bulletg){
                    bulletg.Draw(spriteBatch);
                });
            });
            if (debug)
            {
                spriteBatch.DrawString(font,
                "X: " + player.getPos().X +
                "\nY: " + player.getPos().Y +
                "\nWidth: " + graphicsDevice.Viewport.Width +
                "\nHeight:" + graphicsDevice.Viewport.Height, new Vector2(50, 50), Color.White);
            }
            // TODO: Add your drawing code here
            spriteBatch.End();
        }
    }
}