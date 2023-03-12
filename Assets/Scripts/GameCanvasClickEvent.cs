using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCanvasClickEvent : MonoBehaviour
{
    // Start is called before the first frame update
    AsyncOperation operation;
    void Start()
    {
        ShowMoney();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator loadScene(string which,LoadSceneMode lmd=LoadSceneMode.Single)
    {
        operation=SceneManager.LoadSceneAsync(which,lmd) ;
        operation.allowSceneActivation = true;
        yield return operation;
    }

    public void GotoHome()
    {
        StartCoroutine(loadScene("BeginUI"));
    }

    public void GotoShop()
    {
        StartCoroutine(loadScene("ShopUI"));
    }

    public void ReturnPage()
    {
        switch (AllMessageContainer.gameStatus.gameMode)
        {
            case GameMode.Endless:

                break;
            case GameMode.BreakThrough_1:
            case GameMode.BreakThrough_2:
            case GameMode.BreakThrough_3:
            case GameMode.BreakThrough_4:
            case GameMode.BreakThrough_5:    
            case GameMode.BreakThrough_6:
            case GameMode.BreakThrough_7:
            case GameMode.BreakThrough_8:     
            case GameMode.BreakThrough_9:    
            case GameMode.BreakThrough_10:
            case GameMode.BreakThrough_11:
            case GameMode.BreakThrough_12:
            case GameMode.BreakThrough_13:
            case GameMode.BreakThrough_14:
            case GameMode.BreakThrough_15:
            case GameMode.BreakThrough_16:
            case GameMode.BreakThrough_17:
            case GameMode.BreakThrough_18:
            case GameMode.BreakThrough_19:
            case GameMode.BreakThrough_21:
                StartCoroutine(loadScene("BreakThrough"));
                break;
        }
    }

    void ShowMoney()
    {
        transform.Find("Money").Find("CoinNum").Find("Number").gameObject.GetComponent<Text>().text=
            AllMessageContainer.playerInfo.coin.ToString();
        transform.Find("Money").Find("CrystalNum").Find("Number").gameObject.GetComponent<Text>().text=
            AllMessageContainer.playerInfo.crystal.ToString();
    }
    public void GotoSettings()
    {
        StartCoroutine(loadScene("3DOverlayer", LoadSceneMode.Additive));
    }
}
