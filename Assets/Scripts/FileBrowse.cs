using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class FileBrowse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static string OpenWindowDialog(string title,string filter)
    {

        string filepath = "";
        OpenFileDlg pth = new OpenFileDlg();
        pth.structSize = System.Runtime.InteropServices.Marshal.SizeOf(pth);
        //pth.filter = "JSON file(*.json)";//��ʲô�ļ����;��޸Ĵ˴�
        pth.filter = filter;
        pth.file = new string(new char[256]);
        pth.maxFile = pth.file.Length;
        pth.fileTitle = new string(new char[64]);
        pth.maxFileTitle = pth.fileTitle.Length;
        pth.initialDir = "C:\\";  // default path
        pth.title = title;  //����
        pth.defExt = "PNG";

        //ע�� һ����Ŀ��һ��Ҫȫѡ ����0x00000008�Ҫȱ��
        //0x00080000   �Ƿ�ʹ���°��ļ�ѡ�񴰿ڣ�0x00000200   �Ƿ���Զ�ѡ�ļ�
        pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;

        if (OpenFileDialog.GetOpenFileName(pth))
        {
            filepath = pth.file;//ѡ����ļ�·��;
            return filepath;
        }
        return null;
    }

}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class FileDialog
{
    public int structSize = 0;
    public IntPtr dlgOwner = IntPtr.Zero;
    public IntPtr instance = IntPtr.Zero;
    public String filter = null;
    public String customFilter = null;
    public int maxCustFilter = 0;
    public int filterIndex = 0;
    public String file = null;
    public int maxFile = 0;
    public String fileTitle = null;
    public int maxFileTitle = 0;
    public String initialDir = null;
    public String title = null;
    public int flags = 0;
    public short fileOffset = 0;
    public short fileExtension = 0;
    public String defExt = null;
    public IntPtr custData = IntPtr.Zero;
    public IntPtr hook = IntPtr.Zero;
    public String templateName = null;
    public IntPtr reservedPtr = IntPtr.Zero;
    public int reservedInt = 0;
    public int flagsEx = 0;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]

public class OpenFileDialog
{
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetOpenFileName([In, Out] OpenFileDlg ofd);
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class OpenFileDlg : FileDialog
{

}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class SaveFileDlg : FileDialog
{

}
public class SaveFileDialog
{
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetSaveFileName([In, Out] SaveFileDlg ofd);
}

