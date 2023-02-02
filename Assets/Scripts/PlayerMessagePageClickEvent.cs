using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//�˽ű��������������Ϣҳ���������¼�
public class PlayerMessagePageClickEvent : MonoBehaviour
{
    // Start is called before the first frame update
    AsyncOperation operation;		//�첽���������
    Transform logintrans;
    public Sprite pubImageHead;     //Ĭ��ͷ��

    void Start()
    {
        //imageType="png";
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
        LoadHeadImage(false);
    }

    public void LoadHeadImage(bool isnew)     //����ͷ��
    {
        Texture2D image = new Texture2D(290, 290);
        FileInfo fileInfo = new FileInfo($"{Application.persistentDataPath}\\{AllMessageContainer.playerInfo.playerName}.png");
        if(!fileInfo.Exists)      //û��ͷ���ļ��ͼ���Ĭ��ͷ��
        {
            transform.Find("Upleft").Find("Head").Find("Mask").Find("HeadImage").gameObject.GetComponent<Image>().sprite=pubImageHead;
            return;
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

    static byte[] GetImageByte(string imagePath)
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
}
