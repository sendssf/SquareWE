using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class OnlineMode: MonoBehaviour
{
    // Start is called before the first frame update
    AsyncOperation operation;
    WholeCube whole;
    bool ifSendInvi = false;
    bool ifagreed = false;
    bool ifPrepared = false;
    string otherName = null;
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
            { "nickname2",transform.parent.Find("Name").gameObject.GetComponent<Text>().text}/////需要修改
        };
        string response = WebController.Post(WebController.rootIP + API_Local.sendInvitation, JsonConvert.SerializeObject(json));
        otherName = transform.parent.Find("Name").gameObject.GetComponent<Text>().text;
        switch (response)
        {
            case WebController.Success:
                ///进入等待状态
                ifSendInvi = true;
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
        string response = WebController.Post(WebController.rootIP + API_Local.allInvitations, JsonConvert.SerializeObject(json));
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
                var info = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(response);
                
                //弹出对话框
                break;
        }
    }

    public void agreeInvite()
    {
        var json = new Dictionary<string, string>
        {
            { "nickname1",AllMessageContainer.playerInfo.playerName},
            { "nickname2",transform.parent.Find("Name").gameObject.GetComponent<Text>().text},/////需要修改
            { "option","agree" }
        };
        otherName = transform.parent.Find("Name").gameObject.GetComponent<Text>().text;
        string responses = WebController.Post(WebController.rootIP + API_Local.respondInvitation, JsonConvert.SerializeObject(json));
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
            { "nickname2",transform.parent.Find("Name").gameObject.GetComponent<Text>().text},/////需要修改
            { "option","disagree" }
        };
        string responses = WebController.Post(WebController.rootIP + API_Local.respondInvitation, JsonConvert.SerializeObject(json));
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
            { "nickname2",otherName}/////需要修改
        };
        string response = WebController.Post(WebController.rootIP + API_Local.allRequest, JsonConvert.SerializeObject(json));
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
                        AllMessageContainer.gameStatus.ifonline = true;
                        ifagreed = true;
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
                {"up", Cube.GetChild(i).GetChild(0).GetComponent<Faces>().letter.ToString()},
                {"down",Cube.GetChild(i).GetChild(1).GetComponent<Faces>().letter.ToString() },
                {"left",Cube.GetChild(i).GetChild(2).GetComponent<Faces>().letter.ToString() },
                {"right", Cube.GetChild(i).GetChild(3).GetComponent<Faces>().letter.ToString()},
                {"front", Cube.GetChild(i).GetChild(4).GetComponent<Faces>().letter.ToString()},
                {"back", Cube.GetChild(i).GetChild(5).GetComponent<Faces>().letter.ToString()}
            };
            cube.Add("cube"+i,cubei);
        }
        string response = WebController.Post(WebController.rootIP + API_Local.allRequest, JsonConvert.SerializeObject(cube));
        switch (response)
        {
            case WebController.Success:
                ifPrepared = true;
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
            { "nickname",otherName}/////需要修改
        };
        string response = WebController.Post(WebController.rootIP + API_Local.allRequest, JsonConvert.SerializeObject(json));
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
                var mes = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(response);
                Dictionary<string, string> num;
                string opt;
                Transform OtherCube = GameObject.Find("OtherCube").transform;
                List<string> name = new List<string>();
                if (mes.TryGetValue("cube0", out num))
                {
                    num.TryGetValue("num", out opt);
                    OtherCube.gameObject.GetComponent<Ingradients1>().Generate(int.Parse(opt));
                }
                foreach (Dictionary<string, string> cube in mes.Values)
                {
                    if (cube.TryGetValue("name", out opt))
                    {
                        name.Add(opt);
                    }
                }
                foreach (Dictionary<string, string> cube in mes.Values)
                {
                    for(int i = 0; i < OtherCube.childCount; i++)
                    {
                        if(name.Contains(OtherCube.GetChild(i).name.Substring(5)))
                        {
                            whole.StackLetter(cube["up"].ToCharArray()[0], OtherCube.GetChild(i).GetChild(0).gameObject);
                            whole.StackLetter(cube["down"].ToCharArray()[0], OtherCube.GetChild(i).GetChild(1).gameObject);
                            whole.StackLetter(cube["left"].ToCharArray()[0], OtherCube.GetChild(i).GetChild(2).gameObject);
                            whole.StackLetter(cube["right"].ToCharArray()[0], OtherCube.GetChild(i).GetChild(3).gameObject);
                            whole.StackLetter(cube["front"].ToCharArray()[0], OtherCube.GetChild(i).GetChild(4).gameObject);
                            whole.StackLetter(cube["back"].ToCharArray()[0], OtherCube.GetChild(i).GetChild(5).gameObject);
                            break;
                        }
                        else
                        {
                            DestroyImmediate(OtherCube.GetChild(i).gameObject);
                        }
                    }
                }
                ifPrepared = true;
                break;
        }
    }

    async public void dataCallBack()
    {
        var json = new Dictionary<string, string>
        {
            {"nickname1",AllMessageContainer.playerInfo.playerName },
            { "nickname2",otherName}/////需要修改
        };
        string response = WebController.Post(WebController.rootIP + API_Local.allRequest, JsonConvert.SerializeObject(json));
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
                    if (opt == "move cube")
                    {
                        //wait
                    }
                    else if (opt == "rotate cube")
                    {
                        //wait
                    }
                    else if (opt == "rotate screen")
                    {
                        //wait
                    }
                    else if (opt == "point cube")
                    {
                        string[] result = mes["object"].Split(new char[] { ',' });
                        int i = 0;
                        GameObject c = null;
                        List<GameObject> bg = new List<GameObject>();
                        foreach (string s in result)
                        {
                            if (i % 2 == 0)
                            {
                                c = GameObject.Find("cube*" + s);
                            }
                            else
                            {
                                c.transform.Find(s).GetComponent<Faces>().TimeUp();
                                if (c.transform.Find(s).GetComponent<Faces>().Times() == 1)
                                {
                                    c.transform.Find(s).GetComponent<MeshRenderer>().material.color = new Color(1, 0.6f, 0.6f);//点击改变面颜色
                                }
                                else if (c.transform.Find(s).GetComponent<Faces>().Times() == 2)
                                {
                                    c.transform.Find(s).gameObject.GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0.6f);//点击改变面颜色
                                }
                                else if (c.transform.Find(s).GetComponent<Faces>().Times() == 3)
                                {
                                    GameObject father = c.transform.Find(s).transform.parent.gameObject;
                                    father.transform.parent.GetComponent<WholeCube>().position.Remove(father.transform.position);
                                    father.transform.parent.GetComponent<WholeCube>()._isCleared = true;
                                    for (int j = 0; j < 6; j++)
                                    {
                                        father.transform.GetChild(i).gameObject.GetComponent<Faces>().rb.isKinematic = false;
                                        father.transform.GetChild(i).gameObject.GetComponent<Faces>().rb.useGravity = true;
                                    }
                                    father.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                                    GameObject.Find("Explosion").gameObject.AddComponent<Expolosion>().explosionPos = GameObject.Find("OtherCube").transform;
                                    var ps = Instantiate(father.GetComponent<Cube>().particle, father.transform);
                                    ps.transform.localPosition = Vector3.zero;
                                    ps.Play();
                                    Destroy(father);  
                                }
                            }
                            i++;
                        }
                    }
                    else if (opt == "destroy cube")
                    {
                        string[] result = mes["object"].Split(new char[] { ',' });
                        foreach(string s in result)
                        {
                            Destroy(GameObject.Find("cube*" + s));
                        }
                    }
                    else if (opt == "change color")
                    {
                        //弹出对话框
                    }
                    else if (opt == "get word")
                    {
                        //弹出对话框
                    }
                    else if (opt == "quit")
                    {
                        //弹出对话框
                    }
                }
                break;
        }
    }

    public void TransmitStatus(string option, string obj, string body)
    {
        var json = new Dictionary<string, string>
        {
            {"nickname1",AllMessageContainer.playerInfo.playerName },
            { "nickname2",otherName},/////需要修改
            {"option", option},
            {"object", obj},
            {"body", body}
        };
        string response = WebController.Post(WebController.rootIP + API_Local.allRequest, JsonConvert.SerializeObject(json));
        switch (response)
        {
            case WebController.Success:
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
        }
    }

    private IEnumerator loadScene(string which)
    {
        operation = SceneManager.LoadSceneAsync(which);
        operation.allowSceneActivation = true;
        yield return operation;
    }

}
