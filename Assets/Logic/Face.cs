using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{
    public static char[] alphabet = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    
    char letter;
    int times;//点击次数
    public Collider coll;//碰撞器

    void Start()
    {
        letter = alphabet[Random.Range(0, 26)];//随机赋字母
        times = 0;
        coll = GetComponent<Collider>();

        for(int i = 0; i < 27; i++)
        {
            if (GameObject.Find("square" + i.ToString()).transform.childCount < 6)
            {
                this.transform.parent = GameObject.Find("square" + i.ToString()).transform;//设置父物体
                break;
            }
        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (coll.Raycast(ray, out hit, 100.0F))//检测到碰撞
            {
                times++;
                if (times == 3)//销毁小立方体
                {
                    GameObject father = this.transform.parent.gameObject;//获取父物体
                    for(int i = 0; i < 6; i++)
                    {
                        Destroy(father.transform.GetChild(i).gameObject);
                    }
                    Destroy(father);
                }
            }
        }
    }
}
