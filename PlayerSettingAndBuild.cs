using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
public class PlayerSettingAndBuild : EditorWindow
{
    [MenuItem("工具/多场景一键发布")]
    static void ShowWindow()
    {
        GetWindow<PlayerSettingAndBuild>("多场景一键发布");
    }

    List<OneBuild> builds = new List<OneBuild>();
    string buildPath = "E:/Desktop";
    Vector2 vector2;
    class OneBuild
    {
        public string name;
        public List<bool> scenesBool;
        public OneBuild()
        {
            name = "";
            scenesBool = new List<bool>();
            for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                scenesBool.Add(false);
            }
        }
    }

    void OnGUI()
    {
        vector2 = GUILayout.BeginScrollView(vector2);
        for (int k = 0;k < builds.Count; k++)
        {
            GUILayout.BeginHorizontal();
            builds[k].name = EditorGUILayout.TextField("发布名称", builds[k].name);
            GUILayout.BeginVertical();
            for(int i = 0; i<EditorBuildSettings.scenes.Length; i++)
            {
                builds[k].scenesBool[i] = GUILayout.Toggle(builds[k].scenesBool[i], EditorBuildSettings.scenes[i].path);
            }
            GUILayout.EndVertical();
            if (GUILayout.Button("-", GUILayout.Width(30)))
            {
                builds.RemoveAt(k);
            }
            GUILayout.EndHorizontal();
            GUILayout.Label("----------------");
        }
        if (GUILayout.Button("+"))
        {
            builds.Add(new OneBuild());
        }
        GUILayout.EndScrollView();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("清空关卡"))
        {
            builds.Clear();
        }
            if (GUILayout.Button("保存配置方案"))
        {
            if (builds.Count <= 0)
            {
                Debug.Log("请添加发布场景");
                return;
            }
            List<string> builds2Json = new List<string>();
            foreach (OneBuild onebuild in builds)
            {
                builds2Json.Add(JsonUtility.ToJson(onebuild));
            }
            System.IO.File.WriteAllLines(Application.dataPath + "/PlayerSettingAndBuild.txt", builds2Json);
        }
        if (GUILayout.Button("载入配置方案"))
        {
            if (System.IO.File.Exists(Application.dataPath + "/PlayerSettingAndBuild.txt"))
            {
                string[] Json2builds = System.IO.File.ReadAllLines(Application.dataPath + "/PlayerSettingAndBuild.txt");
                builds.Clear();
                foreach (string buildsJson in Json2builds)
                {
                    builds.Add(JsonUtility.FromJson<OneBuild>(buildsJson));
                }
            }
            else
                Debug.Log("未找到配置方案");
        }
        GUILayout.EndHorizontal();

        GUILayout.FlexibleSpace();

        GUILayout.BeginHorizontal();
        GUILayout.Label("发布路径：" + buildPath);
        if (GUILayout.Button("选择发布路径"))
        {
            buildPath = EditorUtility.SaveFolderPanel("选择发布路径", @"E:\Desktop", "");
        }
        GUILayout.EndHorizontal();
        if (GUILayout.Button("开始发布"))
        {
            if (builds.Count<=0)
            {
                Debug.Log("请添加发布场景");
                return;
            }
            foreach(OneBuild oneBuild in builds)
            {
                if (oneBuild.name == "")
                {
                    Debug.Log("请命名所有发布场景");
                    return;
                }
            }
                if (buildPath == "")
            {
                Debug.Log("请选择发布场景路径");
                return;
            }
            foreach (OneBuild oneBuild in builds)
            {
                List<string> editorBuildSettingsScenes = new List<string>();
                for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
                {
                    if (oneBuild.scenesBool[i])
                    {
                        editorBuildSettingsScenes.Add(EditorBuildSettings.scenes[i].path);
                        Debug.Log(EditorBuildSettings.scenes[i].path);
                    }
                }
                PlayerSettings.productName = oneBuild.name;
                //if (!System.IO.Directory.Exists(buildPath + "/" + oneBuild.name))
                   // System.IO.Directory.CreateDirectory(buildPath + "/" + oneBuild.name);
                BuildPipeline.BuildPlayer(editorBuildSettingsScenes.ToArray(), buildPath + "/" + oneBuild.name  + "/" + oneBuild.name + ".exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
            }
        }
        GUILayout.Space(5);
    }
    
}