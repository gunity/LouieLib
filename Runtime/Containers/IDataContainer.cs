using UnityEngine;

namespace LouieLib.Containers
{
    public interface IDataContainer
    {
        IDataContainer Set<TType>(object data);
        IDataContainer Set<TType, TInstance>() where TInstance : MonoBehaviour, TType;
        IDataContainer Set<TType, TInstance>(out TType instance) where TInstance : MonoBehaviour, TType;
        T Get<T>();
        void Clear();
    }
}