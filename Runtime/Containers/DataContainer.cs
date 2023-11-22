using System;
using System.Collections.Generic;
using UnityEngine;

namespace LouieLib.Containers
{
    internal class DataContainer : IDataContainer
    {
        public static IDataContainer Instance;
        
        private readonly Dictionary<Type, object> _data = new();

        public DataContainer()
        {
            if (Instance != null)
            {
                Instance.Clear();
                Instance = null;
            }

            Instance = this;
        }

        ~DataContainer()
        {
            Instance = null;
        }

        public IDataContainer Set<TType>(object data)
        {
            _data.Add(typeof(TType), data);

            return this;
        }

        public IDataContainer Set<TType, TInstance>() where TInstance : MonoBehaviour, TType
        {
            return Set<TType, TInstance>(out _);
        }

        public IDataContainer Set<TType, TInstance>(out TType instance) where TInstance : MonoBehaviour, TType
        {
            var gameObjectName = typeof(TInstance).Name.ToGameObjectName();
            var gameObject = new GameObject(gameObjectName);
            var component = gameObject.AddComponent<TInstance>();
            
            _data.Add(typeof(TType), component);
            instance = component;

            return this;
        }

        public T Get<T>()
        {
            return (T) _data[typeof(T)];
        }

        public void Clear()
        {
            _data.Clear();
        }
    }
}