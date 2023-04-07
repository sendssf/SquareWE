using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class CrystalToCoinEvent : MonoBehaviour
{
    // Start is called before the first frame update

    int crystalConsume = 0;
    int coinGet = 0;

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

    void ShowError(string message)
    {
        transform.Find("Error").gameObject.GetComponent<Text>().text = message;
    }

    public void UpdateCrystalConsume(string message)
    {
        if(message == null || message.Length == 0)
        {
            ShowError("The crystal consumption cannot be empty!");
            crystalConsume = 0;
            coinGet = 0;
        }
        else
        {
            ShowError("");
            crystalConsume = Convert.ToInt32(message);
            coinGet = 80 * crystalConsume;
            if(crystalConsume > AllMessageContainer.playerInfo.crystal)
            {
                transform.Find("InputNum").Find("Num").GetComponent<Text>().color = red;
            }
            else
            {
                transform.Find("InputNum").Find("Num").GetComponent<Text>().color = pink;
            }
            transform.Find("BringNum").gameObject.GetComponent<Text>().text = coinGet.ToString();
        }
    }

    public void PressOk()
    {
        if(crystalConsume <=0)
        {
            ShowError("The crystal consumption number must larger than zero!");
        }
        else if(crystalConsume >= AllMessageContainer.playerInfo.crystal)
        {
            ShowError("Your crystal is not enough!");
        }
        else
        {
            AllMessageContainer.playerInfo.crystal -= crystalConsume;
            AllMessageContainer.playerInfo.coin += coinGet;
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
        transform.gameObject.SetActive(false);
    }
}
