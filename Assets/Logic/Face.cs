 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{
    public static char[] alphabet = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    
    char letter;
    public int times;//�������
    public Collider coll;//��ײ��

    public bool _isClicked = false;
    public bool _isVisited = false;     //������ĸ��ʱ����

    void Start()
    {
        letter = alphabet[Random.Range(0, 26)];//�������ĸ
        times = 0;
        coll = GetComponent<Collider>();

        //for(int i = 0; i < 27; i++)
        //{
        //    if (GameObject.Find("square" + i.ToString()).transform.childCount < 6)
        //    {
        //        this.transform.parent = GameObject.Find("square" + i.ToString()).transform;//���ø�����
        //        break;
        //    }
        //}
    }


    void Update()
    {
    }
}
