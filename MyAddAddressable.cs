using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

public class MyAddAddressable : Editor
{
    [MenuItem("工具/添加到Addressable %G")]
    static void Add()
    {

        AddressableAssetSettings setting = AssetDatabase.LoadAssetAtPath<AddressableAssetSettings>("Assets/AddressableAssetsData/AddressableAssetSettings.asset");
        
        foreach (GameObject o in Selection.gameObjects)
        {
            AddressableAssetEntry entry = setting.CreateOrMoveEntry(AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(o)), setting.DefaultGroup);
            string[] s = AssetDatabase.GetAssetPath(o).Split('/');
            entry.SetAddress(s[s.Length-1]);
        }
    }
}
