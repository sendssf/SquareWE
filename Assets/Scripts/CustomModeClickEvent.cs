using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum Difficulty
{
    Easy,
    Middle,
    Difficult
}

public class CustomModeClickEvent : MonoBehaviour
{
    // Start is called before the first frame update
    AsyncOperation operation;
    public static int[] customArr;
    public static int customLen;
    public static int customDim;
    string shapeDefineCode;
    Difficulty difficulty;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomBegin()
    {
        AllMessageContainer.gameStatus.gameMode=GameMode.CustomRandom;
        StartCoroutine(loadScene("GamePage"));
    }

    public void GotoSettings()
    {
        transform.Find("Settings").gameObject.SetActive(true);
    }

    public void GotoHome()
    {
        StartCoroutine(loadScene("BeginUI"));
    }

    public void ReturnPage()
    {
        StartCoroutine(loadScene("ChooseModeUI"));
    }

    private IEnumerator loadScene(string which)
    {
        operation=SceneManager.LoadSceneAsync(which);
        operation.allowSceneActivation = true;
        yield return operation;
    }

    public void ChooseDifficult(bool isChoose)
    {
        if (isChoose)
        {
            difficulty=Difficulty.Difficult;
        }
    }

    public void ChooseEasy(bool isChoose)
    {
        if(isChoose)
        {
            difficulty=Difficulty.Easy;
        }
    }

    public void ChooseMiddle(bool isChoose)
    {
        if (!isChoose)
        {
            difficulty=Difficulty.Middle;
        }
    }

    public void SideLongChange(string message)
    {
        if (message!=null && message.Length!=0)
        {
            customDim=Convert.ToInt32(message);
        }
    }

    public void RemoveChange(string message)
    {
        if (message!=null && message.Length!=0)
        {
            customLen=Convert.ToInt32(message);
        }
    }
    public void NormalBegin()
    {
        ShowError("");
        if (!AllMessageContainer.gameStatus.ifExternList)
        {
            if (difficulty==Difficulty.Middle)
            {
                AllMessageContainer.gameStatus.wordFileName="word6.csv";
            }
            else if (difficulty==Difficulty.Easy)
            {
                AllMessageContainer.gameStatus.wordFileName="word4.csv";
            }
            else if (difficulty==Difficulty.Difficult)
            {
                AllMessageContainer.gameStatus.wordFileName="word6.csv";
            }
        }
        if (customDim<3)
        {
            ShowError("The side long is too small!");
            return;
        }
        else if (customDim>8)
        {
            ShowError("The side long is too long!");
            return;
        }
        if (customLen<0)
        {
            ShowError("The number of remove cube is too small!");
            return;
        }
        else if (customLen>customDim*customDim*customDim-10)
        {
            ShowError("The number of remove cube is too large!");
            return;
        }
        if (shapeDefineCode!=null && shapeDefineCode.Length>0)
        {
            try
            {
                shapeDefineCode = shapeDefineCode.Replace(" ", "").Replace("\n", "").Replace("\r", "");
                string[] codes = shapeDefineCode.Split(',');
                customArr = new int[codes.Length];
                for (int i = 0; i<codes.Length; i++)
                {
                    customArr[i]=Convert.ToInt32(codes[i]);
                    if (customArr[i]<0 || customArr[i]>customDim*customDim*customDim)
                    {
                        ShowError("The number in the Shape Difine Code is out of range");
                    }
                }
            }
            catch
            {
                ShowError("The format or grammar of the Shape Define Code is not correct!");
                return;
            }
        }
        AllMessageContainer.gameStatus.gameMode=GameMode.CustomOthers;
        StartCoroutine(loadScene("GamePage"));
    }

    void ShowError(string message)
    {
        transform.Find("Error").gameObject.GetComponent<Text>().text = message;
    }

    public void BrowseWordList()
    {
        string wordListPath = FileBrowse.OpenWindowDialog("Choose Word List",
                "CSV Files (*.csv)\0*.csv\0");
        try
        {
            var res = ReadCsv.ReadCsvFile_Extern(wordListPath);
            if (res.Count<=100)
            {
                ShowError("The word number of the word list must larger than 100!");
                return;
            }
        }
        catch
        {
            ShowError("The form or grammar of the word list is not correct!");
            return;
        }
        transform.Find("Advance").Find("File").Find("Filename").gameObject.GetComponent<Text>().text=wordListPath;
        AllMessageContainer.gameStatus.wordFileName=wordListPath;
        AllMessageContainer.gameStatus.ifExternList=true;
    }

    public void ShapeDefineInput(string message)
    {
        shapeDefineCode = message;
    }
}
