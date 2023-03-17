using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingradients : MonoBehaviour
{
    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 27; i++)
        {
            GameObject cube1 = Instantiate(cube, this.transform);
            cube1.name = "cube" + i.ToString();
            cube1.transform.parent = this.transform;
        }
        this.gameObject.AddComponent<WholeCube>();
    }
}