using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 挂载Text上，更换语言
/// </summary>
public class LocalizedText : MonoBehaviour
{
    public string key;

    // Use this for initialization
    void Start()
    {
        Text text = GetComponent<Text>();
        text.text = LocalizationManager.instance.GetLocalizedValue(key);
    }
}