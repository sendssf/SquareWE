using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//�˽ű���������Shopҳ���������¼�
public class ShopPageClickEvent : MonoBehaviour
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

    public void QuitDetails()
    {
        transform.Find("Details").gameObject.SetActive(false);
    }

    //��������Ʒ���˵�������ຯ��������ΪShowDetails_��Ʒ���ƣ�ֻ���޸����һ���ַ�������
    public void ShowDetails_PerspectiveCamera() 
    {
        transform.Find("Details").gameObject.SetActive(true);
        transform.Find("Details").Find("Contain").Find("Viewport")
            .Find("Content").Find("Describe").GetComponent<Text>()
            .text="I am a Camera";
    }

    public void GotoHome()
    {
        StartCoroutine(loadScene("BeginUI"));
    }

    public void GotoSettings()
    {
        transform.Find("Settings").gameObject.SetActive(true);
    }
    private IEnumerator loadScene(string which)
    {
        operation=SceneManager.LoadSceneAsync(which);
        operation.allowSceneActivation = true;
        yield return operation;
    }
}
