using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

//�˽ű����������½�����������¼�
public class SignUpPageClickEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitSignUp()
    {
        transform.gameObject.SetActive(false);
    }

    public void PressSignIn()
    {
        transform.parent.Find("Register").gameObject.SetActive(true);
        transform.gameObject.SetActive(false);
    }

    public void PressOK()       //ȷ�ϵ�¼
    {
        //Add something to operate the input data
        //��½����
        if(AllMessageContainer.playerInfo.playerName!="NickName" && 
            AllMessageContainer.playerInfo.playerName==AllMessageContainer.loginInfo.nickname)
        {
            if(AllMessageContainer.playerInfo.password==AllMessageContainer.loginInfo.password)
            {
                //��½�ɹ�
                AllMessageContainer.gameStatus.iflogin=true;
            }
            else
            {
                transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                    .gameObject.GetComponent<Text>().text="The password is error! Try again!";
                return;
            }
        }
        else
        {
            //ȷ��������û��Ƿ���ڣ�Ŀǰֻ�ڱ���ȷ��
            if (File.Exists(Application.persistentDataPath+@"\\"+AllMessageContainer.loginInfo.nickname+".json"))
            {
                AllMessageContainer.ReadInfoFromFile(AllMessageContainer.loginInfo.nickname+".json");
                //�ж������Ƿ���ȷ
                if (AllMessageContainer.playerInfo.password==AllMessageContainer.loginInfo.password)
                {
                    AllMessageContainer.gameStatus.iflogin=true;
                    if (transform.parent.name=="PlayerMessage")
                    {
                        transform.parent.gameObject.GetComponent<PlayerMessagePageClickEvent>().LoadPage();
                    }
                }
                else
                {
                    transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                    .gameObject.GetComponent<Text>().text="The password is error! Try again!";
                    return;
                }
            }
            else    //�û�������
            {
                transform.Find("Contain").Find("Viewport").Find("Content").Find("ErrorTips")
                    .gameObject.GetComponent<Text>().text="The user is not exist! You can create a new account!";
                return;
            }
        }
        transform.gameObject.SetActive(false);
    }

    public void GetNickname(string nickname)
    {
        AllMessageContainer.loginInfo.nickname=nickname;
    }

    public void GetPassword(string password)
    {
        AllMessageContainer.loginInfo.password=password;
    }
}
