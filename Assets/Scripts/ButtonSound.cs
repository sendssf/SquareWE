using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//此脚本用来控制鼠标点击时的声音
public class ButtonSound : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip HighlightMusic;
    public AudioClip PressMusic;
    public AudioSource player;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        player.volume=AllMessageContainer.settingsInfo.effectSoundValue*AllMessageContainer.settingsInfo.totalSoundValue;
        if (AllMessageContainer.settingsInfo.totalSoundOpen && AllMessageContainer.settingsInfo.effectSoundOpen)
        {
            player.PlayOneShot(HighlightMusic);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (AllMessageContainer.settingsInfo.totalSoundOpen && AllMessageContainer.settingsInfo.effectSoundOpen)
            {
                player.PlayOneShot(PressMusic);
            }
        }
    }
}
