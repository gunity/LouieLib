using UnityEditor;
using UnityEngine;

namespace LouieLib.Editor
{
    public class CreatorWindow : EditorWindow
    {
        [SerializeField] private TextAsset _starterTemplate;
        
        private string _rootNamespace;
        private string _starterName = "GameStarter";
        private bool _generateScriptFolders = true;

        private static CreatorWindow _window;
        
        [MenuItem("Assets/Create/GUnity/LouieLib Creator")]
        private static void ShowWindow()
        {
            _window = GetWindow<CreatorWindow>();
            _window.minSize = new Vector2(250, 280);
            _window.titleContent = new GUIContent("LouieLib Creator");
            _window.Show();
        }

        private void OnEnable()
        {
            _rootNamespace = Application.productName;
        }

        private void OnGUI()
        {
            Logo();

            _rootNamespace = EditorGUILayout.TextField("Root namespace", _rootNamespace);
            _starterName = EditorGUILayout.TextField("Starter name", _starterName);
            _generateScriptFolders = EditorGUILayout.Toggle("Generate script folders", _generateScriptFolders);
            
            GUILayout.Space(10);
            if (GUILayout.Button("GO", EditorStyles.GoButton))
            {
                GoButton();
            }

            return;

            void Logo()
            {
                var texture = Resources.Load<Texture>("logo");
                GUI.DrawTexture(new Rect(position.width / 2f - 80f, 0f, 160f, 160f), texture);
                EditorGUILayout.Space(160f);
            }
        }

        private void GoButton()
        {
            EditorHelper.RootNamespace = _rootNamespace.Trim();

            if (_generateScriptFolders)
            {
                EditorHelper.CreateEmptyFolder("Assets/Scripts/Systems");
                EditorHelper.CreateEmptyFolder("Assets/Scripts/Proxies");
                EditorHelper.CreateEmptyFolder("Assets/Scripts/Data");
                EditorHelper.CreateEmptyFolder("Assets/Scripts/Starters");
            }

            EditorHelper.CreateScript(_starterTemplate, "Assets/Scripts/Starters", _starterName);
            AssetDatabase.Refresh();
            _window.Close();
        }
    }
}