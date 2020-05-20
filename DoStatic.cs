using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
public class MyEditor : EditorWindow
{
    [MenuItem("工具/自动设置静态")]//工具栏地址
    static void AutoStatic()
    {
        GetWindow<MyEditor>("自动设置静态");//窗口名
    }
    string myPath;//存储TXT列表文件路径
    Vector2 myV2;//ScrollView位置变量
    string[] myStr;//存储物体名数组
    StringBuilder myLog = new StringBuilder("执行结果：");//存储打印信息更新，因为需要大量更新所以采用StringBuilder类型
    string[] myLabel = { "执行结果：" };//存储打印信息结果，每次大量更新结束后才更新并打印
    void OnGUI()
    {
        //修改字体大小和颜色
        GUI.skin.label.fontSize = 12;
        GUI.skin.button.fontSize = 12;
        GUIStyle rSty = new GUIStyle(GUI.skin.label);
        GUIStyle gSty = new GUIStyle(GUI.skin.label);
        rSty.normal.textColor = Color.red;
        gSty.normal.textColor = new Color(0, 0.6f, 0);
        GUILayout.Space(10);
        //创建选择文件路径按键
        if (GUILayout.Button("选择列表文件", GUILayout.Height(30)))
        {
            myPath = EditorUtility.OpenFilePanel("选择列表文件", "D:/", "txt");
        }
        GUILayout.Space(10);
        //显示选择文件状态
        if (myPath != "")
        {
            GUILayout.Label("当前选择列表文件：" + myPath);
        }
        else
        {
            GUILayout.Label("尚未选择列表文件");
        }
        GUILayout.Space(10);
        //创建执行按键，并在没有选择路径点击时发出警告。
        if (GUILayout.Button("执行", GUILayout.Height(50)))
        {
            if (string.IsNullOrEmpty(myPath))
            {
                AddLog("执行失败：没有选择文件。");
                UpLabel();
            }
            else
            {
                DoStatic();
            }
        }
        GUILayout.Space(30);
        //使用方法Tips
        GUILayout.Label("使用方法：");
        GUILayout.Label("1.制作需要设为静态的物体名列表TXT，每行一个。");
        GUILayout.Label("2.点击“选择列表文件”选择TXT后，点击执行即可。");
        GUILayout.Space(30);
        //开始一个滚动视图用来显示打印信息
        myV2 = GUILayout.BeginScrollView(myV2);
        for (int i = 0; i < myLabel.Length; i++)
        {
            if (myLabel[i].Contains("失败"))
                GUILayout.Label(myLabel[i], rSty);
            else if (myLabel[i].Contains("成功"))
                GUILayout.Label(myLabel[i], gSty);
            else
                GUILayout.Label(myLabel[i]);
        }
        GUILayout.EndScrollView();
    }
    //执行函数
    void DoStatic()
    {
        myLog = new StringBuilder("执行结果：");
        myLabel = new string[] { "执行结果：" };
        AddLog("开始执行。");
        myStr = File.ReadAllLines(myPath);
        for (int i = 0; i < myStr.Length; i++)
        {
            if (GameObject.Find(myStr[i]) == null)
            {
                AddLog("设置失败：场景中未找到此物体--" + myStr[i] + "。");
            }
            else
            {
                GameObject.Find(myStr[i]).isStatic = true;
                AddLog("设置成功：物体--" + myStr[i] + "。");
            }
        }
        AddLog("执行完成。");
        UpLabel();
    }
    //增加打印信息函数
    void AddLog(string label)
    {
        myLog.Append("\nTime:" + System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + ":" + System.DateTime.Now.Second.ToString() + " ----- " + label);
    }
    //更新打印信息函数
    void UpLabel()
    {
        myLabel = myLog.ToString().Split('\n');
        myV2 = new Vector2(0, 99999999);
    }
}