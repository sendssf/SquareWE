using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    AsyncOperation operation;
    void Start()
    {
        transform.Find("Failed").gameObject.SetActive(false);
        transform.Find(AllMessageContainer.gameStatus.overlayerName).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(AllMessageContainer.gameStatus.ifonline && !AllMessageContainer.gameStatus.ifHost && OnlineMode.ifPrepared)
        {
            transform.Find(AllMessageContainer.gameStatus.overlayerName).gameObject.SetActive(false);
            StartCoroutine(unloadScene("3DOverlayer"));
        }
    }

    public void SureFinalTry()
    {
        AllMessageContainer.gameStatus.finalTry = true;
        transform.Find("Victory").gameObject.SetActive(false);
        StartCoroutine(unloadScene("3DOverlayer"));
    }

    public void DenyFinalTry()
    {
        AllMessageContainer.gameStatus.finalTry = false;
        StartCoroutine(loadScene("ChooseModeUI"));
    }

    public void QuitSettings()
    {   
        transform.Find("Settings").gameObject.SetActive(false);
        StartCoroutine(unloadScene("3DOverlayer"));
    }

    public void QuitTips()
    {
        transform.Find("Tips").gameObject.SetActive(false);
        StartCoroutine(unloadScene("3DOverlayer"));
    }

    IEnumerator unloadScene(string name)
    {
        operation=SceneManager.UnloadSceneAsync(name);
        yield return operation;
    }

    IEnumerator loadScene(string name)
    {
        operation = SceneManager.LoadSceneAsync(name);
        yield return operation;
    }
}
