using System;
using System.Text;
using _4thof4th.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _4thof4th.Class
{
    class Intro : GameState{

        int ver;
        int ver_min;
        String[] SysInfo;
        StringBuilder s;
        String[] sa;
        SpriteFont debugfont;
        SpriteFont DOSfont;
        SpriteFont kawaiifont;
        float time_acomu;
        bool first;

        //FX:VHS
        //phase1=delay 3 segundos, Empieza el Contador
        //FX:Glitches
        //phase2=glitches y pitidos
        //phase3=Bios POST
        //phase4=OpenBSD Loader
        //phase5=load scene
        enum Phases { phase0, phase1, phase2, phase3, phase4, phase5 };
        Phases phase;

        public Intro(GraphicsDevice graphicsdevice, GraphicsDeviceManager graphics) : base(graphicsdevice, graphics) {}
        
        public override void Update(GameTime gameTime){
            if (first) { time_acomu += (float)gameTime.ElapsedGameTime.TotalSeconds; if(time_acomu>3f)first = false; }
            else { 
            switch (phase) {
                case Phases.phase0:break;
                case Phases.phase1:
                    if (ver <= 4096){
                        ver += 10;
                    }
                    else { phase = Phases.phase2; }
                    break;
                case Phases.phase2:phase = Phases.phase3; break;
                case Phases.phase3: phase = Phases.phase4; break;
                case Phases.phase4: phase = Phases.phase5; break;
                case Phases.phase5: GameStateManager.Instance.ChangeScreen(new sceneLoader(@"maps\scenetest.wiml", false, graphicsDevice, graphics)); break;
            }
           }
        }

        public override void Draw(SpriteBatch spriteBatch){
            graphicsDevice.Clear(Color.Black);
           
            spriteBatch.Begin();
            switch (phase){
                case Phases.phase1:
                    spriteBatch.DrawString(debugfont, "Frecuencia de Reloj recomendada: " + ver_min.ToString().Insert(1, ".") + "Ghz", new Vector2(400, 200), Color.Red);
                    if (ver < 256) { spriteBatch.DrawString(debugfont,"La Frequencia de reloj que actualmente tiene es:" +ver.ToString("X").Insert(1, ".0")+"Ghz", new Vector2(400, 400), Color.Red); } else {
                        spriteBatch.DrawString(debugfont, "La Frequencia de reloj que actualmente tiene es:"+ ver.ToString("X").Insert(1, ".")+"Ghz", new Vector2(400, 400), Color.Red);
                    }
                    ; break;
                case Phases.phase2: break;
                case Phases.phase3: break;
                case Phases.phase4: break;

            }
            spriteBatch.End();
        }

        public override void Initialize(){
            first = true;
            phase = Phases.phase1;
            SysInfo = neio.getSystemInfoFormatted();
            ver = Int32.Parse(SysInfo[5]);
            ver_min = ver + 70;
            sa = new String[]{ "(C)"+SysInfo[2]+" "+SysInfo[1],
                                SysInfo[7]+" "+SysInfo[8]+" ACPI BIOS Revision: "+SysInfo[0],
                                "CPU: "+SysInfo[3]+" Ghz\tCount : "+SysInfo[4],
                                "Press DEL for run Setup\nPress F8 for BIOS POPUP",
                                ""};
        }

        public override void LoadContent(ContentManager content){
            debugfont = content.Load<SpriteFont>("Fonts/debug");
            kawaiifont = content.Load<SpriteFont>("Fonts/KTEGAKI");
            DOSfont = content.Load<SpriteFont>("Fonts/FontDOS");

        }

        public override void UnloadContent(){
        }


    }
}
