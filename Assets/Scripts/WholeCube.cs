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
    }

    void MakeLettersInOrder(GameObject beginQuad)
    {

    }
}
