using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreakThroughClickEvent : MonoBehaviour
{
    // Start is called before the first frame update
    AsyncOperation operation;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GotoHome()
    {
        StartCoroutine(loadScene("BeginUI"));
    }

    public void GotoSettings()
    {
        transform.Find("Settings").gameObject.SetActive(true);
    }

    private IEnumerator loadScene(string which) //º”‘ÿ≥°æ∞
    {
        operation=SceneManager.LoadSceneAsync(which);
        yield return operation;
    }

    public void ReturnLastPage()
    {
        StartCoroutine(loadScene("ChooseModelUI"));
    }
}
