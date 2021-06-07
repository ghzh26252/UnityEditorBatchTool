using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
public class PlayerSettingAndBuild : EditorWindow
{
    [MenuItem("工具/多场景一键发布")]
    static void ShowWindow()
    {
        scenesNumber = EditorBuildSettings.scenes.Length;
        for (int i = 0; i < scenesNumber; i++)
        {
            scenesName.Add(EditorBuildSettings.scenes[i].path.Split('/', '.')[EditorBuildSettings.scenes[i].path.Split('/', '.').Length - 2]);
            //frountBuilds[k].scenesBool[i] = GUILayout.Toggle(frountBuilds[k].scenesBool[i], EditorBuildSettings.scenes[i].path.Split('/', '.')[EditorBuildSettings.scenes[i].path.Split('/', '.').Length - 2]);
        }
        GetWindow<PlayerSettingAndBuild>("多场景一键发布");
    }
    static List<string> scenesName = new List<string>();
    static int scenesNumber;
    static List<FrountBuild> frountBuilds = new List<FrountBuild>();
    static List<BackBuild> backBuilds = new List<BackBuild>();
    static string buildPath = "E:/Desktop";
    static Vector2 vector2;
    class FrountBuild
    {
        public string name;
        public List<bool> scenesBool = new List<bool>();
        public FrountBuild()
        {
            for (int i = 0; i < scenesNumber; i++)
            {
                scenesBool.Add(false);
            }
        }
    }
    class BackBuild
    {
        public string name;
        public List<string> scenesGUID = new List<string>();
    }
    static List<FrountBuild> Back2Frount(List<BackBuild> backBuildsT)
    {
        List<FrountBuild> fbs = new List<FrountBuild>();
        foreach(BackBuild backBuildT in backBuildsT)
        {
            FrountBuild fb = new FrountBuild();
            fb.name = backBuildT.name;
            for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                if (backBuildT.scenesGUID.Contains(EditorBuildSettings.scenes[i].guid.ToString()))
                    fb.scenesBool[i] = true;
            }
            fbs.Add(fb);
        }
        return fbs;
    }
    static List<BackBuild> Frount2Back(List<FrountBuild> frountBuildsT)
    {
        List<BackBuild> bbs = new List<BackBuild>();
        foreach (FrountBuild frountBuildT in frountBuildsT)
        {
            BackBuild bb = new BackBuild();
            bb.name = frountBuildT.name;
            for (int i=0;i< frountBuildT.scenesBool.Count;i++)
            {
                if (frountBuildT.scenesBool[i])
                    bb.scenesGUID.Add(EditorBuildSettings.scenes[i].guid.ToString());
            }
            bbs.Add(bb);
        }
        return bbs;
    }

    void OnGUI()
    {
        vector2 = GUILayout.BeginScrollView(vector2);
        for (int k = 0;k < frountBuilds.Count; k++)
        {
            GUILayout.BeginHorizontal();
            frountBuilds[k].name = EditorGUILayout.TextField("发布名称", frountBuilds[k].name);
            GUILayout.BeginVertical();
            for(int i = 0; i< scenesNumber; i++)
            {
                //frountBuilds[k].scenesBool[i] = GUILayout.Toggle(frountBuilds[k].scenesBool[i], EditorBuildSettings.scenes[i].path.Split('/', '.')[EditorBuildSettings.scenes[i].path.Split('/','.').Length-2]);
                frountBuilds[k].scenesBool[i] = GUILayout.Toggle(frountBuilds[k].scenesBool[i], scenesName[i]);
            }
            GUILayout.EndVertical();
            if (GUILayout.Button("-", GUILayout.Width(30)))
            {
                frountBuilds.RemoveAt(k);
            }
            GUILayout.EndHorizontal();
            GUILayout.Label("--------------------------------------------------------------------------------------------------------------------------------");
        }
        if (GUILayout.Button("+"))
        {
            frountBuilds.Add(new FrountBuild());
        }
        GUILayout.EndScrollView();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("清空关卡"))
        {
            frountBuilds.Clear();
        }
        if (GUILayout.Button("保存配置方案"))
        {
            if (frountBuilds.Count <= 0)
            {
                Debug.Log("请添加发布场景");
                return;
            }
            backBuilds = Frount2Back(frountBuilds);
            List<string> builds2Json = new List<string>();
            foreach (BackBuild backBuildT in backBuilds)
            {
                builds2Json.Add(JsonUtility.ToJson(backBuildT));
            }
            System.IO.File.WriteAllLines(Application.dataPath + "/PlayerSettingAndBuild.txt", builds2Json);
        }
        if (GUILayout.Button("载入配置方案"))
        {
            if (System.IO.File.Exists(Application.dataPath + "/PlayerSettingAndBuild.txt"))
            {
                string[] Json2builds = System.IO.File.ReadAllLines(Application.dataPath + "/PlayerSettingAndBuild.txt");
                backBuilds.Clear();
                foreach (string buildsJson in Json2builds)
                {
                    backBuilds.Add(JsonUtility.FromJson<BackBuild>(buildsJson));
                }
                frountBuilds = Back2Frount(backBuilds);
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
            if (frountBuilds.Count<=0)
            {
                Debug.Log("请添加发布场景");
                return;
            }
            foreach(FrountBuild oneBuild in frountBuilds)
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
            foreach (FrountBuild frountBuildT in frountBuilds)
            {
                List<string> editorBuildSettingsScenes = new List<string>();
                for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
                {
                    if (frountBuildT.scenesBool[i])
                    {
                        editorBuildSettingsScenes.Add(EditorBuildSettings.scenes[i].path);
                        Debug.Log(EditorBuildSettings.scenes[i].path);
                    }
                }
                PlayerSettings.productName = frountBuildT.name;
                BuildPipeline.BuildPlayer(editorBuildSettingsScenes.ToArray(), buildPath + "/" + frountBuildT.name  + "/" + frountBuildT.name + ".exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
            }
        }
        GUILayout.Space(5);
    }
    
}