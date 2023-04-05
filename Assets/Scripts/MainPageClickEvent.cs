using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//此脚本用来管理主页面的鼠标点击事件
public class MainPageClickEvent : MonoBehaviour
{
    // Start is called before the first frame update
    AsyncOperation operation;
    private void Awake()
    {
        if (!AllMessageContainer.gameStatus.ifInit)
        {
            AllMessageContainer.MessageInitial();
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit()
    {
        SendInfoToServer();
        AllMessageContainer.WriteInfoToFile(AllMessageContainer.playerInfo.playerName+".json");
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
        if (AllMessageContainer.gameStatus.iflogin)
        {
            AllMessageContainer.gameStatus.ifStartGame=true;
            StartCoroutine(loadScene("ChooseModeUI"));
        }
        else
        {
            transform.Find("Tips").gameObject.SetActive(true);
        }
    }

    public void QuitNoLoginTips()
    {
        transform.Find("Tips").gameObject.SetActive(false);
    }

    public void SignUpNow()
    {
        transform.Find("Tips").gameObject.SetActive(false);
        StartCoroutine(loadScene("PlayerMessageUI"));
    }

    public void SendInfoToServer()
    {
        WebController.Post(WebController.rootIP + API_Local.postSettings, 
            JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                {"nickname",AllMessageContainer.playerInfo.playerName },
                {"totalSoundOpen",AllMessageContainer.settingsInfo.totalSoundOpen.ToString() },
                {"backSoundOpen",AllMessageContainer.settingsInfo.backSoundOpen.ToString() },
                {"effectSoundOpen",AllMessageContainer.settingsInfo.effectSoundOpen.ToString() },
                {"totalSoundValue",AllMessageContainer.settingsInfo.totalSoundValue.ToString() },
                {"backSoundValue",AllMessageContainer.settingsInfo.backSoundValue.ToString()},
                {"effectSoundValue",AllMessageContainer.settingsInfo.effectSoundValue.ToString()}
            }));
        WebController.Post(WebController.rootIP + API_Local.postInfo,
            JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                {"nickname",AllMessageContainer.playerInfo.playerName },
                {"coin",AllMessageContainer.playerInfo.coin.ToString() },
                {"crystal",AllMessageContainer.playerInfo.crystal.ToString() },
                {"level",AllMessageContainer.playerInfo.level.ToString() },
                {"experience",AllMessageContainer.playerInfo.experience.ToString() },
                {"rank",AllMessageContainer.playerInfo.experience.ToString() }
            }));
    }
}
