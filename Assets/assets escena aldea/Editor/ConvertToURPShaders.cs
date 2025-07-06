using UnityEngine;
using UnityEditor;

public class ConvertToURPShaders
{
    [MenuItem("Tools/Convert All Materials to URP Lit Shader")]
    static void ConvertMaterials()
    {
        string[] guids = AssetDatabase.FindAssets("t:Material");
        Shader urpLit = Shader.Find("Universal Render Pipeline/Lit");

        if (urpLit == null)
        {
            Debug.LogError("URP Lit shader not found. Is URP installed in this project?");
            return;
        }

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (mat.shader.name == "Standard" || mat.shader.name.StartsWith("Legacy Shaders"))
            {
                mat.shader = urpLit;
                Debug.Log($"Converted {mat.name} to URP/Lit");
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
