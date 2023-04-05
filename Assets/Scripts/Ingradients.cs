using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingradients : MonoBehaviour
{
    public GameObject cube;
    public static int num;
    // Start is called before the first frame update
    void Start()
    {
        int[] arr = new int[10] {0,2,5,6,8,9,14,25,16,21};
        num = 3;
        Generate(num,arr);
    }

    void Generate(int num)//几维正方体
    {
        for (int i = 0; i < num*num*num; i++)
        {
            GameObject cube1 = Instantiate(cube, this.transform);
            cube1.name = "cube" + i.ToString();
            cube1.transform.parent = this.transform;
        }
        
        GameObject[] cube0 = new GameObject[num*num*num];
        for (int j = 0; j < num; j++)
        {
            float a = -(num - 1) / 2;
            for (int i = 0; i < num*num; i++)
            {
                if (i % num == 0 && i != 0)
                {
                    a += 1;
                }
                cube0[i + j * num * num] = GameObject.Find("cube" + (i + j * num * num));
                Vector3 pos = new Vector3(i % num - (num - 1) / 2, (num - 1) / 2 - j, a);      
                cube0[i + j * num * num].transform.localPosition = pos;
            }
        }
        this.gameObject.AddComponent<WholeCube>();
    }

    void Generate(int num, params int[] arr)//几维正方体
    {
        for (int i = 0; i < num * num * num; i++)
        {
            GameObject cube1 = Instantiate(cube, this.transform);
            cube1.name = "cube" + i.ToString();
            cube1.transform.parent = this.transform;
        }
        this.gameObject.AddComponent<WholeCube>();
        GameObject[] cube0 = new GameObject[num * num * num];
        for (int j = 0; j < num; j++)
        {
            float a = -(num - 1) / 2;
            for (int i = 0; i < num * num; i++)
            {
                if (i % num == 0 && i != 0)
                {
                    a += 1;
                }
                cube0[i + j * num * num] = GameObject.Find("cube" + (i + j * num * num));
                Vector3 pos = new Vector3(i % num - (num - 1) / 2, (num - 1) / 2 - j, a);
                cube0[i + j * num * num].transform.position = pos;
            }
        }
        foreach (int i in arr)
        {
            Destroy(GameObject.Find("cube" + i));
        }
        this.gameObject.AddComponent<WholeCube>();                                                                                                       
    }
}