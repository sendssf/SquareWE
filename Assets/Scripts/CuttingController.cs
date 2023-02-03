using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CuttingController : MonoBehaviour
{
    // Start is called before the first frame update
    int maxWidth;
    int maxHeight;

    float xoffset;
    float yoffset;
    float mouseMovex;
    float mouseMovey;
    float scale;        //图像缩放比例
    GameObject rowImage;
    GameObject select;
    GameObject upImage;
    Button Upleft;
    Button Upright;
    Button Bottomleft;
    Button Bottomright;
    Button Midbottom;
    Button Midup;
    Button Midleft;
    Button Midright;
    Vector3 mousePos;
    Vector3 mousePosNow;
    static Texture2D finalimage;
    static Texture2D originImage;

    private void Awake()
    {
        maxHeight=800;
        maxWidth=1200;
        rowImage=transform.Find("RowImage").gameObject;
        Upleft=rowImage.transform.Find("Select").Find("Upleft").gameObject.GetComponent<Button>();
        Upright=rowImage.transform.Find("Select").Find("Upright").gameObject.GetComponent<Button>();
        Bottomleft=rowImage.transform.Find("Select").Find("Bottomleft").gameObject.GetComponent<Button>();
        Bottomright=rowImage.transform.Find("Select").Find("Bottomright").gameObject.GetComponent<Button>();
        Midbottom=rowImage.transform.Find("Select").Find("MidBottom").gameObject.GetComponent<Button>();
        Midleft=rowImage.transform.Find("Select").Find("MidLeft").gameObject.GetComponent<Button>();
        Midup=rowImage.transform.Find("Select").Find("MidUp").gameObject.GetComponent<Button>();
        Midright=rowImage.transform.Find("Select").Find("MidRight").gameObject.GetComponent<Button>();
        select = rowImage.transform.Find("Select").gameObject;
        upImage = rowImage.transform.Find("Select").Find("Image").gameObject;
        xoffset=rowImage.GetComponent<RectTransform>().position.x;
        yoffset=rowImage.GetComponent<RectTransform>().position.y;
    }

    void Start()
    {
        
    }

    private void OnEnable()
    {
        select.GetComponent<RectTransform>().anchoredPosition=new Vector2(0f,0f);
        upImage.GetComponent<RectTransform>().anchoredPosition=new Vector2(0f, 0f);
        Cut(finalimage);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos=Input.mousePosition;   //获取鼠标初始坐标

        }
        if (Input.GetMouseButton(0))        //用鼠标增量更新框选区域
        {
            if (Upleft.gameObject.GetComponent<ButtonPressListener>().press)
            {
                mousePosNow=Input.mousePosition;
                mouseMovex=mousePosNow.x-mousePos.x;
                mouseMovey=mousePosNow.y-mousePos.y;
                mousePos=mousePosNow;
                Vector2 sizeNow = new Vector2(select.GetComponent<RectTransform>().sizeDelta.x,
                    select.GetComponent<RectTransform>().sizeDelta.y);
                Vector2 position = new Vector2(select.GetComponent<RectTransform>().anchoredPosition.x,
                    select.GetComponent<RectTransform>().anchoredPosition.y);
                Vector2 uppos = new Vector2(upImage.GetComponent<RectTransform>().anchoredPosition.x,
                    upImage.GetComponent<RectTransform>().anchoredPosition.y);
                if (position.x+mouseMovex>=0 && position.y+mouseMovey<=0)
                {
                    select.GetComponent<RectTransform>().sizeDelta=new Vector2(sizeNow.x-mouseMovex, sizeNow.y+mouseMovey);
                    select.GetComponent<RectTransform>().anchoredPosition=new Vector2(position.x+mouseMovex, position.y+mouseMovey);
                    upImage.GetComponent<RectTransform>().anchoredPosition=new Vector2(uppos.x-mouseMovex, uppos.y-mouseMovey);
                }
            }
            else if (Upright.gameObject.GetComponent<ButtonPressListener>().press)
            {
                mousePosNow=Input.mousePosition;
                mouseMovex=mousePosNow.x-mousePos.x;
                mouseMovey=mousePosNow.y-mousePos.y;
                mousePos=mousePosNow;
                Vector2 sizeNow = new Vector2(select.GetComponent<RectTransform>().sizeDelta.x,
                    select.GetComponent<RectTransform>().sizeDelta.y);
                Vector2 position = new Vector2(select.GetComponent<RectTransform>().anchoredPosition.x,
                    select.GetComponent<RectTransform>().anchoredPosition.y);
                Vector2 uppos = new Vector2(upImage.GetComponent<RectTransform>().anchoredPosition.x,
                    upImage.GetComponent<RectTransform>().anchoredPosition.y);
                if (position.y+mouseMovey<=0 && position.x+select.GetComponent<RectTransform>().sizeDelta.x+mouseMovex<=rowImage.GetComponent<RectTransform>().sizeDelta.x) 
                {
                    select.GetComponent<RectTransform>().sizeDelta=new Vector2(sizeNow.x+mouseMovex, sizeNow.y + mouseMovey);
                    select.GetComponent<RectTransform>().anchoredPosition=new Vector2(position.x, position.y + mouseMovey);
                    upImage.GetComponent<RectTransform>().anchoredPosition=new Vector2(uppos.x, uppos.y-mouseMovey); 
                }
            }
            else if (Bottomleft.gameObject.GetComponent<ButtonPressListener>().press)
            {
                mousePosNow=Input.mousePosition;
                mouseMovex=mousePosNow.x-mousePos.x;
                mouseMovey=mousePosNow.y-mousePos.y;
                mousePos=mousePosNow;
                Vector2 sizeNow = new Vector2(select.GetComponent<RectTransform>().sizeDelta.x,
                    select.GetComponent<RectTransform>().sizeDelta.y);
                Vector2 position = new Vector2(select.GetComponent<RectTransform>().anchoredPosition.x,
                    select.GetComponent<RectTransform>().anchoredPosition.y);
                Vector2 uppos = new Vector2(upImage.GetComponent<RectTransform>().anchoredPosition.x,
                    upImage.GetComponent<RectTransform>().anchoredPosition.y);
                if (position.x + mouseMovex>=0 && select.GetComponent<RectTransform>().sizeDelta.y-position.y-mouseMovey<=rowImage.GetComponent<RectTransform>().sizeDelta.y) 
                {
                    select.GetComponent<RectTransform>().sizeDelta=new Vector2(sizeNow.x-mouseMovex, sizeNow.y-mouseMovey);
                    select.GetComponent<RectTransform>().anchoredPosition=new Vector2(position.x+mouseMovex, position.y);
                    upImage.GetComponent<RectTransform>().anchoredPosition=new Vector2(uppos.x-mouseMovex, uppos.y); 
                }
            }
            else if (Bottomright.gameObject.GetComponent<ButtonPressListener>().press)
            {
                mousePosNow=Input.mousePosition;
                mouseMovex=mousePosNow.x-mousePos.x;
                mouseMovey=mousePosNow.y-mousePos.y;
                mousePos=mousePosNow;
                Vector2 sizeNow = new Vector2(select.GetComponent<RectTransform>().sizeDelta.x,
                    select.GetComponent<RectTransform>().sizeDelta.y);
                Vector2 position = new Vector2(select.GetComponent<RectTransform>().anchoredPosition.x,
                    select.GetComponent<RectTransform>().anchoredPosition.y);
                Vector2 uppos = new Vector2(upImage.GetComponent<RectTransform>().anchoredPosition.x,
                    upImage.GetComponent<RectTransform>().anchoredPosition.y);
                if (position.x+sizeNow.x+mouseMovex<=rowImage.GetComponent<RectTransform>().sizeDelta.x && 
                    position.y+sizeNow.y-mouseMovey<=rowImage.GetComponent<RectTransform>().sizeDelta.y)
                {
                    select.GetComponent<RectTransform>().sizeDelta=new Vector2(sizeNow.x+mouseMovex, sizeNow.y - mouseMovey);
                }
            }
            else if (Midbottom.gameObject.GetComponent<ButtonPressListener>().press)
            {
                mousePosNow=Input.mousePosition;
                mouseMovex=mousePosNow.x-mousePos.x;
                mouseMovey=mousePosNow.y-mousePos.y;
                mousePos=mousePosNow;
                Vector2 position = new Vector2(select.GetComponent<RectTransform>().anchoredPosition.x,
                    select.GetComponent<RectTransform>().anchoredPosition.y);
                Vector2 sizeNow = new Vector2(select.GetComponent<RectTransform>().sizeDelta.x,
                    select.GetComponent<RectTransform>().sizeDelta.y);
                if (position.y+sizeNow.y-mouseMovey<=rowImage.GetComponent<RectTransform>().sizeDelta.y)
                {
                    select.GetComponent<RectTransform>().sizeDelta=new Vector2(sizeNow.x, sizeNow.y-mouseMovey);
                }
            }
            else if (Midleft.gameObject.GetComponent<ButtonPressListener>().press)
            {
                mousePosNow=Input.mousePosition;
                mouseMovex=mousePosNow.x-mousePos.x;
                mouseMovey=mousePosNow.y-mousePos.y;
                mousePos=mousePosNow;
                Vector2 sizeNow = new Vector2(select.GetComponent<RectTransform>().sizeDelta.x,
                    select.GetComponent<RectTransform>().sizeDelta.y);
                Vector2 position = new Vector2(select.GetComponent<RectTransform>().anchoredPosition.x,
                    select.GetComponent<RectTransform>().anchoredPosition.y);
                Vector2 uppos = new Vector2(upImage.GetComponent<RectTransform>().anchoredPosition.x,
                    upImage.GetComponent<RectTransform>().anchoredPosition.y);
                if (position.x+mouseMovex>=0)
                {
                    select.GetComponent<RectTransform>().sizeDelta=new Vector2(sizeNow.x-mouseMovex, sizeNow.y);
                    select.GetComponent<RectTransform>().anchoredPosition=new Vector2(position.x+mouseMovex, position.y);
                    upImage.GetComponent<RectTransform>().anchoredPosition=new Vector2(uppos.x-mouseMovex, uppos.y);
                }
            }
            else if (Midright.gameObject.GetComponent<ButtonPressListener>().press)
            {
                mousePosNow=Input.mousePosition;
                mouseMovex=mousePosNow.x-mousePos.x;
                mouseMovey=mousePosNow.y-mousePos.y;
                mousePos=mousePosNow;
                Vector2 position = new Vector2(select.GetComponent<RectTransform>().anchoredPosition.x,
                    select.GetComponent<RectTransform>().anchoredPosition.y);
                Vector2 sizeNow = new Vector2(rowImage.transform.Find("Select").gameObject.GetComponent<RectTransform>().sizeDelta.x,
                    rowImage.transform.Find("Select").gameObject.GetComponent<RectTransform>().sizeDelta.y);
                if (position.x+select.GetComponent<RectTransform>().sizeDelta.x+mouseMovex<=rowImage.GetComponent<RectTransform>().sizeDelta.x)
                {
                    select.GetComponent<RectTransform>().sizeDelta=new Vector2(sizeNow.x+mouseMovex, sizeNow.y);
                }
            }
            else if (Midup.gameObject.GetComponent<ButtonPressListener>().press)
            {
                mousePosNow=Input.mousePosition;
                mouseMovex=mousePosNow.x-mousePos.x;
                mouseMovey=mousePosNow.y-mousePos.y;
                mousePos=mousePosNow;
                Vector2 sizeNow = new Vector2(select.GetComponent<RectTransform>().sizeDelta.x,
                    select.GetComponent<RectTransform>().sizeDelta.y);
                Vector2 position = new Vector2(select.GetComponent<RectTransform>().anchoredPosition.x,
                    select.GetComponent<RectTransform>().anchoredPosition.y);
                Vector2 uppos = new Vector2(upImage.GetComponent<RectTransform>().anchoredPosition.x,
                    upImage.GetComponent<RectTransform>().anchoredPosition.y);
                if (position.y+mouseMovey<=0) 
                {
                    select.GetComponent<RectTransform>().sizeDelta=new Vector2(sizeNow.x, sizeNow.y+mouseMovey);
                    select.GetComponent<RectTransform>().anchoredPosition=new Vector2(position.x, position.y+mouseMovey);
                    upImage.GetComponent<RectTransform>().anchoredPosition=new Vector2(uppos.x, uppos.y - mouseMovey); 
                }
            }
        }
    }

    void Cut(Texture2D texture2D)
    {
        //加载原始图像
        //finalimage= texture2D;
        if (texture2D.width<=maxWidth)
        {
            if (texture2D.height<=maxHeight)
            {
                scale=1;
                rowImage.GetComponent<RectTransform>().sizeDelta=new Vector2(texture2D.width, texture2D.height);
                upImage.GetComponent<RectTransform>().sizeDelta=new Vector2(texture2D.width, texture2D.height);
                select.GetComponent<RectTransform>().sizeDelta=new Vector2(texture2D.width, texture2D.height);
            }
            else
            {
                scale=texture2D.height/(float)maxHeight;
                rowImage.GetComponent<RectTransform>().sizeDelta=new Vector2((int)(texture2D.width/((double)texture2D.height/maxHeight)), maxHeight);
                upImage.GetComponent<RectTransform>().sizeDelta=new Vector2((int)(texture2D.width/((double)texture2D.height/maxHeight)), maxHeight);
                select.GetComponent<RectTransform>().sizeDelta=new Vector2((int)(texture2D.width/((double)texture2D.height/maxHeight)), maxHeight);
            }
        }
        else
        {
            if (texture2D.height<=maxHeight)
            {
                scale=texture2D.width/(float)maxWidth;
                rowImage.GetComponent<RectTransform>().sizeDelta=new Vector2(maxWidth, (int)(texture2D.height/((double)texture2D.width/maxWidth)));
                upImage.GetComponent<RectTransform>().sizeDelta=new Vector2(maxWidth, (int)(texture2D.height/((double)texture2D.width/maxWidth)));
                select.GetComponent<RectTransform>().sizeDelta=new Vector2(maxWidth, (int)(texture2D.height/((double)texture2D.width/maxWidth)));
            }
            else
            {
                float widthscale = (float)texture2D.width/maxWidth;
                float heightscale = (float)texture2D.height/maxHeight;
                if (heightscale>=widthscale)
                {
                    scale = heightscale;
                    rowImage.GetComponent<RectTransform>().sizeDelta=new Vector2((int)(texture2D.width/((double)texture2D.height/maxHeight)), maxHeight);
                    upImage.GetComponent<RectTransform>().sizeDelta=new Vector2((int)(texture2D.width/((double)texture2D.height/maxHeight)), maxHeight);
                    select.GetComponent<RectTransform>().sizeDelta=new Vector2((int)(texture2D.width/((double)texture2D.height/maxHeight)), maxHeight);
                }
                else
                {
                    scale = widthscale;
                    rowImage.GetComponent<RectTransform>().sizeDelta=new Vector2(maxWidth, (int)(texture2D.height/((double)texture2D.width/maxWidth)));
                    upImage.GetComponent<RectTransform>().sizeDelta=new Vector2(maxWidth, (int)(texture2D.height/((double)texture2D.width/maxWidth)));
                    select.GetComponent<RectTransform>().sizeDelta=new Vector2(maxWidth, (int)(texture2D.height/((double)texture2D.width/maxWidth)));
                }
            }
        }
        rowImage.GetComponent<Image>().sprite=Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0f, 0f));
        upImage.GetComponent<Image>().sprite=rowImage.GetComponent<Image>().sprite;
    }

    public void CutFinish()
    {
        finalimage=ScaleTextureCutOut(originImage, Mathf.CeilToInt(select.GetComponent<RectTransform>().anchoredPosition.x*scale),
            -Mathf.CeilToInt(select.GetComponent<RectTransform>().anchoredPosition.y*scale),
            select.GetComponent<RectTransform>().sizeDelta.x*scale, select.GetComponent<RectTransform>().sizeDelta.y * scale);
        //删除旧头像文件
        FileInfo df = new FileInfo($"{Application.persistentDataPath}\\{AllMessageContainer.playerInfo.playerName}.png");
        if (df.Exists)
        {
            df.Delete();
        }
        //写入新头像
        byte[] data = finalimage.EncodeToPNG();
        File.WriteAllBytes($"{Application.persistentDataPath}\\{AllMessageContainer.playerInfo.playerName}.png", data);
        transform.parent.gameObject.GetComponent<PlayerMessagePageClickEvent>().LoadHeadImage(false);
        transform.gameObject.SetActive(false);
    }

    public void CutPreview()        //裁剪预览
    {
        finalimage=ScaleTextureCutOut(originImage, Mathf.CeilToInt(select.GetComponent<RectTransform>().anchoredPosition.x*scale),
            -Mathf.CeilToInt(select.GetComponent<RectTransform>().anchoredPosition.y*scale),
            select.GetComponent<RectTransform>().sizeDelta.x*scale,select.GetComponent<RectTransform>().sizeDelta.y * scale); 
        transform.Find("Head").Find("Mask").Find("HeadImage").gameObject.GetComponent<Image>().sprite=
            Sprite.Create(finalimage, new Rect(0, 0, finalimage.width, finalimage.height), new Vector2(0f, 0f));
    }
    public Texture2D ScaleTextureCutOut(Texture2D originalTexture, int offsetX, int offsetY, float targetWidth,float targetHeight)
    {
        offsetY=originalTexture.height-offsetY-Mathf.CeilToInt(targetHeight);
        Debug.Log($"{offsetX},{offsetY}");
        Texture2D newTexture = new Texture2D(Mathf.CeilToInt(targetWidth), Mathf.CeilToInt(targetHeight));
        for (int y = 0; y < newTexture.height; y++)
        {
            for (int x = 0; x < newTexture.width; x++)
            {
                newTexture.SetPixel(x, y, originalTexture.GetPixel(offsetX+x,offsetY+y));
            }
        }
        newTexture.anisoLevel = 2;
        newTexture.Apply();
        return newTexture;
    }

    public static void LoadImage(Texture2D texture)
    {
        finalimage=texture;
        originImage=new Texture2D(finalimage.width, finalimage.height);
        originImage.SetPixels32(finalimage.GetPixels32());
    }
}
