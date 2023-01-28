using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerInfo
{
    public string playerName;
    public string playerAccount;
    public string secretKey;
    public string[] worldList;
    public Dictionary<string, string> objectList;
    public int level;
    public int experience;
    public int rank;
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

public class AllMessageContainer : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerInfo playerInfo = new PlayerInfo();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
