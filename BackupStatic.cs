using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BackupStatic : EditorWindow
{
    [MenuItem("工具/备份还原静态")]
    static void Start()
    {
        GetWindow<BackupStatic>("备份还原静态");
    }
    void OnGUI()
    {
        string path = Application.dataPath + "/StaticInsList.txt";
        if (GUILayout.Button("备份", GUILayout.Height(30)))
        {
            GameObject[] gameObjects = FindObjectsOfType<GameObject>();
            List<string> statciList = new List<string>();
            foreach (GameObject gameObject in gameObjects)
            {
                if (GameObjectUtility.GetStaticEditorFlags(gameObject) != 0)
                {
                    statciList.Add(gameObject.GetInstanceID().ToString() + "," + (int)GameObjectUtility.GetStaticEditorFlags(gameObject));
                }
            }
            System.IO.File.WriteAllLines(path, statciList.ToArray());
            Debug.Log("备份完成！");
        }
        if (GUILayout.Button("还原", GUILayout.Height(30)))
        {
            if (System.IO.File.Exists(path))
                foreach (string ins in System.IO.File.ReadAllLines(path))
                {
                    try
                    {
                        GameObjectUtility.SetStaticEditorFlags((GameObject)EditorUtility.InstanceIDToObject(int.Parse(ins.Split(',')[0])), (StaticEditorFlags)int.Parse(ins.Split(',')[1]));
                    }
                    catch
                    {
                        Debug.LogError("未找到物体ID：" + ins);
                    }
                }
            Debug.Log("还原完成！");
        }
    }
}
