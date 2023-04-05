using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnlineMode: MonoBehaviour
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

    public void PressInviteButton()       //邀请
    {
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

    public void IfInvited()       //是否邀请
    {
        var json = new Dictionary<string, string>
        {
            { "nickname",AllMessageContainer.playerInfo.playerName}
        };
        string response = WebController.Post("http://127.0.0.1:8080/api/login/", JsonConvert.SerializeObject(json));
        switch (response)
        {
            case WebController.Nomessage:
                break;
            case WebController.ServerNotFound:
                if (File.Exists($"{Application.persistentDataPath}\\{AllMessageContainer.loginInfo.nickname}.json"))
                {
                    var inform = JsonConvert.DeserializeObject<Dictionary<string, object>>
                        (File.ReadAllText($"{Application.persistentDataPath}\\{AllMessageContainer.loginInfo.nickname}.json"));
                    var playerInfo = JsonConvert.DeserializeObject<Dictionary<string, string>>(inform["playerInfo"].ToString());
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
            default:
                var info = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
                //弹出对话框
                break;
        }
    }

    public void  agreeInvite()
    {
        var json = new Dictionary<string, string>
        {
            { "nickname1",AllMessageContainer.playerInfo.playerName},
            { "nicknmae2",transform.parent.Find("Name").gameObject.GetComponent<Text>().text},/////需要修改
            { "option","agree" }
        };
        string responses = WebController.Post("http://127.0.0.1:8080/api/login/", JsonConvert.SerializeObject(json));
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

    void disagreeInvite()
    {
        var json = new Dictionary<string, string>
        {
            { "nickname1",AllMessageContainer.playerInfo.playerName},
            { "nicknmae2",transform.parent.Find("Name").gameObject.GetComponent<Text>().text},/////需要修改
            { "option","disagree" }
        };
        string responses = WebController.Post("http://127.0.0.1:8080/api/login/", JsonConvert.SerializeObject(json));
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

    void ifAgreed()
    {
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
            case WebController.Nomessage:
                ///进入等待状态
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
            default:
                var mes = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
                string opt;
                if(mes.TryGetValue("option", out opt))
                {
                    if(opt == "disagree")
                    {
                        //弹出对话框
                    }
                    else if(opt == "Timeout")
                    {
                        //弹出对话框 
                    }
                    else if(opt == "agree")
                    {
                        //弹出对话框
                        StartCoroutine(loadScene("OnlineMode"));
                    }
                }
                break;
        }
    }

    public void generateCube()
    {
        Dictionary<string, Dictionary<string, string>> cube = new Dictionary<string, Dictionary<string, string>>();
        Transform Cube = gameObject.transform.Find("Third-orderCube");
        for (int i = 0; i < Cube.childCount; i++)
        {
            var cubei = new Dictionary<string, string>
            {
                {"num", Ingradients.num.ToString()},
                {"name", Cube.GetChild(i).name.Substring(4)},
                {"up", },
                {"down", },
                {"left", },
                {"right", },
                {"front", },
                {"back", }
            };
            cube.Add("cube"+i,cubei);
        }
        string response = WebController.Post("http://127.0.0.1:8080/api/login/", JsonConvert.SerializeObject(cube));
        switch (response)
        {
            case WebController.Success:////发送失败
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

    public void receiveCube()
    {
        var json = new Dictionary<string, string>
        {
            { "nicknmae2",transform.parent.Find("Name").gameObject.GetComponent<Text>().text}/////需要修改
        };
        string response = WebController.Post("http://127.0.0.1:8080/api/login/", JsonConvert.SerializeObject(json));
        switch (response)
        {
            case WebController.Nomessage:
                ///进入等待状态
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
            default:
                var mes = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
                string opt;
                if (mes.TryGetValue("option", out opt))
                {
                    if (opt == "disagree")
                    {
                        //弹出对话框
                    }
                    else if (opt == "Timeout")
                    {
                        //弹出对话框 
                    }
                    else if (opt == "agree")
                    {
                        //弹出对话框
                        StartCoroutine(loadScene("OnlineMode"));
                    }
                }
                break;
        }
    }

    public void dataTransmit()
    {
        var json = new Dictionary<string, string>
        {
            { "nicknmae2",transform.parent.Find("Name").gameObject.GetComponent<Text>().text}/////需要修改
        };
        string response = WebController.Post("http://127.0.0.1:8080/api/login/", JsonConvert.SerializeObject(json));
        switch (response)
        {
            case WebController.Nomessage:
                ///进入等待状态
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
            default:
                var mes = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
                string opt;
                if (mes.TryGetValue("option", out opt))
                {
                    if (opt == "disagree")
                    {
                        //弹出对话框
                    }
                    else if (opt == "Timeout")
                    {
                        //弹出对话框 
                    }
                    else if (opt == "agree")
                    {
                        //弹出对话框
                        StartCoroutine(loadScene("OnlineMode"));
                    }
                }
                break;
        }
    }

    private IEnumerator loadScene(string which)
    {
        operation = SceneManager.LoadSceneAsync(which);
        operation.allowSceneActivation = true;
        yield return operation;
    }
}
