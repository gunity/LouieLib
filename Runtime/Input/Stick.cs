using UnityEngine;

namespace LouieLib.Input
{
    public class Stick
    {
        private readonly RectTransform _background;
        private readonly RectTransform _handle;
        private readonly GameObject _backgroundGameObject;
        private readonly GameObject _handleGameObject;
        private readonly float _radius;
        
        private Vector2 _center;

        public Stick(RectTransform background, RectTransform handle)
        {
            _background = background;
            _handle = handle;
            _backgroundGameObject = background.gameObject;
            _handleGameObject = handle.gameObject;
            _center = Vector2.zero;
            _radius = _background.rect.width / 2;
            
             Hide();
        }
        
        public void Show(Vector2 sourcePosition)
        {
            _center = sourcePosition;
            _backgroundGameObject.SetActive(true);
            _handleGameObject.SetActive(true);
            _background.anchoredPosition = sourcePosition;
            SetPosition(sourcePosition);
        }

        public void Hide()
        {
            _center = Vector2.zero;
            _backgroundGameObject.SetActive(false);
            _handleGameObject.SetActive(false);
        }
        
        public Vector2 SetPosition(Vector2 position)
        {
            var clampMagnitude = Vector2.ClampMagnitude(position - _center, _radius);
            _handle.anchoredPosition = _center + clampMagnitude;
            return clampMagnitude.normalized;
        }
    }
}