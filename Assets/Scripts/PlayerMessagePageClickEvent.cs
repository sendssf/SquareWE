using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//�˽ű��������������Ϣҳ���������¼�
public class PlayerMessagePageClickEvent : MonoBehaviour
{
    // Start is called before the first frame update
    AsyncOperation operation;		//�첽���������
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void returnMainPage()    //������ҳ
    {
        StartCoroutine(loadScene("BeginUI"));	//����Э��
    }

    private IEnumerator loadScene(string which) //���س���
    {
        operation=SceneManager.LoadSceneAsync(which);
        yield return operation;
    }

    public void GotoSettings()  //ǰ������
    {
        transform.Find("Settings").gameObject.SetActive(true);
    }
}