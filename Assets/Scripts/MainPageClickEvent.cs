using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//�˽ű�����������ҳ���������¼�
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
            transform.Find("NoLoginTips").gameObject.SetActive(true);
        }
    }

    public void QuitNoLoginTips()
    {
        transform.Find("NoLoginTips").gameObject.SetActive(false);
    }

    public void SignUpNow()
    {
        transform.Find("NoLoginTips").gameObject.SetActive(false);
        StartCoroutine(loadScene("PlayerMessageUI"));
    }
}
