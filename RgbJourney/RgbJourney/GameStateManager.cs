using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgbJourney
{
    public class GameStateManager : GameComponent
    {
        public event EventHandler OnStateChange;

        Stack<GameState> _gameStates = new Stack<GameState>();
        const int startDrawOrder = 5000;
        const int drawOrderInc = 100;
        int drawOrder;

        public GameState CurrentState => _gameStates.Peek();

        public GameStateManager(Game game) : base(game)
        {
            drawOrder = startDrawOrder;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void PopState()
        {
            if (_gameStates.Count > 0)
            {
                RemoveState();
                drawOrder -= drawOrderInc;
                if(OnStateChange != null)
                    OnStateChange(this, EventArgs.Empty);
            }
        }

        private void RemoveState()
        {
            var state = _gameStates.Peek();
            OnStateChange -= state.StateChange;
            Game.Components.Remove(state);
            _gameStates.Pop();
        }

        public void PushState(GameState state)
        {
            drawOrder += drawOrderInc;
            state.DrawOrder = drawOrder;
            AddState(state);

            if (OnStateChange != null)
                OnStateChange(this, EventArgs.Empty);
        }

        private void AddState(GameState state)
        {
            _gameStates.Push(state);
            Game.Components.Add(state);
            OnStateChange += state.StateChange;
        }

        public void ChangeState(GameState newState)
        {
            while (_gameStates.Count > 0)
                RemoveState();

            newState.DrawOrder = startDrawOrder;
            drawOrder = startDrawOrder;
            AddState(newState);

            if (OnStateChange != null)
                OnStateChange(this, EventArgs.Empty);
        }
    }
}
