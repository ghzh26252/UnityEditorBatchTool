using UnityEditor;
using UnityEngine;

public class EasyCreate
{
    [MenuItem("工具/创建材质球 &m")]
    static void CreateMaterial()
    {
        string path;
        string name = "New Material.mat";
        Object[] a = Selection.GetFiltered<Object>(SelectionMode.Assets);
        if (a.Length > 1 || a.Length == 0) return;
        path = AssetDatabase.GetAssetPath(a[0]);
        if (a[0].GetType() != typeof(DefaultAsset))
        {
            path = path.Remove(path.LastIndexOf('/'));
        }
        AssetDatabase.CreateAsset(new Material(Shader.Find("Universal Render Pipeline/Lit")), path + "/" + name);
    }
}
