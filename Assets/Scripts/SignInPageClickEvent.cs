using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//此脚本用来管理注册界面的鼠标点击事件
public class SignInPageClickEvent : MonoBehaviour
{
    // Start is called before the first frame update
    AsyncOperation operation;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitSignIn()
    {
        transform.gameObject.SetActive(false);
    }
}
