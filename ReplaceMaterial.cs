using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
public class ReplaceMaterial : EditorWindow
{
    [MenuItem("工具/选择物体替换材质")]
    static void ShowWindow()
    {
        GetWindow<ReplaceMaterial>("选择物体替换材质");
    }

    Material _old = null;
    Material _new = null;
    bool includeChilds = true;
    List<GameObject> myList = new List<GameObject>();
    void OnGUI()
    {
        GUIStyle doReName = new GUIStyle(GUI.skin.button)
        {
            fontSize = 20
        };
        _old = (Material)EditorGUILayout.ObjectField("原始材质：", _old,typeof(Material),true);
        _new = (Material)EditorGUILayout.ObjectField("替换材质：", _new, typeof(Material), true);
        GUILayout.FlexibleSpace();
        includeChilds = GUILayout.Toggle(includeChilds, "包含子物体");
        if (GUILayout.Button("替换", doReName))
        {
            Master(Selection.gameObjects);
        }
        GUILayout.Space(5);
    }
    void Master(GameObject[] mygameObjects)
    {
        foreach (GameObject gameObject in mygameObjects)
        {
            for (int i=0;i<gameObject.GetComponent<MeshRenderer>().sharedMaterials.Length;i++)
            {
                if (gameObject.GetComponent<MeshRenderer>().sharedMaterials[i] == _old)
                {
                    Material[] temp = new Material[gameObject.GetComponent<MeshRenderer>().sharedMaterials.Length];
                    temp = gameObject.GetComponent<MeshRenderer>().sharedMaterials;
                    temp[i] = _new;
                    gameObject.GetComponent<MeshRenderer>().sharedMaterials = temp;
                }
            }
            if (includeChilds && gameObject.transform.childCount > 0)
            {
                myList.Clear();
                for (int n = 0; n < gameObject.transform.childCount; n++)
                {
                    myList.Add(gameObject.transform.GetChild(n).gameObject);
                }
                Master(myList.ToArray());
            }
        }
    }
}