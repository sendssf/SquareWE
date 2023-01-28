using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WholeSquare : MonoBehaviour
{
    public static char[] alphabet =new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    
    Square[] all_square = new Square[27];//所有小立方体

    Vector3 screenPosition;//游戏对象的屏幕坐标
    Vector3 mousePositionOnScreen;//鼠标的屏幕坐标
    Vector3 mousePositionInWorld;//鼠标的世界坐标
    
    // Start is called before the first frame update
    void Start()
    {
        //System.Random r = new System.Random();//产生随机数对象
        for (int i = 0; i < 27; i++)
        {
            all_square[i] = new Square();
            for(int j = 0; j < 6; j++)
            {
                all_square[i].faces[j] = alphabet[Random.Range(0, 26)];//给每个面随机赋字母
            }
        }

        all_square[0].x = 5;
        all_square[0].y = 5;
        all_square[0].z = -5;
        all_square[1].x = 5;
        all_square[1].y = 5;
        all_square[1].z = 0;
        all_square[2].x = 5;
        all_square[2].y = 5;
        all_square[2].z = 5;
        all_square[3].x = 0;
        all_square[3].y = 5;
        all_square[3].z = -5;
        all_square[4].x = 0;
        all_square[4].y = 5;
        all_square[4].z = 0;
        all_square[5].x = 0;
        all_square[5].y = 5;
        all_square[5].z = 5;
        all_square[6].x = -5;
        all_square[6].y = 5;
        all_square[6].z = -5;
        all_square[7].x = -5;
        all_square[7].y = 5;
        all_square[7].z = 0;
        all_square[8].x = -5;
        all_square[8].y = 5;
        all_square[8].z = 5;
        all_square[9].x = 5;
        all_square[9].y = 0;
        all_square[9].z = -5;
        all_square[10].x = 5;
        all_square[10].y = 0;
        all_square[10].z = 0;
        all_square[11].x = 5;
        all_square[11].y = 0;
        all_square[11].z = 5;
        all_square[12].x = 0;
        all_square[12].y = 0;
        all_square[12].z = 0;
        all_square[13].x = 0;
        all_square[13].y = 0;
        all_square[13].z = 0;
        all_square[14].x = 0;
        all_square[14].y = 0;
        all_square[14].z = 5;
        all_square[15].x = -5;
        all_square[15].y = 0;
        all_square[15].z = -5;
        all_square[16].x = -5;
        all_square[16].y = 0;
        all_square[16].z = 0;
        all_square[17].x = -5;
        all_square[17].y = 0;
        all_square[17].z = 5;
        all_square[18].x = 5;
        all_square[18].y = -5;
        all_square[18].z = -5;
        all_square[19].x = 5;
        all_square[19].y = -5;
        all_square[19].z = 0;
        all_square[20].x = 5;
        all_square[20].y = -5;
        all_square[20].z = 5;
        all_square[21].x = 0;
        all_square[21].y = -5;
        all_square[21].z = -5;
        all_square[22].x = 0;
        all_square[22].y = -5;
        all_square[22].z = 0;
        all_square[23].x = 0;
        all_square[23].y = -5;
        all_square[23].z = 5;
        all_square[24].x = -5;
        all_square[24].y = -5;
        all_square[24].z = -5;
        all_square[25].x = -5;
        all_square[25].y = -5;
        all_square[25].z = 0;
        all_square[26].x = -5;
        all_square[26].y = -5;
        all_square[26].z = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            MouseClick();
        }
    }

    public void MouseClick()
    {
        screenPosition = Camera.main.WorldToScreenPoint(all_square[13].transform.position);
        mousePositionOnScreen = Input.mousePosition;
        mousePositionOnScreen.z = screenPosition.z;
        mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);

        for(int i = 0;i < 27; i++)
        {
            if (mousePositionInWorld.x > all_square[i].x - 2
                && mousePositionInWorld.x < all_square[i].x + 2
                && mousePositionInWorld.y > all_square[i].y - 2
                && mousePositionInWorld.y < all_square[i].y + 2
                && mousePositionInWorld.z > all_square[i].z - 2
                && mousePositionInWorld.z < all_square[i].z + 2)
                break;
        }
        all_square[i]
    }
}
