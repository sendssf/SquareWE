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
    //string imageType;           
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
        FileInfo[] fileInfo = new DirectoryInfo(Application.persistentDataPath).GetFiles($"{AllMessageContainer.playerInfo.playerName}Head.*");
        if(fileInfo.Length==0)      //û��ͷ���ļ��ͼ���Ĭ��ͷ��
        {
            transform.Find("Upleft").Find("Head").Find("Mask").Find("HeadImage").gameObject.GetComponent<Image>().sprite=pubImageHead;
            return;
        }
        image.LoadImage(GetImageByte(fileInfo[0].FullName));
        if (isnew)
        {
            transform.Find("Cutting").gameObject.SetActive(true);
            CuttingController.LoadImage(image);
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

    public void ChooseHeadImage()   //��ͷ���Ƶ�C�̵Ĵ洢·�������ͷ��󴥷����¼�
    {
        string imagePath=FileBrowse.OpenWindowDialog("ѡ��ͷ��", 
            "Image Files (*.png, *.jpg,*.jpeg,*.bmp)\0*.png;*.jpg;*.jpeg;*.bmp\0");
        if (imagePath==null)
        {
            return;
        }
        //ɾ����ͷ���ļ�
        FileInfo[] df = new DirectoryInfo(Application.persistentDataPath).GetFiles($"{AllMessageContainer.playerInfo.playerName}Head.*");
        if (df.Length!=0)
        {
            foreach (FileInfo file in df)
            {
                file.Delete();
            }
        }
        //������ͷ��
        FileInfo finfo = new FileInfo(imagePath);
        finfo.CopyTo(Path.Combine(Application.persistentDataPath, $"{AllMessageContainer.playerInfo.playerName}Head{finfo.Extension}"));
        LoadHeadImage(true);
    }
}
