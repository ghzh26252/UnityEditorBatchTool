using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
public class ExportStatic : Editor
{
    [MenuItem("工具/导出静态物体名列表")]
    static void Go()
    {
        string myPath;
        GameObject[] gameObjects;
        myPath = EditorUtility.SaveFilePanel("选择保存位置", "D:/", "StaticList", "txt");
        if (myPath == "")
        {
            Debug.Log("未选择路径！");
        }
        else
        {
            Debug.Log("开始导出静态物体名列表。");
            gameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
            List<string> statciList = new List<string>();
            for (int i = 0; i < gameObjects.Length; i++)
            {
                if (gameObjects[i].isStatic)
                {
                    statciList.Add(gameObjects[i].name);
                }
            }
            File.WriteAllLines(myPath, statciList.ToArray());
            Debug.Log("导出完成！");
        }
    }
}