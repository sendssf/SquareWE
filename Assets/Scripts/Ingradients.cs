using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ingradients : MonoBehaviour
{
    public GameObject cube;
    public static int num;
    public int[] arr;
    public ParticleSystem particle;
    bool ifThirdComplete = false;
    // Start is called before the first frame update
    private void Awake()
    {
    }
    void Start()
    {
        //int[] arr = new int[10] {0,2,5,6,8,9,14,25,16,21};
        //Generate(5);
        if (transform.name == "Third-orderCube")
        {
            if (!(AllMessageContainer.gameStatus.ifonline && !AllMessageContainer.gameStatus.ifHost))
            {
                InitCubeShape();
                ifThirdComplete = true;
            }
        }
        else
        {
            Generate(num, arr);
            Transform mycube = GameObject.Find("Third-orderCube").transform;
            for (int i = 0; i < mycube.childCount; i++)
            {
                Transform cube = mycube.GetChild(i), other = transform.GetChild(i);
                for (int j = 0; j < 6; j++)
                {
                    StackLetter(cube.GetChild(j).GetComponent<Faces>().letter, other.GetChild(j).gameObject);
                }
            }
        }
    }

    private void Update()
    {
        if (transform.name == "Third-orderCube")
        {
            if (AllMessageContainer.gameStatus.ifonline && OnlineMode.ifPrepared)
            {
                DataCallBack();
            }
            if (AllMessageContainer.gameStatus.ifonline && !AllMessageContainer.gameStatus.ifHost)
            {
                ReceiveCube();
            }
            if (AllMessageContainer.gameStatus.ifonline && AllMessageContainer.gameStatus.ifHost && WholeCube.isFinished)
            {
                GenerateCubePost();
            }
        }
        else
        {
            if (AllMessageContainer.gameStatus.ifHost && ifThirdComplete)
            {
                Generate(num, arr);
                Transform mycube = GameObject.Find("Third-orderCube").transform;
                for (int i = 0; i < mycube.childCount; i++)
                {
                    Transform cube = mycube.GetChild(i), other = transform.GetChild(i);
                    for (int j = 0; j < 6; j++)
                    {
                        StackLetter(cube.GetChild(j).GetComponent<Faces>().letter, other.GetChild(j).gameObject);
                    }
                }
                ifThirdComplete = false;
            }
        }
    }

    public void GenerateCubePost()
    {
        Dictionary<string, Dictionary<string, string>> cube = new Dictionary<string, Dictionary<string, string>>();
        Transform Cube = GameObject.Find("Third-orderCube").transform;
        for (int i = 0; i < Cube.childCount; i++)
        {
            var cubei = new Dictionary<string, string>
            {
                {"num", Ingradients.num.ToString()},
                {"name", Cube.GetChild(i).name.Substring(4)},
                {"up", Cube.GetChild(i).GetChild(0).GetComponent<Faces>().letter.ToString()},
                {"down",Cube.GetChild(i).GetChild(1).GetComponent<Faces>().letter.ToString() },
                {"left",Cube.GetChild(i).GetChild(2).GetComponent<Faces>().letter.ToString() },
                {"right", Cube.GetChild(i).GetChild(3).GetComponent<Faces>().letter.ToString()},
                {"front", Cube.GetChild(i).GetChild(4).GetComponent<Faces>().letter.ToString()},
                {"back", Cube.GetChild(i).GetChild(5).GetComponent<Faces>().letter.ToString()}
            };
            cube.Add("cube"+i, cubei);
        }
        string info = JsonConvert.SerializeObject(cube);
        Dictionary<string, string> cubeInfo = new Dictionary<string, string>
        {
            {"nickname1",AllMessageContainer.playerInfo.playerName },
            {"nickname2",OnlineMode.playWith },
            {"info",info }
        };
        string response = WebController.Post(WebController.rootIP + API_Local.sendSquareInfo, JsonConvert.SerializeObject(cubeInfo));
        switch (response)
        {
            case WebController.Success:
                OnlineMode.ifPrepared = true;
                Debug.Log("Success");
                break;
            case WebController.ServerNotFound:
                Debug.Log("ServerNotFound");
                break;
        }

    }

    public void InitCubeShape()
    {
        switch (AllMessageContainer.gameStatus.gameMode)
        {
            case GameMode.BreakThrough_1:
                AllMessageContainer.gameStatus.wordFileName = "word4.csv";
                Generate(3);
                num = 3;
                break;
            case GameMode.BreakThrough_2:
                AllMessageContainer.gameStatus.wordFileName = "word4.csv";
                arr = GetRandomInts(10, 27);
                Generate(3, arr);
                num = 3;
                break;
            case GameMode.BreakThrough_3:
                AllMessageContainer.gameStatus.wordFileName = "word4.csv";
                arr = new int[9] { 9, 10, 11, 12, 13, 14, 15, 16, 17 };
                Generate(3, arr);
                num = 3;
                break;
            case GameMode.BreakThrough_4:
                AllMessageContainer.gameStatus.wordFileName = "word4.csv";
                arr = new int[8] { 0, 2, 6, 8, 18, 20, 24, 26 };
                Generate(3, arr);
                num = 3;
                break;
            case GameMode.BreakThrough_5:
                AllMessageContainer.gameStatus.wordFileName = "word4.csv";
                arr = GetRandomInts(5, 27);
                Generate(3, arr);
                num = 3;
                break;
            case GameMode.BreakThrough_6:
                AllMessageContainer.gameStatus.wordFileName = "word4.csv";
                Generate(4);
                num = 4;
                break;
            case GameMode.BreakThrough_7:
                AllMessageContainer.gameStatus.wordFileName = "word4.csv";
                arr = GetRandomInts(20, 64);
                Generate(4, arr);
                num = 4;
                break;
            case GameMode.BreakThrough_8:
                AllMessageContainer.gameStatus.wordFileName = "word4.csv";
                arr = new int[] { 0, 16, 32, 48, 3, 19, 35, 51, 15, 31, 47, 63, 12, 28, 44, 60 };
                Generate(4, arr);
                num = 4;
                break;
            case GameMode.BreakThrough_9:
                AllMessageContainer.gameStatus.wordFileName = "word6.csv";
                arr = new int[] { 5, 6, 9, 10, 17, 18, 20, 23, 24, 27, 33, 34, 36, 39, 40, 43, 53, 54, 57, 58 };
                Generate(4, arr);
                num = 4;
                break;
            case GameMode.BreakThrough_10:
                AllMessageContainer.gameStatus.wordFileName = "word6.csv";
                arr = new int[] { 16, 17, 18, 19, 20, 23, 24, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 39, 40, 43, 44, 45, 46, 47 };
                Generate(4, arr);
                num = 4;
                break;
            case GameMode.BreakThrough_11:
                AllMessageContainer.gameStatus.wordFileName = "word6.csv";
                Generate(5);
                num = 5;
                break;
            case GameMode.BreakThrough_12:
                AllMessageContainer.gameStatus.wordFileName = "word6.csv";
                arr = GetRandomInts(15, 125);
                Generate(5,arr);
                num = 5;
                break;
            case GameMode.BreakThrough_13:
                AllMessageContainer.gameStatus.wordFileName = "word6.csv";
                arr = GetRandomInts(25, 125);
                Generate(5,arr);
                num = 5;
                break;
            case GameMode.BreakThrough_14:
                AllMessageContainer.gameStatus.wordFileName = "word6.csv";
                arr = GetRandomInts(45, 125);
                Generate(5,arr);
                num = 5;
                break;
            case GameMode.BreakThrough_15:
                AllMessageContainer.gameStatus.wordFileName = "word6.csv";
                arr = new int[] {0,1,2,3,4,5,6,7,8,9,10,11,13,14,15,16,17,18,19,20,21,
                    22,23,24,25,26,27,28,29,30,31,33,34,35,39,40,41,43,44,45,46,47,48,
                    49,50,51,53,54,55,59,65,69,70,71,73,74,75,76,77,78,79,80,81,83,84,
                    85,89,90,91,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,
                    108,109,110,111,113,114,115,116,117,118,119,120,121,122,123,124};
                Generate(5,arr);
                num = 5;
                break;
            case GameMode.BreakThrough_16:
                AllMessageContainer.gameStatus.wordFileName = "word6.csv";
                arr = new int[]
                {
                    25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,
                    75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95,96,97,98,99
                };
                Generate(5,arr);
                num = 5;
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
                num = 5;
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
                num = 5;
                break;
            case GameMode.BreakThrough_19:
                AllMessageContainer.gameStatus.wordFileName = "word6.csv";
                Generate(6);
                num = 6;
                break;
            case GameMode.BreakThrough_20:
                AllMessageContainer.gameStatus.wordFileName = "word6.csv";
                arr = GetRandomInts(30, 216);
                Generate(6,arr);
                num = 6;
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
                num = 6;
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
                    num = dim;
                }
                else
                {
                    arr = GetRandomInts(Random.Range(5, dim*dim*dim/2), dim*dim*dim);
                    Generate(dim,arr);
                    num = dim;
                }
                break;
            case GameMode.CustomOthers:
                if (CustomModeClickEvent.customLen==0)
                {
                    Generate(CustomModeClickEvent.customDim);
                    num = CustomModeClickEvent.customDim;
                }
                else
                {
                    if (CustomModeClickEvent.customArr==null)
                    {
                        arr=GetRandomInts(CustomModeClickEvent.customLen,
                            CustomModeClickEvent.customDim*CustomModeClickEvent.customDim*CustomModeClickEvent.customDim);
                        Generate(CustomModeClickEvent.customDim, arr);
                        num = CustomModeClickEvent.customDim;
                    }
                    else
                    {
                        Generate(CustomModeClickEvent.customDim, CustomModeClickEvent.customArr);
                        num = CustomModeClickEvent.customDim;
                    }
                }
                break;
            case GameMode.Endless:

                break;
        }
    }

    public void ReceiveCube()
    {
        var json = new Dictionary<string, string>
        {
            { "nickname1",OnlineMode.playWith},
            {"nickname2",AllMessageContainer.playerInfo.playerName}
        };
        string response = WebController.Post(WebController.rootIP + API_Local.checkSquareInfo, JsonConvert.SerializeObject(json));
        switch (response)
        {
            case WebController.NoMessage:
                ///进入等待状态
                break;
            case WebController.ServerNotFound:
                break;
            default:
                var mes = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string,string>>>(response);
                Dictionary<string, string> num;
                string opt;
                Transform MyCube = GameObject.Find("Third-orderCube").transform, OtherCube = GameObject.Find("OtherCube").transform;
                List<string> name = new List<string>();
                if (mes.TryGetValue("cube0", out num))
                {
                    num.TryGetValue("num", out opt);
                    MyCube.gameObject.GetComponent<Ingradients>().Generate(int.Parse(opt));
                    OtherCube.gameObject.GetComponent<Ingradients>().Generate(int.Parse(opt));
                }
                foreach (Dictionary<string, string> cube in mes.Values)
                {
                    if (cube.TryGetValue("name", out opt))
                    {
                        name.Add(opt);
                    }
                }
                foreach (Dictionary<string, string> cube in mes.Values)
                {
                    for (int i = 0; i < MyCube.childCount; i++)
                    {
                        if (name.Contains(MyCube.GetChild(i).name.Substring(4)))
                        {
                            StackLetter(cube["up"].ToCharArray()[0], MyCube.GetChild(i).GetChild(0).gameObject);
                            StackLetter(cube["down"].ToCharArray()[0], MyCube.GetChild(i).GetChild(1).gameObject);
                            StackLetter(cube["left"].ToCharArray()[0], MyCube.GetChild(i).GetChild(2).gameObject);
                            StackLetter(cube["right"].ToCharArray()[0], MyCube.GetChild(i).GetChild(3).gameObject);
                            StackLetter(cube["front"].ToCharArray()[0], MyCube.GetChild(i).GetChild(4).gameObject);
                            StackLetter(cube["back"].ToCharArray()[0], MyCube.GetChild(i).GetChild(5).gameObject);
                            break;
                        }
                        else
                        {
                            DestroyImmediate(MyCube.GetChild(i).gameObject);
                        }
                    }
                    for (int i = 0; i < OtherCube.childCount; i++)
                    {
                        if (name.Contains(OtherCube.GetChild(i).name.Substring(5)))
                        {
                            StackLetter(cube["up"].ToCharArray()[0], OtherCube.GetChild(i).GetChild(0).gameObject);
                            StackLetter(cube["down"].ToCharArray()[0], OtherCube.GetChild(i).GetChild(1).gameObject);
                            StackLetter(cube["left"].ToCharArray()[0], OtherCube.GetChild(i).GetChild(2).gameObject);
                            StackLetter(cube["right"].ToCharArray()[0], OtherCube.GetChild(i).GetChild(3).gameObject);
                            StackLetter(cube["front"].ToCharArray()[0], OtherCube.GetChild(i).GetChild(4).gameObject);
                            StackLetter(cube["back"].ToCharArray()[0], OtherCube.GetChild(i).GetChild(5).gameObject);
                            break;
                        }
                        else
                        {
                            DestroyImmediate(OtherCube.GetChild(i).gameObject);
                        }
                    }
                }
                transform.gameObject.GetComponent<WholeCube>().UpdateCubeQuadMatch();
                OnlineMode.ifPrepared = true;
                break;
        }
    }

    public void StackLetter(char letter, GameObject quad)
    {
        Texture2D texture;
        texture=Resources.Load<Texture2D>(letter.ToString().ToUpper());
        quad.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", texture);
        quad.GetComponent<Faces>().letter= letter;
        quad.GetComponent<Faces>().visited= true;
    }

    public void Generate(int num)//��ά������
    {
        for (int i = 0; i < num * num * num; i++)
        {
            GameObject cube1 = Instantiate(cube, this.transform);
            if (transform.name == "OtherCube")
            {
                cube1.name = "cube*" + i.ToString();
            }
            else
            {
                cube1.name = "cube" + i.ToString();
            }
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
                if (transform.name == "OtherCube")
                { 
                    cube0[i + j * num * num] = GameObject.Find("cube*" + (i + j * num * num)); 
                }
                else
                {
                    cube0[i + j * num * num] = GameObject.Find("cube" + (i + j * num * num));
                }
                Vector3 pos = new Vector3(i % num - (num - 1) / 2, (num - 1) / 2 - j, a);      
                cube0[i + j * num * num].transform.localPosition = pos;
            }
        }
        if (transform.name == "Third-orderCube")
        {
            gameObject.AddComponent<WholeCube>();
            gameObject.AddComponent<CubeClickEvent>();
        }
    }

    void Generate(int num, params int[] arr)//��ά������
    {
        for (int i = 0; i < num * num * num; i++)
        {
            GameObject cube1 = Instantiate(cube, this.transform);
            if (transform.name == "OtherCube")
            {
                cube1.name = "cube*" + i.ToString();
            }
            else
            {
                cube1.name = "cube" + i.ToString();
            }
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
                if (transform.name == "OtherCube")
                {
                    cube0[i + j * num * num] = GameObject.Find("cube*" + (i + j * num * num));
                }
                else
                {
                    cube0[i + j * num * num] = GameObject.Find("cube" + (i + j * num * num));
                }    
                Vector3 pos = new Vector3(i % num - (num - 1) / 2, (num - 1) / 2 - j, a);
                cube0[i + j * num * num].transform.localPosition = pos;
            }
        }
        foreach (int i in arr)
        {
            if (transform.name == "OtherCube")
            {
                DestroyImmediate(transform.Find("cube*" + i).gameObject);
            }
            else
            {
                DestroyImmediate(transform.Find("cube" + i).gameObject);
            }
        }
        if (transform.name == "Third-orderCube")
        {
            gameObject.AddComponent<WholeCube>();
            gameObject.AddComponent<CubeClickEvent>();
        }
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

    public void DataCallBack()
    {
        var json = new Dictionary<string, string>
        {
            {"nickname1",AllMessageContainer.playerInfo.playerName },
            { "nickname2",OnlineMode.playWith}/////需要修改
        };
        string response = WebController.Post(WebController.rootIP + API_Local.getPackage, JsonConvert.SerializeObject(json));
        switch (response)
        {
            case WebController.NoMessage:
                ///进入等待状态
                break;
            case WebController.ServerNotFound:
                break;
            default:
                var mes = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
                string opt;
                if (mes.TryGetValue("option", out opt))
                {
                    if (opt == "move cube")
                    {
                        OnlineModeCubeMove.MoveCube(GameObject.Find("cube*"+mes["object"]), mes["body"]);
                    }
                    else if (opt == "rotate cube")
                    {
                        //wait
                        OnlineModeCubeMove.RotateCube(new List<GameObject> { GameObject.Find("cube*"+mes["object"]) }, mes["body"]);
                    }
                    else if (opt == "rotate screen")
                    {
                        //wait
                        var rot = mes["body"].Split(",");
                        OnlineModeCubeMove.RotateScreen(GameObject.Find("OtherCube"), rot[0], (float)System.Convert.ToDouble(rot[0]));
                    }
                    else if (opt == "point cube")
                    {
                        string[] result = mes["object"].Split(new char[] { ',' });
                        int i = 0;
                        GameObject c = null;
                        List<GameObject> bg = new List<GameObject>();
                        foreach (string s in result)
                        {
                            if (i % 2 == 0)
                            {
                                c = GameObject.Find("cube*" + s);
                            }
                            else
                            {
                                c.transform.Find(s).GetComponent<Faces>().TimeUp();
                                if (c.transform.Find(s).GetComponent<Faces>().Times() == 1)
                                {
                                    c.transform.Find(s).GetComponent<MeshRenderer>().material.color = new Color(1, 0.6f, 0.6f);//点击改变面颜色
                                }
                                else if (c.transform.Find(s).GetComponent<Faces>().Times() == 2)
                                {
                                    c.transform.Find(s).gameObject.GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0.6f);//点击改变面颜色
                                }
                                else if (c.transform.Find(s).GetComponent<Faces>().Times() == 3)
                                {
                                    GameObject father = c.transform.Find(s).transform.parent.gameObject;
                                    father.transform.parent.GetComponent<WholeCube>().position.Remove(father.transform.position);
                                    father.transform.parent.GetComponent<WholeCube>()._isCleared = true;
                                    for (int j = 0; j < 6; j++)
                                    {
                                        father.transform.GetChild(i).gameObject.GetComponent<Faces>().rb.isKinematic = false;
                                        father.transform.GetChild(i).gameObject.GetComponent<Faces>().rb.useGravity = true;
                                    }
                                    father.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                                    GameObject.Find("Explosion").gameObject.AddComponent<Expolosion>().explosionPos = GameObject.Find("OtherCube").transform;
                                    var ps = Instantiate(father.GetComponent<Cube>().particle, father.transform);
                                    ps.transform.localPosition = Vector3.zero;
                                    ps.Play();
                                    Destroy(father);
                                }
                            }
                            i++;
                        }
                    }
                    else if (opt == "destroy cube")
                    {
                        string[] result = mes["object"].Split(new char[] { ',' });
                        foreach (string s in result)
                        {
                            Destroy(GameObject.Find("cube*" + s));
                        }
                    }
                    else if (opt == "change color")
                    {
                        //弹出对话框
                    }
                    else if (opt == "get word")
                    {
                        //弹出对话框
                    }
                    else if (opt == "quit")
                    {
                        //弹出对话框

                    }
                    else if(opt == "win")
                    {

                    }
                }
                break;
        }
    }
}