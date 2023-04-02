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
    public bool _isClicked = false;
    public bool _isVisited = false;     //生成字母的时候用
    private PhysicsRaycaster m_Raycaster;
    private AudioSource audioSource1, audioSource2;
    void Awake()
    {
        m_Raycaster = FindObjectOfType<PhysicsRaycaster>();
        if (m_Raycaster == null)
        {
            Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
        }
        audioSource1 = this.transform.parent.parent.GetComponent<AudioSource>();
        audioSource1.volume = AllMessageContainer.settingsInfo.effectSoundValue / 10;
    }

    // Update is called once per frame
    private async void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0F))//检测到碰撞
            {
                audioSource1.PlayOneShot(audioSource1.clip);
                if (hit.collider.gameObject == this.gameObject)
                {
                    if (_isClicked)
                    {
                        int beginIndex = WholeCube.Slected.IndexOf(gameObject);
                        for (int i = WholeCube.Slected.Count - 1; i >= beginIndex; i--)
                        {
                            Debug.Log(i);
                            WholeCube.Slected[i].GetComponent<CubeClickEvent>()._isClicked = false;
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
                        if (WholeCube.WordList.ContainsValue(WholeCube.selectedWord))   //回退导致新单词
                        {
                            string link = "";
                            foreach (GameObject _isSelected in WholeCube.Slected)
                            {
                                link += (_isSelected.transform.parent.name + _isSelected.name);
                            }
                            if (!WholeCube.hasLinked.Contains(link))
                            {
                                WholeCube.hasLinked.Add(link);
                                WholeCube.Matched.Add(WholeCube.selectedWord);
                                CombineWord(WholeCube.selectedWord);
                                List<GameObject> dsj = new List<GameObject>();

                                Debug.Log("right" + this.gameObject.GetComponent<Faces>().Times());
                                foreach (GameObject _isSelected in WholeCube.Slected)
                                {
                                    _isSelected.GetComponent<Faces>().TimeUp();
                                }
                                foreach (GameObject _isSelected in WholeCube.Slected)
                                {
                                    if (_isSelected.GetComponent<Faces>().Times() == 1)
                                    {
                                        _isSelected.GetComponent<MeshRenderer>().material.color = new Color(1, 0.6f, 0.6f);//点击改变面颜色
                                        _isSelected.GetComponent<CubeClickEvent>()._isClicked = false;
                                    }
                                    else if (_isSelected.GetComponent<Faces>().Times() == 2)
                                    {
                                        _isSelected.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0.6f);//点击改变面颜色
                                        _isSelected.gameObject.GetComponent<CubeClickEvent>()._isClicked = false;
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
                                        GameObject.Find("Explosion").gameObject.AddComponent<Expolosion>().explosionPos = GameObject.Find("cube13").transform;
                                        audioSource2  = GameObject.Find("Explosion").gameObject.GetComponent<AudioSource>();
                                        audioSource2.volume = AllMessageContainer.settingsInfo.effectSoundValue / 4;
                                        audioSource2.PlayOneShot(audioSource2.clip);
                                        dsj.Add(father);
                                    }
                                }
                                await Task.Delay(800);
                                foreach (var father in dsj)
                                {
                                    WholeCube.cubeMatchQuad.Remove(father);
                                    transform.parent.parent.gameObject.GetComponent<WholeCube>().cubeDict.Remove(father.transform.localPosition);
                                    Destroy(father);
                                }
                                WholeCube.Slected.Clear();
                            }
                        }
                        WholeCube.selectedWord = string.Empty;
                    }
                    else
                    {
                        List<GameObject> neighbor = new List<GameObject>();
                        if (WholeCube.Slected.Count!=0)
                        {
                            neighbor = transform.parent.parent.gameObject.
                            GetComponent<WholeCube>().GetNeighbor_Safe(WholeCube.Slected[WholeCube.Slected.Count-1]);
                        }
                        if (neighbor.Contains(gameObject)||WholeCube.Slected.Count==0)
                        {
                            _isClicked = true;
                            this.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 0.6f, 0, 1);//点击改变面颜色
                            WholeCube.Slected.Add(this.gameObject);
                            foreach (GameObject _isSelected in WholeCube.Slected)
                            {
                                WholeCube.selectedWord += _isSelected.GetComponent<Faces>().letter;
                            }
                            Debug.Log(WholeCube.selectedWord);
                            foreach (string word in WholeCube.WordList.Values)
                            {
                                if (word == WholeCube.selectedWord.ToLower())       //按顺序点产生新单词
                                {
                                    string link = "";
                                    foreach (GameObject _isSelected in WholeCube.Slected)
                                    {
                                        link +=(_isSelected.transform.parent.name + _isSelected.name);
                                    }
                                    if (!WholeCube.hasLinked.Contains(link))
                                    {
                                        WholeCube.hasLinked.Add(link);
                                        WholeCube.Matched.Add(word);
                                        CombineWord(word);
                                        List<GameObject> dsj = new List<GameObject>();
                                        Debug.Log("right" + this.gameObject.GetComponent<Faces>().Times());
                                        foreach (GameObject _isSelected in WholeCube.Slected)
                                        {
                                            _isSelected.GetComponent<Faces>().TimeUp();
                                        }
                                        foreach (GameObject _isSelected in WholeCube.Slected)
                                        {
                                            if (_isSelected.GetComponent<Faces>().Times() == 1)
                                            {
                                                _isSelected.GetComponent<MeshRenderer>().material.color = new Color(1, 0.6f, 0.6f);//点击改变面颜色
                                                _isSelected.GetComponent<CubeClickEvent>()._isClicked = false;
                                            }
                                            else if (_isSelected.GetComponent<Faces>().Times() == 2)
                                            {
                                                _isSelected.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0.6f);//点击改变面颜色
                                                _isSelected.gameObject.GetComponent<CubeClickEvent>()._isClicked = false;
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
                                                GameObject.Find("Explosion").gameObject.AddComponent<Expolosion>().explosionPos = GameObject.Find("cube13").transform;
                                                audioSource2  = GameObject.Find("Explosion").gameObject.GetComponent<AudioSource>();
                                                audioSource2.volume = AllMessageContainer.settingsInfo.effectSoundValue / 4;
                                                audioSource2.PlayOneShot(audioSource2.clip);
                                                dsj.Add(father);
                                            }
                                        }
                                        await Task.Delay(800);
                                        foreach (var father in dsj)
                                        {
                                            WholeCube.cubeMatchQuad.Remove(father);
                                            transform.parent.parent.gameObject.GetComponent<WholeCube>().cubeDict.Remove(father.transform.localPosition);
                                            Destroy(father);
                                        }
                                        WholeCube.Slected.Clear();
                                        break;
                                    }
                                    WholeCube._isUsed = false;
                                }
                            }
                        }
                        else
                        {
                            foreach(GameObject gameObj in WholeCube.Slected)
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
                                gameObj.GetComponent<CubeClickEvent>()._isClicked = false;
                            }
                            WholeCube.Slected.Clear();
                            WholeCube.Slected.Add(gameObject);
                            _isClicked = true;
                            this.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 0.6f, 0, 1);//点击改变面颜色
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
                if (this.gameObject.GetComponent<Faces>().Times() == 0)
                {
                    this.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                }
                else if (this.gameObject.GetComponent<Faces>().Times() == 1)
                {
                    this.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 0.6f, 0.6f);
                }
                else if (this.gameObject.GetComponent<Faces>().Times() == 2)
                {
                    this.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0.6f);
                }
                foreach(GameObject hasClicked in WholeCube.Slected)
                {
                    hasClicked.GetComponent<CubeClickEvent>()._isClicked = false;
                }
                WholeCube.Slected.Clear();
            }
        }
    }

    private void ShowError(string message)
    {
        GameObject.Find("Error").GetComponent<Text>().text = message;
    }

    private void CombineWord(string word)
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

    private void AddMoney(int coin, int crystal)
    {
        AllMessageContainer.playerInfo.coin += coin;
        AllMessageContainer.playerInfo.crystal += crystal;
    }
}