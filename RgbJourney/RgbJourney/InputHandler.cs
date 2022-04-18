using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RgbJourney.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgbJourney
{
    public class InputHandler : GameComponent
    {
        private static KeyboardState _keyboardState;
        private static KeyboardState _lastKeyboardState;

        private static MouseState _mouseState;
        private static MouseState _lastMouseState;

        public static KeyboardState KeyboardState => _keyboardState;
        public static KeyboardState LastKeyboardState => _lastKeyboardState;

        public static MouseState MouseState => _mouseState;
        public static MouseState LastMouseState => _lastMouseState;


        public InputHandler(Game game) : base(game)
        {
            _keyboardState = Keyboard.GetState();
            _mouseState = Mouse.GetState();
        }

        #region MonogameMethods
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            _lastKeyboardState = _keyboardState;
            _keyboardState = Keyboard.GetState();

            _lastMouseState = _mouseState;
            _mouseState = Mouse.GetState();
            base.Update(gameTime);
        }

        #endregion

        public static void Flush()
        {
            _lastKeyboardState = _keyboardState;
            _lastMouseState = _mouseState;
        }

        public static bool KeyReleased(Keys key) =>
            _keyboardState.IsKeyUp(key) && _lastKeyboardState.IsKeyDown(key);

        public static bool KeyPressed(Keys key) =>
            _keyboardState.IsKeyDown(key) && _lastKeyboardState.IsKeyUp(key);

        public static bool KeyDown(Keys key) => _keyboardState.IsKeyDown(key);

        public static Point MouseAsPoint()
        {
            return new Point(_mouseState.X, _mouseState.Y);
        }

        public static Vector2 MouseAsVector2()
        {
            return new Vector2(_mouseState.X, _mouseState.Y);
        }

        public static Point LastMouseAsPoint()
        {
            return new Point(_lastMouseState.X, _lastMouseState.Y);
        }

        public static Vector2 LastMouseAsVector2()
        {
            return new Vector2(_lastMouseState.X, _lastMouseState.Y);
        }

        public static bool CheckMousePress(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return _mouseState.LeftButton == ButtonState.Pressed
                        && _lastMouseState.LeftButton == ButtonState.Released;
                case MouseButton.Right:
                    return _mouseState.RightButton == ButtonState.Pressed
                        && _lastMouseState.RightButton == ButtonState.Released;
                case MouseButton.Middle:
                    return _mouseState.MiddleButton == ButtonState.Pressed
                        && _lastMouseState.MiddleButton == ButtonState.Released;
                default: return false;
            }
        }

        public static bool CheckMouseReleased(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return _mouseState.LeftButton == ButtonState.Released
                        && _lastMouseState.LeftButton == ButtonState.Pressed;
                case MouseButton.Right:
                    return _mouseState.RightButton == ButtonState.Released
                        && _lastMouseState.RightButton == ButtonState.Pressed;
                case MouseButton.Middle:
                    return _mouseState.MiddleButton == ButtonState.Released
                        && _lastMouseState.MiddleButton == ButtonState.Pressed;
                default: return false;
            }
        }

        public static bool IsMouseDown(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return _mouseState.LeftButton == ButtonState.Pressed;
                case MouseButton.Right:
                    return _mouseState.RightButton == ButtonState.Pressed;
                case MouseButton.Middle:
                    return _mouseState.MiddleButton == ButtonState.Pressed;
                default: return false;
            }
        }

        public static bool IsMouseUp(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return _mouseState.LeftButton == ButtonState.Released;
                case MouseButton.Right:
                    return _mouseState.RightButton == ButtonState.Released;
                case MouseButton.Middle:
                    return _mouseState.MiddleButton == ButtonState.Released;
                default: return false;
            }
        }

    }
}
