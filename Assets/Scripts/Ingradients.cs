using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingradients : MonoBehaviour
{
    public GameObject cube;
    public static int num;
    public ParticleSystem particle;
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
            case GameMode.BreakThrough_9:
                AllMessageContainer.gameStatus.wordFileName = "word6.csv";
                arr = new int[] { 5, 6, 9, 10, 17, 18, 20, 23, 24, 27, 33, 34, 36, 39, 40, 43, 53, 54, 57, 58 };
                Generate(4, arr);
                break;
            case GameMode.BreakThrough_10:
                AllMessageContainer.gameStatus.wordFileName = "word6.csv";
                arr = new int[] { 16, 17, 18, 19, 20, 23, 24, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 39, 40, 43, 44, 45, 46, 47 };
                Generate(4, arr);
                break;
            case GameMode.BreakThrough_11:
                AllMessageContainer.gameStatus.wordFileName = "word6.csv";
                Generate(5);
                break;
            case GameMode.BreakThrough_12:
                AllMessageContainer.gameStatus.wordFileName = "word6.csv";
                arr = GetRandomInts(15, 125);
                Generate(5,arr);
                break;
            case GameMode.BreakThrough_13:
                AllMessageContainer.gameStatus.wordFileName = "word6.csv";
                arr = GetRandomInts(25, 125);
                Generate(5,arr);
                break;
            case GameMode.BreakThrough_14:
                AllMessageContainer.gameStatus.wordFileName = "word6.csv";
                arr = GetRandomInts(45, 125);
                Generate(5,arr);
                break;
            case GameMode.BreakThrough_15:
                AllMessageContainer.gameStatus.wordFileName = "word6.csv";
                arr = new int[] {0,1,2,3,4,5,6,7,8,9,10,11,13,14,15,16,17,18,19,20,21,
                    22,23,24,25,26,27,28,29,30,31,33,34,35,39,40,41,43,44,45,46,47,48,
                    49,50,51,53,54,55,59,65,69,70,71,73,74,75,76,77,78,79,80,81,83,84,
                    85,89,90,91,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,
                    108,109,110,111,113,114,115,116,117,118,119,120,121,122,123,124};
                Generate(5,arr);
                break;
            case GameMode.BreakThrough_16:
                AllMessageContainer.gameStatus.wordFileName = "word6.csv";
                arr = new int[]
                {
                    25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,
                    75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95,96,97,98,99
                };
                Generate(5,arr);
                break;
            case GameMode.BreakThrough_17:
                AllMessageContainer.gameStatus.wordFileName = "word6.csv";
                arr = new int[]
                {
                    25,26,27,28,29,30,34,35,39,40,44,45,46,47,48,49,
                    50,51,52,53,54,55,56,57,58,59,60,61,63,64,65,66,67,68,69,70,71,72,73,74,
                    75,76,77,78,79,80,84,85,89,90,94,95,96,97,98,99
                };
                Generate(5, arr);
                break;
            case GameMode.BreakThrough_18:
                AllMessageContainer.gameStatus.wordFileName = "word6.csv";
                arr = new int[]
                {
                    2,7,10,11,12,13,14,17,22,27,35,39,47,
                    50,51,52,53,54,55,59,60,64,65,69,70,71,72,73,74,
                    77,85,89,97,102,107,110,111,112,113,114,117,122
                };
                Generate(5, arr);
                break;
            case GameMode.BreakThrough_19:
                AllMessageContainer.gameStatus.wordFileName = "word6.csv";
                Generate(6);
                break;
            case GameMode.BreakThrough_20:
                AllMessageContainer.gameStatus.wordFileName = "word6.csv";
                arr = GetRandomInts(30, 216);
                Generate(6,arr);
                break;
            case GameMode.BreakThrough_21:
                AllMessageContainer.gameStatus.wordFileName = "word6.csv";
                arr = new int[]
                {
                    43,44,45,46,49,50,51,52,55,56,57,58,61,62,63,64,
                    79,80,81,82,85,88,91,94,97,98,99,100,
                    115,116,117,118,121,124,127,130,133,134,135,136,
                    151,152,153,154,157,158,159,160,163,164,165,166,169,170,171,172
                };
                Generate(6,arr);
                break;
            case GameMode.CustomRandom:
                if (Random.Range(0, 2)==0)
                {
                    AllMessageContainer.gameStatus.wordFileName = "word4.csv";
                }
                else
                {
                    AllMessageContainer.gameStatus.wordFileName = "word6.csv";
                }
                int dim = Random.Range(3, 9);
                if(Random.Range(0, 5)==0)
                {
                    Generate(dim);
                }
                else
                {
                    arr = GetRandomInts(Random.Range(5, dim*dim*dim/2), dim*dim*dim);
                    Generate(dim,arr);
                }
                break;
            case GameMode.CustomOthers:
                if (CustomModeClickEvent.customLen==0)
                {
                    Generate(CustomModeClickEvent.customDim);
                }
                else
                {
                    if (CustomModeClickEvent.customArr==null)
                    {
                        arr=GetRandomInts(CustomModeClickEvent.customLen,
                            CustomModeClickEvent.customDim*CustomModeClickEvent.customDim*CustomModeClickEvent.customDim);
                        Generate(CustomModeClickEvent.customDim, arr);
                    }
                    else
                    {
                        Generate(CustomModeClickEvent.customDim, CustomModeClickEvent.customArr);
                    }
                }
                break;
            case GameMode.Endless:

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
                cube0[i + j * num * num].transform.localPosition = pos;
            }
        }
        gameObject.AddComponent<WholeCube>();
        gameObject.AddComponent<CubeClickEvent>();
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
        gameObject.AddComponent<CubeClickEvent>();
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

    int[] DeleteSameNumber(int[] arr)
    {
        List<int> ints = new List<int>();
        for(int i=0;i<arr.Length; i++)
        {
            if (!ints.Contains(arr[i]))
            {
                ints.Add(arr[i]);
            }
        }
        return ints.ToArray();
    }
}