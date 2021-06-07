using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class ReplaceAssetReference : EditorWindow
{
    [MenuItem("工具/替换资源引用")]
    static void ShowWindow()
    {
        GetWindow<ReplaceAssetReference>("替换资源引用");
    }

    Object _old;
    Object _new;
    Object sence;
    void OnGUI()
    {
        GUIStyle doReName = new GUIStyle(GUI.skin.button);
        doReName.fontSize = 20;
        _old = EditorGUILayout.ObjectField("原始资源：", _old,typeof(Object),false);
        _new =EditorGUILayout.ObjectField("替换资源：", _new, typeof(Object), false);
        sence = EditorGUILayout.ObjectField("场景资源：", sence, typeof(Object), false);
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("替换", doReName))
        {
            string a = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(_old));
            Debug.Log(a);
            string b = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(_new));
            Debug.Log(b);
            string c = AssetDatabase.GetAssetPath(sence);
            Debug.Log(c);
            string[] d = File.ReadAllLines(c);
            List<string> f = new List<string>();
            foreach (string e in d)
            {
                f.Add(e.Replace(a, b));
            }
            File.Delete(c);
            File.WriteAllLines(c, f.ToArray());
            Debug.Log("OK");
        }
        GUILayout.Space(5);
    }
}