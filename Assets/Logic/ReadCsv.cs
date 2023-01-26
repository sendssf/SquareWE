using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//读取StreamingAssets文件夹下的csv文件
public class ReadCsv : MonoBehaviour
{
    // Start is called before the first frame update
    Dictionary<string, string> dict;    //这个字典键为标号，值为单词
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Dictionary<string,string> ReadCsvFile(string filename)
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
}
