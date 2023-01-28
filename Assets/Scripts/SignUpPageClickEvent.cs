using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//此脚本用来管理登陆界面的鼠标点击事件
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
        //登陆操作
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
