using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
public class BatchSetSingleRotation : EditorWindow
{
    [MenuItem("工具/批量设置单轴Rotaion")]
    static void ShowWindow()
    {
        GetWindow<BatchSetSingleRotation>("批量设置单轴Rotaion");
    }

    bool xB = false;
    float xF = 0;
    bool yB = false;
    float yF = 0;
    bool zB = false;
    float zF = 0;

    void OnGUI()
    {
        GUIStyle doReName = new GUIStyle(GUI.skin.button)
        {
            fontSize = 20
        };
        EditorGUILayout.BeginHorizontal();
        xB = GUILayout.Toggle(xB, "X");
        xF = EditorGUILayout.FloatField(xF);
        yB = GUILayout.Toggle(yB, "Y");
        xF = EditorGUILayout.FloatField(yF);
        zB = GUILayout.Toggle(zB, "Z");
        xF = EditorGUILayout.FloatField(zF);
        EditorGUILayout.EndHorizontal();
        GUILayout.FlexibleSpace();
        
        if (GUILayout.Button("设置", doReName))
        {
            Master(Selection.gameObjects);
        }
        GUILayout.Space(5);
    }
    void Master(GameObject[] mygameObjects)
    {
        foreach (GameObject gameObject in mygameObjects)
        {
            gameObject.transform.localEulerAngles = new Vector3(xB ? xF : gameObject.transform.localEulerAngles.x, yB ? yF : gameObject.transform.localEulerAngles.y, zB ? zF : gameObject.transform.localEulerAngles.z);
        }
    }
}