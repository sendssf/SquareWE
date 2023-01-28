using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{
    public static char[] alphabet = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    
    char letter;
    int times;//�������
    public Collider coll;//��ײ��

    void Start()
    {
        letter = alphabet[Random.Range(0, 26)];//�������ĸ
        times = 0;
        coll = GetComponent<Collider>();

        for(int i = 0; i < 27; i++)
        {
            if (GameObject.Find("square" + i.ToString()).transform.childCount < 6)
            {
                this.transform.parent = GameObject.Find("square" + i.ToString()).transform;//���ø�����
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
            if (coll.Raycast(ray, out hit, 100.0F))//��⵽��ײ
            {
                times++;
                if (times == 3)//����С������
                {
                    GameObject father = this.transform.parent.gameObject;//��ȡ������
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
