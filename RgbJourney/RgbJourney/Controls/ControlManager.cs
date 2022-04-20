using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace RgbJourney.Controls
{
    public class ControlManager : List<Control>
    {
        private int _selectedControl = 0;
        public static SpriteFont SpriteFont;

        public EventHandler FocusChanged;

        public ControlManager(SpriteFont spriteFont) : base()
        {
            SpriteFont = spriteFont;
        }

        public ControlManager(SpriteFont spriteFont, int capacity) : base(capacity)
        {
            SpriteFont = spriteFont;
        }

        public ControlManager(SpriteFont spriteFont, IEnumerable<Control> collection) : base(collection)
        {
            SpriteFont = spriteFont;
        }

        public void Update(GameTime gameTime, PlayerIndex playerIndex)
        {
            if (Count == 0)
                return;

            foreach (var control in this)
            {
                if (control.Enabled)
                    control.Update(gameTime);
                if (control.HasFocus)
                    control.HandleInput(playerIndex);

                if (control.GetBounds().Contains(InputHandler.MouseAsVector2()))
                {
                    MouseControl(control);
                }
            }

            if (InputHandler.KeyPressed(Keys.Up))
                PreviousControl();

            if (InputHandler.KeyPressed(Keys.Down))
                NextControl();
        }

        private void MouseControl(Control control)
        {
            foreach (var c in this)
                c.HasFocus = false;

            control.HasFocus = true;

            if (control.TabStop && control.Enabled)
            {
                _selectedControl = IndexOf(control);

                if (FocusChanged != null)
                    FocusChanged(control, EventArgs.Empty);
                
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            foreach (var control in this)
            {
                if (control.Visible)
                {
                    control.Draw(spriteBatch);                    
                }
            }
        }

        public void NextControl()
        {
            if (Count == 0)
                return;

            var currentControl = _selectedControl;
            this[_selectedControl].HasFocus = false;

            do
            {
                _selectedControl++;

                //Control cycling
                if (_selectedControl == Count)
                    _selectedControl = 0;

                if (this[_selectedControl].TabStop && this[_selectedControl].Enabled)
                {
                    if(FocusChanged != null)
                        FocusChanged(this[_selectedControl], EventArgs.Empty);
                    
                    break;
                }

            } while (currentControl != _selectedControl);

            this[_selectedControl].HasFocus = true;
        }

        public void PreviousControl()
        {
            if (Count == 0)
                return;

            var currentControl = _selectedControl;
            this[_selectedControl].HasFocus = false;

            do
            {
                _selectedControl--;
                
                //Control cycling
                if (_selectedControl < 0)
                    _selectedControl = Count - 1;

                if (this[_selectedControl].TabStop && this[_selectedControl].Enabled)
                {
                    if (FocusChanged != null)
                        FocusChanged(this[_selectedControl], EventArgs.Empty);

                    break;
                }

            } while (currentControl != _selectedControl);

            this[_selectedControl].HasFocus = true;
        }
    }
}
