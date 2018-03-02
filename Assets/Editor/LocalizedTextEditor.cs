using UnityEngine;
using UnityEditor;
using System.IO;

/// <summary>
/// 自定义编辑器：
/// </summary>
public class LocalizedTextEditor : EditorWindow
{
    public LocalizationData localizationData;

    //创建一个本地化文本编辑窗口
    [MenuItem("Window/Localized Text Editor")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(LocalizedTextEditor)).Show();
    }

    private void OnGUI()
    {
        if (localizationData != null)
        {
            /*SerializedObject和SerializedProperty是用于以完全通用的方式编辑对象属性的类，可以自动处理预制的撤消和样式UI。

              SerializedObject与SerializedProperty和Editor类一起使用。*/

            //创建一个序列化对象并设置其属性
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("localizationData");


            EditorGUILayout.PropertyField(serializedProperty, true);
            serializedObject.ApplyModifiedProperties();

            //保存数据
            if (GUILayout.Button("Save data"))
            {
                SaveGameData();
            }
        }

        //加载数据
        if (GUILayout.Button("Load data"))
        {
            LoadGameData();
        }

        //创建数据
        if (GUILayout.Button("Create new data"))
        {
            CreateNewData();
        }
    }

    //加载数据
    private void LoadGameData()
    {
        //打开文件浏览窗口
        string filePath = EditorUtility.OpenFilePanel("Select localization data file", Application.streamingAssetsPath, "json");

        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);

            localizationData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
        }
    }

    //保存数据
    private void SaveGameData()
    {
        string filePath = EditorUtility.SaveFilePanel("Save localization data file", Application.streamingAssetsPath, "", "json");

        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = JsonUtility.ToJson(localizationData);
            File.WriteAllText(filePath, dataAsJson);
        }
    }

    //创建数据
    private void CreateNewData()
    {
        localizationData = new LocalizationData();
    }

}