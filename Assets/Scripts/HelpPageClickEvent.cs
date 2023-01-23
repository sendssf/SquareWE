using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//此脚本用来控制帮助界面的鼠标点击事件
public class HelpPageClickEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitHelp()
    {
        transform.gameObject.SetActive(false);
    }
}
