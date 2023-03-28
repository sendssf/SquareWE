using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using System;

public class WholeCube : MonoBehaviour
{
    public static List<GameObject> Slected = new List<GameObject>();
    public static bool _isUsed = false;
    private float width = 1f;
    private int pastedNum = 0;
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
     
    public static char[] alphabet = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    private static List<int> haveGenWordIndex= new List<int>();
    public Dictionary<Vector3,GameObject> cubeDict= new Dictionary<Vector3,GameObject>();
    
    //使27个立方体组成一个大立方体

    void DFS_Start()
    {
        for (int i = 0; i < 27; i++)
        {
            position.Add(this.gameObject.transform.GetChild(i).position, i);
        }
        //定义获取贴图的数量
        //动态加载贴图
        System.Random random = new System.Random(~unchecked((int)System.DateTime.Now.Ticks));
        presentWord1 = WordList[System.Convert.ToString(UnityEngine.Random.Range(0, 5556))];
        for (int i = 0; i < 27; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                visited[i, j] = false;
            }
        }
        dfs(new Vector3(0, 0, 0), 0);
    }

    void DFS_Update()
    {
        if (_isCleared == true)
        {
            foreach (int j in position.Values)
            {
                count3++;
                if (j!=count3)
                {
                    for (int k = 0; k < 6; k++)
                    {
                        visited[count3, k] = true;
                    }
                    count3 = j;
                }
            }
            count3 = -1;
            for (int k = 0; k < 27; k++)
            {
                for (int l = 0; l < 6; l++)
                {
                    if (visited[k, l] == false)
                    {
                        for (int m = 0; m < 4; m++)
                        {
                            if (!position.ContainsKey(this.transform.GetChild(k).position + new Vector3(px[m], 0, py[m])))
                            {
                                if (m == 0 && l == 3)
                                {
                                    dfs(new Vector3(k / 9, (k % 9) / 3, (k % 9) % 3), l);
                                }
                                if (m == 1 && l == 5)
                                {
                                    dfs(new Vector3(k / 9, (k % 9) / 3, (k % 9) % 3), l);
                                }
                                if (m == 2 && l == 2)
                                {
                                    dfs(new Vector3(k / 9, (k % 9) / 3, (k % 9) % 3), l);
                                }
                                if (m == 3 && l == 4)
                                {
                                    dfs(new Vector3(k / 9, (k % 9) / 3, (k % 9) % 3), l);
                                }
                            }
                        }
                        if (!position.ContainsKey(this.transform.GetChild(k).position + new Vector3(0, 1, 0)))
                        {
                            if (l == 0)
                            {
                                dfs(new Vector3(k / 9, (k % 9) / 3, (k % 9) % 3), l);
                            }
                        }
                        if (!position.ContainsKey(this.transform.GetChild(k).position + new Vector3(0, -1, 0)))
                        {
                            if (l == 0)
                            {
                                dfs(new Vector3(k / 9, (k % 9) / 3, (k % 9) % 3), l);
                            }
                        }
                    }
                }
            }
        }
        _isCleared = false;
    }
    private void Awake()
    {
        
    }
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
        haveGenWordIndex.Clear();
        int beginIndex = Random.Range(0, transform.childCount);
        int quadBeginIndex = Random.Range(0, 6);
        MakeLettersInOrder(transform.GetChild(beginIndex).GetChild(quadBeginIndex).gameObject);
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
                presentWord1 = WordList[System.Convert.ToString(UnityEngine.Random.Range(0, 5556))];
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

    void MakeLettersInOrder(GameObject beginQuad)
    {
        var position= beginQuad.transform.localPosition;
        //将所有子物体和坐标添加进字典
        int cubeCount = transform.childCount;
        for(int i = 0; i<cubeCount; i++)
        {
            cubeDict.Add(transform.GetChild(i).transform.localPosition, transform.GetChild(i).gameObject);
        }
        char letter=GenerateRandomLetter();
        string word1 = GenerateRandomWord(letter);
        string word2 = GenerateRandomWord(letter);
        string word3 = GenerateRandomWord(letter);
        StackLetter(letter, beginQuad);
        PasteString(word1.Split(letter)[1],beginQuad);
        PasteString(ReverseString(word1.Split(letter)[0]), beginQuad);
        PasteString(word2.Split(letter)[1],beginQuad);
        PasteString(ReverseString(word2.Split(letter)[0]), beginQuad);
        PasteString(word3.Split(letter)[1],beginQuad);
        PasteString(ReverseString(word3.Split(letter)[0]), beginQuad);
        foreach (GameObject eachObj in cubeDict.Values)
        {
            if (pastedNum>=transform.childCount*6) 
            {
                break;
            }
            for(int i = 0; i<eachObj.transform.childCount; i++)
            {
                if (eachObj.transform.GetChild(i).GetComponent<Faces>().visited)
                {
                    char lt = eachObj.transform.GetChild(i).GetComponent<Faces>().letter;
                    string subWord1 = GenerateRandomWord(lt);
                    string subWord2 = GenerateRandomWord(lt);
                    PasteString(subWord1.Split(lt)[1], eachObj.transform.GetChild(i).gameObject);
                    PasteString(ReverseString(subWord1.Split(lt)[0]), eachObj.transform.GetChild(i).gameObject);
                    PasteString(subWord2.Split(lt)[1], eachObj.transform.GetChild(i).gameObject);
                    PasteString(ReverseString(subWord2.Split(lt)[0]), eachObj.transform.GetChild(i).gameObject);
                    break;
                }
            }
        }
    }

    private void PasteString(string pst,GameObject begin)
    {
        if (pst.Length==0||pastedNum==transform.childCount*6)
        {
            return;
        }
        List<int> haveTry= new List<int>();
        bool _isPastSuccess = false;
        while (!_isPastSuccess) {
            if (haveTry.Count>=12)  //被包围
            {
                foreach(KeyValuePair<Vector3,GameObject> pair in cubeDict)  //全局寻找未贴的面
                {
                    for (int i = 0; i<pair.Value.transform.childCount; i++)
                    {
                        if (pair.Value.transform.GetChild(i).gameObject.GetComponent<Faces>().visited==false)
                        {
                            string wordTemp = GenerateRandomWord(GenerateRandomLetter());
                            StackLetter(wordTemp[0], pair.Value.transform.GetChild(i).gameObject);
                            PasteString(wordTemp.Substring(1), pair.Value.transform.GetChild(i).gameObject);
                        }
                    }
                }
                break;
            }
            int key = Random.Range(0, 12);
            //0~3为本方块的其它面
            if (key==0||key==1||key==2||key==3)
            {
                Dictionary<Vector3, GameObject> selfDict = new Dictionary<Vector3, GameObject>();
                List<Vector3> choice= new List<Vector3>();
                for (int i = 0; i<begin.transform.parent.childCount; i++)
                {
                    if (begin.transform.localPosition!=begin.transform.parent.GetChild(i).localPosition &&
                        begin.transform.localPosition!=-begin.transform.parent.GetChild(i).localPosition)
                    {
                        selfDict.Add(begin.transform.parent.GetChild(i).localPosition, begin.transform.parent.GetChild(i).gameObject);
                        choice.Add(begin.transform.parent.GetChild(i).localPosition);
                    }
                }

                if (selfDict[choice[key]].GetComponent<Faces>().visited==false)
                {
                    StackLetter(pst[0], selfDict[choice[key]]);
                    _isPastSuccess= true;
                    PasteString(pst.Substring(1), selfDict[choice[key]]);
                    break;
                }
                else if (selfDict[choice[key]].GetComponent<Faces>().letter==pst[0])
                {
                    _isPastSuccess= true;
                    PasteString(pst.Substring(1), selfDict[choice[key]]);
                    break;
                }
                else
                {
                    _isPastSuccess= false;
                    if (!haveTry.Contains(key))
                    {
                        haveTry.Add(key);
                    }
                }
            }
            else if (key==4||key==5||key==6||key==7)
            {
                Dictionary<Vector3, GameObject> selfDict = new Dictionary<Vector3, GameObject>();
                Dictionary<Vector3,GameObject> otherDict = new Dictionary<Vector3, GameObject>();
                List<Vector3> choice = new List<Vector3>();
                for (int i = 0; i<begin.transform.parent.childCount; i++)
                {
                    if (begin.transform.localPosition!=begin.transform.parent.GetChild(i).localPosition &&
                        begin.transform.localPosition!=-begin.transform.parent.GetChild(i).localPosition)
                    {
                        selfDict.Add(begin.transform.parent.GetChild(i).localPosition, begin.transform.parent.GetChild(i).gameObject);
                        choice.Add(begin.transform.parent.GetChild(i).localPosition);
                    }
                }
                Vector3 otherCubePos;
                otherCubePos=begin.transform.parent.localPosition+2*begin.transform.localPosition + 2*choice[key % 4];
                if(cubeDict.ContainsKey(otherCubePos))
                {
                    GameObject otherCube= cubeDict[otherCubePos];
                    for(int i = 0; i<otherCube.transform.childCount; i++)
                    {
                        otherDict.Add(otherCube.transform.GetChild(i).localPosition,otherCube.transform.GetChild(i).gameObject);
                    }
                    if (otherDict[-choice[key%4]].GetComponent<Faces>().visited==false)
                    {
                        StackLetter(pst[0], otherDict[-choice[key % 4]]);
                        _isPastSuccess=true;
                        PasteString(pst.Substring(1), otherDict[-choice[key % 4]]);
                        break;
                    }
                    else if (otherDict[-choice[key % 4]].GetComponent<Faces>().letter==pst[0])
                    {
                        _isPastSuccess=true;
                        PasteString(pst.Substring(1), otherDict[-choice[key % 4]]);
                        break;
                    }
                    else
                    {
                        _isPastSuccess=false;
                        if (!haveTry.Contains(key))
                        {
                            haveTry.Add(key);
                        }
                    }
                }
                else
                {
                    _isPastSuccess=false;
                    if (!haveTry.Contains(key))
                    {
                        haveTry.Add(key);
                    }
                }
            }
            else 
            {
                Dictionary<Vector3, GameObject> selfDict = new Dictionary<Vector3, GameObject>();
                Dictionary<Vector3, GameObject> otherDict = new Dictionary<Vector3, GameObject>();
                List<Vector3> choice = new List<Vector3>();
                for (int i = 0; i<begin.transform.parent.childCount; i++)
                {
                    if (begin.transform.localPosition!=begin.transform.parent.GetChild(i).localPosition &&
                        begin.transform.localPosition!=-begin.transform.parent.GetChild(i).localPosition)
                    {
                        selfDict.Add(begin.transform.parent.GetChild(i).localPosition, begin.transform.parent.GetChild(i).gameObject);
                        choice.Add(begin.transform.parent.GetChild(i).localPosition);
                    }
                }
                Vector3 otherCubePos = new Vector3();
                otherCubePos=begin.transform.parent.localPosition+2*choice[key % 4];
                if (cubeDict.ContainsKey(otherCubePos))
                {
                    GameObject otherCube = cubeDict[otherCubePos];
                    for (int i = 0; i<otherCube.transform.childCount; i++)
                    {
                        otherDict.Add(otherCube.transform.GetChild(i).localPosition, otherCube.transform.GetChild(i).gameObject);
                    }
                    if (otherDict[begin.transform.localPosition].GetComponent<Faces>().visited==false)
                    {
                        StackLetter(pst[0], otherDict[begin.transform.localPosition]);
                        _isPastSuccess= true;
                        PasteString(pst.Substring(1), otherDict[begin.transform.localPosition]);
                        break;
                    }
                    else if (otherDict[begin.transform.localPosition].GetComponent<Faces>().letter==pst[0])
                    {
                        _isPastSuccess= true;
                        PasteString(pst.Substring(1), otherDict[begin.transform.localPosition]);
                        break;
                    }
                    else
                    {
                        _isPastSuccess=false;
                        if (!haveTry.Contains(key))
                        {
                            haveTry.Add(key);
                        }
                    }
                }
                else
                {
                    _isPastSuccess=false;
                    if (!haveTry.Contains(key))
                    {
                        haveTry.Add(key);
                    }
                }
            }
        }
    }
    private char GenerateRandomLetter()
    {
        int num = Random.Range(0, 26);
        return alphabet[num];
    }

    private void StackLetter(char letter,GameObject quad)
    {
        Texture2D texture;
        texture=Resources.Load<Texture2D>(letter.ToString().ToUpper());
        quad.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", texture);
        quad.GetComponent<Faces>().letter= letter;
        quad.GetComponent<Faces>().visited= true;
        pastedNum++;
    }

    private string ReverseString(string target)
    {
        string res = new string("");
        for(int i=target.Length-1; i>=0; i--)
        {
            res += target[i];
        }
        return res;
    } 

    string GenerateRandomWord(char mustContain='\0')
    {
        int allWordNum = WordList.Count;
        int index = Random.Range(1, allWordNum+1);
        if (mustContain=='\0') {
            while (true)
            {
                if (haveGenWordIndex.Contains(index)||false)
                {
                    index=Random.Range(1, allWordNum+1);
                }
                else
                {
                    haveGenWordIndex.Add(index);
                    return WordList[index.ToString()].ToUpper();
                }
            } 
        }
        else
        {
            while (true)
            {
                if(!WordList.ContainsKey(index.ToString())||!WordList[index.ToString()].ToUpper().Contains(mustContain))
                {
                    index=Random.Range(1, allWordNum + 1);
                }
                else
                {
                    haveGenWordIndex.Add(index);
                    return WordList[index.ToString()].ToUpper();
                }
            }
        }
    }

    public List<GameObject> GetNeighbor(GameObject begin)
    {
        List<GameObject> result = new List<GameObject>();
        if (true)
        {
            Dictionary<Vector3, GameObject> selfDict = new Dictionary<Vector3, GameObject>();
            List<Vector3> choice = new List<Vector3>();
            for (int i = 0; i<begin.transform.parent.childCount; i++)
            {
                if (begin.transform.localPosition!=begin.transform.parent.GetChild(i).localPosition &&
                    begin.transform.localPosition!=-begin.transform.parent.GetChild(i).localPosition)
                {
                    selfDict.Add(begin.transform.parent.GetChild(i).localPosition, begin.transform.parent.GetChild(i).gameObject);
                    choice.Add(begin.transform.parent.GetChild(i).localPosition);
                }
            }

            for(int i = 0; i<=3; i++)
            {
                result.Add(selfDict[choice[i]]);
            }
        }
        if (true)
        {
            Dictionary<Vector3, GameObject> selfDict = new Dictionary<Vector3, GameObject>();
            Dictionary<Vector3, GameObject> otherDict = new Dictionary<Vector3, GameObject>();
            List<Vector3> choice = new List<Vector3>();
            for (int i = 0; i<begin.transform.parent.childCount; i++)
            {
                if (begin.transform.localPosition!=begin.transform.parent.GetChild(i).localPosition &&
                    begin.transform.localPosition!=-begin.transform.parent.GetChild(i).localPosition)
                {
                    selfDict.Add(begin.transform.parent.GetChild(i).localPosition, begin.transform.parent.GetChild(i).gameObject);
                    choice.Add(begin.transform.parent.GetChild(i).localPosition);
                }
            }
            Vector3 otherCubePos;
            for (int x = 0; x<=3; x++)
            {
                otherCubePos=begin.transform.parent.localPosition+
                    2*(begin.transform.parent.localRotation*begin.transform.localPosition) +
                    2*(begin.transform.parent.localRotation*choice[x]);
                if (cubeDict.ContainsKey(NormalizeCubeVec3(otherCubePos)))
                {
                    GameObject otherCube = cubeDict[NormalizeCubeVec3(otherCubePos)];
                    for (int i = 0; i<otherCube.transform.childCount; i++)
                    {
                        otherDict.Add(otherCube.transform.GetChild(i).localPosition, otherCube.transform.GetChild(i).gameObject);
                    }
                    result.Add(otherDict[NormalizeVec3(Quaternion.Inverse(otherCube.transform.localRotation)*(-1*(begin.transform.parent.localRotation*choice[x])))]);
                    otherDict.Clear();
                }
            }
        }
        if (true)
        {
            Dictionary<Vector3, GameObject> selfDict = new Dictionary<Vector3, GameObject>();
            Dictionary<Vector3, GameObject> otherDict = new Dictionary<Vector3, GameObject>();
            List<Vector3> choice = new List<Vector3>();
            for (int i = 0; i<begin.transform.parent.childCount; i++)
            {
                if (begin.transform.localPosition!=begin.transform.parent.GetChild(i).localPosition &&
                    begin.transform.localPosition!=-begin.transform.parent.GetChild(i).localPosition)
                {
                    selfDict.Add(begin.transform.parent.GetChild(i).localPosition, begin.transform.parent.GetChild(i).gameObject);
                    choice.Add(begin.transform.parent.GetChild(i).localPosition);
                }
            }
            Vector3 otherCubePos = new Vector3();
            for (int x = 0; x<=3; x++)
            {
                otherCubePos=begin.transform.parent.localPosition+2*choice[x];
                if (cubeDict.ContainsKey(otherCubePos))
                {
                    GameObject otherCube = cubeDict[otherCubePos];
                    for (int i = 0; i<otherCube.transform.childCount; i++)
                    {
                        otherDict.Add(otherCube.transform.GetChild(i).localPosition, otherCube.transform.GetChild(i).gameObject);
                    }
                    result.Add(otherDict[NormalizeVec3(Quaternion.Inverse(otherCube.transform.localRotation)*
                        begin.transform.parent.localRotation*begin.transform.localPosition)]);
                    otherDict.Clear();
                }
            }
        }
        return result;
    }

    Vector3 NormalizeCubeVec3(Vector3 normal)
    {
        if (Mathf.Abs(normal.x)>=0.9f)
        {
            if (normal.x>0)
            {
                normal.x=1f;
            }
            else
            {
                normal.x=-1f;
            }
        }
        else
        {
            normal.x=0f;
        }
        if (Mathf.Abs(normal.y)>=0.9f)
        {
            if (normal.y>0)
            {
                normal.y=1f;
            }
            else
            {
                normal.y=-1f;
            }
        }
        else
        {
            normal.y=0f;
        }
        if (Mathf.Abs(normal.z)>=0.9f)
        {

            if (normal.z > 0)
            {
                normal.z = 1f;
            }
            else
            {
                normal.z = -1f;
            }
        }
        else
        {
            normal.z=0f;
        }
        return normal;
    }

    Vector3 NormalizeVec3(Vector3 normal)
    {
        if (Mathf.Abs(normal.x)>=0.4f)
        {
            if (normal.x>0)
            {
                normal.x=0.5f;
            }
            else
            {
                normal.x=-0.5f;
            }
        }
        else
        {
            normal.x=0f;
        }
        if (Mathf.Abs(normal.y)>=0.4f)
        {
            if (normal.y>0)
            {
                normal.y=0.5f;
            }
            else
            {
                normal.y=-0.5f;
            }
        }
        else
        {
            normal.y=0f;
        }
        if (Mathf.Abs(normal.z)>=0.4f)
        {

            if (normal.z > 0)
            {
                normal.z = 0.5f;
            }
            else
            {
                normal.z = -0.5f;
            }
        }
        else
        {
            normal.z=0f;
        }
        return normal;
    }

    Vector3 GenerateRandomVector3()
    {
        int num = Random.Range(0, 6);
        if (num == 0)
        {
            return new Vector3 { x = width/2, y= 0, z = 0 };
        }
        else if (num == 1)
        {
            return new Vector3 { x=0, y= width/2, z=0 };
        }
        else if (num == 2)
        {
            return new Vector3 { x=0, y= 0, z=width/2 };
        }
        else if (num==3)
        {
            return new Vector3 { x= -width/2, y=0, z= 0 };
        }
        else if (num==4)
        {
            return new Vector3 { x= 0, y=-width/2, z= 0 };
        }
        else
        {
            return new Vector3 { x=0, y= 0, z=-width/2 };
        }
    }
}
