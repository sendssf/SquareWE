using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System;

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
        var json = new Dictionary<string, string>
        {
            { "nickname",AllMessageContainer.loginInfo.nickname},
            { "password",AllMessageContainer.loginInfo.password}
        };
        string response=WebController.Post("http://127.0.0.1:8080/api/login/", JsonConvert.SerializeObject(json));
        switch (response)
        {
            case WebController.Success:
                if (AllMessageContainer.playerInfo.playerName==AllMessageContainer.loginInfo.nickname)
                {
                    //登录成功且已加载玩家与登陆玩家一致
                    AllMessageContainer.gameStatus.iflogin=true;
                    UpdateAllInfo(AllMessageContainer.loginInfo.nickname);
                    transform.gameObject.SetActive(false);
                    if (transform.parent.name=="PlayerMessage")
                    {
                        transform.parent.gameObject.GetComponent<PlayerMessagePageClickEvent>().LoadPage();
                    }
                }
                else
                {
                    if (File.Exists($"{Application.persistentDataPath}\\{AllMessageContainer.loginInfo.nickname}.json"))    //玩家信息文件存在
                    {
                        UpdateAllInfo(AllMessageContainer.loginInfo.nickname);
                        ReadInfoState res = AllMessageContainer.ReadInfoFromFile($"{AllMessageContainer.loginInfo.nickname}.json");
                        if (res == ReadInfoState.Success)
                        {
                            AllMessageContainer.gameStatus.iflogin=true;
                            if (transform.parent.name=="PlayerMessage")
                            {
                                transform.parent.gameObject.GetComponent<PlayerMessagePageClickEvent>().LoadPage();
                            }
                            transform.gameObject.SetActive(false);
                        }
                        else if (res==ReadInfoState.FileCannotRead)
                        {
                            transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                                .gameObject.GetComponent<Text>().text="The player file cannot read. Please check if the file has been occupied by other applications";
                        }
                    }
                    else
                    {
                        //从server拉取文件
                        string resp2 = WebController.Post("http://127.0.0.1:8080/api/all_info/",
                            JsonConvert.SerializeObject(new Dictionary<string, string>
                            {
                                { "nickname",AllMessageContainer.loginInfo.nickname}
                            }));
                        switch (resp2)
                        {
                            case WebController.PlayerNotExist:
                                transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                                    .gameObject.GetComponent<Text>().text="The player is not exist. Please check your nickname";
                                break;
                            case WebController.ServerNotFound:
                                transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                                    .gameObject.GetComponent<Text>().text="Load Information failed becuase your network or the server have some problems.";
                                break;
                            default://下载成功
                                File.WriteAllText($"{Application.persistentDataPath}\\{AllMessageContainer.loginInfo.nickname}.json", resp2);
                                AllMessageContainer.ReadInfoFromFile($"{AllMessageContainer.loginInfo.nickname}.json");
                                AllMessageContainer.gameStatus.iflogin=true;
                                if (transform.parent.name=="PlayerMessage")
                                {
                                    transform.parent.gameObject.GetComponent<PlayerMessagePageClickEvent>().LoadPage();
                                }
                                transform.gameObject.SetActive(false);
                                break;
                        }
                    }
                }
                break;
            case WebController.PasswordError:
                transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                    .gameObject.GetComponent<Text>().text="The password is error! Please check the nickname and the password. ";
                break;
            case WebController.PlayerNotExist:
                transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                    .gameObject.GetComponent<Text>().text="The player is not exist. Please check your nickname";
                break;
            case WebController.ServerNotFound:
                if (File.Exists($"{Application.persistentDataPath}\\{AllMessageContainer.loginInfo.nickname}.json"))
                {
                    var info = JsonConvert.DeserializeObject<Dictionary<string, object>>
                        (File.ReadAllText($"{Application.persistentDataPath}\\{AllMessageContainer.loginInfo.nickname}.json"));
                    var playerInfo = JsonConvert.DeserializeObject<Dictionary<string, string>>(info["playerInfo"].ToString());
                    if (playerInfo["password"]==AllMessageContainer.loginInfo.password)
                    {
                        transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                            .gameObject.GetComponent<Text>().text="Log in success! But the server connection is error. " +
                            "You can play the game normally and we will try to upload your data later";
                        AllMessageContainer.gameStatus.iflogin=true;
                        if (transform.parent.name=="PlayerMessage")
                        {
                            transform.parent.gameObject.GetComponent<PlayerMessagePageClickEvent>().LoadPage();
                        }
                    }
                    else
                    {
                        transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                            .gameObject.GetComponent<Text>().text="The password is error! Please check the nickname and the password. ";
                    }
                }
                else
                {
                    transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                        .gameObject.GetComponent<Text>().text="Server connection error, and the player has no local record. " +
                        "So you cannot log in this account";
                }
                break;
        }
    }

    private void UpdateAllInfo(string nickname)        //尽力从server更新玩家全部信息
    {
        string res = WebController.Post("http://127.0.0.1:8080/api/all_info/", JsonConvert.SerializeObject(new Dictionary<string, string>
        {
            {"nickname",nickname }
        }));
        if(res!=WebController.Success && res!=WebController.ServerNotFound && res!=WebController.PlayerNotExist)
        {
            using (FileStream fs=new FileStream($"{Application.persistentDataPath}\\{nickname}.json", FileMode.OpenOrCreate))
            {
                fs.Seek(0, SeekOrigin.Begin);
                fs.SetLength(0);
                using(StreamWriter sw=new StreamWriter(fs))
                {
                    sw.WriteLine(res);
                }
            }
        }
    }

    public void GetNickname(string nickname)
    {
        AllMessageContainer.loginInfo.nickname=nickname;
    }

    public void GetPassword(string password)
    {
        AllMessageContainer.loginInfo.password=password;
    }

    private void ShowTips(string message)
    {

    }
}
