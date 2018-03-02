using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// 本地化管理类
/// </summary>
public class LocalizationManager : MonoBehaviour
{
    //单例
    public static LocalizationManager instance;

    private Dictionary<string, string> localizedText;
    private bool isReady = false;
    string missingTextString = "Localized text not found";

    //避免重复创建
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        SystemLanguage sl = Application.systemLanguage;

    }

    //加载本地化文件
    public void LoadLocalizedText(string fileName)
    {
        //创建对应的键值对
        localizedText = new Dictionary<string, string>();

        //获取文件路径
        string filePath = Path.Combine(Application.streamingAssetsPath,fileName);
        
        //如果文件系统中存在指定路径
        if (File.Exists(filePath))
        {
            //读取文件所有行并且返回一个字符串
            string dataAsJson = File.ReadAllText(filePath);

            /*JSON采用完全独立于编程语言的文本格式来存储和表示数据。简洁和清晰的层次结构使得 JSON 成为理想的数据交换语言。*/
            /*JsonUtility.FromJson<T>(string json)这种方法使用Unity序列化器; 因此您创建的类型T必须由序列化程序支持。它必须是一个用Serializable属性标记的普通类/结构体。且非继承*/

            //将JSON格式的输入转换成LocalizationData下的属性
            LocalizationData loadData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            //使用生成匹配的键值对
            for (int i = 0; i < loadData.items.Length; i++)
                localizedText.Add(loadData.items[i].key,loadData.items[i].value);

            Debug.Log("Data loaded, dictionary contains: " + localizedText.Count + " entries");
        }
        else
            Debug.LogError("Cannot find file!");

        isReady = true;
    }

    public string GetLocalizedValue(string key)
    {
        string result = missingTextString;
        if (localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }

        return result;

    }

    public bool GetIsReady()
    {
        return isReady;
    }
}
