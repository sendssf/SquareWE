using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�˽ű�������������ҳ���������¼�
public class SettingsPageClickEvent : MonoBehaviour
{
    // Start is called before the first frame update
    bool isopen = true;
    bool isbackgroundopen = true;
    bool iseffectopen = true;
    public Sprite AllMusicOn;       //��Ч��ʱ��ͼƬ
    public Sprite AllMusicOff;      //��Ч�ر�ʱ��ͼƬ
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VolumeOnOff()   //��������������
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

    public void BackgroundVolumeOnOff() //���ı������ֿ���
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

    public void EffectVolumeOnOff()     //������Ч����
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

    public void QuitSettings()      //�˳�����
    {
        transform.gameObject.SetActive(false);
    }

    public void SettingsOK()        //�������
    {
        //add something to operate the input data

        transform.gameObject.SetActive(false);
    }
}
