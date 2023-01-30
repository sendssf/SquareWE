using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//此脚本用来管理玩家信息页面的鼠标点击事件
public class PlayerMessagePageClickEvent : MonoBehaviour
{
    // Start is called before the first frame update
    AsyncOperation operation;		//异步对象控制器
    Transform logintrans;
    void Start()
    {
        logintrans=transform.Find("Login");
        if (!AllMessageContainer.gameStatus.iflogin)
        {
            if (!logintrans.gameObject.activeSelf)
            {
                logintrans.gameObject.SetActive(true);
            }
        }
        if(AllMessageContainer.playerInfo.playerName!= null)
        {
            LoadPage();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void returnMainPage()    //返回主页
    {
        StartCoroutine(loadScene("BeginUI"));	//调用协程
    }

    private IEnumerator loadScene(string which) //加载场景
    {
        operation=SceneManager.LoadSceneAsync(which);
        yield return operation;
    }

    public void GotoSettings()  //前往设置
    {
        transform.Find("Settings").gameObject.SetActive(true);
    }

    public void LoadPage()
    {
        transform.Find("Upleft").Find("BasicInfo").gameObject.GetComponent<Text>().text=
            $"{AllMessageContainer.playerInfo.playerName}\n{AllMessageContainer.playerInfo.playerAccount}";
        transform.Find("Upleft").Find("Level").Find("Num").gameObject.GetComponent<Text>().text=
            AllMessageContainer.playerInfo.level.ToString();
        transform.Find("Upleft").Find("Level").Find("FullExp").gameObject.GetComponent<Image>().fillAmount=
            ((float)AllMessageContainer.playerInfo.experience)/AllMessageContainer.fullExp[AllMessageContainer.playerInfo.level];
        transform.Find("Upleft").Find("Level").Find("ExpValue").gameObject.GetComponent<Text>().text=
            $"{AllMessageContainer.playerInfo.experience}/{AllMessageContainer.fullExp[AllMessageContainer.playerInfo.level]}";
    }
}
