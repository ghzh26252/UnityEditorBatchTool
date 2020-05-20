using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
public class RemoveCollider : EditorWindow
{
    static List<GameObject> myList = new List<GameObject>();

    [MenuItem("工具/移除所有碰撞(包含子物体)")]


    static void DoRemoveCollider()
    {
        Master(Selection.gameObjects);
    }
    static void Master(GameObject[] mygameObjects)
    {
        foreach (GameObject gameObject in mygameObjects)
        {
            if (gameObject.GetComponents<Collider>().Length>0)
                foreach(Collider colloder in gameObject.GetComponents<Collider>())
                    DestroyImmediate(colloder);
            if (gameObject.transform.childCount > 0)
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