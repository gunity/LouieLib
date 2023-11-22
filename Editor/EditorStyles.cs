using UnityEngine;

namespace LouieLib.Editor
{
    public static class EditorStyles
    {
        public static readonly GUIStyle GoButton;

        static EditorStyles()
        {
            GoButton = new GUIStyle("Button")
            {
                fixedHeight = 35,
                fontStyle = FontStyle.Bold
            };
        }
    }
}