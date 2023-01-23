using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//此脚本用来管理粒子特效的行为
public class ParticalControl1 : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 dir = Vector3.right;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x>=80)
        {
            dir=Vector3.left;
        }
        else if (transform.position.x<=-80)
        {
            dir=Vector3.right;
        }
        transform.Translate(dir*0.5f);
    }
}
