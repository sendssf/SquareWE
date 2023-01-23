using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//此脚本用来管理Shop页面的鼠标点击事件
public class ShopPageClickEvent : MonoBehaviour
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

    public void QuitDetails()
    {
        transform.Find("Details").gameObject.SetActive(false);
    }

    //用来给商品添加说明，此类函数命名都为ShowDetails_商品名称，只需修改最后一行字符串内容
    public void ShowDetails_PerspectiveCamera() 
    {
        transform.Find("Details").gameObject.SetActive(true);
        transform.Find("Details").Find("Contain").Find("Viewport")
            .Find("Content").Find("Describe").GetComponent<Text>()
            .text="I am a Camera";
    }

    public void GotoHome()
    {
        StartCoroutine(loadScene("BeginUI"));
    }

    public void GotoSettings()
    {
        transform.Find("Settings").gameObject.SetActive(true);
    }
    private IEnumerator loadScene(string which)
    {
        operation=SceneManager.LoadSceneAsync(which);
        operation.allowSceneActivation = true;
        yield return operation;
    }
}
