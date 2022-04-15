using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgbJourney
{
    public class InputHandler : GameComponent
    {
        private static KeyboardState _keyboardState;
        private static KeyboardState _lastKeyboardState;

        public static KeyboardState KeyboardState => _keyboardState;
        public static KeyboardState LastKeyboardState => _lastKeyboardState;



        public InputHandler(Game game) : base(game)
        {
            _keyboardState = Keyboard.GetState();
        }

        #region MonogameMethods
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        #endregion

        public static void Flush() => _lastKeyboardState = _keyboardState;

        public static bool KeyReleased(Keys key) =>
            _keyboardState.IsKeyUp(key) && _lastKeyboardState.IsKeyDown(key);

        public static bool KeyPressed(Keys key) => 
            _keyboardState.IsKeyDown(key) && _lastKeyboardState.IsKeyUp(key);

        public static bool KeyDown(Keys key) => _keyboardState.IsKeyDown(key);
    }
}
