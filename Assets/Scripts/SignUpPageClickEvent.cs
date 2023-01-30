using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

//此脚本用来管理登陆界面的鼠标点击事件
public class SignUpPageClickEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitSignUp()
    {
        transform.gameObject.SetActive(false);
    }

    public void PressSignIn()
    {
        transform.parent.Find("Register").gameObject.SetActive(true);
        transform.gameObject.SetActive(false);
    }

    public void PressOK()       //确认登录
    {
        //Add something to operate the input data
        //登陆操作
        if(AllMessageContainer.playerInfo.playerName!="NickName" && 
            AllMessageContainer.playerInfo.playerName==AllMessageContainer.loginInfo.nickname)
        {
            if(AllMessageContainer.playerInfo.password==AllMessageContainer.loginInfo.password)
            {
                //登陆成功
                AllMessageContainer.gameStatus.iflogin=true;
            }
            else
            {
                transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                    .gameObject.GetComponent<Text>().text="The password is error! Try again!";
                return;
            }
        }
        else
        {
            //确认输入的用户是否存在，目前只在本地确认
            if (File.Exists(Application.persistentDataPath+@"\\"+AllMessageContainer.loginInfo.nickname+".json"))
            {
                AllMessageContainer.ReadInfoFromFile(AllMessageContainer.loginInfo.nickname+".json");
                //判断密码是否正确
                if (AllMessageContainer.playerInfo.password==AllMessageContainer.loginInfo.password)
                {
                    AllMessageContainer.gameStatus.iflogin=true;
                    if (transform.parent.name=="PlayerMessage")
                    {
                        transform.parent.gameObject.GetComponent<PlayerMessagePageClickEvent>().LoadPage();
                    }
                }
                else
                {
                    transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                    .gameObject.GetComponent<Text>().text="The password is error! Try again!";
                    return;
                }
            }
            else    //用户不存在
            {
                transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                    .gameObject.GetComponent<Text>().text="The user is not exist! You can create a new account!";
                return;
            }
        }
        transform.gameObject.SetActive(false);
    }

    public void GetNickname(string nickname)
    {
        AllMessageContainer.loginInfo.nickname=nickname;
    }

    public void GetPassword(string password)
    {
        AllMessageContainer.loginInfo.password=password;
    }
}
