using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.UI;

public class OnlineMode: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PressInviteButton()       //确认邀请
    {
        //Add something to operate the input data
        var json = new Dictionary<string, string>
        {
            { "nickname1",AllMessageContainer.playerInfo.playerName},
            { "nicknmae2",transform.parent.Find("Name").gameObject.GetComponent<Text>().text}/////需要修改
        };
        //JsonConvert.DeserializeObject<Dictionary<string, string>>();
        //JsonConvert.SerializeObject(json);
        string response = WebController.Post("http://127.0.0.1:8080/api/login/", JsonConvert.SerializeObject(json));
        switch (response)
        {
            case WebController.Success:
                ///进入等待状态
                string result = WebController.Post("http://127.0.0.1:8080/api/login/", JsonConvert.SerializeObject(json));
                //if result
                break;
            case WebController.PlayerNotExist:
                transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                    .gameObject.GetComponent<Text>().text = "The player is not exist. Please check your nickname";
                break;
            case WebController.ServerNotFound:
                if (File.Exists($"{Application.persistentDataPath}\\{AllMessageContainer.loginInfo.nickname}.json"))
                {
                    var info = JsonConvert.DeserializeObject<Dictionary<string, object>>
                        (File.ReadAllText($"{Application.persistentDataPath}\\{AllMessageContainer.loginInfo.nickname}.json"));
                    var playerInfo = JsonConvert.DeserializeObject<Dictionary<string, string>>(info["playerInfo"].ToString());
                    if (playerInfo["password"] == AllMessageContainer.loginInfo.password)
                    {
                        transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                            .gameObject.GetComponent<Text>().text = "Log in success! But the server connection is error. " +
                            "You can play the game normally and we will try to upload your data later";
                        AllMessageContainer.gameStatus.iflogin = true;
                        if (transform.parent.name == "PlayerMessage")
                        {
                            transform.parent.gameObject.GetComponent<PlayerMessagePageClickEvent>().LoadPage();
                        }
                    }
                    else
                    {
                        transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                            .gameObject.GetComponent<Text>().text = "The password is error! Please check the nickname and the password. ";
                    }
                }
                else
                {
                    transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                        .gameObject.GetComponent<Text>().text = "Server connection error, and the player has no local record. " +
                        "So you cannot log in this account";
                }
                break;
        }
    }

    public void IfInvited()       //确认邀请
    {
        //Add something to operate the input data
        var json = new Dictionary<string, string>
        {
            { "nickname",AllMessageContainer.playerInfo.playerName}
        };
        string response = WebController.Post("http://127.0.0.1:8080/api/login/", JsonConvert.SerializeObject(json));
        switch (response)
        {
            case WebController.Nomessage:
                break;
            case WebController.:////json?
                ////对话框
                if(agree)
                {
                    var agreeInfo = new Dictionary<string, string>
                    {
                         { "nickname1",AllMessageContainer.playerInfo.playerName},
                         { "nicknmae2",transform.parent.Find("Name").gameObject.GetComponent<Text>().text},/////需要修改
                         { "option","agree" }
                    };
                    string responses = WebController.Post("http://127.0.0.1:8080/api/login/", JsonConvert.SerializeObject(agreeInfo));
                    switch (responses)
                    {
                        case WebController.Success:
                            break;
                        case WebController.ServerNotFound:
                            if (File.Exists($"{Application.persistentDataPath}\\{AllMessageContainer.loginInfo.nickname}.json"))
                            {
                                var info = JsonConvert.DeserializeObject<Dictionary<string, object>>
                                    (File.ReadAllText($"{Application.persistentDataPath}\\{AllMessageContainer.loginInfo.nickname}.json"));
                                var playerInfo = JsonConvert.DeserializeObject<Dictionary<string, string>>(info["playerInfo"].ToString());
                                if (playerInfo["password"] == AllMessageContainer.loginInfo.password)
                                {
                                    transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                                        .gameObject.GetComponent<Text>().text = "Log in success! But the server connection is error. " +
                                        "You can play the game normally and we will try to upload your data later";
                                    AllMessageContainer.gameStatus.iflogin = true;
                                    if (transform.parent.name == "PlayerMessage")
                                    {
                                        transform.parent.gameObject.GetComponent<PlayerMessagePageClickEvent>().LoadPage();
                                    }
                                }
                                else
                                {
                                    transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                                        .gameObject.GetComponent<Text>().text = "The password is error! Please check the nickname and the password. ";
                                }
                            }
                            else
                            {
                                transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                                    .gameObject.GetComponent<Text>().text = "Server connection error, and the player has no local record. " +
                                    "So you cannot log in this account";
                            }
                            break;
                    }
                }
                else
                {
                    var disagreeInfo = new Dictionary<string, string>
                    {
                         { "nickname1",AllMessageContainer.playerInfo.playerName},
                         { "nicknmae2",transform.parent.Find("Name").gameObject.GetComponent<Text>().text},/////需要修改
                         { "option","disagree" }
                    };
                    string responses = WebController.Post("http://127.0.0.1:8080/api/login/", JsonConvert.SerializeObject(disagreeInfo));
                    switch (responses)
                    {
                        case WebController.Success:
                            break;
                        case WebController.ServerNotFound:
                            if (File.Exists($"{Application.persistentDataPath}\\{AllMessageContainer.loginInfo.nickname}.json"))
                            {
                                var info = JsonConvert.DeserializeObject<Dictionary<string, object>>
                                    (File.ReadAllText($"{Application.persistentDataPath}\\{AllMessageContainer.loginInfo.nickname}.json"));
                                var playerInfo = JsonConvert.DeserializeObject<Dictionary<string, string>>(info["playerInfo"].ToString());
                                if (playerInfo["password"] == AllMessageContainer.loginInfo.password)
                                {
                                    transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                                        .gameObject.GetComponent<Text>().text = "Log in success! But the server connection is error. " +
                                        "You can play the game normally and we will try to upload your data later";
                                    AllMessageContainer.gameStatus.iflogin = true;
                                    if (transform.parent.name == "PlayerMessage")
                                    {
                                        transform.parent.gameObject.GetComponent<PlayerMessagePageClickEvent>().LoadPage();
                                    }
                                }
                                else
                                {
                                    transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                                        .gameObject.GetComponent<Text>().text = "The password is error! Please check the nickname and the password. ";
                                }
                            }
                            else
                            {
                                transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                                    .gameObject.GetComponent<Text>().text = "Server connection error, and the player has no local record. " +
                                    "So you cannot log in this account";
                            }
                            break;
                    }
                }
            case WebController.ServerNotFound:
                if (File.Exists($"{Application.persistentDataPath}\\{AllMessageContainer.loginInfo.nickname}.json"))
                {
                    var info = JsonConvert.DeserializeObject<Dictionary<string, object>>
                        (File.ReadAllText($"{Application.persistentDataPath}\\{AllMessageContainer.loginInfo.nickname}.json"));
                    var playerInfo = JsonConvert.DeserializeObject<Dictionary<string, string>>(info["playerInfo"].ToString());
                    if (playerInfo["password"] == AllMessageContainer.loginInfo.password)
                    {
                        transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                            .gameObject.GetComponent<Text>().text = "Log in success! But the server connection is error. " +
                            "You can play the game normally and we will try to upload your data later";
                        AllMessageContainer.gameStatus.iflogin = true;
                        if (transform.parent.name == "PlayerMessage")
                        {
                            transform.parent.gameObject.GetComponent<PlayerMessagePageClickEvent>().LoadPage();
                        }
                    }
                    else
                    {
                        transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                            .gameObject.GetComponent<Text>().text = "The password is error! Please check the nickname and the password. ";
                    }
                }
                else
                {
                    transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                        .gameObject.GetComponent<Text>().text = "Server connection error, and the player has no local record. " +
                        "So you cannot log in this account";
                }
                break;
        }
    }


}
