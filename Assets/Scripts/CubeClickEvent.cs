using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CubeClickEvent : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update

    private PhysicsRaycaster m_Raycaster;
    void Awake()
    {
        PhysicsRaycaster m_Raycaster = FindObjectOfType<PhysicsRaycaster>();
        if (m_Raycaster == null)
        {
            Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
        }
    }

    // Update is called once per frame
    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject squad = eventData.pointerCurrentRaycast.gameObject;
        if (squad == null)
            return;
        squad.GetComponent<MeshRenderer>().material.color = new Color(1, 0.6f, 0, 0.6f);//点击改变面颜色
    }
}

