using System;
using System.Collections.Generic;
using LouieLib.Containers;
using LouieLib.Input.Data;
using UnityEngine;

namespace LouieLib.Input.Services
{
    public interface IMobileInput
    {
        event Action<string, bool> OnButton;
        
        Vector2 MoveDirection { get; }
        Vector2 LookDirection { get; }
        void HideMobileInput();
    }

    public class MobileInput : MonoBehaviour, IMobileInput
    {
        public event Action<string, bool> OnButton;

        public Vector2 MoveDirection { get; private set; }
        public Vector2 LookDirection { get; private set; }

        private readonly IMobileInputData _mobileInputData = Container.Get<IMobileInputData>();
        
        private MobileInputHelper _mobileInputHelper;
        private RectTransform _canvas;
        private Stick _stick;

        private void Start()
        {
            _mobileInputHelper = new MobileInputHelper(_mobileInputData);
            _canvas = _mobileInputHelper.CreateCanvas(gameObject) as RectTransform;
            _stick = _mobileInputHelper.CreateStick(_canvas);
            
            CreateButtons();
        }

        private void CreateButtons()
        {
            foreach (var buttonModel in _mobileInputData.Buttons)
            {
                var position = new Vector2(
                    _mobileInputData.ReferenceResolution.x * buttonModel.Position.x,
                    _mobileInputData.ReferenceResolution.y * buttonModel.Position.y);
                var buttonProxy = _mobileInputHelper.CreateButton(buttonModel, _canvas, position);

                buttonProxy.PointerDown += () => OnButton?.Invoke(buttonModel.Action, true);
                buttonProxy.PointerUp += () => OnButton?.Invoke(buttonModel.Action, false);
            }
        }

        private void Update()
        {
            MoveDirection = GetMoveDirection();
            LookDirection = GetLookDirection();
        }

        private Vector2 GetMoveDirection()
        {
            foreach (var touch in UnityEngine.Input.touches)
            {
                if (touch.position.x > Screen.width / 2f)
                {
                    continue;
                }
                
                RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas, touch.position, null,
                    out var touchPosition);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _stick.Show(touchPosition);
                        return Vector2.zero;
                    case TouchPhase.Stationary:
                    case TouchPhase.Moved:
                        return _stick.SetPosition(touchPosition);
                    case TouchPhase.Canceled:
                    case TouchPhase.Ended:
                        _stick.Hide();
                        return Vector2.zero;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            return Vector2.zero;
        }

        private Vector2 GetLookDirection()
        {
            foreach (var touch in UnityEngine.Input.touches)
            {
                if (touch.position.x < Screen.width / 2f)
                {
                    continue;
                }

                if (_mobileInputHelper.IsBlocked(touch.position))
                {
                    continue;
                }

                return touch.deltaPosition / 10f * _mobileInputData.LookSensitivity;
            }
            
            return Vector2.zero;
        }
        
        public void HideMobileInput()
        {
            _canvas.gameObject.SetActive(false);
        }
    }
}