using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreakThroughClickEvent : MonoBehaviour
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

    public void GotoHome()
    {
        StartCoroutine(loadScene("BeginUI"));
    }

    public void GotoSettings()
    {
        transform.Find("Settings").gameObject.SetActive(true);
    }

    private IEnumerator loadScene(string which) //º”‘ÿ≥°æ∞
    {
        operation=SceneManager.LoadSceneAsync(which);
        yield return operation;
    }

    public void ReturnLastPage()
    {
        StartCoroutine(loadScene("ChooseModeUI"));
    }

    public void GotoLevel1()
    {
        AllMessageContainer.gameStatus.gameMode=GameMode.BreakThrough_1;
        GotoGamePage();
    }

    public void GotoLevel2()
    {
        AllMessageContainer.gameStatus.gameMode= GameMode.BreakThrough_2;
        GotoGamePage();
    }
    public void GotoLevel3()
    {
        AllMessageContainer.gameStatus.gameMode= GameMode.BreakThrough_3;
        GotoGamePage();
    }

    public void GotoLevel4()
    {
        AllMessageContainer.gameStatus.gameMode= GameMode.BreakThrough_4;
        GotoGamePage();
    }

    public void GotoLevel5()
    {
        AllMessageContainer.gameStatus.gameMode= GameMode.BreakThrough_5;
        GotoGamePage();
    }

    public void GotoLevel6()
    {
        AllMessageContainer.gameStatus.gameMode= GameMode.BreakThrough_6;
        GotoGamePage();
    }

    public void GotoLevel7()
    {
        AllMessageContainer.gameStatus.gameMode= GameMode.BreakThrough_7;
        GotoGamePage();
    }

    public void GotoLevel8()
    {
        AllMessageContainer.gameStatus.gameMode= GameMode.BreakThrough_8;
        GotoGamePage();
    }

    public void GotoLevel9()
    {
        AllMessageContainer.gameStatus.gameMode= GameMode.BreakThrough_9;
        GotoGamePage();
    }

    public void GotoLevel10()
    {
        AllMessageContainer.gameStatus.gameMode= GameMode.BreakThrough_10;
        GotoGamePage();
    }

    public void GotoLevel11()
    {
        AllMessageContainer.gameStatus.gameMode= GameMode.BreakThrough_11;
        GotoGamePage();
    }

    public void GotoLevel12()
    {
        AllMessageContainer.gameStatus.gameMode= GameMode.BreakThrough_12;
        GotoGamePage();
    }

    public void GotoLevel13()
    {
        AllMessageContainer.gameStatus.gameMode= GameMode.BreakThrough_13;
        GotoGamePage();
    }

    public void GotoLevel14()
    {
        AllMessageContainer.gameStatus.gameMode= GameMode.BreakThrough_14;
        GotoGamePage();
    }

    public void GotoLevel15()
    {
        AllMessageContainer.gameStatus.gameMode= GameMode.BreakThrough_15;
        GotoGamePage();
    }

    public void GotoLevel16()
    {
        AllMessageContainer.gameStatus.gameMode=GameMode.BreakThrough_16;
        GotoGamePage();
    }

    public void GotoLevel17()
    {
        AllMessageContainer.gameStatus.gameMode= GameMode.BreakThrough_17;
        GotoGamePage();
    }

    public void GotoLevel18()
    {
        AllMessageContainer.gameStatus.gameMode= GameMode.BreakThrough_18;
        GotoGamePage();
    }

    public void GotoLevel19()
    {
        AllMessageContainer.gameStatus.gameMode= GameMode.BreakThrough_19;
        GotoGamePage();
    }

    public void GotoLevel20()
    {
        AllMessageContainer.gameStatus.gameMode=GameMode.BreakThrough_20;
        GotoGamePage();
    }

    public void GotoLevel21()
    {
        AllMessageContainer.gameStatus.gameMode= GameMode.BreakThrough_21;
        GotoGamePage();
    }

    public void GotoGamePage()
    {
        StartCoroutine(loadScene("GamePage"));
    }
}
