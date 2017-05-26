using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace _4thof4th.GameStates
{
    class GameStateManager
    {

        // Instance of the game state manager     
        private static GameStateManager instance;

        // Stack for the screens     
        private Stack<IGameState> screens  = new  Stack<IGameState>();
        private ContentManager content;

        public static GameStateManager Instance{
            get
            {
                if (instance == null)
                {
                    instance = new GameStateManager();
                }
                return instance;
            }
        }

        public void SetContent(ContentManager content){
            this.content = content;
        }
        public void AddScreen(GameState screen){
            try
            {
                // Add the screen to the stack
                screens.Push(screen);
                // Initialize the screen
                screens.Peek().Initialize();
                // Call the LoadContent on the screen
                if (content != null)
                {
                    screens.Peek().LoadContent(content);
                }
            }
            catch (Exception){
                throw;
            }
        }


        // Removes the top screen from the stack
        public void RemoveScreen(){
            if (screens.Count > 0)
            {
                try
                {
                    var screen = screens.Peek();
                    screens.Pop();
                }
                catch (Exception){
                    throw;
                }
            }
        }

        // Clears all the screen from the list
        public void ClearScreens()
        {
            while (screens.Count > 0)
            {
                screens.Pop();
            }
        }
        public void ChangeScreen(GameState screen)
        {
            try
            {
                ClearScreens();
                AddScreen(screen);
            }
            catch (Exception){
                throw;
            }
        }

        // Updates the top screen. 
        public void Update(GameTime gameTime){
            try{if (screens.Count > 0)screens.Peek().Update(gameTime);}
            catch (Exception){
                throw;
            }
        }

        // Renders the top screen.
        public void Draw(SpriteBatch spriteBatch){
            try{
                if (screens.Count > 0)screens.Peek().Draw(spriteBatch);
            }
            catch (Exception){
                throw;
            }
        }
        public void UnloadContent()
        {
            foreach (GameState state in screens){
                state.UnloadContent();
            }
        }
    }
}
