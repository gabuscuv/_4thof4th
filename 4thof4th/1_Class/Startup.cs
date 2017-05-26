using _4thof4th.Class;
using _4thof4th.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace _4thof4th
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Startup : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Startup()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                // Resolucion por Defecto
                //PreferredBackBufferWidth = 800,PreferredBackBufferHeight = 600
                PreferredBackBufferWidth = 1280,PreferredBackBufferHeight = 720
                //PreferredBackBufferWidth = 1920,PreferredBackBufferHeight = 1080
            };
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameStateManager.Instance.SetContent(Content);
            GameStateManager.Instance.AddScreen(new Menu(GraphicsDevice,graphics));
            //GameStateManager.Instance.AddScreen(new sceneLoader(@"maps\scenetest.wiml", GraphicsDevice));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {   
            GameStateManager.Instance.Update(gameTime);
            /// Global Input
          // Cerrar el Juego
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
                || Keyboard.GetState().IsKeyDown(Keys.Escape))Exit();
          // Pantalla Completa
            if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) 
                && Keyboard.GetState().IsKeyDown(Keys.Enter)) graphics.ToggleFullScreen();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime){
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GameStateManager.Instance.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
