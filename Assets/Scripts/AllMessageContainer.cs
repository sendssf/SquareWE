using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;

public enum ReadInfoState   //读取文件状态
{
    Success,
    FileNotExit,
    FileCannotRead,
    FileCannotWrite,
    PasswordError
}

public enum LevelFullExp    //每一级的最大经验值
{
    Level1=100, Level2=200, Level3=400,
    Level4=800, Level5=1200,Level6=1800,Level7=2500,Level8=3000,
    Level9=4000, Level10=5000, Level11=6500, Level12=8500, Level13=11000,
    Level14=14000, Level15=18000, Level16=25000, Level17=30000, Level18=37000,
    Level19=45000, Level20=55000, Level21=70000, Level22=90000, Level23=120000,
    Level24=150000, Level25=200000, Level26=300000, Level27=500000, Level28=700000,
    Level29=1000000, Level30=5000000
}

public enum GameMode
{
    BreakThrough_1=1, BreakThrough_2=2,BreakThrough_3=3,BreakThrough_4=4,
    BreakThrough_5=5, BreakThrough_6=6, BreakThrough_7=7, BreakThrough_8=8,
    BreakThrough_9=9, BreakThrough_10=10, BreakThrough_11=11, BreakThrough_12=12,
    BreakThrough_13=13, BreakThrough_14=14, BreakThrough_15=15,BreakThrough_16=16,
    BreakThrough_17=17, BreakThrough_18=18, BreakThrough_19=19, BreakThrough_20=20,
    BreakThrough_21=21, 
    CustomOthers=22,
    CustomRandom=30,
    Endless = 31
}

public struct PlayerInfo
{
    public string playerName;
    public string playerAccount;
    public string password;
    public string email;
    public Dictionary<string,string> worldList;
    public Dictionary<string, string> objectList;
    public List<string> friendList;   //键为昵称，值为info字典
    public int level;
    public int experience;
    public int rank;
    public int coin;
    public int crystal;
}

public struct RegistInfo
{
    public string nickname;
    public string password;
    public string emailaddr;
    public string account;
}

public struct LoginInfo
{
    public string nickname;
    public string password;
}

public struct SettingsInfo
{
    public bool totalSoundOpen;
    public bool backSoundOpen;
    public bool effectSoundOpen;
    public float totalSoundValue;
    public float backSoundValue;
    public float effectSoundValue;
}


public struct GameStatus
{
    public bool iflogin;
    public bool ifStartGame;
    public bool ifInit;
    public bool ifonline;
    public GameMode gameMode;
    public bool ifUpdateFriendImage;
    public bool changeFriendInfo;
    public string wordFileName;
    public bool ifExternList;
    public string overlayerName;
    public bool finalTry;
    public bool ifHost;
}

public class AllMessageContainer : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerInfo playerInfo = new PlayerInfo();
    public static RegistInfo registInfo = new RegistInfo();         //注册时用到的信息
    public static SettingsInfo settingsInfo = new SettingsInfo();
    public static LoginInfo loginInfo = new LoginInfo();              //登录时用到的信息
    public static GameStatus gameStatus = new GameStatus();
    public static int[] fullExp;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void MessageInitial()
    {
        gameStatus.iflogin=false;
        gameStatus.ifStartGame=false;
        gameStatus.ifInit=true;
        gameStatus.ifUpdateFriendImage=false;
        gameStatus.changeFriendInfo=false;
        gameStatus.ifExternList=false;
        gameStatus.ifonline = false;
        gameStatus.finalTry=false;
        gameStatus.ifHost=false;
        fullExp=new int[31];
        fullExp[0]=0; fullExp[1]=(int)LevelFullExp.Level1; fullExp[2]=(int)LevelFullExp.Level2;
        fullExp[3]=(int)LevelFullExp.Level3; fullExp[4]=(int)LevelFullExp.Level4; fullExp[5]=(int)LevelFullExp.Level5;
        fullExp[6]=(int)LevelFullExp.Level6; fullExp[7]=(int)LevelFullExp.Level7; fullExp[8]=(int)LevelFullExp.Level8;
        fullExp[9]=(int)LevelFullExp.Level9; fullExp[10]=(int)LevelFullExp.Level10; fullExp[11]=(int)LevelFullExp.Level11;
        fullExp[12]=(int)LevelFullExp.Level12; fullExp[13]=(int)LevelFullExp.Level13; fullExp[14]=(int)LevelFullExp.Level14;
        fullExp[15]=(int)LevelFullExp.Level15; fullExp[16]=(int)LevelFullExp.Level16; fullExp[17]=(int)LevelFullExp.Level17;
        fullExp[18]=(int)LevelFullExp.Level18; fullExp[19]=(int)LevelFullExp.Level19; fullExp[20]=(int)LevelFullExp.Level20;
        fullExp[21]=(int)LevelFullExp.Level21; fullExp[22]=(int)LevelFullExp.Level22; fullExp[23]=(int)LevelFullExp.Level23;
        fullExp[24]=(int)LevelFullExp.Level24; fullExp[25]=(int)LevelFullExp.Level25; fullExp[26]=(int)LevelFullExp.Level26;
        fullExp[27]=(int)LevelFullExp.Level27; fullExp[28]=(int)LevelFullExp.Level28; fullExp[29]=(int)LevelFullExp.Level29;
        fullExp[30]=(int)LevelFullExp.Level30;
        ResetPlayerInfo();
        playerInfo.worldList=new Dictionary<string, string>();
        playerInfo.objectList=new Dictionary<string, string>();
        playerInfo.friendList=new List<string>();
        var files = new DirectoryInfo(Application.persistentDataPath).GetFiles("*.json");
        if (files.Length==0)    //如果没保存用户的数据
        {
            settingsInfo.totalSoundValue=1.0f;
            settingsInfo.backSoundValue=0.25f;
            settingsInfo.effectSoundValue=0.25f;
            settingsInfo.totalSoundOpen=true;
            settingsInfo.backSoundOpen=true;
            settingsInfo.effectSoundOpen=true;
        }
        else
        {
            //对文件按照写入时间排序
            Array.Sort(files,delegate(FileInfo x,FileInfo y)
            {
                return y.LastWriteTime.CompareTo(x.LastWriteTime);
            });

            //取最近一次写入的文件载入
            ReadInfoFromFile(files[0].Name);
        }
    }

    public static void WriteInfoToFile(string filename)
    {
        Dictionary<string, object> info=new Dictionary<string, object>();
        Dictionary<string,string> playerInfo=new Dictionary<string,string>();
        Dictionary<string,string> settingsInfo=new Dictionary<string,string>();
        List<string> friendInfo = new List<string>();

        playerInfo.Add("playerName", AllMessageContainer.playerInfo.playerName);
        playerInfo.Add("password", AllMessageContainer.playerInfo.password);
        playerInfo.Add("playerAccount",AllMessageContainer.playerInfo.playerAccount);
        playerInfo.Add("email", AllMessageContainer.playerInfo.email);
        playerInfo.Add("coin", AllMessageContainer.playerInfo.coin.ToString());
        playerInfo.Add("crystal",AllMessageContainer.playerInfo.crystal.ToString());
        playerInfo.Add("level",AllMessageContainer.playerInfo.level.ToString());
        playerInfo.Add("experience", AllMessageContainer.playerInfo.experience.ToString());
        playerInfo.Add("rank", AllMessageContainer.playerInfo.rank.ToString());

        settingsInfo.Add("effectSoundValue",AllMessageContainer.settingsInfo.effectSoundValue.ToString());
        settingsInfo.Add("effectSoundOpen",AllMessageContainer.settingsInfo.effectSoundOpen.ToString());
        settingsInfo.Add("totalSoundValue", AllMessageContainer.settingsInfo.totalSoundValue.ToString());
        settingsInfo.Add("totalSoundOpen",AllMessageContainer.settingsInfo.totalSoundOpen.ToString());
        settingsInfo.Add("backSoundValue",AllMessageContainer.settingsInfo.backSoundValue.ToString());
        settingsInfo.Add("backSoundOpen",AllMessageContainer.settingsInfo.backSoundOpen.ToString());

        if (AllMessageContainer.playerInfo.worldList.Count==0)
        {
            AllMessageContainer.playerInfo.worldList.Add("0", "0");
        }
        if(AllMessageContainer.playerInfo.objectList.Count==0)
        {
            AllMessageContainer.playerInfo.objectList.Add("0", "0");
        }
        if (AllMessageContainer.playerInfo.friendList.Count==0)
        {
            AllMessageContainer.playerInfo.friendList.Add("0");
        }

        foreach (string frd in AllMessageContainer.playerInfo.friendList)
        {
            friendInfo.Add(frd);
        }

        info.Add("playerInfo", playerInfo);
        info.Add("settingsInfo", settingsInfo);
        info.Add("wordList", AllMessageContainer.playerInfo.worldList);
        info.Add("objectList", AllMessageContainer.playerInfo.objectList);
        info.Add("friendList", friendInfo);

        using(FileStream fs=new FileStream(Application.persistentDataPath + @"\\" + filename, FileMode.OpenOrCreate)) 
        {
            fs.Seek(0, SeekOrigin.Begin);
            fs.SetLength(0);
            using(StreamWriter sw=new StreamWriter(fs))
            {
                sw.Write(JsonConvert.SerializeObject(info));
            }
        }
    }

    public static ReadInfoState ReadInfoFromFile(string filename)
    {
        //清除已经载入的字典数据
        if(playerInfo.worldList.Count!=0)
        {
            playerInfo.worldList.Clear();
        }
        if(playerInfo.objectList.Count!=0)
        {
            playerInfo.objectList.Clear();
        }
        if (playerInfo.friendList.Count!=0)
        {
            playerInfo.friendList.Clear();
        }

        string content;
        using (FileStream fs = new FileStream(Application.persistentDataPath+@"\\"+filename, FileMode.Open))
        {
            if (fs==null)
            {
                return ReadInfoState.FileNotExit;
            }
            using (StreamReader sr = new StreamReader(fs))
            {
                if (sr==null)
                {
                    return ReadInfoState.FileCannotRead;
                }
                content= sr.ReadToEnd();
            }
        }
        Dictionary<string, object> info =
            JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
        Dictionary<string, string> wordlst = JsonConvert.DeserializeObject<Dictionary<string,string>>(info["wordList"].ToString());
        Dictionary<string, string> objectlst = JsonConvert.DeserializeObject<Dictionary<string, string>>(info["objectList"].ToString());
        Dictionary<string, string> plrInfo = JsonConvert.DeserializeObject<Dictionary<string, string>>(info["playerInfo"].ToString());
        Dictionary<string, string> setInfo = JsonConvert.DeserializeObject<Dictionary<string, string>>(info["settingsInfo"].ToString());
        List<string> friendlst = JsonConvert.DeserializeObject<List<string>>(info["friendList"].ToString());
        foreach (KeyValuePair<string, string> word in wordlst)
        {
            playerInfo.worldList.Add(word.Key, word.Value);
        }
        foreach (KeyValuePair<string, string> obj in objectlst)
        {
            playerInfo.objectList.Add(obj.Key, obj.Value);
        }
        foreach(string frd in friendlst)
        {
            playerInfo.friendList.Add(frd);
        }
        playerInfo.playerName=plrInfo["playerName"];
        playerInfo.password=plrInfo["password"];
        playerInfo.playerAccount=plrInfo["playerAccount"];
        playerInfo.email=plrInfo["email"];
        playerInfo.level=Convert.ToInt32(plrInfo["level"]);
        playerInfo.coin=Convert.ToInt32(plrInfo["coin"]);
        playerInfo.crystal=Convert.ToInt32(plrInfo["crystal"]);
        playerInfo.experience=Convert.ToInt32(plrInfo["experience"]);
        playerInfo.rank=Convert.ToInt32(plrInfo["rank"]);

        settingsInfo.totalSoundValue=(float)Convert.ToDouble(setInfo["totalSoundValue"]);
        settingsInfo.totalSoundOpen=Convert.ToBoolean(setInfo["totalSoundOpen"]);
        settingsInfo.backSoundValue=(float)Convert.ToDouble(setInfo["backSoundValue"]);
        settingsInfo.backSoundOpen=Convert.ToBoolean(setInfo["backSoundOpen"]);
        settingsInfo.effectSoundValue=(float)Convert.ToDouble(setInfo["effectSoundValue"]);
        settingsInfo.effectSoundOpen=Convert.ToBoolean(setInfo["effectSoundOpen"]);
        return ReadInfoState.Success;
    }

    public static void ResetPlayerInfo()
    {
        playerInfo.playerName="NickName"; //函数结束后依然为这个说明载入失败或没有文件
        playerInfo.playerAccount="Account";
        playerInfo.coin=0;
        playerInfo.crystal=0;
        playerInfo.experience=0;
        playerInfo.rank=0;
        playerInfo.level=1;
    }

    public static List<string> GetFriendListFromServer(string nickname)
    {
        var res = new Dictionary<string, string>();
        string result = WebController.Post(WebController.rootIP + API_Local.allInfo,
            JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                {"nickname",nickname }
            }));
        if (result==WebController.ServerNotFound||result==WebController.PlayerNotExist)
        {
            return null;
        }
        else
        {
            var allinfo = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
            var frdList = JsonConvert.DeserializeObject<List<string>>(allinfo["friendList"].ToString());
            return frdList;
        }
    }

    public static Dictionary<string,string> GetFriendsInfoFromServer(string nickname)
    {
        var res=new Dictionary<string,string>();
        string result = WebController.Post(WebController.rootIP + API_Local.allInfo,
            JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                {"nickname",nickname }
            }));
        if (result==WebController.ServerNotFound||result==WebController.PlayerNotExist)
        {
            return null;
        }
        else
        {
            var allinfo=JsonConvert.DeserializeObject<Dictionary<string,object>>(result);
            var plyInfo = JsonConvert.DeserializeObject<Dictionary<string, string>>(allinfo["playerInfo"].ToString());
            var wordlst = JsonConvert.DeserializeObject<Dictionary<string, string>>(allinfo["wordList"].ToString());

            res.Add("level", plyInfo["level"]);
            res.Add("rank", plyInfo["rank"]);
            res.Add("wordnumber", wordlst.Count.ToString());
            res.Add("experience", plyInfo["experience"]);
        }
        return res;
    }

    public static string SendBasicInfo()
    {
        string res = WebController.Post(WebController.rootIP + API_Local.postInfo,
            JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                {"nickname",playerInfo.playerName },
                {"coin",playerInfo.coin.ToString() },
                {"crystal",playerInfo.crystal.ToString() },
                {"level",playerInfo.level.ToString() },
                {"experience",playerInfo.experience.ToString() },
                {"rank",playerInfo.experience.ToString() }
            }));
        return res;
    }

    public static string SendSettingsInfo()
    {
        string res = WebController.Post(WebController.rootIP + API_Local.postSettings,
            JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                {"nickname",AllMessageContainer.playerInfo.playerName },
                {"totalSoundOpen",AllMessageContainer.settingsInfo.totalSoundOpen.ToString() },
                {"backSoundOpen",AllMessageContainer.settingsInfo.backSoundOpen.ToString() },
                {"effectSoundOpen",AllMessageContainer.settingsInfo.effectSoundOpen.ToString() },
                {"totalSoundValue",AllMessageContainer.settingsInfo.totalSoundValue.ToString() },
                {"backSoundValue",AllMessageContainer.settingsInfo.backSoundValue.ToString()},
                {"effectSoundValue",AllMessageContainer.settingsInfo.effectSoundValue.ToString()}
            }));
        return res;
    }

    static public void UpdateLevel()
    {
        for(; ; )
        {
            if (playerInfo.experience >= fullExp[playerInfo.level])
            {
                playerInfo.level++;
                playerInfo.experience -=fullExp[playerInfo.level - 1];
                playerInfo.crystal += 10;
                playerInfo.coin += 100;
                SendBasicInfo();
            }
            else
            {
                break;
            }
        }
    }
}
