using System.Collections.Generic;
using LouieLib.Containers;

namespace LouieLib.Systems
{
    internal class LouieSystem : ILouieSystem
    {
        private readonly List<MonoSystem> _systems = new();

        public ILouieSystem Add(MonoSystem monoSystem)
        {
            _systems.Add(monoSystem);
            return this;
        }

        public void Build()
        {
            _systems.ForEach(system =>
            {
                system.Start();
            });
        }

        public void Update()
        {
            _systems.ForEach(system =>
            {
                system.Update();
            });
        }

        public void FixedUpdate()
        {
            _systems.ForEach(system =>
            {
                system.FixedUpdate();
            });
        }

        public static void ClearSystem()
        {
            Container.Clear();
        }
    }
}