using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _4thof4th.GameStates
{
   public abstract class GameState : IGameState
    {
        protected GraphicsDevice graphicsDevice;
        protected GraphicsDeviceManager graphics;
        public NEIO neio = new NEIO();
        protected Rectangle mainFrame;

        public GameState(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
        {
            this.graphicsDevice = graphicsDevice;
            this.graphics = graphics;
            mainFrame = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
        }

        public abstract void Initialize();
        public abstract void LoadContent(ContentManager content);
        public abstract void UnloadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
