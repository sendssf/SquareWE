using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//此脚本用来管理鼠标光标
public class MouseController : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D texture;
    public Texture2D transfer;
    void Start()
    {
        Cursor.visible = true;
        Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(transfer,Vector2.zero, CursorMode.Auto);
        }
        if(Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
        }
    }

}
