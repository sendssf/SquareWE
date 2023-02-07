using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faces : MonoBehaviour
{
    // Start is called before the first frame update
    private MeshRenderer meshRenderer;
    public Rigidbody rb;
    private Texture texture;
    private MeshCollider meshCollider;
    public char[] alphabet = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

    public char letter = new char();
    public int times = 0;//点击次数
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
        //定义获取贴图的数量
        //动态加载贴图
        System.Random random = new System.Random(~unchecked((int)DateTime.Now.Ticks));
        letter = alphabet[random.Next(0, alphabet.Length)];
        texture = Resources.Load(letter.ToString()) as Texture;
        meshRenderer.material.SetTexture("_MainTex", texture);
        rb =this.gameObject.AddComponent<Rigidbody>();
        meshCollider.convex = true;
        rb.useGravity = false;
        rb.isKinematic = true;
    }
    public void TimeUp()
    {
        times += 1;
        if(times == 4)
        {
            times = 0;
        }
    }

    public int Times()
    {
        return times;
    }
}
