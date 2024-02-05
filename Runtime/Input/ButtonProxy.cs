using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LouieLib.Input
{
    public class ButtonProxy : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public event Action PointerDown;
        public event Action PointerUp;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            PointerDown?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            PointerUp?.Invoke();
        }
    }
}