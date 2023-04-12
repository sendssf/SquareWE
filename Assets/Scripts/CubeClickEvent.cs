using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.UI;
using System;

public class CubeClickEvent : MonoBehaviour
{
    // Start is called before the first frame update
    private PhysicsRaycaster m_Raycaster;
    private AudioSource audioSource1, audioSource2;
    string prepareWord = "";
    string link = "";
    public static bool ifShowVictory = false;
    private AudioClip combine;
    OnlineMode onlineMode;
    void Awake()
    {
        AllMessageContainer.gameStatus.finalTry = false;
        m_Raycaster = FindObjectOfType<PhysicsRaycaster>();
        if (m_Raycaster == null)
        {
            Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
        }
        audioSource1 = transform.GetComponent<AudioSource>();
        audioSource1.volume = AllMessageContainer.settingsInfo.effectSoundValue;
        audioSource2  = GameObject.Find("Explosion").gameObject.GetComponent<AudioSource>();
        audioSource2.volume = AllMessageContainer.settingsInfo.effectSoundValue;
        combine = GameObject.Find("EventSystem").GetComponent<AudioSource>().clip;
    }
    private void Start()
    {
        ifShowVictory = false;
    }

    // Update is called once per frame
    private async void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EndShowCombine();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0F))//检测到碰撞
            {
                if (hit.transform.parent.parent.name == "Third-orderCube")
                {
                    audioSource1.PlayOneShot(audioSource1.clip);
                    if (hit.transform.GetComponent<Faces>()._isClicked)
                    {
                        int beginIndex = WholeCube.Slected.IndexOf(hit.transform.gameObject);
                        for (int i = WholeCube.Slected.Count - 1; i >= beginIndex; i--)
                        {
                            WholeCube.Slected[i].GetComponent<Faces>()._isClicked = false;
                            if (WholeCube.Slected[i].GetComponent<Faces>().Times() == 0)
                            {
                                WholeCube.Slected[i].GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                            }
                            else if (WholeCube.Slected[i].GetComponent<Faces>().Times() == 1)
                            {
                                WholeCube.Slected[i].GetComponent<MeshRenderer>().material.color = new Color(1, 0.6f, 0.6f);
                            }
                            else if (WholeCube.Slected[i].GetComponent<Faces>().Times() == 2)
                            {
                                WholeCube.Slected[i].GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0.6f);
                            }
                            WholeCube.Slected.RemoveAt(i);
                        }
                        foreach (GameObject _isSelected in WholeCube.Slected)
                        {
                            WholeCube.selectedWord += _isSelected.GetComponent<Faces>().letter;
                        }
                        Debug.Log(WholeCube.selectedWord);
                        if (WholeCube.WordList.ContainsValue(WholeCube.selectedWord.ToLower()))   //回退导致新单词
                        {
                            link = "";
                            foreach (GameObject _isSelected in WholeCube.Slected)
                            {
                                link += (_isSelected.transform.parent.name + _isSelected.name);
                            }
                            if (!WholeCube.hasLinked.Contains(link))
                            {
                                ShowCanCombine(WholeCube.selectedWord.ToLower());
                                prepareWord = WholeCube.selectedWord.ToLower();
                            }
                        }
                        WholeCube.selectedWord = string.Empty;
                    }
                    else
                    {
                        List<GameObject> neighbor = new List<GameObject>();
                        if (WholeCube.Slected.Count != 0)
                        {
                            neighbor = transform.gameObject.GetComponent<WholeCube>().GetNeighbor_Safe(WholeCube.Slected[WholeCube.Slected.Count - 1]);
                        }
                        if (neighbor.Contains(hit.transform.gameObject) || WholeCube.Slected.Count == 0)
                        {
                            hit.transform.GetComponent<Faces>()._isClicked = true;
                            hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 0.6f, 0, 1);//点击改变面颜色
                            WholeCube.Slected.Add(hit.transform.gameObject);
                            foreach (GameObject _isSelected in WholeCube.Slected)
                            {
                                WholeCube.selectedWord += _isSelected.GetComponent<Faces>().letter;
                            }
                            Debug.Log(WholeCube.selectedWord);
                            if (WholeCube.WordList.ContainsValue(WholeCube.selectedWord.ToLower()))   //顺序导致新单词
                            {
                                link = "";
                                foreach (GameObject _isSelected in WholeCube.Slected)
                                {
                                    link += (_isSelected.transform.parent.name + _isSelected.name);
                                }
                                if (!WholeCube.hasLinked.Contains(link))
                                {
                                    //Debug.Log("EnterShow");
                                    ShowCanCombine(WholeCube.selectedWord.ToLower());
                                    prepareWord = WholeCube.selectedWord.ToLower();
                                }
                            }
                        }
                        else
                        {
                            foreach (GameObject gameObj in WholeCube.Slected)
                            {
                                if (gameObj.GetComponent<Faces>().Times() == 0)
                                {
                                    gameObj.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                                }
                                else if (gameObj.GetComponent<Faces>().Times() == 1)
                                {
                                    gameObj.GetComponent<MeshRenderer>().material.color = new Color(1, 0.6f, 0.6f);
                                }
                                else if (gameObj.GetComponent<Faces>().Times() == 2)
                                {
                                    gameObj.GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0.6f);
                                }
                                gameObj.GetComponent<Faces>()._isClicked = false;
                            }
                            WholeCube.Slected.Clear();
                            WholeCube.Slected.Add(hit.transform.gameObject);
                            hit.transform.GetComponent<Faces>()._isClicked = true;
                            hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 0.6f, 0, 1);//点击改变面颜色
                            foreach (GameObject _isSelected in WholeCube.Slected)
                            {
                                WholeCube.selectedWord += _isSelected.GetComponent<Faces>().letter;
                            }
                        }
                        WholeCube.selectedWord = string.Empty;
                    }
                }
            }
            else
            {
                foreach(GameObject hasClicked in WholeCube.Slected)
                {
                    if (hasClicked.transform.gameObject.GetComponent<Faces>().Times() == 0)
                    {
                        hasClicked.transform.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                    }
                    else if (hasClicked.transform.gameObject.GetComponent<Faces>().Times() == 1)
                    {
                        hasClicked.transform.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 0.6f, 0.6f);
                    }
                    else if (hasClicked.transform.gameObject.GetComponent<Faces>().Times() == 2)
                    {
                        hasClicked.transform.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0.6f);
                    }
                    hasClicked.GetComponent<Faces>()._isClicked = false;
                }
                WholeCube.Slected.Clear();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            await PressSpaceToSure();
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            await VictoryPrimary();
        }
        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            await VictoryFinally();
        }
        if (transform.childCount<=3 && !AllMessageContainer.gameStatus.finalTry && !ifShowVictory 
            && !AllMessageContainer.gameStatus.finalTry)
        {
            ifShowVictory = true;
            if (AllMessageContainer.gameStatus.ifonline == false)
            {
                await VictoryPrimary();
            }
            else
            {
                OnlineMode.Victory();
                await VictoryFinally();
            }
        }
        if (transform.childCount == 0 && !ifShowVictory)
        {
            ifShowVictory = true;
            await VictoryFinally();
        }
    }

    async private Task PressSpaceToSure()
    {
        if (GameObject.Find("BackgroundCavas").transform.Find("WillCombine").gameObject.activeInHierarchy)
        {
            WholeCube.hasLinked.Add(link);
            WholeCube.Matched.Add(WholeCube.selectedWord);
            CombineWord(prepareWord);
            AllMessageContainer.UpdateLevel();
            GameObject.Find("BackgroundCavas").GetComponent<GameCanvasClickEvent>().ShowMoney();
            List<GameObject> dsj = new List<GameObject>();

            //Debug.Log("right" + this.gameObject.GetComponent<Faces>().Times());
            foreach (GameObject _isSelected in WholeCube.Slected)
            {
                _isSelected.GetComponent<Faces>().TimeUp();
                if (AllMessageContainer.gameStatus.ifonline == true)
                {
                    OnlineMode.TransmitStatus("point cube", _isSelected.transform.parent.gameObject.name.Substring(4)+","+_isSelected.name, "");//可能延时出问题
                }
            }
            foreach (GameObject _isSelected in WholeCube.Slected)
            {
                if (_isSelected.GetComponent<Faces>().Times() == 1)
                {
                    _isSelected.GetComponent<MeshRenderer>().material.color = new Color(1, 0.6f, 0.6f);//点击改变面颜色
                    _isSelected.GetComponent<Faces>()._isClicked = false;
                    audioSource2.PlayOneShot(combine);
                }
                else if (_isSelected.GetComponent<Faces>().Times() == 2)
                {
                    _isSelected.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0.6f);//点击改变面颜色
                    _isSelected.gameObject.GetComponent<Faces>()._isClicked = false;
                    audioSource2.PlayOneShot(combine);
                }
                else if (_isSelected.GetComponent<Faces>().Times() == 3)
                {
                    GameObject father = _isSelected.transform.parent.gameObject;
                    father.transform.parent.GetComponent<WholeCube>().position.Remove(father.transform.position);
                    father.transform.parent.GetComponent<WholeCube>()._isCleared = true;
                    for (int i = 0; i < 6; i++)
                    {
                        father.transform.GetChild(i).gameObject.GetComponent<Faces>().rb.isKinematic = false;
                        father.transform.GetChild(i).gameObject.GetComponent<Faces>().rb.useGravity = true;
                    }
                    father.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    GameObject.Find("Explosion").gameObject.AddComponent<Expolosion>().explosionPos = GameObject.Find("Third-orderCube").transform;
                    var ps = Instantiate(father.GetComponent<Cube>().particle,father.transform);
                    ps.transform.localPosition = Vector3.zero;
                    ps.Play();
                    audioSource2.PlayOneShot(audioSource2.clip);
                    dsj.Add(father);
                }
            }
            await Task.Delay(800);
            foreach (var father in dsj)
            {
                WholeCube.cubeMatchQuad.Remove(father);
                gameObject.GetComponent<WholeCube>().cubeDict.Remove(father.transform.localPosition);
                Destroy(father);
            }
            WholeCube.Slected.Clear();
        }
    }

    private void ShowError(string message)
    {
        GameObject.Find("Error").GetComponent<Text>().text = message;
    }

    private async Task VictoryPrimary()
    {
        AddExperience(500);
        AddMoney(50, 3);
        AllMessageContainer.SendBasicInfo();
        await GameObject.Find("BackgroundCavas").gameObject.GetComponent<GameCanvasClickEvent>().VictoryShow();
        return;
    }

    private async Task VictoryFinally()
    {
        AddExperience(1000);
        AddMoney(150, 15);
        AllMessageContainer.SendBasicInfo();
        await GameObject.Find("BackgroundCavas").gameObject.GetComponent<GameCanvasClickEvent>().FinalVictory();
        return;
    }

    public void CombineWord(string word)
    {
        if(word == null || word.Length == 0)
        {
            Debug.Log("word empty!");
        }
        else
        {
            if (AllMessageContainer.playerInfo.worldList.ContainsKey(word))
            {
                AllMessageContainer.playerInfo.worldList[word] = (Convert.ToInt32(AllMessageContainer.playerInfo.worldList[word]) + 1).ToString();
            }
            else
            {
                AllMessageContainer.playerInfo.worldList.Add(word, "1");
            }
            string res = WebController.Post(WebController.rootIP + API_Local.postWord, JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                {"nickname", AllMessageContainer.playerInfo.playerName },
                {"word", word }
            }));
            switch(res)
            {
                case WebController.ServerNotFound:
                    ShowError("Network error! Cannot connect with server. Please check and try again!");
                    break;
                case WebController.Success:
                    AddExperience(word);
                    AddMoney(word);
                    string res2 = AllMessageContainer.SendBasicInfo();
                    switch (res2)
                    {
                        case WebController.ServerNotFound:
                            ShowError("Network error! Cannot connect with server. Please check and try again!");
                            break;
                        case WebController.Success:

                            break;
                    }
                    break;
            }
        }
    }

    private void AddExperience(string word)
    {
        float len = word.Length;
        float div = Convert.ToInt32(AllMessageContainer.playerInfo.worldList[word]);
        AllMessageContainer.playerInfo.experience +=Mathf.CeilToInt(50+ (1000 - div) * 0.1f * Mathf.Log(len));
    }

    private void AddExperience(int experience)
    {
        AllMessageContainer.playerInfo.experience += experience;
    }

    private void AddMoney(string word)
    {
        float len = word.Length;
        float div = Convert.ToInt32(AllMessageContainer.playerInfo.worldList[word]);
        AllMessageContainer.playerInfo.coin += Mathf.CeilToInt(5 + (1000 - div) * 0.01f * Mathf.Log(len));
        if(len > 6)
        {
            AllMessageContainer.playerInfo.crystal += Mathf.CeilToInt(1 + Mathf.Log(len - 6));
        }
    }

    private void ShowCanCombine(string word)
    {
        GameObject.Find("BackgroundCavas").transform.Find("WillCombine").gameObject.SetActive(true);
        GameObject.Find("Word").GetComponent<Text>().text = word;
    }

    private void EndShowCombine()
    {
        GameObject.Find("BackgroundCavas").transform.Find("WillCombine").gameObject.SetActive(false);
        GameObject.Find("Word").GetComponent<Text>().text = "";
    }

    private void AddMoney(int coin, int crystal)
    {
        AllMessageContainer.playerInfo.coin += coin;
        AllMessageContainer.playerInfo.crystal += crystal;
    }
}