using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//此脚本用来管理主页面的鼠标点击事件
public class MainPageClickEvent : MonoBehaviour
{
    // Start is called before the first frame update
    AsyncOperation operation;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void GotoPlayerMessageScene()
    {
        StartCoroutine(loadScene("PlayerMessageUI"));
    }
    private IEnumerator loadScene(string which)
    {
        operation=SceneManager.LoadSceneAsync(which);
        operation.allowSceneActivation = true;
        yield return operation;
    }
    public void GotoHelp()
    {
        transform.Find("Help").gameObject.SetActive(true);
    }
    public void GotoSettings()
    {
        transform.Find("Settings").gameObject.SetActive(true);
    }
    public void GotoShop()
    {
        StartCoroutine(loadScene("ShopUI"));
    }

    public void GotoChooseMode()
    {
        StartCoroutine(loadScene("ChooseModeUI"));
    }
}
