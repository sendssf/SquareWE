using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class WholeCube : MonoBehaviour
{
    public static List<GameObject> Slected = new List<GameObject>();
    public static bool _isUsed = false;
    private float width = 1f;
    private int pastedNum = 0;
    public static List<string> Matched = new List<string>();
    public static string selectedWord = string.Empty;
    public static Dictionary<string, string> WordList = ReadCsv.ReadCsvFile("word6.csv");
    public static char[] alphabet = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    private static List<int> haveGenWordIndex= new List<int>();
    public Dictionary<Vector3,GameObject> cubeDict= new Dictionary<Vector3,GameObject>();
    //使27个立方体组成一个大立方体
    private void Awake()
    {
        
    }
    void Start()
    {
        Debug.Log("start");
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
        int beginIndex=Random.Range(0, transform.childCount);
        int quadBeginIndex = Random.Range(0, 6);
        MakeLettersInOrder(transform.GetChild(beginIndex).GetChild(quadBeginIndex).gameObject);
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
                if(/*haveGenWordIndex.Contains(index) ||*/ !WordList[index.ToString()].ToUpper().Contains(mustContain))
                {
                    index=Random.Range(1, allWordNum + 1);
                }
                else
                {
                    haveGenWordIndex.Add(index);
                    Debug.Log(WordList[index.ToString()].ToUpper());
                    return WordList[index.ToString()].ToUpper();
                }
            }
        }
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
