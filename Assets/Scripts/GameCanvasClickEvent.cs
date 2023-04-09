using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCanvasClickEvent : MonoBehaviour
{
    // Start is called before the first frame update
    AsyncOperation operation;
    public AudioClip combine;
    bool isbBeginWaiting = false;
    void Start()
    {
        ShowMoney();
        if(AllMessageContainer.gameStatus.ifonline && 
            !AllMessageContainer.gameStatus.ifHost && !OnlineMode.ifPrepared && isbBeginWaiting == false)
        {
            AllMessageContainer.gameStatus.overlayerName = "waiting";
            isbBeginWaiting =true;
            StartCoroutine(loadScene("3DOverlayer", LoadSceneMode.Additive));
        }
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
        if (AllMessageContainer.gameStatus.ifonline)
        {
            AllMessageContainer.gameStatus.ifonline = false;
            AllMessageContainer.gameStatus.ifHost = false;
            OnlineMode.QuitOnlineMode();
        }
        AllMessageContainer.gameStatus.ifStartGame = false;
        StartCoroutine(loadScene("BeginUI"));
    }

    public void GotoShop()
    {
        if (!AllMessageContainer.gameStatus.ifonline)
        {
            StartCoroutine(loadScene("ShopUI"));
        }
    }

    public void ReturnPage()
    {
        if (!AllMessageContainer.gameStatus.ifonline)
        {
            switch (AllMessageContainer.gameStatus.gameMode)
            {
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
                case GameMode.CustomOthers:
                case GameMode.CustomRandom:
                    StartCoroutine(loadScene("CustomMode"));
                    break;
                case GameMode.Endless:
                    StartCoroutine(loadScene("ChooseModeUI"));
                    break;
            }
        }
        else
        {
            AllMessageContainer.gameStatus.ifonline = false;
            AllMessageContainer.gameStatus.ifHost = false;
            AllMessageContainer.gameStatus.ifStartGame = false;
            //Ã·«∞Ω· ¯
            OnlineMode.QuitOnlineMode();
            StartCoroutine(loadScene("PlayerMessageUI"));
        }
    }

    public void PlusButton()
    {
        AllMessageContainer.gameStatus.overlayerName = "Tips";
        StartCoroutine(loadScene("3DOverlayer", LoadSceneMode.Additive));
    }

    public void ShowMoney()
    {
        transform.Find("Money").Find("CoinNum").Find("Number").gameObject.GetComponent<Text>().text=
            AllMessageContainer.playerInfo.coin.ToString();
        transform.Find("Money").Find("CrystalNum").Find("Number").gameObject.GetComponent<Text>().text=
            AllMessageContainer.playerInfo.crystal.ToString();
    }
    public void GotoSettings()
    {
        AllMessageContainer.gameStatus.overlayerName = "Settings";
        StartCoroutine(loadScene("3DOverlayer", LoadSceneMode.Additive));
    }
    public async Task VictoryShow()
    {
        GameObject.Find("VictoryEffect").transform.Find("YellowFire").gameObject.SetActive(true);
        await Task.Delay(200);
        GameObject.Find("VictoryEffect").transform.Find("PurpleFire").gameObject.SetActive(true);
        await Task.Delay(200);
        GameObject.Find("VictoryEffect").transform.Find("GreenFire").gameObject.SetActive(true);
        await Task.Delay(1000);
        AllMessageContainer.gameStatus.overlayerName = "Victory";
        StartCoroutine(loadScene("3DOverlayer", LoadSceneMode.Additive));
        GameObject.Find("VictoryEffect").transform.Find("YellowFire").gameObject.SetActive(false);
        GameObject.Find("VictoryEffect").transform.Find("PurpleFire").gameObject.SetActive(false);
        GameObject.Find("VictoryEffect").transform.Find("GreenFire").gameObject.SetActive(false);
        return;
    }

    public async Task FinalVictory()
    {
        GameObject.Find("VictoryEffect").transform.Find("YellowFire").gameObject.SetActive(true);
        await Task.Delay(200);
        GameObject.Find("VictoryEffect").transform.Find("PurpleFire").gameObject.SetActive(true);
        await Task.Delay(200);
        GameObject.Find("VictoryEffect").transform.Find("GreenFire").gameObject.SetActive(true);
        await Task.Delay(800);
        AllMessageContainer.gameStatus.overlayerName = "FinalVictory";
        StartCoroutine(loadScene("3DOverlayer", LoadSceneMode.Additive));
        await Task.Delay(2000);
        GameObject.Find("VictoryEffect").transform.Find("YellowFire").gameObject.SetActive(false);
        GameObject.Find("VictoryEffect").transform.Find("PurpleFire").gameObject.SetActive(false);
        GameObject.Find("VictoryEffect").transform.Find("GreenFire").gameObject.SetActive(false);
        if (AllMessageContainer.gameStatus.ifonline)
        {
            AllMessageContainer.gameStatus.ifonline = false;
            AllMessageContainer.gameStatus.ifStartGame = false;
            AllMessageContainer.gameStatus.ifHost = false;
            StartCoroutine(loadScene("PlayerMessageUI"));
        }
        else
        {
            StartCoroutine(loadScene("ChooseModeUI"));
        }
        return;
    }
}
