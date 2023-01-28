using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WholeSquare : MonoBehaviour
{
    
    Square[] all_square = new Square[27];//所有小立方体
   
    void Start()
    { 
        for (int i = 0; i < 27; i++)
        {
            all_square[i] = new Square();
            all_square[i].name = "square" + i.ToString();
        }

        /* all_square[0].x = 5;
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
        all_square[26].z = 5;*/
    }

    void Update()
    {
    }

}
