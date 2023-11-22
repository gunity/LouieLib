using System.IO;
using UnityEditor;
using UnityEngine;

namespace LouieLib.Editor
{
    public static class EditorHelper
    {
        public static string RootNamespace
        {
            get => EditorSettings.projectGenerationRootNamespace;
            set => EditorSettings.projectGenerationRootNamespace = value;
        }
        
        public static void CreateScript(TextAsset template, string path, string name)
        {
            var scriptText = template.text;
            scriptText = scriptText.Replace("#SCRIPT_NAME#", name);
            scriptText = scriptText.Replace("#NAMESPACE#", GetNamespace(path));
        
            File.WriteAllText($"{path}/{name}.cs", scriptText);
        }

        public static void CreateEmptyFolder(string path)
        {
            if (Directory.Exists(path))
            {
                return;
            }
            
            Directory.CreateDirectory(path);
            File.Create($"{path}/.gitkeep");
        }

        private static string GetNamespace(string path)
        {
            var result = RootNamespace;
            var pathsSplit = path.Split('/');
            
            foreach (var folderName in pathsSplit)
            {
                if (string.IsNullOrEmpty(folderName))
                {
                    continue;
                }

                if (folderName is "Assets" or "Scripts")
                {
                    continue;
                }

                result += $".{folderName}";
            }

            return result;
        }
    }
}