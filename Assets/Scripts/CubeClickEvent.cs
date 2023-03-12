using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading.Tasks;
public class CubeClickEvent : MonoBehaviour
{
    // Start is called before the first frame update
    bool _isClicked = false;
    private PhysicsRaycaster m_Raycaster;
    void Awake()
    {
        m_Raycaster = FindObjectOfType<PhysicsRaycaster>();
        if (m_Raycaster == null)
        {
            Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
        }
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
                if (hit.collider.gameObject == this.gameObject)
                {
                    if (_isClicked)
                    {
                        _isClicked = false;
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
                        for (int i = 0; i < WholeCube.Slected.Count; i++)
                        {
                            GameObject _isSelected = WholeCube.Slected[i].gameObject;
                            if (_isSelected == this.gameObject)
                            {
                                WholeCube.Slected.Remove(_isSelected);
                            }
                        }
                    }
                    else
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
                            if (word == WholeCube.selectedWord.ToLower())
                            {
                                for (int i = 0; i < WholeCube.Matched.Count; i++)
                                {
                                    if(WholeCube.Matched[i] == word)
                                    {
                                        WholeCube._isUsed = true;
                                    }
                                }
                                if (WholeCube._isUsed == false)
                                {
                                    WholeCube.Matched.Add(word);
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
                                            GameObject.Find("Explosion").gameObject.AddComponent<Expolosion>().explosionPos = GameObject.Find("cube13").transform;
                                            await Task.Delay(800);
                                            Destroy(father);
                                        }
                                    }
                                    WholeCube.Slected.Clear();
                                    break;
                                }
                                WholeCube._isUsed = false;
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
                WholeCube.Slected.Clear();
            }
        }
    }
}