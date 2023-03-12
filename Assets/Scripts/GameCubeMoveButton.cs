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

public class GameCubeMoveButton : MonoBehaviour,ISelectHandler,IDeselectHandler
{
    // Start is called before the first frame update
    static PressState pressState=PressState.None;
    GameObject mainCube;
    void Start()
    {
        mainCube=GameObject.Find("MainCube");
    }

    // Update is called once per frame
    void Update()
    {
        if(pressState==PressState.RotateScreen) 
        {
            if(Input.GetKeyDown(KeyCode.W))     //向上转动
            {
                mainCube.transform.Rotate(-5*Time.deltaTime,0,5*Time.deltaTime);
            }
        }
        else if(pressState==PressState.RotateCube)
        {
            
        }
        else if(pressState==PressState.MoveCube)
        {
            
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (transform.name=="RotateScreen")
        {
            pressState= PressState.RotateScreen;
        }
        else if (transform.name=="RotateCube")
        {
            pressState=PressState.RotateCube;
        }
        else if (transform.name=="MoveCube")
        {
            pressState=PressState.MoveCube;
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        pressState=PressState.None;
    }
}
