using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
public class BatchArray : EditorWindow
{
    [MenuItem("工具/批量阵列")]
    static void ShowWindow()
    {
        GetWindow<BatchArray>("批量阵列");
    }
    GameObject one;
    int number1 = 0;
    int number2 = 0;
    int number3 = 0;
    float interval1 = 1;
    float interval2 = 1;
    float interval3 = 1;
    void OnGUI()
    {
        EditorGUILayout.LabelField("X:");
        EditorGUILayout.BeginHorizontal();
        number1 = EditorGUILayout.IntField("数量:",number1);
        if (number1 < 0)
            number1 = 0;
        interval1 = EditorGUILayout.FloatField("间隔:",interval1);
        if (interval1 < 1)
            interval1 = 1;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Z:");
        EditorGUILayout.BeginHorizontal();
        number2 = EditorGUILayout.IntField("数量:", number2);
        if (number2 < 0)
            number2 = 0;
        interval2 = EditorGUILayout.FloatField("间隔:", interval2);
        if (interval2 == 0)
            interval2 = 1;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Y:");
        EditorGUILayout.BeginHorizontal();
        number3 = EditorGUILayout.IntField("数量:", number3);
        if (number3 < 0)
            number3 = 0;
        interval3 = EditorGUILayout.FloatField("间隔:", interval3);
        if (interval3 < 1)
            interval3 = 1;
        EditorGUILayout.EndHorizontal();

        GUILayout.FlexibleSpace();
        GUIStyle doReName = new GUIStyle(GUI.skin.button)
        {
            fontSize = 20
        };
        if (GUILayout.Button("阵列", doReName))
        {
            if (Selection.gameObjects.Length == 1)
            {
                one = Selection.gameObjects[0];
                for (int k = 0; k < number3 || k==0; k++)
                    for (int j = 0; j < number2 || j == 0; j++)
                        for (int i = 0; i < number1 || i == 0; i++)
                            if (i != 0 || k != 0 || j != 0)
                                Instantiate(one, new Vector3(one.transform.position.x + interval1 * i, one.transform.position.y + interval3 * k, one.transform.position.z + interval2 * j), one.transform.rotation, one.transform.parent);
            }
            else if(Selection.gameObjects.Length>1)
                Debug.Log("请选择一个物体");
            else
                Debug.Log("没有选择物体");
        }
        EditorGUILayout.Separator();
    }
}
