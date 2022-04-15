using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgbJourney
{
    public abstract partial class GameState : DrawableGameComponent
    {
        List<GameComponent> _childComponents;

        public List<GameComponent> ChildComponents => _childComponents;

        GameState _tag;

        public GameState Tag => _tag;

        protected GameStateManager StateManager;

        public GameState(Game game, GameStateManager stateManager) : base(game)
        {
            StateManager = stateManager;
            _childComponents = new List<GameComponent>();
            _tag = this;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _childComponents)
            {
                if(component.Enabled)
                    component.Update(gameTime);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            DrawableGameComponent drawComponent;
            foreach (var component in _childComponents)
            {
                if (component is DrawableGameComponent)
                {
                    drawComponent = (DrawableGameComponent)component;
                    if (drawComponent.Visible)
                        drawComponent.Draw(gameTime);
                }
            }
            base.Draw(gameTime);
        }

        internal protected virtual void StateChange(object sender, EventArgs e)
        {
            if (StateManager.CurrentState == Tag)
                Show();
            else
                Hide();
        }

        protected virtual void Show()
        {
            Visible = true;
            Enabled = true;

            foreach (var component in _childComponents)
            {
                component.Enabled = true;
                if(component is DrawableGameComponent)
                {
                    ((DrawableGameComponent)component).Visible = true;
                }
            }
        }

        protected virtual void Hide()
        {
            Visible = false;
            Enabled = false;

            foreach (var component in _childComponents)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent)
                {
                    ((DrawableGameComponent)component).Visible = false;
                }
            }
        }
    }
}
