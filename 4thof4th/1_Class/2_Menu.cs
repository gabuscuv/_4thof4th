using System;
using _4thof4th.GameStates;
using _4thof4th.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace _4thof4th.Class
{
    class Menu : GameState{
        //Variables de Fuente y Colores
		private SpriteFont font;
        private SpriteFont font_small;

        private Color background;
        private Color color_font;
        private Color color_font_selected;

        String[,] Options={{"Nueva Partida","Cargar Mapa","Ajustes","Acerca de","Salir"},
							{"Debug Mode","Resolucion","Pantalla Completa","vSync",""}};
        private int[] position_options;
        private int space_between_options=35;
        private byte[] op={0,0};
        private bool[] locks;
        private bool debug = false;
        GameState loadtmp=null;

        private float circule = MathHelper.Pi * 2;
        private float rotation = 0f;

        private float timeout;

        private Texture2D rect;
     // private Texture2D shadow;
        private Texture2D star;
        private Texture2D bottom;

        enum MenuStates{Main,LoadMap,Settings,About,Loading};
        MenuStates MenuState;
        public Menu(GraphicsDevice graphicsdevice,GraphicsDeviceManager graphics) : base(graphicsdevice, graphics){
        }
        public override void Draw(SpriteBatch spriteBatch){
            graphicsDevice.Clear(background);

            spriteBatch.Begin();
           if(debug)spriteBatch.DrawString(font, "Debug mode Activado", new Vector2(0, 0), Color.Black);

            //spriteBatch.draw(background,etc);
            //spriteBatch.draw(star,scaling_stuff,rotation_stuff,etc);
            switch (MenuState){
			case MenuStates.Main:drawmenu(spriteBatch,0);break;
			case MenuStates.Settings: drawmenu(spriteBatch, 1); break;
			case MenuStates.About:
                    spriteBatch.DrawString(font, "Juego creado por Gabriel Bustillo del Cuvillo\n" +
                                        "Creado con Usagi Engine tambien de Gabriel Bustillo del Cuvillo", new Vector2((mainFrame.Width / 2) - 400, (mainFrame.Height / 2)), Color.Black); break;
            case MenuStates.Loading:
                    spriteBatch.Draw(rect, new Vector2((mainFrame.Width / 2)+200, (mainFrame.Height / 2)+200), Color.White);
                    spriteBatch.DrawString(font, "Cargando...", new Vector2((mainFrame.Width / 2) + 200, (mainFrame.Height / 2) + 200), Color.Black);break;
			}
            spriteBatch.Draw(bottom, new Vector2(230, mainFrame.Height-50), Color.White);
            spriteBatch.DrawString(font_small, "Z para Aceptar  X para Cancelar/volver ↑↓←→ para Navegar", new Vector2(270, mainFrame.Height-45), Color.Black);

            spriteBatch.End();
        }

        private void spinstar(GameTime gameTime) {
            if (rotation < circule) { rotation += 0.05f; } else rotation = 0.01f;
            //rotation+=(float)gameTime.ElapsedGameTime.TotalSeconds%circule;

        }

        public override void Initialize(){
            position_options = new int[] { mainFrame.Right-200,mainFrame.Bottom-250};
            locks = new bool[2];
            MenuState = MenuStates.Main;
            background = new Color(253,172,62);
            color_font = new Color(253,220,159);
            color_font_selected = new Color(248, 237, 165);
        }

        private void drawmenu(SpriteBatch spriteBatch, byte x) {
            for (int i = 0; i < Options.GetLength(1); i++){
                if(!Options[x, i].Equals("")) { 
                spriteBatch.Draw(rect, new Vector2(position_options[0], position_options[1] + (i * space_between_options)), Color.White);
                    if (op[x] == i)
                    {
                        spriteBatch.Draw(star, new Vector2(position_options[0] - 40, (position_options[1] +10 ) + (i * space_between_options)), star.Bounds, Color.White, rotation,
                        new Vector2(130,136), 0.15f, SpriteEffects.None, 0f);
                        spriteBatch.DrawString(font, Options[x, i].ToString(), new Vector2(position_options[0]+10, position_options[1] + (i * space_between_options)), color_font_selected);
                    }
                    else
                    {
                        spriteBatch.DrawString(font, Options[x, i].ToString(), new Vector2(position_options[0]+10, position_options[1] + (i * space_between_options)), color_font);
                    }
                }
            }
        }

        public override void LoadContent(ContentManager content){
            font = content.Load<SpriteFont>("Fonts/KTEGAKI");
            font_small = content.Load<SpriteFont>("Fonts/KTEGAKI_SMALL");
            star = content.Load<Texture2D>("Sprites/misc/star");

            // Box Stuff
            List<Color> colorbg =new List<Color>();
            colorbg.Add(background);
            List<Color> colorborder = new List<Color>();
            colorborder.Add(color_font);
            rect = utils.CreateRoundedRectangleTexture(graphicsDevice,170,30,4,1,0,colorbg,colorborder,0f,0f);
            bottom = utils.CreateRoundedRectangleTexture(graphicsDevice, 600, 30, 4, 1, 0, colorbg, colorborder, 0f, 0f);
            colorbg = null;colorborder = null;
            //	background=content.Load<Texture2D>("/Images/Background/titlemenu");
        }

        public override void UnloadContent(){
            
		}

        public override void Update(GameTime gameTime){
            spinstar(gameTime);
            if (!locks[0]) { 
            if (MenuState== MenuStates.Main){
            // Esto solamente es aplicable al menu principal
            if (Keyboard.GetState().IsKeyDown(Keys.Down)){
                        if (op[0] <= 3) { op[0]++; locks[0] = true; rotation = 0f; }
                        else { if (!debug && op[0] == 1) op[0] = 2; }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up)) {
                if (op[0] != 0) { op[0]--; locks[0] = true; }
            }
			}else{
            // Esto es aplicable a los menus Secundarios
			if (Keyboard.GetState().IsKeyDown(Keys.Down)){
				if(op[1]<=2){op[1]++; locks[0] = true; rotation = 0f; }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up)) {
                    if(op[1]!=0){op[1]--; locks[0] = true; }
            }
				
			}
            if (Keyboard.GetState().IsKeyDown(Keys.Z)) {
                    if (!locks[1]) { 
				switch(MenuState){
					case MenuStates.Main:
					switch(op[0]){
                        case 0:loadtmp = new Intro(graphicsDevice, graphics); MenuState = MenuStates.Loading; break;
                        case 1:loadtmp = new sceneLoader(@"maps\scenetest.wiml",debug, graphicsDevice,graphics); MenuState = MenuStates.Loading; break;
						case 2:MenuState= MenuStates.Settings; locks[1] = true; break;
						case 3:MenuState= MenuStates.About; locks[1] = true; break;
                        case 4:Startup.exit = true;break;
					}break;
					case MenuStates.Settings:
                            switch (op[1])
                            {
                                //Debug mode
                                case 0: if (debug) { debug = false; } else { debug = true; }; locks[1] = true; break;
                                //Resolucion
                                case 1: locks[1] = true; break;
                                //Pantalla COmpleta
                                case 2: graphics.ToggleFullScreen();break ;
                                case 3: if (graphics.SynchronizeWithVerticalRetrace)graphics.SynchronizeWithVerticalRetrace = false;
                                        else graphics.SynchronizeWithVerticalRetrace = true;
                                    graphics.ApplyChanges(); graphicsDevice.Reset(); locks[1] = true; break;
                            }
                            break;
					case MenuStates.About: break;
                    case MenuStates.Loading: GameStateManager.Instance.ChangeScreen(loadtmp); break;
                        }
                    }
            }
			if(Keyboard.GetState().IsKeyDown(Keys.X)){
				if(MenuState== MenuStates.Settings || MenuState == MenuStates.About)
                {
					op[1]=0;
					MenuState= MenuStates.Main;
				}
			}
            }
            if (locks[0] && ((
                Keyboard.GetState().IsKeyUp(Keys.Down) && Keyboard.GetState().IsKeyUp(Keys.Up))
                || timeout > 0.4f
                )) { locks[0] = false; timeout = 0f; }
            else {if(locks[0])timeout += (float)gameTime.ElapsedGameTime.TotalSeconds; }
            if (locks[1] && Keyboard.GetState().IsKeyUp(Keys.Z)) { locks[1] = false; }
        }
    }
}
