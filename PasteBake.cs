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
        GameObject seleObj = Selection.activeGameObject;
        GameObject newObj = Instantiate(seleObj, seleObj.transform.position, seleObj.transform.rotation, seleObj.transform.parent);
        Master(seleObj, newObj);
    }

    static void IsMesh(GameObject _old, GameObject _new)
    {
        _new.GetComponent<MeshRenderer>().lightmapIndex = _old.GetComponent<MeshRenderer>().lightmapIndex;
        _new.GetComponent<MeshRenderer>().lightmapScaleOffset = _old.GetComponent<MeshRenderer>().lightmapScaleOffset;
        PasteBake n = _new.AddComponent<PasteBake>();
        n.source = _old.GetComponent<MeshRenderer>();
    }

    static void Master(GameObject _old, GameObject _new)
    {
        if (_old.GetComponent<MeshRenderer>())
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

            