using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HonorCs : MonoBehaviour
{
    Material mat;

    private void Start()
    {
        mat = this.GetComponent<MeshRenderer>().materials[1]; // ��ȡ�߿����
    }
    void OnMouseEnter()//3D ��������ã�2d ��event trigger
    {
        mat.SetFloat("_Size", 0.06f);//����ƶ��������ϣ��߿�Ӵ�
        mat.SetColor("_OutlineColor", new Color(1, 0.6f, 0));//����ƶ��������ϣ��߿����ɫ

    }
    void OnMouseExit()
    {
        mat.SetFloat("_Size", 0.04f);//����ƿ����߿��ϸ
        mat.SetColor("_OutlineColor", new Color(0, 0, 0, 0.6f));//����ƿ����߿��Ϊ��ɫ
    }
}
