using System;
using System.Collections.Generic;

namespace LouieLib.Systems
{
    public abstract class MultiSystem : MonoSystem
    {
        private readonly Dictionary<Enum, List<MonoSystem>> _states = new();
        private Enum _currentState;

        protected MultiSystem(Enum defaultState)
        {
            _currentState = defaultState;
        }

        public override void Start()
        {
            var systems = _states[_currentState];
            foreach (var monoSystem in systems)
            {
                monoSystem.Start();
            }
        }

        public override void Update()
        {
            var systems = _states[_currentState];
            foreach (var monoSystem in systems)
            {
                monoSystem.Update();
            }
        }

        public override void FixedUpdate()
        {
            var systems = _states[_currentState];
            foreach (var monoSystem in systems)
            {
                monoSystem.FixedUpdate();
            }
        }

        protected void Add(Enum activeState, MonoSystem monoSystem, Enum nextState = null)
        {
            if (nextState != null)
            {
                monoSystem.OnEnd += () =>
                {
                    _currentState = nextState;
                    Start();
                };
            }
            
            if (_states.TryGetValue(activeState, out var systems))
            {
                systems.Add(monoSystem);
                return;
            }

            _states.Add(activeState, new List<MonoSystem> { monoSystem });
        }
    }
}