using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

enum PressState
{
    None,
    RotateScreen,
    RotateCube,
    MoveCube
}

public class GameCubeMoveButton : MonoBehaviour,IPointerDownHandler
{
    // Start is called before the first frame update
    static PressState pressState=PressState.None;
    bool behave = false;
    GameObject mainCube;
    Dictionary<GameObject,string> rotateCubeDict = new Dictionary<GameObject, string>();
    List<GameObject> rotateCubeList= new List<GameObject>();
    GameObject moveCube = null;
    string moveCubeType = null;
    float hasMoved = 0;
    List<float> hasRotate=new List<float>();
    void Start()
    {
        mainCube=GameObject.Find("Third-orderCube");
    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.W))
        {
            if (transform.name=="RotateScreen" && pressState==PressState.RotateScreen)
            {
                mainCube.transform.Rotate(-30*Time.deltaTime, 0, 30*Time.deltaTime,Space.World);
            }
        }
        else if(Input.GetKey(KeyCode.S))
        {
            if (transform.name=="RotateScreen" && pressState==PressState.RotateScreen)
            {
                mainCube.transform.Rotate(30*Time.deltaTime, 0, -30*Time.deltaTime,Space.World);
            }
        }
        else if(Input.GetKey(KeyCode.D))
        {
            if (transform.name=="RotateScreen" && pressState==PressState.RotateScreen)
            {
                mainCube.transform.Rotate(30*Time.deltaTime, 0, 30*Time.deltaTime, Space.World);
            }
        }
        else if(Input.GetKey(KeyCode.A))
        {
            if (transform.name=="RotateScreen" && pressState==PressState.RotateScreen)
            {
                mainCube.transform.Rotate(-30*Time.deltaTime, 0, -30*Time.deltaTime, Space.World);
            }
        }
        else if(Input.GetKey(KeyCode.Q))
        {
            if (transform.name=="RotateScreen" && pressState==PressState.RotateScreen)
            {
                mainCube.transform.Rotate(0, 30*Time.deltaTime, 0,Space.World);
            }
        }
        else if(Input.GetKey(KeyCode.E))
        {
            if (transform.name=="RotateScreen" && pressState==PressState.RotateScreen)
            {
                mainCube.transform.Rotate(0, -30*Time.deltaTime, 0, Space.World);
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if(transform.name=="RotateCube" && pressState==PressState.RotateCube)
            {
                foreach(GameObject quad in WholeCube.Slected)
                {
                    GameObject cube = quad.transform.parent.gameObject;
                    if (!rotateCubeDict.ContainsKey(cube))
                    {
                        rotateCubeDict.Add(cube, "Z+");
                        rotateCubeList.Add(cube);
                        hasRotate.Add(0);
                    }
                }
            }
            else if(transform.name=="MoveCube" && pressState == PressState.MoveCube)
            {
                if(WholeCube.Slected.Count == 1)
                {
                    Vector3 nextPos = new Vector3();
                    nextPos = WholeCube.NormalizeCubeVec3(WholeCube.Slected[0].transform.parent.localPosition + Vector3.up);
                    bool canMove = true;
                    Debug.Log(WholeCube.cubeMatchQuad.Count);
                    foreach(KeyValuePair<GameObject,CubeInfo> pair in WholeCube.cubeMatchQuad)
                    {
                        if (nextPos==pair.Value.pos || (WholeCube.IsEdge(WholeCube.Slected[0].transform.parent.gameObject).Contains("Y+")))
                        {
                            Debug.Log(nextPos==pair.Value.pos);
                            Debug.Log((WholeCube.IsEdge(WholeCube.Slected[0].transform.parent.gameObject).Contains("Y+")));
                            canMove = false;
                            break;
                        }
                    }
                    if (canMove)
                    {
                        moveCube=WholeCube.Slected[0].transform.parent.gameObject;
                        moveCubeType="Y+";
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (transform.name=="RotateCube" && pressState==PressState.RotateCube)
            {
                foreach (GameObject quad in WholeCube.Slected)
                {
                    GameObject cube = quad.transform.parent.gameObject;
                    if (!rotateCubeDict.ContainsKey(cube))
                    {
                        rotateCubeDict.Add(cube, "Z-");
                        rotateCubeList.Add(cube);
                        hasRotate.Add(0);
                    }
                }
            }
            else if(transform.name=="MoveCube" && pressState== PressState.MoveCube)
            {
                if (WholeCube.Slected.Count == 1)
                {
                    Vector3 nextPos = new Vector3();
                    nextPos = WholeCube.NormalizeCubeVec3(WholeCube.Slected[0].transform.parent.localPosition - 
                        Vector3.up);
                    Debug.Log(WholeCube.Slected[0].transform.parent.parent.up);
                    bool canMove = true;
                    Debug.Log(WholeCube.cubeMatchQuad.Count);
                    foreach (KeyValuePair<GameObject, CubeInfo> pair in WholeCube.cubeMatchQuad)
                    {
                        if (nextPos==pair.Value.pos|| (WholeCube.IsEdge(WholeCube.Slected[0].transform.parent.gameObject).Contains("Y-")))
                        {
                            Debug.Log(nextPos==pair.Value.pos);
                            Debug.Log((WholeCube.IsEdge(WholeCube.Slected[0].transform.parent.gameObject).Contains("Y-")));
                            canMove = false;
                            break;
                        }
                    }
                    if (canMove)
                    {
                        moveCube=WholeCube.Slected[0].transform.parent.gameObject;
                        moveCubeType="Y-";
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (transform.name=="RotateCube" && pressState==PressState.RotateCube)
            {
                
                foreach (GameObject quad in WholeCube.Slected)
                {
                    GameObject cube = quad.transform.parent.gameObject;
                    if (!rotateCubeDict.ContainsKey(cube))
                    {
                        rotateCubeDict.Add(cube, "X-");
                        rotateCubeList.Add(cube);
                        hasRotate.Add(0);
                    }
                }
            }
            else if (transform.name=="MoveCube" && pressState== PressState.MoveCube)
            {
                if (WholeCube.Slected.Count == 1)
                {
                    Vector3 nextPos = new Vector3();
                    nextPos = WholeCube.NormalizeCubeVec3(WholeCube.Slected[0].transform.parent.localPosition +
                        Vector3.right);
                    Debug.Log(WholeCube.Slected[0].transform.parent.parent.up);
                    bool canMove = true;
                    Debug.Log(WholeCube.cubeMatchQuad.Count);
                    foreach (KeyValuePair<GameObject, CubeInfo> pair in WholeCube.cubeMatchQuad)
                    {
                        if (nextPos==pair.Value.pos|| (WholeCube.IsEdge(WholeCube.Slected[0].transform.parent.gameObject).Contains("X+")))
                        {
                            Debug.Log(nextPos==pair.Value.pos);
                            Debug.Log((WholeCube.IsEdge(WholeCube.Slected[0].transform.parent.gameObject).Contains("X+")));
                            canMove = false;
                            break;
                        }
                    }
                    if (canMove)
                    {
                        moveCube=WholeCube.Slected[0].transform.parent.gameObject;
                        moveCubeType="X+";
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            
            if (transform.name=="RotateCube" && pressState==PressState.RotateCube)
            {
                foreach (GameObject quad in WholeCube.Slected)
                {
                    GameObject cube = quad.transform.parent.gameObject;
                    if (!rotateCubeDict.ContainsKey(cube))
                    {
                        rotateCubeDict.Add(cube, "X+");
                        rotateCubeList.Add(cube);
                        hasRotate.Add(0);
                    }
                }
            }
            else if (transform.name=="MoveCube" && pressState== PressState.MoveCube)
            {
                if (WholeCube.Slected.Count == 1)
                {
                    Vector3 nextPos = new Vector3();
                    nextPos = WholeCube.NormalizeCubeVec3(WholeCube.Slected[0].transform.parent.localPosition -
                        Vector3.right);
                    Debug.Log(WholeCube.Slected[0].transform.parent.parent.up);
                    bool canMove = true;
                    Debug.Log(WholeCube.cubeMatchQuad.Count);
                    foreach (KeyValuePair<GameObject, CubeInfo> pair in WholeCube.cubeMatchQuad)
                    {
                        if (nextPos==pair.Value.pos|| (WholeCube.IsEdge(WholeCube.Slected[0].transform.parent.gameObject).Contains("X-")))
                        {
                            Debug.Log(nextPos==pair.Value.pos);
                            Debug.Log((WholeCube.IsEdge(WholeCube.Slected[0].transform.parent.gameObject).Contains("X-")));
                            canMove = false;
                            break;
                        }
                    }
                    if (canMove)
                    {
                        moveCube=WholeCube.Slected[0].transform.parent.gameObject;
                        moveCubeType="X-";
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (transform.name=="RotateCube" && pressState==PressState.RotateCube)
            {
                foreach (GameObject quad in WholeCube.Slected)
                {
                    GameObject cube = quad.transform.parent.gameObject;
                    if (!rotateCubeDict.ContainsKey(cube))
                    {
                        rotateCubeDict.Add(cube, "Y+");
                        rotateCubeList.Add(cube);
                        hasRotate.Add(0);
                    }
                }
            }
            else if (transform.name=="MoveCube" && pressState== PressState.MoveCube)
            {
                if (WholeCube.Slected.Count == 1)
                {
                    Vector3 nextPos = new Vector3();
                    nextPos = WholeCube.NormalizeCubeVec3(WholeCube.Slected[0].transform.parent.localPosition -
                        Vector3.forward);
                    Debug.Log(WholeCube.Slected[0].transform.parent.parent.up);
                    bool canMove = true;
                    Debug.Log(WholeCube.cubeMatchQuad.Count);
                    foreach (KeyValuePair<GameObject, CubeInfo> pair in WholeCube.cubeMatchQuad)
                    {
                        if (nextPos==pair.Value.pos|| (WholeCube.IsEdge(WholeCube.Slected[0].transform.parent.gameObject).Contains("Z-")))
                        {
                            Debug.Log(nextPos==pair.Value.pos);
                            Debug.Log((WholeCube.IsEdge(WholeCube.Slected[0].transform.parent.gameObject).Contains("Z-")));
                            canMove = false;
                            break;
                        }
                    }
                    if (canMove)
                    {
                        moveCube=WholeCube.Slected[0].transform.parent.gameObject;
                        moveCubeType="Z-";
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (transform.name=="RotateCube" && pressState==PressState.RotateCube)
            {
                foreach (GameObject quad in WholeCube.Slected)
                {
                    GameObject cube = quad.transform.parent.gameObject;
                    if (!rotateCubeDict.ContainsKey(cube))
                    {
                        rotateCubeDict.Add(cube, "Y-");
                        rotateCubeList.Add(cube);
                        hasRotate.Add(0);
                    }
                }
            }
            else if (transform.name=="MoveCube" && pressState== PressState.MoveCube)
            {
                if (WholeCube.Slected.Count == 1)
                {
                    Vector3 nextPos = new Vector3();
                    nextPos = WholeCube.NormalizeCubeVec3(WholeCube.Slected[0].transform.parent.localPosition +
                        Vector3.forward);
                    Debug.Log(WholeCube.Slected[0].transform.parent.parent.up);
                    bool canMove = true;
                    Debug.Log(WholeCube.cubeMatchQuad.Count);
                    foreach (KeyValuePair<GameObject, CubeInfo> pair in WholeCube.cubeMatchQuad)
                    {
                        if (nextPos==pair.Value.pos|| (WholeCube.IsEdge(WholeCube.Slected[0].transform.parent.gameObject).Contains("Z+")))
                        {
                            Debug.Log(nextPos==pair.Value.pos);
                            Debug.Log((WholeCube.IsEdge(WholeCube.Slected[0].transform.parent.gameObject).Contains("Z+")));
                            canMove = false;
                            break;
                        }
                    }
                    if (canMove)
                    {
                        moveCube=WholeCube.Slected[0].transform.parent.gameObject;
                        moveCubeType="Z+";
                    }
                }
            }
        }
        else if(Input.GetKeyDown(KeyCode.F))
        {
            Dictionary<string, Dictionary<string, string>> info = new Dictionary<string, Dictionary<string, string>>();
            foreach (KeyValuePair<GameObject,CubeInfo> pair in WholeCube.cubeMatchQuad)
            {
                Dictionary<string, string> sub = new Dictionary<string, string>();
                foreach(KeyValuePair<GameObject,Vector3> quadpair in WholeCube.cubeMatchQuad[pair.Key].quadPos)
                {
                    sub.Add(quadpair.Key.name, quadpair.Value.ToString());
                }
                info.Add(pair.Key.name, sub);
            }
            File.WriteAllText("E:\\aaa.json", JsonConvert.SerializeObject(info));
        }
        UpdateCubeRotate();
        UpdateCubeMove();
    }

    void UpdateCubeMove()
    {
        if(moveCube!=null && moveCubeType!=null)
        {
            if (moveCubeType=="X+")
            {
                moveCube.transform.Translate(Vector3.right*Time.deltaTime, moveCube.transform.parent);
                hasMoved+=Time.deltaTime*1;
                if (hasMoved>=1)
                {
                    moveCube.transform.Translate(1-hasMoved, 0, 0, moveCube.transform.parent);
                    WholeCube.cubeMatchQuad[moveCube].pos=WholeCube.NormalizeCubeVec3(moveCube.transform.localPosition);
                    moveCube=null;
                    moveCubeType=null;
                    hasMoved=0;
                }
            }
            else if (moveCubeType=="X-")
            {
                moveCube.transform.Translate(-Vector3.right*Time.deltaTime, moveCube.transform.parent);
                hasMoved+=Time.deltaTime*1;
                if (hasMoved>=1)
                {
                    moveCube.transform.Translate(-1+hasMoved, 0, 0, moveCube.transform.parent);
                    WholeCube.cubeMatchQuad[moveCube].pos=WholeCube.NormalizeCubeVec3(moveCube.transform.localPosition);
                    moveCube=null;
                    moveCubeType=null;
                    hasMoved=0;
                }
            }
            else if (moveCubeType=="Z+")
            {
                moveCube.transform.Translate(Vector3.forward*Time.deltaTime, moveCube.transform.parent);
                hasMoved+=Time.deltaTime*1;
                if (hasMoved>=1)
                {
                    moveCube.transform.Translate(0, 0, 1-hasMoved, moveCube.transform.parent);
                    WholeCube.cubeMatchQuad[moveCube].pos=WholeCube.NormalizeCubeVec3(moveCube.transform.localPosition);
                    moveCube=null;
                    moveCubeType=null;
                    hasMoved=0;
                }
            }
            else if (moveCubeType=="Z-")
            {
                moveCube.transform.Translate(-Vector3.forward*Time.deltaTime, moveCube.transform.parent);
                hasMoved+=Time.deltaTime*1;
                if (hasMoved>=1)
                {
                    moveCube.transform.Translate(0, 0, -1+hasMoved, moveCube.transform.parent);
                    WholeCube.cubeMatchQuad[moveCube].pos=WholeCube.NormalizeCubeVec3(moveCube.transform.localPosition);
                    moveCube=null;
                    moveCubeType=null;
                    hasMoved=0;
                }
            }
            else if (moveCubeType=="Y+")
            {
                moveCube.transform.Translate(Vector3.up*Time.deltaTime, moveCube.transform.parent);
                hasMoved+=Time.deltaTime*1;
                if (hasMoved>=1)
                {
                    moveCube.transform.Translate(0, 1-hasMoved, 0, moveCube.transform.parent);
                    WholeCube.cubeMatchQuad[moveCube].pos=WholeCube.NormalizeCubeVec3(moveCube.transform.localPosition);
                    moveCube=null;
                    moveCubeType=null;
                    hasMoved=0;
                }
            }
            else if (moveCubeType=="Y-")
            {
                moveCube.transform.Translate(-Vector3.up*Time.deltaTime, moveCube.transform.parent);
                hasMoved+=Time.deltaTime*1;
                if (hasMoved>=1)
                {
                    moveCube.transform.Translate(0, -1+hasMoved, 0, moveCube.transform.parent);
                    WholeCube.cubeMatchQuad[moveCube].pos=WholeCube.NormalizeCubeVec3(moveCube.transform.localPosition);
                    moveCube=null;
                    moveCubeType=null;
                    hasMoved=0;
                }
            }
        }
    }

    void UpdateCubeRotate()
    {
        for(int i = 0; i<rotateCubeList.Count; i++) { 
            GameObject cube = rotateCubeList[i];
            if (rotateCubeDict[cube]=="Z+")
            {
                cube.transform.Rotate(0, 0, Time.deltaTime*90, Space.Self);
                hasRotate[i]+=Time.deltaTime*90;
                if (hasRotate[i]>=90)
                {
                    cube.transform.Rotate(0, 0, 90-hasRotate[i], Space.Self);
                    for (int j = 0; j<cube.transform.childCount; j++)
                    {
                        WholeCube.cubeMatchQuad[cube].quadPos[cube.transform.GetChild(j).gameObject]=
                            WholeCube.NormalizeVec3(cube.transform.localRotation*cube.transform.GetChild(j).localPosition);
                    }
                    hasRotate[i]=90;
                }
            }
            else if (rotateCubeDict[cube]=="Z-")
            {
                cube.transform.Rotate(0, 0, -Time.deltaTime*90, Space.Self);
                hasRotate[i]+=Time.deltaTime*90;
                if (hasRotate[i]>=90)
                {
                    cube.transform.Rotate(0, 0, -90+hasRotate[i], Space.Self);
                    for(int j = 0; j<cube.transform.childCount; j++)
                    {
                        WholeCube.cubeMatchQuad[cube].quadPos[cube.transform.GetChild(j).gameObject]=
                            WholeCube.NormalizeVec3(cube.transform.localRotation * cube.transform.GetChild(j).localPosition);
                    }
                    hasRotate[i]=90;
                }
            }
            else if (rotateCubeDict[cube]=="X+")
            {
                cube.transform.Rotate(Time.deltaTime*90, 0, 0, Space.Self);
                hasRotate[i]+=Time.deltaTime*90;
                if (hasRotate[i]>=90)
                {
                    cube.transform.Rotate(90-hasRotate[i], 0, 0, Space.Self);
                    for (int j = 0; j<cube.transform.childCount; j++)
                    {
                        WholeCube.cubeMatchQuad[cube].quadPos[cube.transform.GetChild(j).gameObject]=
                            WholeCube.NormalizeVec3(cube.transform.localRotation * cube.transform.GetChild(j).localPosition);
                    }
                    hasRotate[i] = 90;
                }
            }
            else if (rotateCubeDict[cube]=="X-")
            {
                cube.transform.Rotate(-Time.deltaTime*90, 0, 0, Space.Self);
                hasRotate[i]+=Time.deltaTime*90;
                if (hasRotate[i]>=90)
                {
                    cube.transform.Rotate(-90+hasRotate[i], 0, 0, Space.Self);
                    for (int j = 0; j<cube.transform.childCount; j++)
                    {
                        WholeCube.cubeMatchQuad[cube].quadPos[cube.transform.GetChild(j).gameObject]=
                            WholeCube.NormalizeVec3(cube.transform.localRotation * cube.transform.GetChild(j).localPosition);
                    }
                    hasRotate[i] = 90;
                }
            }
            else if (rotateCubeDict[cube]=="Y+")
            {
                cube.transform.Rotate(0, Time.deltaTime*90, 0, Space.Self);
                hasRotate[i]+=Time.deltaTime*90;
                if (hasRotate[i]>=90)
                {
                    cube.transform.Rotate(0, 90 - hasRotate[i], 0, Space.Self);
                    for (int j = 0; j<cube.transform.childCount; j++)
                    {
                        WholeCube.cubeMatchQuad[cube].quadPos[cube.transform.GetChild(j).gameObject]=
                            WholeCube.NormalizeVec3(cube.transform.localRotation * cube.transform.GetChild(j).localPosition);
                    }
                    hasRotate[i] = 90;
                }
            }
            else if (rotateCubeDict[cube]=="Y-")
            {
                cube.transform.Rotate(0, -Time.deltaTime*90, 0, Space.Self);
                hasRotate[i]+=Time.deltaTime*90;
                if (hasRotate[i]>=90)
                {
                    cube.transform.Rotate(0, -90 + hasRotate[i], 0, Space.Self);
                    for (int j = 0; j<cube.transform.childCount; j++)
                    {
                        WholeCube.cubeMatchQuad[cube].quadPos[cube.transform.GetChild(j).gameObject]=
                            WholeCube.NormalizeVec3(cube.transform.localRotation * cube.transform.GetChild(j).localPosition);
                    }
                    hasRotate[i] = 90;
                }
            }
        }
        for(int i=0;i<rotateCubeList.Count;i++)
        {
            if (hasRotate[i]==90)
            {
                rotateCubeDict.Remove(rotateCubeList[i]);
                rotateCubeList.RemoveAt(i);
                hasRotate.RemoveAt(i);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (transform.name=="RotateScreen")
        {
            behave=!behave;
            if (behave)
            {
                pressState= PressState.RotateScreen;
                transform.Find("Image").gameObject.SetActive(true);
                if (transform.parent.Find("RotateCube").Find("Image").gameObject.activeInHierarchy)
                {
                    transform.parent.Find("RotateCube").Find("Image").gameObject.SetActive(false);
                    transform.parent.Find("RotateCube").GetComponent<GameCubeMoveButton>().behave=false;
                }
                if (transform.parent.Find("MoveCube").Find("Image").gameObject.activeInHierarchy)
                {
                    transform.parent.Find("MoveCube").Find("Image").gameObject.SetActive(false);
                    transform.parent.Find("MoveCube").GetComponent<GameCubeMoveButton>().behave=false;
                }
            }
            else
            {
                pressState=PressState.None;
                transform.Find("Image").gameObject.SetActive(false);
            }
        }
        else if (transform.name=="RotateCube")
        {
            behave=!behave;
            if (behave)
            {
                pressState= PressState.RotateCube;
                transform.Find("Image").gameObject.SetActive(true);
                if (transform.parent.Find("RotateScreen").Find("Image").gameObject.activeInHierarchy)
                {
                    transform.parent.Find("RotateScreen").Find("Image").gameObject.SetActive(false);
                    transform.parent.Find("RotateScreen").GetComponent<GameCubeMoveButton>().behave=false;
                }
                if (transform.parent.Find("MoveCube").Find("Image").gameObject.activeInHierarchy)
                {
                    transform.parent.Find("MoveCube").Find("Image").gameObject.SetActive(false);
                    transform.parent.Find("MoveCube").GetComponent<GameCubeMoveButton>().behave=false;
                }
            }
            else
            {
                pressState=PressState.None;
                transform.Find("Image").gameObject.SetActive(false);
            }
        }
        else if (transform.name=="MoveCube")
        {
            behave=!behave;
            if (behave)
            {
                pressState= PressState.MoveCube;
                transform.Find("Image").gameObject.SetActive(true);
                if (transform.parent.Find("RotateScreen").Find("Image").gameObject.activeInHierarchy)
                {
                    transform.parent.Find("RotateScreen").Find("Image").gameObject.SetActive(false);
                    transform.parent.Find("RotateScreen").GetComponent<GameCubeMoveButton>().behave=false;
                }
                if (transform.parent.Find("RotateCube").Find("Image").gameObject.activeInHierarchy)
                {
                    transform.parent.Find("RotateCube").Find("Image").gameObject.SetActive(false);
                    transform.parent.Find("RotateCube").GetComponent<GameCubeMoveButton>().behave=false;
                }
            }
            else
            {
                pressState=PressState.None;
                transform.Find("Image").gameObject.SetActive(false);
            }
        }
    }
}
