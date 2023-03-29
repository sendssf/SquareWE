using System;
using System.Collections;
using System.Collections.Generic;
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
    List<float> hasRotate=new List<float>();
    void Start()
    {
        mainCube=GameObject.Find("Third-orderCube");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.W))
        {
            if (transform.name=="RotateScreen" && pressState==PressState.RotateScreen)
            {
                mainCube.transform.Rotate(-20*Time.deltaTime, 0, 20*Time.deltaTime,Space.World);
            }
        }
        else if(Input.GetKey(KeyCode.S))
        {
            if (transform.name=="RotateScreen" && pressState==PressState.RotateScreen)
            {
                mainCube.transform.Rotate(20*Time.deltaTime, 0, -20*Time.deltaTime,Space.World);
            }
        }
        else if(Input.GetKey(KeyCode.D))
        {
            if (transform.name=="RotateScreen" && pressState==PressState.RotateScreen)
            {
                mainCube.transform.Rotate(20*Time.deltaTime, 0, 20*Time.deltaTime, Space.World);
            }
        }
        else if(Input.GetKey(KeyCode.A))
        {
            if (transform.name=="RotateScreen" && pressState==PressState.RotateScreen)
            {
                mainCube.transform.Rotate(-20*Time.deltaTime, 0, -20*Time.deltaTime, Space.World);
            }
        }
        else if(Input.GetKey(KeyCode.Q))
        {
            if (transform.name=="RotateScreen" && pressState==PressState.RotateScreen)
            {
                mainCube.transform.Rotate(0, 20*Time.deltaTime, 0,Space.World);
            }
        }
        else if(Input.GetKey(KeyCode.E))
        {
            if (transform.name=="RotateScreen" && pressState==PressState.RotateScreen)
            {
                mainCube.transform.Rotate(0, -20*Time.deltaTime, 0, Space.World);
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
        }
        UpdateCubeRotate();
    }

    void UpdateCubeRotate()
    {
        for(int i = 0; i<rotateCubeList.Count; i++) { 
            GameObject cube = rotateCubeList[i];
            if (rotateCubeDict[cube]=="Z+")
            {
                cube.transform.Rotate(cube.transform.parent.forward,Time.deltaTime*90,Space.World);
                hasRotate[i]+=Time.deltaTime*90;
                if (hasRotate[i]>=90)
                {
                    cube.transform.Rotate(cube.transform.parent.forward, 90-hasRotate[i],Space.World);
                    hasRotate[i]=90;
                }
            }
            else if (rotateCubeDict[cube]=="Z-")
            {
                cube.transform.Rotate(cube.transform.parent.forward, -Time.deltaTime*90, Space.World);
                hasRotate[i]+=Time.deltaTime*90;
                if (hasRotate[i]>=90)
                {
                    cube.transform.Rotate(cube.transform.parent.forward, -90+hasRotate[i], Space.World);
                    hasRotate[i]=90;
                }
            }
            else if (rotateCubeDict[cube]=="X+")
            {
                cube.transform.Rotate(cube.transform.parent.right, Time.deltaTime*90, Space.World);
                hasRotate[i]+=Time.deltaTime*90;
                if (hasRotate[i]>=90)
                {
                    cube.transform.Rotate(cube.transform.parent.right, 90-hasRotate[i],Space.World);
                    hasRotate[i] = 90;
                }
            }
            else if (rotateCubeDict[cube]=="X-")
            {
                cube.transform.Rotate(cube.transform.parent.right, -Time.deltaTime*90, Space.World);
                hasRotate[i]+=Time.deltaTime*90;
                if (hasRotate[i]>=90)
                {
                    cube.transform.Rotate(cube.transform.parent.right, -90+hasRotate[i], Space.World);
                    hasRotate[i] = 90;
                }
            }
            else if (rotateCubeDict[cube]=="Y+")
            {
                cube.transform.Rotate(cube.transform.parent.up, Time.deltaTime*90, Space.World);
                hasRotate[i]+=Time.deltaTime*90;
                if (hasRotate[i]>=90)
                {
                    cube.transform.Rotate(cube.transform.parent.up, 90-hasRotate[i], Space.World);
                    hasRotate[i] = 90;
                }
            }
            else if (rotateCubeDict[cube]=="Y-")
            {
                cube.transform.Rotate(cube.transform.parent.up, -Time.deltaTime*90, Space.World);
                hasRotate[i]+=Time.deltaTime*90;
                if (hasRotate[i]>=90)
                {
                    cube.transform.Rotate(cube.transform.parent.up, -90+hasRotate[i], Space.World);
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
