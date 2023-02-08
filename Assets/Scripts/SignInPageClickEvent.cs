using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

//此脚本用来管理注册界面的鼠标点击事件
public class SignInPageClickEvent : MonoBehaviour
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

    public void QuitSignIn()
    {
        transform.Find("OK").gameObject.SetActive(true);
        transform.gameObject.SetActive(false);
    }

    public void GetNickname(string nickname)
    {
        AllMessageContainer.registInfo.nickname = nickname;
    }

    public void GetEmail(string email)
    {
        AllMessageContainer.registInfo.emailaddr = email;
    }

    public void GetPassword(string password)
    {
        AllMessageContainer.registInfo.password = password;
    }

    void GenerateAccount()
    {
        string firstpara = UnityEngine.Random.Range(10000000, 99999999).ToString();
        string secondpara = UnityEngine.Random.Range(10000000,999999999).ToString();
        string account=firstpara+secondpara;
        AllMessageContainer.registInfo.account = account;
    }

    public void SignInOk()
    {
        //add: check message is ok
        GenerateAccount();
        //add: server Options
        var json = new Dictionary<string, string>
        {
            { "nickname", AllMessageContainer.registInfo.nickname },
            { "password", AllMessageContainer.registInfo.password },
            { "emailaddr", AllMessageContainer.registInfo.emailaddr },
            { "account", AllMessageContainer.registInfo.account }
        };
        //WebController.Post("http://127.0.0.1:8080/api/signup/", JsonConvert.SerializeObject(json));
        string response = transform.parent.gameObject.GetComponent<WebController>().Post("http://127.0.0.1:8080/api/signup/", JsonConvert.SerializeObject(json));
        switch (response)
        {
            case WebController.NicknameExist:
                ShowTips("Sign in failed!\nYour nickname has already existed. Please change another one. ");
                break;
            case WebController.ServerNotFound:
                ShowTips("Sign in success! \nBut it seems that your network or server have some problems. " +
                    "We will save your account and game information at your computer. When we can connect your computer with server, " +
                    "we will upload the information to server automatically");
                transform.Find("Contain").Find("Viewport").Find("Content").Find("Account")
                    .gameObject.GetComponent<Text>().text=$"Account:{AllMessageContainer.registInfo.account}";
                transform.Find("Contain").Find("Viewport").Find("Content").Find("Remember")
                    .gameObject.GetComponent<Text>().text="Please Remember!";
                InitPlayerInfo();
                AllMessageContainer.WriteInfoToFile(AllMessageContainer.playerInfo.playerName+".json");
                if (transform.parent.name=="PlayerMessage")
                {
                    transform.parent.gameObject.GetComponent<PlayerMessagePageClickEvent>().LoadPage();
                }
                AllMessageContainer.gameStatus.iflogin= true;
                transform.Find("OK").gameObject.SetActive(false);
                break;
            case WebController.Success:
                transform.Find("Contain").Find("Viewport").Find("Content").Find("Success").gameObject.GetComponent<Text>().text=
                    "Sign In Success!";
                transform.Find("Contain").Find("Viewport").Find("Content").Find("Account")
                    .gameObject.GetComponent<Text>().text=$"Account:{AllMessageContainer.registInfo.account}";
                transform.Find("Contain").Find("Viewport").Find("Content").Find("Remember")
                    .gameObject.GetComponent<Text>().text="Please Remember!";
                InitPlayerInfo();
                AllMessageContainer.WriteInfoToFile(AllMessageContainer.playerInfo.playerName+".json");
                if (transform.parent.name=="PlayerMessage")
                {
                    transform.parent.gameObject.GetComponent<PlayerMessagePageClickEvent>().LoadPage();
                }
                AllMessageContainer.gameStatus.iflogin= true;
                transform.Find("OK").gameObject.SetActive(false);
                break;
            default:
                ShowTips("It seems that an unexpected error occured.");
                break;

        }
    }

    void InitPlayerInfo()
    {
        AllMessageContainer.playerInfo.playerName=AllMessageContainer.registInfo.nickname;
        AllMessageContainer.playerInfo.password=AllMessageContainer.registInfo.password;
        AllMessageContainer.playerInfo.email=AllMessageContainer.registInfo.emailaddr;
        AllMessageContainer.playerInfo.playerAccount= AllMessageContainer.registInfo.account;

        AllMessageContainer.playerInfo.level=1;
        AllMessageContainer.playerInfo.experience=0;
        AllMessageContainer.playerInfo.crystal=0;
        AllMessageContainer.playerInfo.coin=0;
        AllMessageContainer.playerInfo.worldList=new Dictionary<string, string>();
        AllMessageContainer.playerInfo.objectList=new Dictionary<string, string>();
    }

    void ShowTips(string message)
    {
        transform.parent.Find("Tips").gameObject.SetActive(true);
        transform.Find("Tips").Find("Contain").Find("Viewport").Find("Content").Find("Tip")
            .gameObject.GetComponent<Text>().text= message;
    }
}
