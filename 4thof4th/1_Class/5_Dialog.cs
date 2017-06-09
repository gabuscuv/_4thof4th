using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using _4thof4th.Utils;

namespace _4thof4th
{
    class Dialog{
        NEIO neio = new NEIO();
        Random random = new Random();
        private StringBuilder textonScreen;
        private string[] text;
        private Texture2D dialog = null;
        private byte[] dialog_cont;
        private bool kbr;
        private float[] acom;
        public Dialog(GraphicsDevice graphicsDevice, string text) {
            dialog_cont = new byte[3];
            acom = new float[2];
            textonScreen = new StringBuilder();
            List<Color> colorbg = new List<Color>();
            colorbg.Add(Color.TransparentBlack);
            List<Color> colorborder = new List<Color>();
            colorborder.Add(Color.Green);
            dialog = utils.CreateRoundedRectangleTexture(graphicsDevice, 230, 90, 4, 1, 0, colorbg, colorborder, 0f, 0f);
            this.text = text.Split(';');
        }

        public bool Update(GameTime gameTime,KeyboardState keyboard) {

                if (acom[0] > 0.01f)
                {
                if (text[dialog_cont[0]].Length != textonScreen.Length){
                    if (!text[dialog_cont[0]].Substring(dialog_cont[1]).StartsWith("_us_")){
                        textonScreen.Append(text[dialog_cont[0]][dialog_cont[1]]);
                        dialog_cont[1]++;
                        acom[0] = 0;
                        // Sistema de salto de lineas automatico cada 18 caracteres
                        if (dialog_cont[2]++ > 18) {
                            if (text[dialog_cont[0]][dialog_cont[1]] == ' ') { textonScreen.Append("\n"); dialog_cont[2] = 0; }
                        }
                    }
                    else {
                        int[] debug_int={ dialog_cont[0],dialog_cont[1],text[dialog_cont[0]].LastIndexOf('_')  };
                        string debug = text[dialog_cont[0]].Substring(dialog_cont[1],text[dialog_cont[0]].LastIndexOf('_') - dialog_cont[1]);
                        text[dialog_cont[0]]=text[dialog_cont[0]].Replace(text[dialog_cont[0]].Substring(dialog_cont[1], text[dialog_cont[0]].LastIndexOf('_') -dialog_cont[1] +1),usagiscript(text[dialog_cont[0]].Substring(dialog_cont[1], text[dialog_cont[0]].LastIndexOf('_') - dialog_cont[1]) ?? ""));
                    }
                }
                    else if (keyboard.IsKeyDown(Keys.Z)) { textonScreen.Clear(); dialog_cont[1] = 0; dialog_cont[0]++; }
                    if (dialog_cont[0] == text.GetLength(0)) { return true; }
                }

           /*if (text[0].Length < textonScreen.Length && Keyboard.GetState().IsKeyDown(Keys.Z)){
                textonScreen.Clear(); textonScreen.Append(text[dialog_cont[0]]);
            }*/
            if (Keyboard.GetState().IsKeyDown(Keys.Enter)) return true;
            if (kbr) {
                if (acom[1] > 0.5f){neio.KeyboardLedsController(random.Next(0,3));acom[1] = 0f; }
                acom[1] += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            acom[0] += (float)gameTime.ElapsedGameTime.TotalSeconds;
            return false;
        }


        /// <summary>
        /// Usagi Script - Sistema para habilitar y desactivar funciones del motor a nivel dialogo/script
        /// Normalmente funciones relacionadas con el NEIO (No-Ethical Input and Output) pero no reservado a dicho motor
        /// </summary>
        /// <param name="x">el comando, en formato _us_comando_</param>
        public string usagiscript(String x) {
            switch (x) {
               case "_us_kbr_en":kbr = true; dialog_cont[0]++; break;
               case "_us_kbr_dis":kbr = false; dialog_cont[0]++; break;
               case "_us_get_user": return neio.getUser();
               case "_us_get_files":String[] tmp = neio.getRandomNameFiles(5);String finish = "";
                    for (int i = 0; i < tmp.GetLength(0); i++)finish += tmp[i] + ",";
                    return finish;
                case "_us_beep": NeoRetroAudioEngine.beeperengine(new int[,] { { 32, 32 } });break;
                case "_us_beep2": NeoRetroAudioEngine.beeperengine(new int[,] { { 80, 1000 }, { 80, 1000 } }); break;


            }
            return null;
        }
        public void Draw(Vector2 pos,SpriteBatch spriteBatch,SpriteFont font) {
            spriteBatch.Draw(dialog, pos, Color.White);
            spriteBatch.DrawString(font, textonScreen.ToString(), new Vector2(pos.X+10, pos.Y+20), Color.White);
        }
    }
}
