using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _4thof4th
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        bool debug = true;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        private SpriteFont font;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                // Resolucion por Defecto
                
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720
                //PreferredBackBufferWidth = 1920,
                //PreferredBackBufferHeight = 1080
            };
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            player = new Player("sprite",this);
            font = Content.Load<SpriteFont>("debug");
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            player.Update(debug);

            
         /// Global Input
            if (Keyboard.GetState().IsKeyDown(Keys.D)) {
                if (debug)debug = false;else debug = true;
            }
          // Cerrar el Juego
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
                || Keyboard.GetState().IsKeyDown(Keys.Escape))Exit();
          // Pantalla Completa
            if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) 
                && Keyboard.GetState().IsKeyDown(Keys.Enter)) graphics.ToggleFullScreen();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            player.Draw(spriteBatch);
            if(debug){
                spriteBatch.DrawString(font,
                "X: "+player.getX()+
                "\nY: "+player.getY()+
                "\nWidth: "+GraphicsDevice.Viewport.Width+
                "\nHeight:"+GraphicsDevice.Viewport.Height, new Vector2(50,50),Color.Black);
            }
            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
