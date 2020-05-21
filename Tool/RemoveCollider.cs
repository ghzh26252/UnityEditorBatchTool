using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
public class RemoveCollider : EditorWindow
{
    static List<GameObject> myList = new List<GameObject>();

    [MenuItem("工具/移除所有碰撞(包含子物体)")]


    static void DoRemoveCollider()
    {
        Master(Selection.GetFiltered<GameObject>(SelectionMode.Deep));
    }
    static void Master(GameObject[] gameObjects)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.GetComponents<Collider>().Length>0)
                foreach(Collider colloder in gameObject.GetComponents<Collider>())
                    DestroyImmediate(colloder);
        }
    }
}