using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//��ȡStreamingAssets�ļ����µ�csv�ļ�
public class ReadCsv : MonoBehaviour
{   public static Dictionary<string,string> ReadCsvFile(string filename)
    {   
        var res=new Dictionary<string,string>();
        using (var sr=new StreamReader(Application.streamingAssetsPath + @"\\"+filename))
        {
            while(!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] value = line.Split(",");
                res.Add(value[0], value[1]);
            }
        }
        return res;
    }

    public static Dictionary<string, string> ReadCsvFile_Extern(string filename)
    {
        var res = new Dictionary<string, string>();
        using (var sr = new StreamReader(filename))
        {
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] value = line.Split(",");
                res.Add(value[0], value[1]);
            }
        }
        return res;
    }
}
