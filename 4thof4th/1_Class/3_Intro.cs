using System;
using System.Text;
using _4thof4th.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _4thof4th.Class
{
    class Intro : GameState{
        private char[] loading= {'-','\u005C', '|','/' };
        private int cont=0;
        private int cont2;
        private int cont3=0;
        private int[] mb;
        private int ver;
        private int ver_min;
        private StringBuilder s;
        private string[] tmp;
        private string[] SysInfo;
        private string[] credits;
        private Color colorbgRuntime;
        private Color colorfontRuntime;
        private SpriteFont fontRuntime;
        private SpriteFont fontdebug;
        private SpriteFont DOSfont;
        private SpriteFont kawaiifont;
        float timeout;
        float timeoutw=1f;
        float[] transparent;
        Vector2[] creditposition;
        bool first;

        //Effect Aberration;

        Texture2D player;

        //FX:VHS
        //phase1=delay 2 segundos, Empieza el Contador
        //FX:Glitches
        //phase2=glitches y pitidos
        //phase3=Bios POST
        //phase4=OpenBSD Loader
        //phase5=load scene
        enum Phases { phase0, phase1, phase2, phase3, phase4, phase5 };
        Phases phase;

        public Intro(GraphicsDevice graphicsdevice, GraphicsDeviceManager graphics) : base(graphicsdevice, graphics) {}
        
        public override void Update(GameTime gameTime){
            switch (phase) {
                case Phases.phase0: if (first) { fontRuntime = kawaiifont; colorbgRuntime= new Color(253, 172, 62);  colorfontRuntime = new Color(253, 220, 159); first = false; }
                                    if (timeout > 3f) { phase = Phases.phase1; first = true; } else timeout += (float)gameTime.ElapsedGameTime.TotalSeconds;
                                    break;
                case Phases.phase1: if (first) { fontRuntime = fontdebug; colorbgRuntime = Color.Black; colorfontRuntime = Color.Red; first = false; }
                    if (ver <= 4096) ver += 10;
                    else { first = true; phase = Phases.phase2; }
                                    break;
                case Phases.phase2:/* if (first) { Console.Beep(); first = false; };*/ first = true; phase = Phases.phase3; break;
                case Phases.phase3:
                    // 930, 100
                    if (first) { NeoRetroAudioEngine.beeperengine(new int[,] { {98,500},{130,1000},{130,200}, { 130, 200 },{ 146, 200 },{155,200}, {130,200 } });timeoutw = 0.4f; timeout = 0f; fontRuntime = DOSfont; colorbgRuntime = Color.Black; colorfontRuntime = Color.White; first = false; }
                    if ((SysInfo.GetLongLength(0) != cont) || (mb[1] > mb[0]) ){
                        if ((SysInfo.GetLongLength(0) != cont))
                        {
                            if (timeout > timeoutw)
                            {
                                { s.Append(SysInfo[cont]); cont++; }
                                if (cont > 4 && cont < 6) { timeoutw = 1f; } else { timeoutw = 0.4f; }
                                if (SysInfo.GetLongLength(0) == cont) { s.Append(mb[0]); }
                                timeout = 0f;

                            }
                            else { timeout += (float)gameTime.ElapsedGameTime.TotalSeconds; }
                        }
                        else {
                            s.Replace(mb[0].ToString(),(mb[0]+=20).ToString());
                            if (mb[0] >= mb[1]) { s.Append(" OK"); }
                        }

                        break;
                    }
                    else { if (timeout < timeoutw) { first = true; cont = 0; phase = Phases.phase4;break; } else { timeout += (float)gameTime.ElapsedGameTime.TotalSeconds; } }
                     break;

                case Phases.phase4: if (first) { s.Clear(); fontRuntime = DOSfont; timeoutw = 1f; cont = 0; cont2 = 0; first = false; }
                    refresh_loading();
                    if (SysInfo.GetLength(0)!=cont2) {
                        if (timeout > timeoutw) {
                            s.Append(tmp[cont2]);cont2++;timeout = 0f;
                        }
                        timeout += (float) gameTime.ElapsedGameTime.TotalSeconds;
                        if (SysInfo.GetLength(0) == cont2) { cont3 = 0; timeout = 0f; timeoutw = 5f; }
                    } else { if (timeout < timeoutw) {
                                s.Replace("0x"+cont3.ToString("X"),"0x"+(cont3+=12).ToString("X"));
                                timeout += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        } else { first = true; phase = Phases.phase5; } }
                                    break;
                case Phases.phase5:
                    if (first) { };
                    refresh_loading();

                    if (!(cont != credits.GetLength(0))){

                    }
                    else { GameStateManager.Instance.ChangeScreen(new sceneLoader(@"maps\scenetest.wiml", false, graphicsDevice, graphics)); }
                    break;
            }
        }

        private void refresh_loading() {if (cont != (loading.GetLength(0) - 1)) { cont++; } else { cont = 0; }}

        public override void Draw(SpriteBatch spriteBatch){
            graphicsDevice.Clear(colorbgRuntime);
            switch (phase) {
                case Phases.phase0:
                case Phases.phase1:
                case Phases.phase2: 
                case Phases.phase3: //spriteBatch.Begin(0, null, null, null, null, Aberration, null); break;
                case Phases.phase4: spriteBatch.Begin();break;
                case Phases.phase5:  spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend); break;
            }
            
            switch (phase){
                case Phases.phase0:
                case Phases.phase1:
                    spriteBatch.DrawString(fontRuntime, "Frecuencia de Reloj recomendada: " + ver_min.ToString().Insert(1, ".") + "Ghz", new Vector2(400, 200), colorfontRuntime);
                    if (ver < 400) { spriteBatch.DrawString(fontRuntime,"La Frequencia de reloj que actualmente tiene es:" +ver.ToString().Insert(1, ".0")+"Ghz", new Vector2(400, 400), colorfontRuntime); } else {
                        spriteBatch.DrawString(fontRuntime, "La Frequencia de reloj que actualmente tiene es:"+ ver.ToString("X").Insert(1, ".")+"Ghz", new Vector2(400, 400), colorfontRuntime);
                    }; break;
                case Phases.phase2: break;
                case Phases.phase3: spriteBatch.DrawString(fontRuntime,s,new Vector2(mainFrame.Left,mainFrame.Top), colorfontRuntime); break;
                case Phases.phase4: spriteBatch.DrawString(fontRuntime, s.ToString()+loading[cont].ToString(), new Vector2(mainFrame.Left, mainFrame.Top), colorfontRuntime); break;
                case Phases.phase5: spriteBatch.DrawString(fontRuntime, s.ToString() + loading[cont].ToString(), new Vector2(mainFrame.Left, mainFrame.Top), colorfontRuntime)
                        
                        ; break;

            }
            spriteBatch.End();
        }

        public override void Initialize(){
            first = true;
            colorbgRuntime = Color.Black;
            colorfontRuntime = Color.White;
            s = new StringBuilder();
            phase = Phases.phase1;
            tmp = neio.getSystemInfoFormatted();
            SysInfo = new String[]{ "(C)"+tmp[2]+" "+tmp[1]+"\n",
                                tmp[7]+" "+tmp[8]+" ACPI BIOS Revision: "+tmp[0]+"\n",
                                "CPU: "+tmp[3]+"\tCount : "+tmp[4]+"\n",
                                "Press DEL for run Setup\nPress F8 for BIOS POPUP"+"\n",
                                "Initializing USB Controller",".",".","Done.\n"};
            ver = Int32.Parse(tmp[5]);
            ver_min = ver + 70;
            mb = new int[] {0, Int32.Parse(tmp[6]) };
            tmp = new string[] { "/boot/kernel/kernel", " text=", "0xEA43C0", " data=", "0xE0E38+0xC72F0", " syms=", "[0x4+0xDE570+0x4+0x5E51C]", "\nBooting..." };
            credits = new String[] { "_4thof4th" ," Un Juego de Gabriel Bustillo del Cuvillo","Realizado con Usagi Engine\nDesarrollado por Gabriel Bustillo del Cuvillo",
            "Musica, Codigo, y mucho más por Gabriel Bustillo del Cuvillo","Ilustrado por Carlos Ruiz Santiago para _4thof4th "};
        }

        public override void LoadContent(ContentManager content){
            fontdebug = content.Load<SpriteFont>("Fonts/debug");
            kawaiifont = content.Load<SpriteFont>("Fonts/KTEGAKI");
            fontRuntime = kawaiifont;
            DOSfont = content.Load<SpriteFont>("Fonts/FontDOS");
            player = content.Load<Texture2D>(@"Sprites/characters/player");
        }

        public override void UnloadContent(){
        }


    }
}
