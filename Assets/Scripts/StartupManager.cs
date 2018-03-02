using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// 挂载在LocalizationManager上，一旦更换语言重新加载此场景
/// </summary>
public class StartupManager : MonoBehaviour
{
    // Use this for initialization
    private IEnumerator Start()
    {
        while (!LocalizationManager.instance.GetIsReady())
        {
            yield return null;
        }

        SceneManager.LoadScene("MenuScreen");
    }

}