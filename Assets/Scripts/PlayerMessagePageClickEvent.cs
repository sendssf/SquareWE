using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//�˽ű��������������Ϣҳ���������¼�
public class PlayerMessagePageClickEvent : MonoBehaviour
{
    // Start is called before the first frame update
    AsyncOperation operation;		//�첽���������
    Transform logintrans;
    public Sprite pubImageHead;     //Ĭ��ͷ��
    public GameObject objItem;
    GameObject objContainer;        //��Ϸ��Ʒ����
    //ArrayList objs;              //�Ѿ���ʾ����Ʒ

    public Sprite PerspectiveCamera;
    public Sprite SmartPotion;
    public Sprite Hammer;
    public Sprite SuperiorHammer;
    public Sprite AddTimeClock;
    public Sprite SuperiorAddTimeClock;
    public Sprite ForgetPotion;
    public Sprite SuperiorForgetPotion;
    public Sprite Dictionary;
    public Sprite Eraser;
    public Sprite SuperiorEraser;

    void Start()
    {
        //imageType="png";
        objContainer=transform.Find("Body").Find("ObjectList").Find("RankContainer").Find("Viewport").Find("Content").gameObject;
        logintrans=transform.Find("Login");
        if (!AllMessageContainer.gameStatus.iflogin)
        {
            if (!logintrans.gameObject.activeSelf)
            {
                logintrans.gameObject.SetActive(true);
            }
        }
        if(AllMessageContainer.playerInfo.playerName!= null)
        {
            LoadPage();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void returnMainPage()    //������ҳ
    {
        StartCoroutine(loadScene("BeginUI"));	//����Э��
    }

    private IEnumerator loadScene(string which) //���س���
    {
        operation=SceneManager.LoadSceneAsync(which);
        yield return operation;
    }

    public void GotoSettings()  //ǰ������
    {
        transform.Find("Settings").gameObject.SetActive(true);
    }

    public void LoadPage()
    {
        transform.Find("Upleft").Find("BasicInfo").gameObject.GetComponent<Text>().text=
            $"{AllMessageContainer.playerInfo.playerName}\n{AllMessageContainer.playerInfo.playerAccount}";
        transform.Find("Upleft").Find("Level").Find("Num").gameObject.GetComponent<Text>().text=
            AllMessageContainer.playerInfo.level.ToString();
        transform.Find("Upleft").Find("Level").Find("FullExp").gameObject.GetComponent<Image>().fillAmount=
            ((float)AllMessageContainer.playerInfo.experience)/AllMessageContainer.fullExp[AllMessageContainer.playerInfo.level];
        transform.Find("Upleft").Find("Level").Find("ExpValue").gameObject.GetComponent<Text>().text=
            $"{AllMessageContainer.playerInfo.experience}/{AllMessageContainer.fullExp[AllMessageContainer.playerInfo.level]}";
        ShowObject();
        LoadHeadImage(false);
    }

    public void LoadHeadImage(bool isnew)     //����ͷ��
    {
        Texture2D image = new Texture2D(290, 290);
        FileInfo fileInfo = new FileInfo($"{Application.persistentDataPath}\\{AllMessageContainer.playerInfo.playerName}.png");
        if(!fileInfo.Exists)      //û��ͷ���ļ��ʹӷ�������ȡ
        {
            string res = WebController.GetHeadImage(WebController.rootIP + API_Local.sendAvater, AllMessageContainer.playerInfo.playerName);
            if (res==WebController.Success)
            {
                fileInfo=new FileInfo($"{Application.persistentDataPath}\\{AllMessageContainer.playerInfo.playerName}.png");
            }
            else
            {
                transform.Find("Upleft").Find("Head").Find("Mask").Find("HeadImage").gameObject.GetComponent<Image>().sprite=pubImageHead;
                return;
            }
        }
        image.LoadImage(GetImageByte(fileInfo.FullName));
        if (isnew)
        {
            CuttingController.LoadImage(image);
            transform.Find("Cutting").gameObject.SetActive(true);
            return;
        }
        Sprite sprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0f, 0f));
        transform.Find("Upleft").Find("Head").Find("Mask").Find("HeadImage").gameObject.GetComponent<Image>().sprite= sprite;
    }

    public static byte[] GetImageByte(string imagePath)
    {
        using (FileStream fs = new FileStream(imagePath, FileMode.Open))
        {
            byte[] imageByte = new byte[fs.Length];
            fs.Read(imageByte, 0, imageByte.Length);
            return imageByte;
        }
    }
    
    public void PressHeadImage()   //��ͷ���Ƶ�C�̵Ĵ洢·�������ͷ��󴥷����¼�
    {
        if (AllMessageContainer.gameStatus.iflogin)
        {
            int maxWidth = 1200;
            int maxHeight = 800;
            GameObject showImage = transform.Find("ShowHeadImage").Find("Image").gameObject;
            Texture2D image = new Texture2D(290, 290);
            FileInfo fileInfo = new FileInfo($"{Application.persistentDataPath}\\{AllMessageContainer.playerInfo.playerName}.png");
            if (!fileInfo.Exists)      //û��ͷ���ļ��ͼ���Ĭ��ͷ��
            {
                image=pubImageHead.texture;
            }
            else
            {
                image.LoadImage(GetImageByte(fileInfo.FullName));
            }
            transform.Find("ShowHeadImage").gameObject.SetActive(true);
            if (image.width<=maxWidth)
            {
                if (image.height<=maxHeight)
                {
                    showImage.GetComponent<RectTransform>().sizeDelta=new Vector2(image.width, image.height);
                }
                else
                {
                    showImage.GetComponent<RectTransform>().sizeDelta=new Vector2((int)(image.width/((double)image.height/maxHeight)), maxHeight);
                }
            }
            else
            {
                if (image.height<=maxHeight)
                {
                    showImage.GetComponent<RectTransform>().sizeDelta=new Vector2(maxWidth, (int)(image.height/((double)image.width/maxWidth)));
                }
                else
                {
                    float widthscale = (float)image.width/maxWidth;
                    float heightscale = (float)image.height/maxHeight;
                    if (heightscale>=widthscale)
                    {
                        showImage.GetComponent<RectTransform>().sizeDelta=new Vector2((int)(image.width/((double)image.height/maxHeight)), maxHeight);
                    }
                    else
                    {
                        showImage.GetComponent<RectTransform>().sizeDelta=new Vector2(maxWidth, (int)(image.height/((double)image.width/maxWidth)));
                    }
                }
            }
            showImage.GetComponent<Image>().sprite=Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f));
        }
        else
        {
            transform.Find("Login").gameObject.SetActive(true);
        }
    }

    public void ChooseHeadImage()
    {
        transform.Find("ShowHeadImage").gameObject.SetActive(false);
        string imagePath = FileBrowse.OpenWindowDialog("ѡ��ͷ��",
                "Image Files (*.png, *.jpg,*.jpeg,*.bmp)\0*.png;*.jpg;*.jpeg;*.bmp\0");
        if (imagePath==null)
        {
            return;
        }
        //ɾ����ͷ���ļ�
        FileInfo df = new FileInfo($"{Application.persistentDataPath}\\{AllMessageContainer.playerInfo.playerName}.png");
        if (df.Exists)
        {
            df.Delete();
        }
        //������ͷ��
        Texture2D image = new Texture2D(290, 290);
        image.LoadImage(GetImageByte(imagePath));
        byte[] destimage = image.EncodeToPNG();
        File.WriteAllBytes($"{Application.persistentDataPath}\\{AllMessageContainer.playerInfo.playerName}.png", destimage);
        LoadHeadImage(true);
    }

    public void QuitShowHeadImage()
    {
        transform.Find("ShowHeadImage").gameObject.SetActive(false);
    }

    public void QuitTips()
    {
        transform.Find("Tips").gameObject.SetActive(false);
    }

    public void GotoFriend()
    {
        if (AllMessageContainer.gameStatus.iflogin)
        {
            transform.Find("Friends").gameObject.SetActive(true);
        }
        else
        {
            ShowTips("This is the friend system. If you want to use it, you should sign up first.");
        }
    }

    public void QuitFriend()
    {
        transform.Find("Friends").gameObject.SetActive(false) ;
    }

    public void ShowObject()
    {
        int all = objContainer.transform.childCount;
        for(int i = 0; i<all; i++)
        {
            Destroy(objContainer.transform.GetChild(i).gameObject);
        }
        foreach(KeyValuePair<string,string> obj in AllMessageContainer.playerInfo.objectList)
        {
            if (obj.Key=="0")
            {
                continue;
            }
            var item=Instantiate(objItem, objContainer.transform);
            switch (obj.Key)
            {
                case "PerspectiveCamera":
                    item.transform.Find("Image").gameObject.GetComponent<Image>().sprite=PerspectiveCamera;
                    item.transform.Find("Name").gameObject.GetComponent<Text>().text="Perspective Camera";
                    break;
                case "SmartPotion":
                    item.transform.Find("Image").gameObject.GetComponent<Image>().sprite=SmartPotion;
                    item.transform.Find("Name").gameObject.GetComponent<Text>().text="Smart Potion";
                    break;
                case "Hammer":
                    item.transform.Find("Image").gameObject.GetComponent<Image>().sprite=Hammer;
                    item.transform.Find("Name").gameObject.GetComponent<Text>().text="Hammer";
                    break;
                case "SuperiorHammer":
                    item.transform.Find("Image").gameObject.GetComponent<Image>().sprite=SuperiorHammer;
                    item.transform.Find("Name").gameObject.GetComponent<Text>().text="Superior Hammer";
                    break;
                case "AddTimeClock":
                    item.transform.Find("Image").gameObject.GetComponent<Image>().sprite=AddTimeClock;
                    item.transform.Find("Name").gameObject.GetComponent<Text>().text="Add Time Clock";
                    break;
                case "SuperiorAddTimeClock":
                    item.transform.Find("Image").gameObject.GetComponent<Image>().sprite=SuperiorAddTimeClock;
                    item.transform.Find("Name").gameObject.GetComponent<Text>().text="Superior Add Time Clock";
                    break;
                case "ForgetPotion":
                    item.transform.Find("Image").gameObject.GetComponent<Image>().sprite=ForgetPotion;
                    item.transform.Find("Name").gameObject.GetComponent<Text>().text="Forget Potion";
                    break;
                case "SuperiorForgetPotion":
                    item.transform.Find("Image").gameObject.GetComponent<Image>().sprite=SuperiorForgetPotion;
                    item.transform.Find("Name").gameObject.GetComponent<Text>().text="Superior Forget Potion";
                    break;
                case "Dictionary":
                    item.transform.Find("Image").gameObject.GetComponent<Image>().sprite=Dictionary;
                    item.transform.Find("Name").gameObject.GetComponent<Text>().text="Dictionary";
                    break;
                case "Eraser":
                    item.transform.Find("Image").gameObject.GetComponent<Image>().sprite=Eraser;
                    item.transform.Find("Name").gameObject.GetComponent<Text>().text="Eraser";
                    break;
                case "SuperiorEraser":
                    item.transform.Find("Image").gameObject.GetComponent<Image>().sprite=SuperiorEraser;
                    item.transform.Find("Name").gameObject.GetComponent<Text>().text="Superior Eraser";
                    break;
            }

            item.transform.Find("Number").gameObject.GetComponent<Text>().text="Number:"+obj.Value;
        }
    }

    void ShowTips(string message)
    {
        transform.Find("Tips").gameObject.SetActive(true);
        transform.Find("Tips").Find("Contain").Find("Viewport").Find("Content").Find("Tip").gameObject
            .GetComponent<Text>().text=message;
    }
}
