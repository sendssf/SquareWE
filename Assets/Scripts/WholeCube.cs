using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WholeCube : MonoBehaviour
{
    public static List<GameObject> Slected = new List<GameObject>();
    public static bool _isUsed = false;
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
    }

    void MakeLettersInOrder(GameObject beginQuad)
    {
        //将所有子物体和坐标添加进字典
        int cubeCount = transform.childCount;
        for(int i = 0; i<cubeCount; i++)
        {
            cubeDict.Add(transform.GetChild(i).transform.position, transform.GetChild(i).gameObject);
        }

    }

    string GenerateRandomWord(char mustContain='\0')
    {
        int allWordNum = WordList.Count;
        int index = Random.Range(0, allWordNum);
        if (mustContain=='\0') {
            while (true)
            {
                if (haveGenWordIndex.Contains(index))
                {
                    index=Random.Range(0, allWordNum);
                }
                else
                {
                    haveGenWordIndex.Add(index);
                    return WordList[index.ToString()];
                }
            } 
        }
        else
        {
            while (true)
            {
                if(haveGenWordIndex.Contains(index) || !WordList[index.ToString()].Contains(mustContain))
                {
                    index=Random.Range(0, allWordNum);
                }
                else
                {
                    haveGenWordIndex.Add(index);
                    return WordList[index.ToString()];
                }
            }
        }
    }
}
