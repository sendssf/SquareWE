using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HonorCs : MonoBehaviour
{
    Material mat;

    private void Start()
    {
        mat = this.GetComponent<MeshRenderer>().materials[1]; // 获取边框材质
    }
    void OnMouseEnter()//3D 物体才能用，2d 用event trigger
    {
        mat.SetFloat("_Size", 0.06f);//鼠标移动到物体上，边框加粗
        mat.SetColor("_OutlineColor", new Color(1, 0.6f, 0));//鼠标移动到物体上，边框变橘色

    }
    void OnMouseExit()
    {
        mat.SetFloat("_Size", 0.04f);//鼠标移开，边框变细
        mat.SetColor("_OutlineColor", new Color(0, 0, 0, 0.6f));//鼠标移开，边框变为黑色
    }
}
