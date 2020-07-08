using System;
using UnityEditor;
using UnityEngine;
public class SmartBoxCollider : MonoBehaviour
{
    static GameObject parent;
    [MenuItem("工具/适配子物体BoxCollider")]
    static void PasteCenter()
    {
        Master(parent = Selection.activeGameObject);
        BoxCollider collider = parent.AddComponent<BoxCollider>();
        collider.center = new Vector3((maxX + minX) / 2, (maxY + minY) / 2, (maxZ + minZ) / 2);
        collider.size = new Vector3(maxX - minX, maxY - minY, maxZ - minZ);
    }
    static void Master(GameObject obj)
    {
        if (obj.GetComponent<MeshRenderer>())
        {
            IsMesh(obj);
        }
        if (obj.transform.childCount > 0)
        {
            for (int n = 0; n < obj.transform.childCount; n++)
            {
                Master(obj.transform.GetChild(n).gameObject);
            }
        }
    }
    static void IsMesh(GameObject obj)
    {
        foreach(Vector3 point in obj.GetComponent<MeshFilter>().sharedMesh.vertices)
        {
            Compare(parent.transform.InverseTransformPoint(obj.transform.TransformPoint(point)));
        }
    }
    static float maxX  = float.MinValue;
    static float maxY = float.MinValue;
    static float maxZ = float.MinValue;
    static float minX = float.MaxValue;
    static float minY = float.MaxValue;
    static float minZ = float.MaxValue;
    static void Compare(Vector3 point)
    {
        maxX = point.x > maxX ? point.x : maxX;
        maxY = point.y > maxY ? point.y : maxY;
        maxZ = point.z > maxZ ? point.z : maxZ;
        minX = point.x < minX ? point.x : minX;
        minY = point.y < minY ? point.y : minY;
        minZ = point.z < minZ ? point.z : minZ;
    }


}

            