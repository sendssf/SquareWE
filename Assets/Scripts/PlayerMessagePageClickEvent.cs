using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//�˽ű��������������Ϣҳ���������¼�
public class PlayerMessagePageClickEvent : MonoBehaviour
{
    // Start is called before the first frame update
    AsyncOperation operation;		//�첽���������
    Transform logintrans;
    void Start()
    {
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
    }
}
