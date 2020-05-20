using UnityEditor;
using UnityEngine;
public class PasteBake : MonoBehaviour
{
    public MeshRenderer source;
    private void OnValidate()
    {
        if (source)
        {
            GetComponent<MeshRenderer>().lightmapIndex = source.lightmapIndex;
            GetComponent<MeshRenderer>().lightmapScaleOffset = source.lightmapScaleOffset;
        }
    }

    [MenuItem("工具/复制烘焙模型 %#d")]
    static void PasteCenter()
    {
        GameObject[] seleObj = Selection.gameObjects;
        for (int i = 0; i < seleObj.Length; i++)
        {
            GameObject newObj = Instantiate(seleObj[i], seleObj[i].transform.position, seleObj[i].transform.rotation, seleObj[i].transform.parent);
            Master(seleObj[i], newObj);
        }
    }

    static void IsMesh(GameObject _old, GameObject _new)
    {
        _new.GetComponent<MeshRenderer>().lightmapIndex = _old.GetComponent<MeshRenderer>().lightmapIndex;
        _new.GetComponent<MeshRenderer>().lightmapScaleOffset = _old.GetComponent<MeshRenderer>().lightmapScaleOffset;
        _new.AddComponent<PasteBake>();
        _new.GetComponent<PasteBake>().source = _old.GetComponent<MeshRenderer>();
        _new.GetComponent<PasteBake>().hideFlags = HideFlags.NotEditable;
        _new.GetComponent<MeshFilter>().hideFlags = HideFlags.NotEditable;
        _new.GetComponent<MeshRenderer>().hideFlags = HideFlags.NotEditable;
    }

    static void Master(GameObject _old, GameObject _new)
    {
        if ((GameObjectUtility.GetStaticEditorFlags(_old) & StaticEditorFlags.LightmapStatic) != 0)
        {
            GameObjectUtility.SetStaticEditorFlags(_new, GameObjectUtility.GetStaticEditorFlags(_old) ^ StaticEditorFlags.LightmapStatic);
        }
        if (_old.GetComponent<MeshRenderer>() != null)
        {
            IsMesh(_old, _new);
        }
        if (_old.transform.childCount > 0)
        {
            for (int n = 0; n < _old.transform.childCount; n++)
            {
                Master(_old.transform.GetChild(n).gameObject, _new.transform.GetChild(n).gameObject);
            }
        }
    }
}

            