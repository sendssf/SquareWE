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
        //�����ȡ��ͼ������
        //��̬������ͼ
        texture = Resources.Load("A") as Texture;
        meshRenderer.material.SetTexture("_MainTex", texture);
    }
}
