using System.Collections.Generic;
using LouieLib.Input.Models;
using UnityEngine;

namespace LouieLib.Input.Data
{
    public interface IMobileInputData
    {
        Vector2 ReferenceResolution { get; }
        float LookSensitivity { get; }
        List<ButtonModel> Buttons { get; }
        StickModel Stick { get; }
    }

    [CreateAssetMenu(fileName = "MobileInputData", menuName = "LouieLib Data/Mobile Input", order = 0)]
    public class MobileInputData : ScriptableObject, IMobileInputData
    {
        public Vector2 ReferenceResolution => _referenceResolution;
        public float LookSensitivity => _lookSensitivity;
        public List<ButtonModel> Buttons => _buttons;
        public StickModel Stick => _stick;

        [SerializeField] private Vector2 _referenceResolution;
        [SerializeField] private float _lookSensitivity;
        [SerializeField] private List<ButtonModel> _buttons;
        [SerializeField] private StickModel _stick;
    }
}