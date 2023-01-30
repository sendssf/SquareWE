using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        string firstpara = Random.Range(10000000, 99999999).ToString();
        string secondpara = Random.Range(10000000,999999999).ToString();
        string account=firstpara+secondpara;
        AllMessageContainer.registInfo.account = account;
        AllMessageContainer.playerInfo.playerAccount= account;
        transform.Find("Contain").Find("Viewport").Find("Content").Find("Account")
            .gameObject.GetComponent<Text>().text=$"Account:{account}";
        transform.Find("Contain").Find("Viewport").Find("Content").Find("Remember")
            .gameObject.GetComponent<Text>().text="Please Remember!";
    }

    public void SignInOk()
    {
        //add: check message is ok
        GenerateAccount();
        //add: server Options
        InitPlayerInfo();
        AllMessageContainer.WriteInfoToFile(AllMessageContainer.playerInfo.playerName+".json");
        if (transform.parent.name=="PlayerMessage")
        {
            transform.parent.gameObject.GetComponent<PlayerMessagePageClickEvent>().LoadPage();
        }
        AllMessageContainer.gameStatus.iflogin= true;
        transform.Find("OK").gameObject.SetActive(false);
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
}
