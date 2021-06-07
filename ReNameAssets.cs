using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
public class ReNameAssets : EditorWindow
{
    [MenuItem("工具/选择资源重命名")]
    static void ShowWindow()
    {
        GetWindow<ReNameAssets>("选择资源重命名");
    }

    string[] ReNameType = { "替换", "删除", "增加", "序列" };
    int myType;

    string _old = "";
    string _new = "";

    string[] deChoice1 = { "前", "后", "从正数第", "从倒数第" };
    string[] deChoice2 = { "到正数第", "到倒数第" };
    int deType1;
    int deType2;
    int deNumber1 = 1;
    int deNumber2 = 2;

    string[] inChoice1 = { "前端", "后端", "中间" };
    string[] inChoice2 = { "正数第", "倒数第" };
    int inType1;
    int inType2;
    int inNumber;
    string inSert = "";

    int startNumber = 1;
    int add = 1;
    bool fixedDigit = true;
    int digit = 3;
    string addString;

    void OnGUI()
    {
        GUIStyle doReName = new GUIStyle(GUI.skin.button)
        {
            fontSize = 20
        };
        myType = GUILayout.SelectionGrid(myType, ReNameType, ReNameType.Length);
        switch (myType)
        {
            case 0:
                _old = EditorGUILayout.TextField("查找：", _old);
                _new = EditorGUILayout.TextField("替换：",_new);
                break;
            case 1:
                GUILayout.BeginHorizontal();
                deType1 = EditorGUILayout.Popup(deType1, deChoice1);
                deNumber1 = EditorGUILayout.IntField(deNumber1);
                GUILayout.Label("位");
                GUILayout.EndHorizontal();
                if (deType1 >= 2)
                {
                    GUILayout.BeginHorizontal();
                    deType2 = EditorGUILayout.Popup(deType2, deChoice2);
                    deNumber2 = EditorGUILayout.IntField(deNumber2);
                    GUILayout.Label("位");
                    GUILayout.EndHorizontal();
                }
                if (deNumber1 == 0)
                    deNumber1 = 1;
                if (deNumber2 == 0)
                    deNumber2 = 1;
                if (deType1 == 2 && deType2 == 0 && deNumber2 <= deNumber1)
                {
                    deNumber2 = deNumber1 + 1;
                }
                if (deType1 == 3)
                {
                    deType2 = 1;
                    if (deNumber1 < 2)
                        deNumber1 = 2;
                    if (deNumber2 >= deNumber1)
                        deNumber2 = deNumber1 - 1;
                }
                break;
            case 2:
                inType1 = GUILayout.SelectionGrid(inType1, inChoice1, inChoice1.Length);
                if (inType1 == 2)
                {
                    GUILayout.BeginHorizontal();
                    inType2 = EditorGUILayout.Popup(inType2, inChoice2);
                    inNumber = EditorGUILayout.IntField(inNumber);
                    if (inNumber == 0)
                        inNumber = 1;
                    if (inType2 == 0)
                        GUILayout.Label("后");
                    else
                        GUILayout.Label("前");
                    GUILayout.EndHorizontal();
                }
                inSert = EditorGUILayout.TextField("增加内容：",inSert);
                break;
            case 3:
                startNumber = EditorGUILayout.IntField("起始序号：",startNumber);
                add = EditorGUILayout.IntField("序号间隔：",add);
                if (add == 0)
                    add = 1;
                GUILayout.BeginHorizontal();
                fixedDigit = GUILayout.Toggle(fixedDigit, "最低序号位数（不足位用0补齐）");
                if (fixedDigit)
                    digit = EditorGUILayout.IntField(digit);
                GUILayout.EndHorizontal();
                break;
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("命名", doReName))
        {
            Master(Selection.GetFiltered<Object>(SelectionMode.Assets));
        }
        GUILayout.Space(5);
    }
    void Master(Object[] myObjects)
    {
        for (int i = 0; i < myObjects.Length; i++)
        {
            string path = AssetDatabase.GetAssetPath(myObjects[i]);
            string nameOld = path.Split('/')[path.Split('/').Length - 1];
            string nameNew = null;
            switch (myType)
            {
                case 0:
                    nameNew = TiHuan(nameOld);
                    break;
                case 1:
                    nameNew = ShanChu(nameOld);
                    break;
                case 2:
                    nameNew = ZengJia(nameOld);
                    break;
                case 3:
                    nameNew = XuLie(nameOld);
                    break;
            }
            AssetDatabase.RenameAsset(path, nameNew);
        }
    }
    string TiHuan(string nameOld)
    {
        return nameOld.Replace(_old, _new);
    }
    string ShanChu(string nameOld)
    {
        if (nameOld.Length <= Mathf.Abs(deNumber1 - deNumber2) + 1)
            Debug.Log("物体：" + nameOld + "名称长度不足，未执行删除操作。");
        else
        {
            if (deType1 == 0)
                return nameOld.Remove(0, deNumber1);
            if (deType1 == 1)
                return nameOld.Remove(nameOld.Length - deNumber1, deNumber1);
            if (deType1 == 2 && deType2 == 0)
                return nameOld.Remove(deNumber1 - 1, deNumber2 - deNumber1 + 1);
            if (deType1 == 2 && deType2 == 1)
                return nameOld.Remove(deNumber1 - 1, nameOld.Length - deNumber2 - deNumber1 + 2);
            if (deType1 == 3 && deType2 == 1)
                return nameOld.Remove(nameOld.Length - deNumber1, deNumber1 - deNumber2 + 1);
        }
        return nameOld;
    }
    string ZengJia(string nameOld)
    {
        if (inType1 == 0)
            return nameOld.Insert(0, inSert);
        if (inType1 == 1)
            return nameOld.Insert(nameOld.Length, inSert);
        if (inType1 == 2)
        {
            if (inType2 == 0)
                return nameOld.Insert(inNumber, inSert);
            else
                return nameOld.Insert(nameOld.Length - inNumber, inSert);
        }
        return nameOld;
    }
    string XuLie(string nameOld)
    {
        addString = startNumber.ToString();
        if (fixedDigit)
        {
            while (addString.Length < digit)
                addString = "0" + addString;
        }
        startNumber += add;
        return nameOld + addString;
    }
}