using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WholeCube : MonoBehaviour
{
    public static List<GameObject> Slected = new List<GameObject>();
    public static bool _isUsed = false;
    public static List<string> Matched = new List<string>();
    public static string selectedWord = string.Empty;
    public static Dictionary<string, string> WordList = ReadCsv.ReadCsvFile("word6.csv");

    public bool _isCleared = false;
    public Dictionary<Vector3, int> position= new Dictionary<Vector3, int>() ;
    public int[] px = new int[4] { 1, 0, -1, 0 };
    public int[] py = new int[4] { 0, 1, 0, -1 };
    public bool[,] visited = new bool[27, 6];
    string presentWord = null, presentWord1 = null;
    int count = 0, count1 = 0, count2 = 0, count3 = -1;
    public static List<string> SelectedWord = new List<string>();
    private MeshRenderer meshRenderer;
    private Texture texture;
    //使27个立方体组成一个大立方体
    void Start()
    {
        GameObject[] cube = new GameObject[27];
        int a = -1;
        for (int i = 0; i < 9; i++)
        {
            if (i % 3 == 0 && i != 0)
            {
                a += 1;
            }
            cube[i] = GameObject.Find("cube" + i);
            Vector3 pos = new Vector3(i % 3 - 1, 1, a);
            cube[i].transform.position = pos;
        }
        a = -1;
        for (int i = 0; i < 9; i++)
        {
            if (i % 3 == 0 && i != 0)
            {
                a += 1;
            }
            cube[i + 9] = GameObject.Find("cube" + (i + 9));
            Vector3 pos = new Vector3(i % 3 - 1, 0, a);
            cube[i + 9].transform.position = pos;
        }
        a = -1;
        for (int i = 0; i < 9; i++)
        {
            if (i % 3 == 0 && i != 0)
            {
                a += 1;
            }
            cube[i + 18] = GameObject.Find("cube" + (i + 18));
            Vector3 pos = new Vector3(i % 3 - 1, -1, a);
            cube[i + 18].transform.position = pos;
        }



        for(int i = 0; i < 27; i++)
        {
            position.Add(this.gameObject.transform.GetChild(i).position, i);
        }
        //定义获取贴图的数量
        //动态加载贴图
        System.Random random = new System.Random(~unchecked((int)DateTime.Now.Ticks));
        /*letter = alphabet[random.Next(0, alphabet.Length)];
        texture = Resources.Load(letter.ToString()) as Texture;
        meshRenderer.material.SetTexture("_MainTex", texture);*/
        presentWord1 = WordList[Convert.ToString(UnityEngine.Random.Range(0, 5556))];
        Debug.Log(presentWord1);
        for (int i = 0; i < 27; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                visited[i, j] = false;
            }
        }
       dfs(new Vector3(0,0,0), 0);
    }

    private void Update()
    {
        if (_isCleared == true)
        {
            foreach(int j in position.Values)
            {
                count3++;
                if(j!=count3)
                {
                    for (int k = 0; k < 6; k++)
                    {
                        visited[count3, k] = true;
                    }
                    count3 = j;
                }
            }
            count3 = -1;
            for(int k = 0; k < 27; k++)
            {
                for(int l = 0; l < 6; l++)
                {
                    if (visited[k,l] == false)
                    {
                        Debug.Log(k + " " + l);
                        for(int m = 0; m < 4; m++)
                        {
                            if (!position.ContainsKey(this.transform.GetChild(k).position + new Vector3(px[m], 0, py[m])))
                            {
                                Debug.Log(k + " " + l);
                                if (m == 0 && l == 3)
                                {
                                    Debug.Log("hhhhhhhhhhhhhhhhhhhhh");
                                    dfs(new Vector3(k / 9, (k % 9) / 3, (k % 9) % 3) , l);
                                }
                                if (m == 1 && l == 5)
                                {
                                    dfs(new Vector3(k / 9, (k % 9) / 3, (k % 9) % 3) , l);
                                }
                                if (m == 2 && l == 2)
                                {
                                    dfs(new Vector3(k / 9, (k % 9) / 3, (k % 9) % 3) , l);
                                }
                                if (m == 3 && l == 4)
                                {
                                    dfs(new Vector3(k / 9, (k % 9) / 3, (k % 9) % 3) , l);
                                }
                            }
                        }
                        if (!position.ContainsKey(this.transform.GetChild(k).position + new Vector3(0, 1, 0)))
                        {
                            if ( l == 0)
                            {
                                dfs(new Vector3(k / 9, (k % 9) / 3, (k % 9) % 3) , l);
                            }
                        }
                        if (!position.ContainsKey(this.transform.GetChild(k).position + new Vector3(0, -1, 0)))
                        {
                            if (l == 0)
                            {
                                dfs(new Vector3(k / 9, (k % 9) / 3, (k % 9) % 3) , l);
                            }
                        }
                    }
                }
            }
        }
        _isCleared = false;
    }

    Transform getSquad(Vector3 cube, int squad)
    {
        return this.transform.GetChild((int)(cube.x * 9 + cube.y * 3 + cube.z)).GetChild(squad);
    }

    void dfs(Vector3 cube, int squad)//参数为访问哪个立方体和访问立方体的哪个面
    {
        if (cube.x < 0 || cube.x > 2 || cube.y < 0 || cube.y > 2 || cube.z < 0 || cube.z > 2 || squad > 5 || squad < 0)
        {
            return;
        }
        if (visited[(int)(cube.x*9+cube.y*3+cube.z), squad] == true)
        {
            /*if (presentWord != presentWord1)
            {
                if (presentWord1.Substring(presentWord.Length, 1).ToUpper() != getSquad(cube, squad).GetComponent<Faces>().letter.ToString())
                {
                    return;
                }
            }*/
            return;
        }
        if(presentWord == presentWord1)
        {
            count = 0;
            SelectedWord.Add(presentWord1);
            presentWord = null;
            foreach (string word in WordList.Values)
            {
                if(word.Substring(0,1) == presentWord1.Substring(presentWord1.Length-1,1))
                {
                    count1++;
                }
            }
            if (count1 == 0)
            {
                presentWord1 = WordList[Convert.ToString(UnityEngine.Random.Range(0, 5556))];
                Debug.Log(presentWord1);
            }
            else
            {
                count2 = 0;
                count = 1;
                count1 = UnityEngine.Random.Range(0, 10000) % (count1-1)+1;
                foreach (string word in WordList.Values)
                {
                    if (word.Substring(0, 1) == presentWord1.Substring(presentWord1.Length - 1,1))
                    {
                        count2++;
                        if (count1 == count2)
                        {
                            presentWord1 = word;
                            presentWord += presentWord1.Substring(0, 1);
                            Debug.Log(presentWord1);
                            count1 = 0;
                            count2 = 0;
                        }
                    }
                }
            }
        }
        visited[(int)(cube.x * 9 + cube.y * 3 + cube.z), squad] = true;
        texture = Resources.Load(presentWord1.Substring(count, 1).ToUpper()) as Texture;
        getSquad(cube, squad).GetComponent<Faces>().letter = presentWord1.Substring(count, 1).ToUpper().ToCharArray()[0];
        meshRenderer = getSquad(cube,squad).GetComponent<MeshRenderer>();
        meshRenderer.material.SetTexture("_MainTex", texture);
        presentWord += presentWord1.Substring(count, 1);
        Debug.Log(cube+" "+squad+" "+ count+" "+presentWord);
        count++;
        if(squad == 0)//顶部
        {  
            for (int i = 0; i < 4; i++)
            {
                if (position.ContainsKey(getSquad(cube, squad).parent.position + new Vector3(px[i], 0, py[i])))
                {
                    dfs(cube + new Vector3(0, py[i], px[i]), squad);
                }
                else
                {
                    if(i == 0)
                    {
                        dfs(cube, squad+3);
                    }
                    else if(i == 1)
                    {
                        dfs(cube, squad + 5);
                    }
                    else if(i == 2)
                    {
                        dfs(cube, squad + 2);
                    }
                    else if(i==3)
                    {
                        dfs(cube, squad + 4);
                    }
                }
            }
        }
        if (squad == 1)//下部
        {
            for (int i = 0; i < 4; i++)
            {
                if (position.ContainsKey(getSquad(cube, squad).parent.position + new Vector3(px[i], 0, py[i])))
                {
                    dfs(cube + new Vector3(0, py[i], px[i]), squad);
                }
                else
                {
                    if (i == 0)
                    {
                        dfs(cube, squad + 2);
                    }
                    else if (i == 1)
                    {
                        dfs(cube, squad + 4);
                    }
                    else if (i == 2)
                    {
                        dfs(cube, squad + 1);
                    }
                    else if (i == 3)
                    {
                        dfs(cube, squad + 3);
                    }
                }
            }
        }
        if (squad == 2)//左部
        {
            for (int i = 0; i < 4; i++)
            {
                if (position.ContainsKey(getSquad(cube, squad).parent.position + new Vector3(0, px[i], py[i])))
                {
                    dfs(cube + new Vector3(-px[i], py[i], 0), squad);
                }
                else
                {
                    if (i == 0)
                    {
                        dfs(cube, squad - 2);
                    }
                    else if (i == 1)
                    {
                        dfs(cube, squad + 3);
                    }
                    else if (i == 2)
                    {
                        dfs(cube, squad - 1);
                    }
                    else if (i == 3)
                    {
                        dfs(cube, squad + 2);
                    }
                }
            }
        }
        if (squad == 3)//右部
        {
            for (int i = 0; i < 4; i++)
            {
                if (position.ContainsKey(getSquad(cube, squad).parent.position + new Vector3(0, px[i], py[i])))
                {
                    dfs(cube + new Vector3(-px[i], py[i], 0), squad);
                }
                else
                {
                    if (i == 0)
                    {
                        dfs(cube, squad - 3);
                    }
                    else if (i == 1)
                    {
                        dfs(cube, squad + 2);
                    }
                    else if (i == 2)
                    {
                        dfs(cube, squad - 2);
                    }
                    else if (i == 3)
                    {
                        dfs(cube, squad + 1);
                    }
                }
            }
        }
        if (squad == 4)//前部
        {
            for (int i = 0; i < 4; i++)
            {
                if (position.ContainsKey(getSquad(cube, squad).parent.position + new Vector3(px[i], py[i], 0)))
                {
                    dfs(cube + new Vector3(-py[i], 0, px[i]), squad);
                }
                else
                {
                    if (i == 0)
                    {
                        dfs(cube, squad - 1);
                    }
                    else if (i == 1)
                    {
                        dfs(cube, squad - 4);
                    }
                    else if (i == 2)
                    {
                        dfs(cube, squad - 2);
                    }
                    else if (i == 3)
                    {
                        dfs(cube, squad - 3);
                    }
                }
            }
        }
        if (squad == 5)//后部
        {
            for (int i = 0; i < 4; i++)
            {
                if (position.ContainsKey(getSquad(cube, squad).parent.position + new Vector3(px[i], py[i], 0)))
                {
                    dfs(cube + new Vector3(-py[i], 0, px[i]), squad);
                }
                else
                {
                    if (i == 0)
                    {
                        dfs(cube, squad - 2);
                    }
                    else if (i == 1)
                    {
                        dfs(cube, squad - 5);
                    }
                    else if (i == 2)
                    {
                        dfs(cube, squad - 3);
                    }
                    else if (i == 3)
                    {
                        dfs(cube, squad - 4);
                    }
                }
            }
        }
    }
}
