using LouieLib.Containers;
using LouieLib.Systems;
using UnityEngine;

namespace LouieLib
{
    public abstract class StarterBase : MonoBehaviour
    {
        private IDataContainer _dataContainer;
        private LouieSystem _system;
        
        private void Start()
        {
            _dataContainer = new DataContainer();
            _system = new LouieSystem();
            
            OnBind(_dataContainer);
            OnInstall(_system);
        }

        protected abstract void OnBind(IDataContainer container);
        
        protected abstract void OnInstall(ILouieSystem system);

        private void Update() => _system.Update();

        private void FixedUpdate() => _system.FixedUpdate();

        private void OnApplicationQuit() => LouieSystem.ClearSystem();

        private void OnDestroy() => LouieSystem.ClearSystem();
    }
}