using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//此脚本用来管理设置页面的鼠标点击事件
public class SettingsPageClickEvent : MonoBehaviour
{
    // Start is called before the first frame update
    bool isopen = true;
    bool isbackgroundopen = true;
    bool iseffectopen = true;
    public Sprite AllMusicOn;       //音效打开时的图片
    public Sprite AllMusicOff;      //音效关闭时的图片
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VolumeOnOff()   //更改总音量开关
    {
        isopen =!isopen;
        if (isopen)
        {
            transform.Find("Contain").Find("Viewport").Find("Content")
                .Find("TotalSound").Find("OnOff").gameObject.GetComponent<Image>().sprite= AllMusicOn;
        }
        else
        {
            transform.Find("Contain").Find("Viewport").Find("Content")
                .Find("TotalSound").Find("OnOff").gameObject.GetComponent<Image>().sprite= AllMusicOff;
        }
    }

    public void BackgroundVolumeOnOff() //更改背景音乐开关
    {
        isbackgroundopen = !isbackgroundopen;
        if(isbackgroundopen)
        {
            transform.Find("Contain").Find("Viewport").Find("Content")
               .Find("BackgroundSound").Find("OnOff").gameObject.GetComponent<Image>().sprite= AllMusicOn;
        }
        else
        {
            transform.Find("Contain").Find("Viewport").Find("Content")
               .Find("BackgroundSound").Find("OnOff").gameObject.GetComponent<Image>().sprite= AllMusicOff;
        }
    }

    public void EffectVolumeOnOff()     //更改音效开关
    {
        iseffectopen = !iseffectopen;
        if (iseffectopen)
        {
            transform.Find("Contain").Find("Viewport").Find("Content")
               .Find("EffectSound").Find("OnOff").gameObject.GetComponent<Image>().sprite= AllMusicOn;
        }
        else
        {
            transform.Find("Contain").Find("Viewport").Find("Content")
               .Find("EffectSound").Find("OnOff").gameObject.GetComponent<Image>().sprite= AllMusicOff;
        }
    }

    public void QuitSettings()      //退出设置
    {
        transform.gameObject.SetActive(false);
    }

    public void SettingsOK()        //完成设置
    {
        //add something to operate the input data

        transform.gameObject.SetActive(false);
    }
}
