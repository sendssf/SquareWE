
using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public enum WebStatus
{
    Success,
    ServerNotFound,
    PlayerNotExist,
    PasswordError,
    NicknameExist
}

public class WebException : ApplicationException//由用户程序引发，用于派生自定义的异常类型
{
    /// <summary>
    /// 默认构造函数
    /// </summary>
    public WebException() { }

    public WebException(string message)
    : base(message)
    { }

    public WebException(string message, Exception inner)
    : base(message, inner)
    { }

    //public PayOverflowException(System.Runtime.Serialization.SerializationInfo info,
    // System.Runtime.Serialization.StreamingContext context)
    // : base(info, context) { }
}

public class API_Local
{
    public const string sign_up = "/api/signup/";
    public const string login = "/api/login/";
    public const string postAvater = "/api/post_avatar/";
    public const string sendAvater = "/api/send_avatar/";
    public const string postSettings = "/api/post_settings/";
    public const string postInfo = "/api/post_info/";
    public const string postWord = "/api/post_word/";
    public const string postObject = "/api/post_object/";
    public const string allInfo = "/api/all_info/";
    public const string addFriend = "/api/add_friend/";
    public const string acceptFriend = "/api/accept_friend/";
    public const string rejectFrient = "/api/reject_friend/";
    public const string allRequest = "/api/all_requests/";
    public const string deleteObject = "/api/delete_object/";
    public const string deleteFriend = "/api/delete_friend/";
    public const string sendMessage = "/api/send_message/";
    public const string getMessage = "/api/get_messages/";
    public const string sendInvitation = "/api/send_invitation/";
    public const string allInvitations = "/api/all_invitations/";
    public const string respondInvitation = "/api/respond_invi/";
    public const string getCondition = "/api/get_condition/";
    public const string checkSquareInfo = "/api/check_squareinfo/";
    public const string sendSquareInfo = "/api/send_squareinfo/";
    public const string sendPackage = "/api/send_pkg/";
    public const string getPackage = "/api/get_pkg/";
}

//挂载在UI
public class WebController : MonoBehaviour
{
    // Start is called before the first frame update
    public const string ServerNotFound = "ServerNotFound";
    public const string Success = "Success";
    public const string PlayerNotExist = "PlayerNotExist";
    public const string PasswordError = "PasswordError";
    public const string NicknameExist = "NicknameExist";
    public const string FileNotExist = "FileNotExist";
    public const string NoMessage = "NoMessage";
    public const string SendFailed = "SendFailed";
    
    public const string rootIP = "http://183.172.233.187:8080";
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PostAsync(string url,string postData)
    {
        StartCoroutine(PostRequest(url, postData));  
    }

    public static string GetHeadImage(string url, string nickname)
    {
        using (UnityWebRequest webRequest = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST))
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

            while (!operation.isDone) { }
            if (webRequest.result==UnityWebRequest.Result.Success)
            {
                string content = webRequest.downloadHandler.text;
                if(content=="Player does not exist")
                {
                    return PlayerNotExist;
                }
                Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
                if (dict["pic"]==""||dict["pic"].Length==0)
                {
                    return FileNotExist;
                }
                if (nickname==AllMessageContainer.playerInfo.playerName)
                {
                    File.WriteAllBytes($"{Application.persistentDataPath}\\{nickname}.png", Convert.FromBase64String(dict["pic"]));
                }
                else
                {
                    if (!Directory.Exists($"{Application.persistentDataPath}\\searchTemp"))
                    {
                        Directory.CreateDirectory($"{Application.persistentDataPath}\\searchTemp");
                    }
                    File.WriteAllBytes($"{Application.persistentDataPath}\\SearchTemp\\{nickname}.png", Convert.FromBase64String(dict["pic"]));
                }
                return Success;
            }
            else
            {
                return ServerNotFound;
            }
        }
    }

    public void GetHeadImageAsync(string url,string nickname)   
    {
        Dictionary<string, string> req = new Dictionary<string, string>
        {
            { "nickname",nickname}
        };

        try
        {
            PostAsync(url, nickname);
        }
        catch (WebException e)
        {
            switch(e.Message)
            {
                case ServerNotFound:
                    throw e;
                case PlayerNotExist:
                    throw e;
                default:
                    string content=e.Message;
                    Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
                    File.WriteAllBytes($"{Application.persistentDataPath}\\{nickname}.png", Convert.FromBase64String(dict["pic"]));
                    throw new WebException(Success);
            }
        }
    }


    public void PostPicturesAsync(string url,string path)
    {
        //StartCoroutine(PostPicturesAsyncRequest(url, path));
        byte[] image = File.ReadAllBytes(path);
        string imagestring = Convert.ToBase64String(image);
        Dictionary<string, string> file = new Dictionary<string, string>
        {
            {"pic","begin"},
            {"nickname",AllMessageContainer.playerInfo.playerName}
        };
        try
        {
            PostAsync(url, JsonConvert.SerializeObject(file));
        }
        catch(WebException e)
        {
            if (e.Message!=Success)
            {
                throw e;
            }
        }
        for (int i = 0; ; i+=10000)
        {
            int len = 10000;
            bool exit = false;
            if (i+len>imagestring.Length)
            {
                len=imagestring.Length-i;
                exit=true;
            }
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                { "pic",imagestring.Substring(i,len) },
                { "nickname",AllMessageContainer.playerInfo.playerName}
            };
            try
            {
                PostAsync(url, JsonConvert.SerializeObject(data));
            }
            catch(WebException e)
            {
                if(e.Message!=Success) 
                { 
                    throw e;
                }
            }
            if (exit)
            {
                break;
            }
        }
        file["pic"]="end";
        PostAsync(url,JsonConvert.SerializeObject(file));
    }

    public static string Post(string url, string postData)
    {
        using (UnityWebRequest webRequest = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST))
        {
            UploadHandler upload = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(postData));
            webRequest.uploadHandler = upload;
            webRequest.uploadHandler.contentType= "application/json";

            DownloadHandler downloadHandler = new DownloadHandlerBuffer();
            webRequest.downloadHandler = downloadHandler;
            UnityWebRequestAsyncOperation operation = webRequest.SendWebRequest();
            
            while(!operation.isDone) { }

            if (webRequest.result==UnityWebRequest.Result.Success)
            {
                switch (webRequest.downloadHandler.text)
                {
                    case "nickname exists":
                        return NicknameExist;
                    case "Sign up successfully":
                        return Success;
                    case "Player does not exist":
                        return PlayerNotExist;
                    case "Wrong password":
                        return PasswordError;
                    case "Log in successfully":
                        return Success;
                    case "Upload successfully":
                        return Success;
                    case "Send successfully":
                        return Success;
                    case "Accept successfully":
                        return Success;
                    case "Reject successfully":
                        return Success;
                    case "No message":
                        return NoMessage;
                    case "Delete successfully":
                        return Success;
                    default:
                        return webRequest.downloadHandler.text;
                }
            }
            else
            {
                return ServerNotFound;
            }
        }
    }
    private IEnumerator PostRequest(string url,string postData)
    {
        using (UnityWebRequest webRequest= new UnityWebRequest(url,UnityWebRequest.kHttpVerbPOST))
        {
            UploadHandler upload=new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(postData));
            webRequest.uploadHandler = upload;
            webRequest.uploadHandler.contentType= "application/json";

            DownloadHandler downloadHandler = new DownloadHandlerBuffer();
            webRequest.downloadHandler = downloadHandler;
            yield return webRequest.SendWebRequest();

            if (webRequest.result==UnityWebRequest.Result.Success)
            {
                //Debug.Log(webRequest.downloadHandler.text);
                switch(webRequest.downloadHandler.text)
                {
                    case "nickname exists":
                        throw new WebException(NicknameExist);
                    case "Sign up successfully":
                        throw new WebException(Success);
                    case "Player does not exist":
                        throw new WebException(PlayerNotExist);
                    case "Wrong password":
                        throw new WebException(PasswordError);
                    case "Log in successfully":
                        throw new WebException(Success);
                    case "Upload successfully":
                        throw new WebException(Success);
                    default:
                        throw new WebException(webRequest.downloadHandler.text);
                }
            }
            else
            {
                throw new WebException(ServerNotFound);
            }
        }
    }
}
