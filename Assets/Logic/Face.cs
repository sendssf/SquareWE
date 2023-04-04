 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{
    public static char[] alphabet = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    
    char letter;
    public int times;//点击次数
    public Collider coll;//碰撞器

    public bool _isClicked = false;
    public bool _isVisited = false;     //生成字母的时候用

    void Start()
    {
        letter = alphabet[Random.Range(0, 26)];//随机赋字母
        times = 0;
        coll = GetComponent<Collider>();

        //for(int i = 0; i < 27; i++)
        //{
        //    if (GameObject.Find("square" + i.ToString()).transform.childCount < 6)
        //    {
        //        this.transform.parent = GameObject.Find("square" + i.ToString()).transform;//设置父物体
        //        break;
        //    }
        //}
    }


    void Update()
    {
    }
}
