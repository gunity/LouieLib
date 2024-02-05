using System;
using UnityEngine;

namespace LouieLib.Input.Models
{
    [Serializable]
    public class ButtonModel
    {
        public Sprite Sprite => _sprite;
        public ButtonType ButtonType => _buttonType;
        public Color NormalColor => _normalColor;
        public Color ActiveColor => _activeColor;
        public string Action => _action;
        public bool BlockTouch => _blockTouch;
        public Alignment Alignment => _alignment;
        public Vector2 Position => _position;
        public Vector2 Size => _size;
        
        [SerializeField] private Sprite _sprite;
        [SerializeField] private ButtonType _buttonType;
        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _activeColor;
        [SerializeField] private string _action;
        [SerializeField] private bool _blockTouch;
        [SerializeField] private Alignment _alignment;
        [SerializeField] private Vector2 _position;
        [SerializeField] private Vector2 _size;
    }
}