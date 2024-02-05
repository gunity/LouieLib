using System;
using UnityEngine;

namespace LouieLib.Input.Models
{
    [Serializable]
    public class StickModel
    {
        public Sprite Background => _background;
        public Vector2 BackgroundSize => _backgroundSize;
        public Color BackgroundColor => _backgroundColor;
        public Sprite Handle => _handle;
        public Vector2 HandleSize => _handleSize;
        public Color HandleColor => _handleColor;

        [SerializeField] private Sprite _background;
        [SerializeField] private Vector2 _backgroundSize;
        [SerializeField] private Color _backgroundColor;
        [SerializeField] private Sprite _handle;
        [SerializeField] private Vector2 _handleSize;
        [SerializeField] private Color _handleColor;
    }
}