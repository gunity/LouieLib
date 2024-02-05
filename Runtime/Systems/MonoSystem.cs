using System;

namespace LouieLib.Systems
{
    public abstract class MonoSystem
    {
        internal event Action OnEnd; 
        
        public virtual void Start()
        {
            // nothing
        }

        public virtual void Update()
        {
            // nothing
        }

        public virtual void FixedUpdate()
        {
            // nothing
        }

        protected void Finish()
        {
            OnEnd?.Invoke();
        }
    }
}