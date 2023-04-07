using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageNumManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int newMessageNum = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRedPoint();
    }

    void UpdateRedPoint()
    {
        if (newMessageNum == 0)
        {
            transform.Find("Mask").Find("Back").Find("Number").gameObject.GetComponent<Text>().text = 0.ToString();
            transform.Find("Mask").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("Mask").Find("Back").Find("Number").gameObject.GetComponent<Text>().text = newMessageNum.ToString();
            transform.Find("Mask").gameObject.SetActive(true);
        }
    }
}
