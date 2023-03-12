using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    //将六个面组成一个立方体

    private void Awake()
    {
        Transform[] squad = new Transform[6];
        for (int i = 0; i < 6; i++)
        {
            squad[i] = this.gameObject.transform.GetChild(i);
        }

        squad[0].Rotate(90, 0, 0);
        Vector3 pos = new Vector3(0, 0.5f, 0);
        squad[0].localPosition = pos;

        squad[1].Rotate(-90, 0, 0);
        pos = new Vector3(0, -0.5f, 0);
        squad[1].localPosition = pos;

        squad[2].Rotate(0, 90, 0);
        pos = new Vector3(-0.5f, 0, 0);
        squad[2].localPosition = pos;

        squad[3].Rotate(0, -90, 0);
        pos = new Vector3(0.5f, 0, 0);
        squad[3].localPosition = pos;

        squad[4].Rotate(0, 0, 0);
        pos = new Vector3(0, 0, -0.5f);
        squad[4].localPosition = pos;

        squad[5].Rotate(0, 180, 0);
        pos = new Vector3(0, 0, 0.5f);
        squad[5].localPosition = pos;
    }
    void Start()
    {

    }
        
}
