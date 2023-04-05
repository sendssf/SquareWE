using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinToCrystalEvent : MonoBehaviour
{
    // Start is called before the first frame update

    int getCrystal = 0;
    int costCoin = 0;
    Color red;
    Color pink;
    void Start()
    {
        red = transform.Find("Error").gameObject.GetComponent<Text>().color;
        pink = transform.Find("Each").gameObject.GetComponent<Text>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PressOK()
    {
        if(costCoin <=0)
        {
            ShowError("The crystal number must larger than zero!");
        }
        else if(costCoin > AllMessageContainer.playerInfo.coin)
        {
            ShowError("Your coin is not enough!");
        }
        else
        {
            AllMessageContainer.playerInfo.coin -= costCoin;
            AllMessageContainer.playerInfo.crystal += getCrystal;
            string res = AllMessageContainer.SendBasicInfo();
            if(res == WebController.ServerNotFound)
            {
                ShowError("Network error! Please check your network connection and try again!");
            }
            transform.parent.gameObject.GetComponent<ShopPageClickEvent>().UpdateRemainMoney();
            gameObject.SetActive(false);
        }
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
    }

    void ShowError(string message)
    {
        transform.Find("Error").gameObject.GetComponent<Text>().text = message;
    }

    public void UpdateGetCrystal(string message)
    {
        if(message == null || message.Length == 0)
        {
            ShowError("The crystal number you want to get cannot be empty!");
            costCoin = 0;
        }
        else
        {
            ShowError("");
            getCrystal = Convert.ToInt32(message);
            costCoin = 100 * getCrystal;
            if(costCoin > AllMessageContainer.playerInfo.coin)
            {
                transform.Find("CostNum").gameObject.GetComponent<Text>().color = red;
            }
            else
            {
                transform.Find("CostNum").gameObject.GetComponent<Text>().color = pink;
            }
            transform.Find("CostNum").gameObject.GetComponent<Text>().text = costCoin.ToString();
        }
    }
}
