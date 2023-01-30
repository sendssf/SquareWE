using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerInfo
{
    public string playerName;
    public string playerAccount;
    public string password;
    public string email;
    public string[] worldList;
    public Dictionary<string, string> objectList;
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
}

public class AllMessageContainer : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerInfo playerInfo = new PlayerInfo();
    public static RegistInfo registInfo = new RegistInfo();         //注册时用到的信息
    public static SettingsInfo settingsInfo = new SettingsInfo();
    public static LoginInfo loginInfo = new LoginInfo();              //登录时用到的信息
    public static GameStatus gameStatus = new GameStatus();       

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
        settingsInfo.totalSoundValue=1.0f;
        settingsInfo.backSoundValue=0.25f;
        settingsInfo.effectSoundValue=0.25f;
        settingsInfo.totalSoundOpen=true;
        settingsInfo.backSoundOpen=true;
        settingsInfo.effectSoundOpen=true;
    }
}
