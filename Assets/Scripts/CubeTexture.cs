using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTexture : MonoBehaviour
{
    // Start is called before the first frame update
    private MeshRenderer meshRenderer;

    private Texture texture;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        //定义获取贴图的数量
        //动态加载贴图
        texture = Resources.Load("A") as Texture;
        meshRenderer.material.SetTexture("_MainTex", texture);
    }
}
