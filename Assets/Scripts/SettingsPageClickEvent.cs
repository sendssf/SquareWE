using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//�˽ű�������������ҳ���������¼�
public class SettingsPageClickEvent : MonoBehaviour
{
    // Start is called before the first frame update
    //bool isopen = true;
    //bool isbackgroundopen = true;
    //bool iseffectopen = true;
    public Sprite AllMusicOn;       //��Ч��ʱ��ͼƬ
    public Sprite AllMusicOff;      //��Ч�ر�ʱ��ͼƬ
    public GameObject Register;
    public GameObject Login;
    AsyncOperation operation;
    string ipNew;
    string portNew;

    Transform accounttrans;
    void Start()
    {
        accounttrans=transform.Find("Contain").Find("Viewport").Find("Content").Find("Account");
    }

    // Update is called once per frame
    void Update()
    {
        //�������ý��������˺Ź���ť���Ƿ񼤻�״̬
        if (!AllMessageContainer.gameStatus.ifStartGame)
        {
            if (AllMessageContainer.gameStatus.iflogin)
            {
                accounttrans.Find("SignUp").gameObject.GetComponent<Button>().interactable = false;
                accounttrans.Find("Checkout").gameObject.GetComponent<Button>().interactable = true;
            }
            else
            {
                accounttrans.Find("SignUp").gameObject.GetComponent<Button>().interactable = true;
                accounttrans.Find("Checkout").gameObject.GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            accounttrans.Find("SignUp").gameObject.GetComponent<Button>().interactable = false;
            accounttrans.Find("Checkout").gameObject.GetComponent<Button>().interactable = false;
            accounttrans.Find("SignIn").gameObject.GetComponent<Button>().interactable= false;
        }
    }

    private void OnEnable()
    {
        transform.Find("Contain").Find("Viewport").Find("Content")
                .Find("TotalSound").Find("Slider").gameObject.GetComponent<Slider>().value=AllMessageContainer.settingsInfo.totalSoundValue;
        transform.Find("Contain").Find("Viewport").Find("Content")
               .Find("BackgroundSound").Find("Slider").gameObject.GetComponent<Slider>().value=AllMessageContainer.settingsInfo.backSoundValue;
        transform.Find("Contain").Find("Viewport").Find("Content")
               .Find("EffectSound").Find("Slider").gameObject.GetComponent<Slider>().value=AllMessageContainer.settingsInfo.effectSoundValue;
        if (AllMessageContainer.settingsInfo.totalSoundOpen)
        {
            transform.Find("Contain").Find("Viewport").Find("Content")
                .Find("TotalSound").Find("OnOff").gameObject.GetComponent<Image>().sprite= AllMusicOn;
            transform.Find("Contain").Find("Viewport").Find("Content")
                .Find("TotalSound").Find("Slider").gameObject.GetComponent<Slider>().interactable=true;
        }
        else
        {
            transform.Find("Contain").Find("Viewport").Find("Content")
               .Find("TotalSound").Find("OnOff").gameObject.GetComponent<Image>().sprite= AllMusicOff;
            transform.Find("Contain").Find("Viewport").Find("Content")
                .Find("TotalSound").Find("Slider").gameObject.GetComponent<Slider>().interactable=false;
        }
        if(AllMessageContainer.settingsInfo.backSoundOpen)
        {
            transform.Find("Contain").Find("Viewport").Find("Content")
               .Find("BackgroundSound").Find("OnOff").gameObject.GetComponent<Image>().sprite= AllMusicOn;
            transform.Find("Contain").Find("Viewport").Find("Content")
               .Find("BackgroundSound").Find("Slider").gameObject.GetComponent<Slider>().interactable= true;
        }
        else
        {
            transform.Find("Contain").Find("Viewport").Find("Content")
               .Find("BackgroundSound").Find("OnOff").gameObject.GetComponent<Image>().sprite= AllMusicOff;
            transform.Find("Contain").Find("Viewport").Find("Content")
               .Find("BackgroundSound").Find("Slider").gameObject.GetComponent<Slider>().interactable= false;
        }
        if(AllMessageContainer.settingsInfo.effectSoundOpen)
        {
            transform.Find("Contain").Find("Viewport").Find("Content")
               .Find("EffectSound").Find("OnOff").gameObject.GetComponent<Image>().sprite= AllMusicOn;
            transform.Find("Contain").Find("Viewport").Find("Content")
               .Find("EffectSound").Find("Slider").gameObject.GetComponent<Slider>().interactable= true;
        }
        else
        {
            transform.Find("Contain").Find("Viewport").Find("Content")
               .Find("EffectSound").Find("OnOff").gameObject.GetComponent<Image>().sprite= AllMusicOff;
            transform.Find("Contain").Find("Viewport").Find("Content")
               .Find("EffectSound").Find("Slider").gameObject.GetComponent<Slider>().interactable= false;
        }
        transform.Find("Contain").Find("Viewport").Find("Content").Find("IP").Find("OldValue").gameObject
            .GetComponent<Text>().text = WebController.rootIP.Split("//")[1].Split(":")[0];
        transform.Find("Contain").Find("Viewport").Find("Content").Find("IP").Find("OldPort").gameObject
            .GetComponent<Text>().text = "Port:"+WebController.rootIP.Split(":")[2];
        transform.Find("Contain").Find("Viewport").Find("Content").Find("IP").Find("GetIP").Find("IP")
            .gameObject.GetComponent<Text>().text = "";
        transform.Find("Contain").Find("Viewport").Find("Content").Find("IP").Find("GetPort").Find("Port")
            .gameObject.GetComponent<Text>().text = "";
    }

    public void VolumeOnOff()   //��������������
    {
        AllMessageContainer.settingsInfo.totalSoundOpen=!AllMessageContainer.settingsInfo.totalSoundOpen;
        if (AllMessageContainer.settingsInfo.totalSoundOpen)
        {
            transform.Find("Contain").Find("Viewport").Find("Content")
                .Find("TotalSound").Find("OnOff").gameObject.GetComponent<Image>().sprite= AllMusicOn;
            transform.Find("Contain").Find("Viewport").Find("Content")
                .Find("TotalSound").Find("Slider").gameObject.GetComponent<Slider>().interactable=true;
        }
        else
        {
            transform.Find("Contain").Find("Viewport").Find("Content")
                .Find("TotalSound").Find("OnOff").gameObject.GetComponent<Image>().sprite= AllMusicOff;
            transform.Find("Contain").Find("Viewport").Find("Content")
                .Find("TotalSound").Find("Slider").gameObject.GetComponent<Slider>().interactable=false;
        }
    }

    public void BackgroundVolumeOnOff() //���ı������ֿ���
    {
        AllMessageContainer.settingsInfo.backSoundOpen=!AllMessageContainer.settingsInfo.backSoundOpen;
        if(AllMessageContainer.settingsInfo.backSoundOpen)
        {
            transform.Find("Contain").Find("Viewport").Find("Content")
               .Find("BackgroundSound").Find("OnOff").gameObject.GetComponent<Image>().sprite= AllMusicOn;
            transform.Find("Contain").Find("Viewport").Find("Content")
               .Find("BackgroundSound").Find("Slider").gameObject.GetComponent<Slider>().interactable= true;
        }
        else
        {
            transform.Find("Contain").Find("Viewport").Find("Content")
               .Find("BackgroundSound").Find("OnOff").gameObject.GetComponent<Image>().sprite= AllMusicOff;
            transform.Find("Contain").Find("Viewport").Find("Content")
               .Find("BackgroundSound").Find("Slider").gameObject.GetComponent<Slider>().interactable= false;
        }
    }

    public void EffectVolumeOnOff()     //������Ч����
    {
        AllMessageContainer.settingsInfo.effectSoundOpen=!AllMessageContainer.settingsInfo.effectSoundOpen;
        if (AllMessageContainer.settingsInfo.effectSoundOpen)
        {
            transform.Find("Contain").Find("Viewport").Find("Content")
               .Find("EffectSound").Find("OnOff").gameObject.GetComponent<Image>().sprite= AllMusicOn;
            transform.Find("Contain").Find("Viewport").Find("Content")
               .Find("EffectSound").Find("Slider").gameObject.GetComponent<Slider>().interactable= true;
        }
        else
        {
            transform.Find("Contain").Find("Viewport").Find("Content")
               .Find("EffectSound").Find("OnOff").gameObject.GetComponent<Image>().sprite= AllMusicOff;
            transform.Find("Contain").Find("Viewport").Find("Content")
               .Find("EffectSound").Find("Slider").gameObject.GetComponent<Slider>().interactable= false;
        }
    }

    public void QuitSettings()      //�˳�����
    {
        transform.gameObject.SetActive(false);
    }

    IEnumerator unloadScene(string name)
    {
        operation=SceneManager.UnloadSceneAsync(name);
        yield return operation;
    }

    public void SettingsOK()        //�������
    {
        //add something to operate the input data
        WebController.rootIP = $"http://{ipNew}:{portNew}";
        AllMessageContainer.SendSettingsInfo();
        transform.gameObject.SetActive(false);
    }

    public void AccountSignUp()     //��¼
    {
        transform.parent.Find("Login").gameObject.SetActive(true);
        transform.gameObject.SetActive(false);
    }

    public void AccountSignIn()     //ע��
    {
        transform.parent.Find("Register").gameObject.SetActive(true) ;
        transform.gameObject.SetActive(false);
    }

    public void AccountCheckout()   //�˳���¼
    {
        AllMessageContainer.ResetPlayerInfo();
        if (transform.parent.name=="PlayerMessage")
        {
            transform.parent.gameObject.GetComponent<PlayerMessagePageClickEvent>().LoadPage();
        }
        AllMessageContainer.gameStatus.iflogin=false;
        AllMessageContainer.gameStatus.changeFriendInfo=true;
        AllMessageContainer.loginInfo.password = string.Empty;
        AllMessageContainer.loginInfo.nickname = string.Empty;
        FriendsController.friendMessageAll.Clear();
        transform.gameObject.SetActive(false);
    }

    public void TotalVolumeChange(float volume)
    {
        AllMessageContainer.settingsInfo.totalSoundValue=volume;
    }

    public void BackVolumeChange(float volume)
    {
        AllMessageContainer.settingsInfo.backSoundValue=volume;
    }

    public void EffectVolumeChange(float volume)
    {
        AllMessageContainer.settingsInfo.effectSoundValue=volume;
    }

    public void UpdateIP(string ip)
    {
        ipNew = ip;
    }

    public void UpdatePort(string port)
    {
        portNew = port;
    }
}
