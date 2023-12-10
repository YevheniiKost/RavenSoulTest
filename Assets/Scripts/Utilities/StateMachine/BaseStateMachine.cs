using System;
using System.Collections.Generic;
using UnityEngine;

namespace RavenSoul.Utilities.StateMachine
{
    public class BaseStateMachine : IStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;
        private bool _canUpdate = true;

        public BaseStateMachine()
        {
            _states = new Dictionary<Type, IState>();
        }

        public void AddState<TState>(TState state) where TState : IState
        {
            if (_states.ContainsKey(typeof(TState)))
            {
                Debug.LogError($"State Machine already has {typeof(TState).Name} state");
            }
            else
            {
                _states[typeof(TState)] = state;
            }
        }


        public void SwitchTo<TState>() where TState : IState
        {
            _canUpdate = false;
            var state = _states[typeof(TState)];
            state.Prepare();

            _activeState?.Exit();

            _activeState = state;
            _activeState.Enter();
            _canUpdate = true;
        }

        public void ExitCurrent()
        {
            _activeState?.Exit();
        }

        public void Update()
        {
            if (!_canUpdate) return;
            
            _activeState?.Update();
        }
    }
}
