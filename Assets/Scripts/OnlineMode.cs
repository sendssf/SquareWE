using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using static UnityEditor.Progress;

public class OnlineMode: MonoBehaviour
{
    // Start is called before the first frame update
    AsyncOperation operation;
    bool ifSendInvi = false;
    bool ifagreed = false;
    public static bool ifPrepared = false;
    float waitTime = 0;
    float assureIfInviteTime = 0;
    public static Dictionary<string,float> allInvitations = new Dictionary<string,float>();
    public static string operateWhoInv; //正在处理谁的邀请
    public static string inviteWhoToPlay;   //正在邀请谁
    public string inviteResult;
    public static string playWith;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        assureIfInviteTime += Time.deltaTime;
        if(ifSendInvi)
        {
            waitTime += Time.deltaTime;
            IfAgreed();
        }
        if(assureIfInviteTime >= 1)
        {
            assureIfInviteTime = 0;
            IfInvited();
        }
        if(allInvitations.Count > 0)
        {
            UpdateAllInviteDict();
        }
    }

    public void PressInviteButton()       //挂载Invite按钮
    {
        var json = new Dictionary<string, string>
        {
            { "nickname1",AllMessageContainer.playerInfo.playerName},
            { "nickname2",inviteWhoToPlay}/////需要修改
        };
        string response = WebController.Post(WebController.rootIP + API_Local.sendInvitation, JsonConvert.SerializeObject(json));
        switch (response)
        {
            case WebController.Success:
                transform.Find("Waiting").gameObject.SetActive(true);
                ifSendInvi = true;
                break;
            case WebController.PlayerNotExist:
                transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                    .gameObject.GetComponent<Text>().text = "The player is not exist. Please check your nickname";
                break;
            case WebController.ServerNotFound:
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
            case WebController.NoMessage:
                break;
            case WebController.ServerNotFound:
                break;
            default:
                var info = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(response)["nickname"];
                foreach (var item in info)
                {
                    if (!allInvitations.ContainsKey(item))
                    {
                        allInvitations.Add(item,0);
                    }
                }
                UpdateInviteNumber();
                //弹出对话框
                break;
        }
    }

    void UpdateInviteNumber()
    {
        if(allInvitations.Count == 0)
        {
            transform.Find("Invitation").Find("Mask").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("Invitation").Find("Mask").gameObject.SetActive(true);
            transform.Find("Invitation").Find("Mask").Find("Back").Find("Number")
                .gameObject.GetComponent<Text>().text = allInvitations.Count.ToString();
        }
    }

    void UpdateAllInviteDict()
    {
        var temp = new Dictionary<string, float>();
        foreach(var item in allInvitations)
        {
            if (item.Value + Time.deltaTime <= 60) {
                temp.Add(item.Key, item.Value + Time.deltaTime); 
            }
            else    //超时
            {
                if(transform.Find("InvitationPage").gameObject.activeInHierarchy)
                {
                    GameObject content = transform.Find("InvitationPage").Find("FriendList")
                        .Find("Viewport").Find("Content").gameObject;
                    for(int i = 0; i<content.transform.childCount; i++)
                    {
                        if(content.transform.GetChild(i).Find("Name").gameObject.GetComponent<Text>().text == item.Key)
                        {
                            Destroy(content.transform.GetChild(i).gameObject);
                            break;
                        }
                    }
                }
            }
        }
        allInvitations = temp;
    }

    public void AgreeInvite()
    {
        var json = new Dictionary<string, string>
        {
            { "nickname1",AllMessageContainer.playerInfo.playerName},
            { "nickname2",operateWhoInv},/////需要修改
            { "option","agree" }
        };
        string responses = WebController.Post(WebController.rootIP + API_Local.respondInvitation, JsonConvert.SerializeObject(json));
        switch (responses)
        {
            case WebController.Success:
                AllMessageContainer.gameStatus.ifonline = true;
                playWith = operateWhoInv;
                StartCoroutine(loadScene("OnlineMode"));
                break;
            case WebController.ServerNotFound:
                gameObject.GetComponent<FriendsController>().InvitationPageShowError("Network Error! Please check and try again later!");
                break;
        }
    }

    public void DisagreeInvite()
    {
        var json = new Dictionary<string, string>
        {
            { "nickname1",AllMessageContainer.playerInfo.playerName},
            { "nickname2",operateWhoInv},/////需要修改
            { "option","disagree" }
        };
        string responses = WebController.Post(WebController.rootIP + API_Local.respondInvitation, JsonConvert.SerializeObject(json));
        switch (responses)
        {
            case WebController.Success:
                GameObject content = transform.Find("InvitationPage").Find("FriendList")
                        .Find("Viewport").Find("Content").gameObject;
                for (int i = 0; i<content.transform.childCount; i++)
                {
                    if (content.transform.GetChild(i).Find("Name").gameObject.GetComponent<Text>().text == operateWhoInv)
                    {
                        Destroy(content.transform.GetChild(i).gameObject);
                        break;
                    }
                }
                break;
            case WebController.ServerNotFound:
                gameObject.GetComponent<FriendsController>().InvitationPageShowError("Network Error! Please check and try again later!");
                break;
        }
    }

    public static void Victory()
    {
        WebController.Post(WebController.rootIP + API_Local.postInfo, JsonConvert.SerializeObject(new Dictionary<string, string>
        {
            {"nickname1",AllMessageContainer.playerInfo.playerName },
            {"nickname2",playWith },
            {"option","win" },
            {"body","time" },
            {"object","a,a,a,a" }
        }));
    }

    public static void QuitOnlineMode()
    {
        WebController.Post(WebController.rootIP + API_Local.postInfo, JsonConvert.SerializeObject(new Dictionary<string, string>
        {
            {"nickname1",AllMessageContainer.playerInfo.playerName },
            {"nickname2",playWith },
            {"option","quit" },
            {"body","time" },
            {"object","a,a,a,a" }
        }));
    }

    void IfAgreed()
    {
        var json = new Dictionary<string, string>
        {
            { "nickname1",AllMessageContainer.playerInfo.playerName},
            { "nickname2",inviteWhoToPlay},/////需要修改
            {"time", waitTime.ToString()}
        };
        string response = WebController.Post(WebController.rootIP + API_Local.getCondition, JsonConvert.SerializeObject(json));
        switch (response)
        {
            case WebController.NoMessage:
                ///进入等待状态
                break;
            case WebController.ServerNotFound:
                transform.Find("Waiting").gameObject.SetActive(false);
                gameObject.GetComponent<FriendsController>().FriendPageShowError("Network Error! Please check and try again!");
                break;
            default:
                transform.Find("Waiting").gameObject.SetActive(false);
                var mes = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
                string opt;
                ifSendInvi = false;
                waitTime = 0;
                if(mes.TryGetValue("option", out opt))
                {
                    if(opt == "disagree")
                    {
                        //弹出对话框
                        transform.Find("InviteTips").gameObject.SetActive(true);
                        transform.Find("InviteTips").Find("Info").gameObject.GetComponent<Text>().text=
                            $"The player {inviteWhoToPlay} has rejected your invitation. You can try to invite other players." +
                            $"Press OK to quit.";
                    }
                    else if(opt == "Timeout")
                    { 
                        //弹出对话框 
                        transform.Find("InviteTips").gameObject.SetActive(true);
                        transform.Find("InviteTips").Find("Info").gameObject.GetComponent<Text>().text=
                            $"The player {inviteWhoToPlay} doesn't reply your invitation for 1 minute and your invitation become invalid. " +
                            $"You can send your invitation again or try to invite other players. Press OK to quit.";
                    }
                    else if(opt == "agree")
                    {
                        //弹出对话框
                        transform.Find("InviteTips").gameObject.SetActive(true);
                        transform.Find("InviteTips").Find("Info").gameObject.GetComponent<Text>().text=
                            $"The player {inviteWhoToPlay} has agreed your invitation! Press OK to edit your game mode and then you can play together!";
                        AllMessageContainer.gameStatus.ifonline = true;
                        AllMessageContainer.gameStatus.ifStartGame = true;
                        AllMessageContainer.gameStatus.ifHost = true;
                        ifagreed = true;
                        playWith = inviteWhoToPlay;
                        //StartCoroutine(loadScene("OnlineMode"));
                    }
                }
                break;
        }
    }

    public void InviteTipPressOK()
    {
        if (ifagreed)
        {
            StartCoroutine(loadScene("CustomMode"));
        }
        else
        {
            transform.Find("InviteTips").gameObject.SetActive(false);
        }
    }

    static public void TransmitStatus(string option, string obj, string body)
    {
        var json = new Dictionary<string, string>
        {
            {"nickname1",AllMessageContainer.playerInfo.playerName },
            {"nickname2",playWith},/////需要修改
            {"option", option},
            {"object", obj},
            {"body", body}
        };
        string response = WebController.Post(WebController.rootIP + API_Local.sendPackage, JsonConvert.SerializeObject(json));
        switch (response)
        {
            case WebController.Success:
                ///进入等待状态
                break;
            case WebController.ServerNotFound:
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
