using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _4thof4th.GameStates
{
    interface IGameState
    {
        void Initialize();

        void LoadContent(ContentManager content);

        void UnloadContent();

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);
    }
}