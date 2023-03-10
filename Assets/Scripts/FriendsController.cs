using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

enum SearchMode
{
    Exact,
    Fuzzy,
    Email
}

public class FriendsController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject friendItem;
    public GameObject newFriendItem;
    public GameObject applicationItem;
    public GameObject addFriendObj;     //点击添加按钮对应的对象
    public Sprite normalHead;
    private Dictionary<string,string> applicationList;
    private SearchMode searchMode;
    private string newFriendSearchInfo;
    private string validateInfo;
    public string addFriendname;
    float time;
    static List<string> waitList;              //等待同意的好友昵称
    private List<string> friendsHaveLoaded=new List<string>();     //已经加载的好友
    GameObject friendlist;
    GameObject newFriendList;   //添加好友栏

    void Start()
    {
        waitList = new List<string>();
        time=0;
        friendlist=transform.Find("FriendList").Find("Viewport").Find("Content").gameObject;
        newFriendList=transform.Find("AddPage").Find("FriendList").Find("Viewport").Find("Content").gameObject;
        for(int i = 0; i<friendlist.transform.childCount; i++)
        {
            Destroy(friendlist.transform.GetChild(i).gameObject);
        }
        LoadFriends();
        applicationList = GetFriendApplication();
        if (applicationList!=null && applicationList.Count!=0)
        {
            if (transform.Find("Application").Find("Mask").gameObject.activeInHierarchy==false)
            {
                transform.Find("Application").Find("Mask").gameObject.SetActive(true);
                transform.Find("Application").Find("Mask").Find("Back").Find("Number").gameObject
                    .GetComponent<Text>().text=applicationList.Count.ToString();
            }
        }
        else
        {
            transform.Find("Application").Find("Mask").gameObject.SetActive(false);
        }
        searchMode=SearchMode.Exact;
        ClearNewFriendList();
        addFriendname=null;
    }

    private void OnEnable()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        time+=Time.deltaTime;
        if (time>=30)
        {
            time=0; 
            applicationList = GetFriendApplication();
            if (applicationList!=null && applicationList.Count!=0)
            {
                if (transform.Find("Application").Find("Mask").gameObject.activeInHierarchy==false)
                {
                    transform.Find("Application").Find("Mask").gameObject.SetActive(true);
                    transform.Find("Application").Find("Mask").Find("Back").Find("Number").gameObject
                        .GetComponent<Text>().text=applicationList.Count.ToString();
                }
            }
            else
            {
                transform.Find("Application").Find("Mask").gameObject.SetActive(false);
            }
            if (transform.Find("Application").Find("Mask").gameObject.activeInHierarchy==true)
            {
                LoadApplicationList();
            }
        }
        if (AllMessageContainer.gameStatus.changeFriendInfo)
        {
            //删除所有好友的显示，准备进行刷新
            for (int i = 0; i<friendlist.transform.childCount; i++)
            {
                Destroy(friendlist.transform.GetChild(i).gameObject);
            }
            AllMessageContainer.gameStatus.changeFriendInfo = false;
            GameObject applist = transform.Find("ApplicationPage").Find("FriendList").
                Find("Viewport").Find("Content").gameObject;
            //删除申请列表所有信息
            for (int i = 0; i<applist.transform.childCount; i++)
            {
                Destroy(applist.transform.GetChild(i).gameObject);
            }
            waitList.Clear();
            friendsHaveLoaded.Clear();
            LoadFriends();
        }
    }

    private Dictionary<string,string> GetFriendApplication()
    {
        string result;
        List<string> reqname;
        List<string> reqmes;
        var res=new Dictionary<string,string>();
        result =WebController.Post("http://127.0.0.1:8080/api/all_requests/", JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                {"nickname",AllMessageContainer.playerInfo.playerName }
            }));
        if (result!=WebController.ServerNotFound && result!=WebController.PlayerNotExist)
        {
            Dictionary<string, List<string>> frd = JsonConvert.DeserializeObject<Dictionary<string,List<string>>>(result);
            reqname=frd["requests"];
            reqmes=frd["verification"];
            for(int i = 0; i<reqmes.Count; i++)
            {
                res.Add(reqname[i], reqmes[i]);
            }

            return res;
        }
        else
        {
            return null;
        }
    }

    public void LoadFriends()
    {
        int friendnum = friendlist.transform.childCount;
        foreach(string frd in AllMessageContainer.playerInfo.friendList)
        {
            if (frd=="0")
            {
                continue;
            }
            if (!friendsHaveLoaded.Contains(frd))
            {
                var item = Instantiate(friendItem, friendlist.transform);
                item.transform.Find("Image").gameObject.GetComponent<Image>().sprite= normalHead;
                item.transform.Find("Name").gameObject.GetComponent<Text>().text= frd;

                var msgInfo = AllMessageContainer.GetFriendsInfoFromServer(frd);
                item.transform.Find("Info").gameObject.GetComponent<Text>().text=
                    $"Level:{msgInfo["level"]}    Rank:{msgInfo["rank"]}";
                StartCoroutine(GetFriendImageAsync(frd, item));
                friendsHaveLoaded.Add(frd);
            }
        }
    }

    public IEnumerator GetFriendImageAsync(string nickname,GameObject item)
    {
        
        using (UnityWebRequest webRequest = new UnityWebRequest("http://127.0.0.1:8080/api/send_avatar/", UnityWebRequest.kHttpVerbPOST))
        {
            Dictionary<string, string> req = new Dictionary<string, string>
        {
            { "nickname",nickname}
        };
            UploadHandler upload = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(req)));
            webRequest.uploadHandler= upload;
            webRequest.uploadHandler.contentType= "application/json";
            DownloadHandler download = new DownloadHandlerBuffer();
            webRequest.downloadHandler= download;
            UnityWebRequestAsyncOperation operation = webRequest.SendWebRequest();

            yield return operation;

            if (webRequest.result==UnityWebRequest.Result.Success)
            {
                string content = webRequest.downloadHandler.text;
                Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
                if (dict["pic"]==null||dict["pic"]==""||dict["pic"].Length==0)
                {
                    item.transform.Find("Image").GetComponent<Image>().sprite=normalHead;
                }
                else
                {
                    byte[] picture = Convert.FromBase64String(dict["pic"]);
                    //File.WriteAllBytes($"{Application.persistentDataPath}\\Friends\\{nickname}.png", picture);
                    Texture2D texture = new Texture2D(290, 290);
                    texture.LoadImage(picture);
                    item.transform.Find("Image").GetComponent<Image>().sprite=Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0f, 0f));
               }
           }
        }
    }

    public void ChangeSearchMode(int num)
    {
        switch (num)
        {
            case 0:
                searchMode=SearchMode.Exact;
                break;
            case 1:
                searchMode=SearchMode.Fuzzy;
                break;
            case 2:
                searchMode=SearchMode.Email;
                break;
        }
    }

    public void GotoAdd()
    {
        if (transform.Find("ApplicationPage").gameObject.activeInHierarchy)
        {
            transform.Find("ApplicationPage").gameObject.SetActive(false);
        }
        transform.Find("AddPage").gameObject.SetActive(true);
    }

    public void QuitAdd()
    {
        transform.Find("AddPage").gameObject.SetActive(false);
    }

    public void UpdateNewFriendSearchInfo(string info)
    {
        newFriendSearchInfo=info;
    }

    public void SearchNewFriend()
    {
        ClearNewFriendList();
        if (Directory.Exists($"{Application.persistentDataPath}\\searchTemp"))
        {
            Directory.Delete($"{Application.persistentDataPath}\\searchTemp", true);
        }
        transform.Find("AddPage").Find("ErrorInfo").gameObject.SetActive(false);
        switch (searchMode)
        {
            case SearchMode.Exact:  //搜到的玩家唯一
                string result = WebController.
                    Post("http://127.0.0.1:8080/api/all_info/", JsonConvert.SerializeObject(new Dictionary<string, string>
                {
                    {"nickname",newFriendSearchInfo }
                }));
                if (result==WebController.ServerNotFound)
                {
                    AddPageShowError("Network Error! We cannot connect with the server!");
                }
                else if (result==WebController.PlayerNotExist)
                {
                    AddPageShowError("The player you search is not exist! Please check and try again or try the Fuzzy Search");
                }
                else
                {   
                    var SearchInfo = AllMessageContainer.GetFriendsInfoFromServer(newFriendSearchInfo);
                    var item = Instantiate(newFriendItem, newFriendList.transform);
                    item.transform.Find("Name").gameObject.GetComponent<Text>().text=newFriendSearchInfo;
                    item.transform.Find("Info").gameObject.GetComponent<Text>().text=
                        $"Level:{SearchInfo["level"]}    Rank:{SearchInfo["rank"]}    Word Number:{SearchInfo["wordnumber"]}";
                    string res = WebController.GetHeadImage("http://127.0.0.1:8080/api/send_avatar/", newFriendSearchInfo);
                    if (res==WebController.FileNotExist)        //未上传头像文件
                    {
                        item.transform.Find("Image").gameObject.GetComponent<Image>().sprite = normalHead;
                        return;
                    }
                    Texture2D texture2D = new Texture2D(290, 290);
                    if (!Directory.Exists($"{Application.persistentDataPath}\\searchTemp"))
                    {
                        Directory.CreateDirectory($"{Application.persistentDataPath}\\searchTemp");
                    }
                    texture2D.LoadImage(PlayerMessagePageClickEvent.GetImageByte($"{Application.persistentDataPath}\\SearchTemp\\{newFriendSearchInfo}.png"));
                    item.transform.Find("Image").gameObject.GetComponent<Image>().sprite=Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0, 0));
                }
                break;
            case SearchMode.Fuzzy:

                break;
            case SearchMode.Email:

                break;
        }
    }

    private void AddPageShowError(string info)
    {
        transform.Find("AddPage").Find("ErrorInfo").gameObject.SetActive(true);
        transform.Find("AddPage").Find("ErrorInfo").gameObject.GetComponent<Text>().text=info;
    }

    private void FriendPageShowError(string info)
    {
        transform.Find("Tips").gameObject.GetComponent<Text>().text= info;
    }

    public void ApplicationPageError(string info)
    {
        transform.Find("ApplicationPage").Find("ErrorInfo").gameObject.SetActive(true);
        transform.Find("ApplicationPage").Find("ErrorInfo").gameObject.GetComponent<Text>().text= info;
    }

    private void ClearNewFriendList()
    {
        int all = newFriendList.transform.childCount;
        for (int i = 0; i<all; i++)
        {
            Destroy(newFriendList.transform.GetChild(i).gameObject);
        }
    }

    public void UpdateValidateInfo(string info)
    {
        validateInfo=info;
    }

    public void ValidateOK()
    {
        //发送好友请求
        string result = WebController
            .Post("http://127.0.0.1:8080/api/add_friend/", JsonConvert.SerializeObject(new Dictionary<string, string>{
                {"nickname1", AllMessageContainer.playerInfo.playerName},
                {"nickname2", addFriendname},
                {"verification", validateInfo}
            }));
        if(result==WebController.Success)
        {
            addFriendObj.transform.Find("State").gameObject.SetActive(true);
            transform.Find("Validate").gameObject.SetActive(false);
            waitList.Add(addFriendname);
        }
        else
        {
            AddPageShowError("Network Error! Please check your network connection and try again later.");
        }
    }

    public void ValidateCancel()
    {
        addFriendname=null;
        transform.Find("Validate").gameObject.SetActive(false);
    }

    public void QuitApplication()
    {
        transform.Find("ApplicationPage").gameObject.SetActive(false);
    }

    public void GotoApplication()
    {
        if (transform.Find("AddPage").gameObject.activeInHierarchy)
        {
            transform.Find("AddPage").gameObject.SetActive(false);
        }
        transform.Find("ApplicationPage").gameObject.SetActive(true);
        LoadApplicationList();
    }

    private void LoadApplicationList()
    {
        GameObject applist = transform.Find("ApplicationPage").Find("FriendList").Find("Viewport").Find("Content").gameObject;
        //检查如果有重复的就从applicationList里面删掉
        for(int i=0;i<applist.transform.childCount;i++)
        {
            string pname = applist.transform.GetChild(i).Find("Name").gameObject.GetComponent<Text>().text;
            if (applicationList.ContainsKey(pname))
            {
                applicationList.Remove(pname);
            }
        }
        //将applicationList里面的玩家插入表单
        foreach(KeyValuePair<string,string> apl in applicationList)
        {
            var item = Instantiate(applicationItem, applist.transform);
            item.transform.Find("Name").gameObject.GetComponent<Text>().text=apl.Key;
            item.transform.Find("Image").gameObject.GetComponent<Image>().sprite=normalHead;
            var info = AllMessageContainer.GetFriendsInfoFromServer(apl.Key);
            item.transform.Find("Info").gameObject.GetComponent<Text>().text=
                $"Level:{info["level"]}    Rank:{info["rank"]}    Word number:{info["wordnumber"]}";
            item.transform.Find("Back").Find("VeriInfo").gameObject.GetComponent<Text>().text=apl.Value;
            StartCoroutine(GetFriendImageAsync(apl.Key, item));
        }
    }

    public void DeleteFriend(string nickname,GameObject item)
    {
        string res = WebController.Post("http://127.0.0.1:8080/api/delete_friend/", JsonConvert.
            SerializeObject(new Dictionary<string, string>
            {
                {"nickname1",AllMessageContainer.playerInfo.playerName },
                {"nickname2",nickname }
            }));
        if (res==WebController.Success)
        {
            FriendPageShowError("");
            Destroy(item);
            AllMessageContainer.playerInfo.friendList.Remove(nickname);
            friendsHaveLoaded.Remove(nickname);
            LoadFriends();
        }
        else if (res==WebController.ServerNotFound)
        {
            FriendPageShowError("Network Error! Please check your network connection and try again later!");
        }
    }

    public void AgreeSuccess(string nickname)
    {
        waitList.Remove(nickname);
        AllMessageContainer.playerInfo.friendList.Add(nickname);
        applicationList.Remove(nickname);
        if (applicationList.Count==0)
        {
            transform.Find("Application").Find("Mask").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("Application").Find("Mask").Find("Back").Find("Number").gameObject
                .GetComponent<Text>().text=applicationList.Count.ToString();
        }
        LoadFriends();
    }

    public void RejectSuccess(string nickname)
    {
        waitList.Remove(nickname);
        AllMessageContainer.playerInfo.friendList.Remove(nickname);
        applicationList.Remove(nickname);
        if (applicationList.Count==0)
        {
            transform.Find("Application").Find("Mask").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("Application").Find("Mask").Find("Back").Find("Number").gameObject
                .GetComponent<Text>().text=applicationList.Count.ToString();
        }
    }
}
