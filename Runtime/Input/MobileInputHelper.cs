using System;
using LouieLib.Input.Data;
using LouieLib.Input.Models;
using UnityEngine;
using UnityEngine.UI;

namespace LouieLib.Input
{
    internal class MobileInputHelper
    {
        private readonly IMobileInputData _mobileInputData;

        public MobileInputHelper(IMobileInputData mobileInputData)
        {
            _mobileInputData = mobileInputData;
        }
        
        public ButtonProxy CreateButton(ButtonModel buttonModel, Transform parent, Vector2 position)
        {
            var buttonGameObject = new GameObject($"[BUTTON] {buttonModel.Action}");

            var buttonRectTransform = buttonGameObject.AddComponent<RectTransform>();
            buttonRectTransform.SetParent(parent);
            GetAnchors(buttonModel.Alignment, out var min, out var max);
            buttonRectTransform.anchorMin = min;
            buttonRectTransform.anchorMax = max;
            buttonRectTransform.anchoredPosition = position;
            buttonRectTransform.sizeDelta = buttonModel.Size;
            buttonRectTransform.pivot = new Vector2(0.5f, 1f);

            var buttonImage = buttonGameObject.AddComponent<Image>();
            buttonImage.sprite = buttonModel.Sprite;

            var button = buttonGameObject.AddComponent<Button>();

            var buttonColors = button.colors;
            buttonColors.normalColor = buttonModel.NormalColor;
            buttonColors.disabledColor = buttonModel.NormalColor;
            buttonColors.highlightedColor = buttonModel.NormalColor;
            buttonColors.selectedColor = buttonModel.NormalColor;
            buttonColors.pressedColor = buttonModel.ActiveColor;
            button.colors = buttonColors;
            
            return buttonGameObject.AddComponent<ButtonProxy>();
        }
        
        public Transform CreateCanvas(GameObject gameObject)
        {
            var canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            var canvasScaler = gameObject.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = _mobileInputData.ReferenceResolution;

            gameObject.AddComponent<GraphicRaycaster>();

            return canvas.transform;
        }
        
        public bool IsBlocked(Vector2 touchPosition)
        {
            var blocked = false;
            foreach (var buttonModel in _mobileInputData.Buttons)
            {
                if (!buttonModel.BlockTouch)
                {
                    continue;
                }
                
                var buttonPosition = new Vector2(
                    Screen.width * buttonModel.Position.x,
                    Screen.height * buttonModel.Position.y);
                
                switch (buttonModel.ButtonType)
                {
                    case ButtonType.Rectangle:
                        // TODO
                        break;
                    case ButtonType.Circle:
                        var distance = Vector2.Distance(buttonPosition, touchPosition);
                        if (distance > CanvasToScreenSize(buttonModel.Size).x / 2f)
                        {
                            continue;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                blocked = true;
                break;
            }

            return blocked;
        }

        public Stick CreateStick(Transform parent)
        {
            var background = Create("[STICK_BACKGROUND]", _mobileInputData.Stick.BackgroundSize, 
                _mobileInputData.Stick.Background, _mobileInputData.Stick.BackgroundColor);
            var handle = Create("[STICK_HANDLE]", _mobileInputData.Stick.HandleSize, 
                _mobileInputData.Stick.Handle, _mobileInputData.Stick.HandleColor);
            return new Stick(background, handle);

            RectTransform Create(string name, Vector2 size, Sprite sprite, Color color)
            {
                GetAnchors(Alignment.MiddleLeft, out var min, out var max);
                
                var gameObject = new GameObject(name);
                var rectTransform = gameObject.AddComponent<RectTransform>();
                rectTransform.SetParent(parent);
                rectTransform.anchorMin = min;
                rectTransform.anchorMax = max;
                rectTransform.anchoredPosition = new Vector2(
                    _mobileInputData.ReferenceResolution.x * 0.15f, 
                    _mobileInputData.ReferenceResolution.x * -0.1f);
                rectTransform.sizeDelta = size;
                rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                rectTransform.anchorMax = new Vector2(0.5f, 0.5f);

                var image = gameObject.AddComponent<Image>();
                image.sprite = sprite;
                image.color = color;

                return rectTransform;
            }
        }

        private Vector2 CanvasToScreenSize(Vector2 canvasSize)
        {
            return canvasSize / _mobileInputData.ReferenceResolution * new Vector2(Screen.width, Screen.height);
        }

        private void GetAnchors(Alignment alignment, out Vector2 min, out Vector2 max)
        {
            switch (alignment)
            {
                case Alignment.TopLeft:
                    min = new Vector2(0f, 1f);
                    max = new Vector2(0f, 1f);
                    return;
                case Alignment.TopCenter:
                    min = new Vector2(0.5f, 1f);
                    max = new Vector2(0.5f, 1f);
                    return;
                case Alignment.TopRight:
                    min = new Vector2(1f, 1f);
                    max = new Vector2(1f, 1f);
                    return;
                case Alignment.MiddleLeft:
                    min = new Vector2(0f, 0.5f);
                    max = new Vector2(0f, 0.5f);
                    return;
                case Alignment.MiddleCenter:
                    min = new Vector2(0.5f, 0.5f);
                    max = new Vector2(0.5f, 0.5f);
                    return;
                case Alignment.MiddleRight:
                    min = new Vector2(1f, 0.5f);
                    max = new Vector2(1f, 0.5f);
                    return;
                case Alignment.BottomLeft:
                    min = new Vector2(0f, 0f);
                    max = new Vector2(0f, 0f);
                    return;
                case Alignment.BottomCenter:
                    min = new Vector2(0.5f, 0f);
                    max = new Vector2(0.5f, 0f);
                    return;
                case Alignment.BottomRight:
                    min = new Vector2(1f, 0f);
                    max = new Vector2(1f, 0f);
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null);
            }
        }
    }
}