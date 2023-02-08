using System;
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
    string objName;
    int number;
    int eachCoin;
    int eachCrystal;
    bool islegal;
    Color pink;
    GameObject coinNum;
    GameObject crystalNum;
    void Start()
    {
        pink=transform.Find("Confirm").Find("Contain").Find("Viewport").Find("Content").Find("Total").gameObject.GetComponent<Text>().color;
        coinNum=transform.Find("Confirm").Find("Contain").Find("Viewport").Find("Content").Find("Total").Find("CoinNum").gameObject;
        crystalNum=transform.Find("Confirm").Find("Contain").Find("Viewport").Find("Content").Find("Total").Find("CrystalNum").gameObject;
    }

    private void OnEnable()
    {
        UpdateRemainMoney();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitDetails()
    {
        transform.Find("Details").gameObject.SetActive(false);
    }

    public void QuitTips()
    {
        transform.Find("Tips").gameObject.SetActive(false);
    }

    public void QuitConfirm()
    {
        transform.Find("Confirm").gameObject.SetActive(false);
    }

    private void ShowTips(string message)
    {
        transform.Find("Tips").Find("Contain").Find("Viewport").Find("Content").Find("Tips")
            .gameObject.GetComponent<Text>().text=message;
    }

    private void ShowError(string message)
    {
        transform.Find("Confirm").Find("Contain").Find("Viewport").Find("Content").Find("ErrorInfo")
            .gameObject.GetComponent<Text>().text=message;
    }

    //用来给商品添加说明，此类函数命名都为ShowDetails_商品名称，只需修改最后一行字符串内容
    public void ShowDetails_PerspectiveCamera() 
    {
        transform.Find("Details").gameObject.SetActive(true);
        transform.Find("Details").Find("Contain").Find("Viewport")
            .Find("Content").Find("Describe").GetComponent<Text>()
            .text="You can use it to see the letters on the surface of the next layer of geometry that you wouldn't otherwise see.";
    }

    public void Purchase_PerspectiveCamera()
    {
        objName="PerspectiveCamera";
        eachCoin=100;
        eachCrystal=6;
        transform.Find("Confirm").gameObject.SetActive(true);
    }

    private bool Purchase(int coin,int crystal)
    {
        if (coin<=AllMessageContainer.playerInfo.coin && crystal<=AllMessageContainer.playerInfo.crystal)
        {
            AllMessageContainer.playerInfo.coin -= coin;
            AllMessageContainer.playerInfo.crystal -= crystal;
            return true;
        }
        else if (coin<=AllMessageContainer.playerInfo.coin && crystal>AllMessageContainer.playerInfo.crystal)
        {
            ShowError($"Your crystals are not enough! You need {crystal-AllMessageContainer.playerInfo.crystal} extra crystals. ");
        }
        else if (coin>AllMessageContainer.playerInfo.coin && crystal<=AllMessageContainer.playerInfo.crystal)
        {
            ShowError($"Your coins are not enough! You need {coin-AllMessageContainer.playerInfo.coin} extra coins");
        }
        else
        {
            ShowError($"Your coins and crystals are both not enough! You need {coin-AllMessageContainer.playerInfo.coin} " +
                $"extra coins and {crystal-AllMessageContainer.playerInfo.crystal} extra crystals in total. ");
        }
        return false;
    }

    public void ShowDetails_SmartPotion()
    {
        transform.Find("Details").gameObject.SetActive(true);
        transform.Find("Details").Find("Contain").Find("Viewport")
            .Find("Content").Find("Describe").GetComponent<Text>()
            .text="You can get a hint from the blinking effect in any game.";
    }

    public void ShowDetails_Hammer()
    {
        transform.Find("Details").gameObject.SetActive(true);
        transform.Find("Details").Find("Contain").Find("Viewport")
            .Find("Content").Find("Describe").GetComponent<Text>()
            .text="You can use it to increase the number of uses on a visible geometric surface, " +
            "as if you hit the surface with a hammer but don't break it";
    }

    public void Purchase_Hammer()
    {
        objName="Hammer";
        eachCoin=100;
        eachCrystal=6;
        transform.Find("Confirm").gameObject.SetActive(true) ;
    }

    public void ShowDetails_SuperiorHammer()
    {
        transform.Find("Details").gameObject.SetActive(true);
        transform.Find("Details").Find("Contain").Find("Viewport")
            .Find("Content").Find("Describe").GetComponent<Text>()
            .text="You can hit a geometry with it and completely break it";
    }

    public void Purchase_SuperiorHammer()
    {
        objName="SuperiorHammer";
        eachCoin=100;
        eachCrystal=6;
        transform.Find("Confirm").gameObject.SetActive(true);
    }

    public void Purchase_SmartPotion()
    {
        objName="SmartPotion";
        eachCoin=100;
        eachCrystal=6;
        transform.Find("Confirm").gameObject.SetActive(true);
    }

    public void ShowDetails_AddTimeClock()
    {
        transform.Find("Details").gameObject.SetActive(true);
        transform.Find("Details").Find("Contain").Find("Viewport")
            .Find("Content").Find("Describe").GetComponent<Text>()
            .text="You can use it to add the remaining time in some game modes. Each clock can add 10 seconds";
    }

    public void Purchase_AddTimeClock()
    {
        objName="AddTimeClock";
        eachCoin=100;
        eachCrystal=6;
        transform.Find("Confirm").gameObject.SetActive(true);
    }

    public void ShowDetails_SuperiorAddTimeClock()
    {
        transform.Find("Details").gameObject.SetActive(true);
        transform.Find("Details").Find("Contain").Find("Viewport")
            .Find("Content").Find("Describe").GetComponent<Text>()
            .text="You can use it to add more remaining time in some game modes. Each clock can add 25 seconds";
    }

    public void Purchase_SuperiorAddTimeClock()
    {
        objName="SuperiorAddTimeClock";
        eachCoin=100;
        eachCrystal=6;
        transform.Find("Confirm").gameObject.SetActive(true);
    }

    public void ShowDetails_ForgetPotion()
    {
        transform.Find("Details").gameObject.SetActive(true);
        transform.Find("Details").Find("Contain").Find("Viewport")
            .Find("Content").Find("Describe").GetComponent<Text>()
            .text="The potion allows the timer to forget that time has passed, so it can be used to reduce the" +
            " amount of time that has already been used. Each potion can let the timer forget 10 seconds. "+
            "Of course that in some game modes, the effect of this potion is equal to the Add Time Clock.";
    }

    public void Purchase_ForgetPotion()
    {
        objName="ForgetPotion";
        eachCoin=100;
        eachCrystal=6;
        transform.Find("Confirm").gameObject.SetActive(true);
    }

    public void ShowDetails_SuperiorForgetPotion()
    {
        transform.Find("Details").gameObject.SetActive(true);
        transform.Find("Details").Find("Contain").Find("Viewport")
            .Find("Content").Find("Describe").GetComponent<Text>()
            .text="The potion allows the timer to forget more time that has passed, so it can be used to reduce the" +
            " amount of time that has already been used. Each potion can let the timer forget 25 seconds. "+
            "Of course that in some game modes, the effect of this potion is equal to the Add Time Clock.";
    }

    public void Purchase_SuperiorForgetPotion()
    {
        objName="SuperiorForgetPotion";
        eachCoin=100;
        eachCrystal=6;
        transform.Find("Confirm").gameObject.SetActive(true);
    }

    public void ShowDetails_Dictionary()
    {
        transform.Find("Details").gameObject.SetActive(true);
        transform.Find("Details").Find("Contain").Find("Viewport")
            .Find("Content").Find("Describe").GetComponent<Text>()
            .text="Using it in the game, you get a chance to look up words in the thesaurus and retrieve them without counting the time spent already. ";
    }

    public void Purchase_Dictionary()
    {
        objName="Dictionary";
        eachCoin=100;
        eachCrystal=6;
        transform.Find("Confirm").gameObject.SetActive(true);
    }
    public void ShowDetails_Eraser()
    {
        transform.Find("Details").gameObject.SetActive(true);
        transform.Find("Details").Find("Contain").Find("Viewport")
            .Find("Content").Find("Describe").GetComponent<Text>()
            .text="You can use it to erase letters from any visible surface of the geometry, and a different letter will"+
            "be randomly generated on that surface";
    }

    public void Purchase_Eraser()
    {
        objName="Eraser";
        eachCoin=100;
        eachCrystal=6;
        transform.Find("Confirm").gameObject.SetActive(true);
    }

    public void ShowDetails_SuperiorEraser()
    {
        transform.Find("Details").gameObject.SetActive(true);
        transform.Find("Details").Find("Contain").Find("Viewport")
            .Find("Content").Find("Describe").GetComponent<Text>()
            .text="You can use it to erase letters from any visible surface of the geometry, and a different letter will"+
            "be added to the surface by your determination";
    }

    public void Purchase_SuperiorEraser()
    {
        objName="SuperiorEraser";
        eachCoin=100;
        eachCrystal=6;
        transform.Find("Confirm").gameObject.SetActive(true);
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

    private void UpdateCoinCrystal(int coin,int crystal)
    {
        if (coin<=AllMessageContainer.playerInfo.coin)
        {
            coinNum.GetComponent<Text>().color=pink;
        }
        else
        {
            coinNum.GetComponent<Text>().color=Color.red;
        }
        if (crystal<=AllMessageContainer.playerInfo.crystal)
        {
            crystalNum.GetComponent<Text>().color=pink;
        }
        else
        {
            crystalNum.GetComponent<Text>().color=Color.red;
        }
        coinNum.GetComponent<Text>().text=coin.ToString();
        crystalNum.GetComponent<Text>().text=crystal.ToString();
    }

    public void GetObjectNum(string num)
    {
        if (num==null)
        {
            ShowError("The number cannot be empty!");
            islegal=false;
            return;
        }
        try
        {
            number=Convert.ToInt32(num);
            if (number<=0)
            {
                ShowError("The number must larger than zero");
                return;
            }
            UpdateCoinCrystal(eachCoin*number,eachCrystal*number);
        }
        catch
        {
            ShowError("The number must be an integer!");
            islegal=false;
            return;
        }
        islegal=true;
        return;
    }

    public void OkToPurchase()
    {
        if (islegal)
        {
            bool res = Purchase(eachCoin*number, eachCrystal*number);
            if (res)
            {
                if (AllMessageContainer.playerInfo.objectList.ContainsKey(objName))
                {
                    int prevnum = Convert.ToInt32(AllMessageContainer.playerInfo.objectList[objName]);
                    prevnum=prevnum+number;
                    AllMessageContainer.playerInfo.objectList[objName]=prevnum.ToString();
                }
                else
                {
                    AllMessageContainer.playerInfo.objectList.Add(objName, number.ToString());
                }
                Debug.Log(AllMessageContainer.playerInfo.objectList.Count);
                transform.Find("Confirm").gameObject.SetActive(false);
                UpdateRemainMoney();
            }
        }
        else
        {
            ShowError("Please change the number first!");
        }
    }

    private void UpdateRemainMoney()
    {
        transform.Find("Panel").Find("CoinNum").Find("Number").gameObject.GetComponent<Text>().text=AllMessageContainer.playerInfo.coin.ToString();
        transform.Find("Panel").Find("CrystalNum").Find("Number").gameObject.GetComponent<Text>().text=AllMessageContainer.playerInfo.crystal.ToString();
    }
}
