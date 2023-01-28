using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void PressOK()
    {
        //Add something to operate the input data
        //��½����
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
