using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseModePageClickEvent : MonoBehaviour
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

    public void GotoSettings()
    {
        transform.Find("Settings").gameObject.SetActive(true);
    }

    public void GotoHome()
    {
        AllMessageContainer.gameStatus.ifStartGame=false;
        StartCoroutine(loadScene("BeginUI"));
    }
    private IEnumerator loadScene(string which) //º”‘ÿ≥°æ∞
    {
        operation=SceneManager.LoadSceneAsync(which);
        yield return operation;
    }

    public void GotoBreakThrough()
    {
        StartCoroutine(loadScene("BreakThrough"));
    }

    public void GotoCustom()
    {
        StartCoroutine(loadScene("CustomMode"));
    }

    public void GotoEndless()
    {
        AllMessageContainer.gameStatus.gameMode=GameMode.Endless;
        StartCoroutine(loadScene("GamePage"));
    }
}
