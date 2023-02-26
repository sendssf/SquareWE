using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FriendsController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject friendItem;
    public Sprite normalHead;
    GameObject friendlist;
    void Start()
    {
        friendlist=transform.Find("FriendList").Find("Viewport").Find("Content").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void LoadFriends()
    {
        int friendnum = friendlist.transform.childCount;
        for (int i = 0; i<friendnum; i++)
        {
            Destroy(friendlist.transform.GetChild(i).gameObject);
        }
        foreach(KeyValuePair<string,Dictionary<string,string>> frd in AllMessageContainer.playerInfo.friendList)
        {
            if (frd.Key=="0")
            {
                continue;
            }
            var item = Instantiate(friendItem, friendlist.transform);
            item.transform.Find("Image").gameObject.GetComponent<Image>().sprite= normalHead;
            item.transform.Find("Name").gameObject.GetComponent<Text>().text= frd.Key;
            item.transform.Find("Info").gameObject.GetComponent<Text>().text=
                $"Level:{AllMessageContainer.playerInfo.level}    Rank:{AllMessageContainer.playerInfo.rank}    "+
                $"Word Number:{AllMessageContainer.playerInfo.worldList.Count}";

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
                if (content==null||content==""||content.Length==0)
                {
                    
                }
                else
                {
                    Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
                    File.WriteAllBytes($"{Application.persistentDataPath}\\Friends\\{nickname}.png", Convert.FromBase64String(dict["pic"]));

                }
            }
        }
    }
}
