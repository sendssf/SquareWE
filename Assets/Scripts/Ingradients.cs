using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingradients : MonoBehaviour
{
    public GameObject cube;
    // Start is called before the first frame update
    private void Awake()
    {
    }
    void Start()
    {
        //int[] arr = new int[10] {0,2,5,6,8,9,14,25,16,21};
        //Generate(5);
        InitCubeShape();
    }

    public void InitCubeShape()
    {
        int[] arr;
        switch (AllMessageContainer.gameStatus.gameMode)
        {
            case GameMode.BreakThrough_1:
                AllMessageContainer.gameStatus.wordFileName = "word4.csv";
                Generate(3);
                break;
            case GameMode.BreakThrough_2:
                AllMessageContainer.gameStatus.wordFileName = "word4.csv";
                arr = GetRandomInts(10, 27);
                Generate(3, arr);
                break;
            case GameMode.BreakThrough_3:
                AllMessageContainer.gameStatus.wordFileName = "word4.csv";
                arr = new int[9] { 9, 10, 11, 12, 13, 14, 15, 16, 17 };
                Generate(3, arr);
                break;
            case GameMode.BreakThrough_4:
                AllMessageContainer.gameStatus.wordFileName = "word4.csv";
                arr = new int[8] { 0, 2, 6, 8, 18, 20, 24, 26 };
                Generate(3, arr);
                break;
            case GameMode.BreakThrough_5:
                AllMessageContainer.gameStatus.wordFileName = "word4.csv";
                arr = GetRandomInts(5, 27);
                Generate(3, arr);
                break;
            case GameMode.BreakThrough_6:
                AllMessageContainer.gameStatus.wordFileName = "word4.csv";
                Generate(4);
                break;
            case GameMode.BreakThrough_7:
                AllMessageContainer.gameStatus.wordFileName = "word4.csv";
                arr = GetRandomInts(20, 64);
                Generate(4, arr);
                break;
            case GameMode.BreakThrough_8:
                AllMessageContainer.gameStatus.wordFileName = "word4.csv";
                arr = new int[] { 0, 16, 32, 48, 3, 19, 35, 51, 15, 31, 47, 63, 12, 28, 44, 60 };
                Generate(4, arr);
                break;
        }
    }

    void Generate(int num)//��ά������
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
                cube0[i + j * num * num].transform.position = pos;
            }
        }
        gameObject.AddComponent<WholeCube>();
    }

    void Generate(int num, params int[] arr)//��ά������
    {
        for (int i = 0; i < num * num * num; i++)
        {
            GameObject cube1 = Instantiate(cube, this.transform);
            cube1.name = "cube" + i.ToString();
        }
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
            DestroyImmediate(transform.Find("cube"+i).gameObject);
        }
        gameObject.AddComponent<WholeCube>();
    }

    int[] GetRandomInts(int len,int max)
    {
        int[] arr = new int[len];
        List<int> hasGenerate = new List<int>();
        for(int i = 0; i<len; i++)
        {
            int res = Random.Range(0,max);
            if (hasGenerate.Contains(res))
            {
                i--;
                continue;
            }
            else
            {
                arr[i] = res;
                hasGenerate.Add(res);
            }
        }
        return arr;
    }
}