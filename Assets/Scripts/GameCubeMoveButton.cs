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

public struct RotateHelper
{
    public Vector3 target;
    public float hasRotate;
}

public class GameCubeMoveButton : MonoBehaviour,IPointerDownHandler
{
    // Start is called before the first frame update
    static PressState pressState=PressState.None;
    bool behave = false;
    GameObject mainCube;
    Dictionary<GameObject, RotateHelper> rotateCubeDict = new Dictionary<GameObject, RotateHelper>();
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
                    Quaternion begin = new Quaternion(cube.transform.rotation.x, 
                        cube.transform.rotation.y, cube.transform.rotation.z, cube.transform.rotation.w);
                    Vector3 beginVec3 = begin.eulerAngles;
                    Vector3 endVec3 = new Vector3(beginVec3.x, beginVec3.y, beginVec3.z+90);
                    RotateHelper rotateHelper = new RotateHelper();
                    rotateHelper.hasRotate=0;
                    rotateHelper.target=endVec3;
                    rotateCubeDict.Add(cube, rotateHelper);
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (transform.name=="RotateCube" && pressState==PressState.RotateCube)
            {

            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (transform.name=="RotateCube" && pressState==PressState.RotateCube)
            {

            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (transform.name=="RotateCube" && pressState==PressState.RotateCube)
            {

            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (transform.name=="RotateCube" && pressState==PressState.RotateCube)
            {

            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (transform.name=="RotateCube" && pressState==PressState.RotateCube)
            {

            }
        }
    }

    void UpdateCubeRotate()
    {
        foreach(KeyValuePair<GameObject,RotateHelper> pair in rotateCubeDict)
        {
            Vector3 angle = pair.Key.transform.rotation.eulerAngles;
            if (angle.x!=pair.Value.target.x)
            {
                pair.Key.transform.rotation=Quaternion.Euler(angle+Time.deltaTime*pair.Value.target);
                
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
