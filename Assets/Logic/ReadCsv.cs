using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//读取StreamingAssets文件夹下的csv文件
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
