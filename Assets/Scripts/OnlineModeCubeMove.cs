using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineModeCubeMove : MonoBehaviour
{
    // Start is called before the first frame update

    class CubeRotateHelper
    {
        public string option;
        public float hasRotate;
    }

    class CubeMoveHelper
    {
        public string option;
        public float hasMove;
    }

    static Dictionary<GameObject, CubeRotateHelper> cubeRotateDict = new Dictionary<GameObject, CubeRotateHelper>();
    static List<GameObject> cubeRotateList;
    static List<GameObject> cubeeMoveList;
    static Dictionary<GameObject, CubeMoveHelper> cubeMoveDict = new Dictionary<GameObject, CubeMoveHelper>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateCubeUpdate();
        MoveCubeUpdate();
    }

    public static void RotateScreen(GameObject otherCube, string option, float degree)
    {
        switch(option)
        {
            case "W":
                otherCube.transform.Rotate(-degree, 0, degree,Space.World);
                break;
            case "S":
                otherCube.transform.Rotate(degree, 0, -degree,Space.World);
                break;
            case "A":
                otherCube.transform.Rotate(-degree, 0 ,-degree, Space.World);
                break;
            case "D":
                otherCube.transform.Rotate(degree,0,degree, Space.World);
                break;
            case "Q":
                otherCube.transform.Rotate(0, degree, 0, Space.World);
                break;
            case "E":
                otherCube.transform.Rotate(0, -degree, 0, Space.World);
                break;
        }
    }

    public static void RotateCube(List<GameObject> cubeList, string option)
    {
        foreach (GameObject cube in cubeList)
        {
            CubeRotateHelper helper = new CubeRotateHelper();
            helper.option = option;
            helper.hasRotate = 0;
            cubeRotateDict.Add(cube, helper);
            cubeRotateList.Add(cube);
        }
    }

    private void RotateCubeUpdate()
    {
        for(int i=cubeRotateList.Count - 1;i>=0;i--)
        {
            GameObject cube = cubeRotateList[i];
            if (cubeRotateDict[cube].option == "Z+")
            {
                cube.transform.Rotate(0, 0, 90*Time.deltaTime, Space.Self);
                cubeRotateDict[cube].hasRotate +=90*Time.deltaTime;
                if (cubeRotateDict[cube].hasRotate>=90)
                {
                    cube.transform.Rotate(0,0,90 - cubeRotateDict[cube].hasRotate,Space.Self);
                    cubeRotateDict[cube].hasRotate = 90;
                }
            }
            else if (cubeRotateDict[cube].option == "Z-")
            {
                cube.transform.Rotate(0, 0, -90*Time.deltaTime, Space.Self);
                cubeRotateDict[cube].hasRotate +=90*Time.deltaTime;
                if (cubeRotateDict[cube].hasRotate>=90)
                {
                    cube.transform.Rotate(0, 0, -90 + cubeRotateDict[cube].hasRotate, Space.Self);
                    cubeRotateDict[cube].hasRotate = 90;
                }
            }
            else if (cubeRotateDict[cube].option == "Y+")
            {
                cube.transform.Rotate(0, 90*Time.deltaTime,0, Space.Self);
                cubeRotateDict[cube].hasRotate +=90*Time.deltaTime;
                if (cubeRotateDict[cube].hasRotate>=90)
                {
                    cube.transform.Rotate(0, 90 - cubeRotateDict[cube].hasRotate,0 , Space.Self);
                    cubeRotateDict[cube].hasRotate = 90;
                }
            }
            else if (cubeRotateDict[cube].option == "Y-")
            {
                cube.transform.Rotate(0, -90*Time.deltaTime, 0, Space.Self);
                cubeRotateDict[cube].hasRotate +=90*Time.deltaTime;
                if (cubeRotateDict[cube].hasRotate>=90)
                {
                    cube.transform.Rotate(0, -90 + cubeRotateDict[cube].hasRotate, 0, Space.Self);
                    cubeRotateDict[cube].hasRotate = 90;
                }
            }
            else if (cubeRotateDict[cube].option == "X+")
            {
                cube.transform.Rotate(90*Time.deltaTime,0, 0, Space.Self);
                cubeRotateDict[cube].hasRotate +=90*Time.deltaTime;
                if (cubeRotateDict[cube].hasRotate>=90)
                {
                    cube.transform.Rotate(90 - cubeRotateDict[cube].hasRotate, 0, 0, Space.Self);
                    cubeRotateDict[cube].hasRotate = 90;
                }
            }
            else if (cubeRotateDict[cube].option == "X-")
            {
                cube.transform.Rotate(-90*Time.deltaTime, 0, 0, Space.Self);
                cubeRotateDict[cube].hasRotate +=90*Time.deltaTime;
                if (cubeRotateDict[cube].hasRotate>=90)
                {
                    cube.transform.Rotate(-90 + cubeRotateDict[cube].hasRotate, 0, 0, Space.Self);
                    cubeRotateDict[cube].hasRotate = 90;
                }
            }
        }

        for(int i=cubeRotateList.Count-1; i>=0; i--)
        {
            if (cubeRotateDict[cubeRotateList[i]].hasRotate == 90)
            {
                cubeRotateDict.Remove(cubeRotateList[i]);
                cubeRotateList.RemoveAt(i);
            }
        }
    }

    public void MoveCube(GameObject cube, string option)
    {
        CubeMoveHelper helper = new CubeMoveHelper();
        helper.option = option;
        helper.hasMove = 0;
        cubeMoveDict.Add(cube, helper);
        cubeeMoveList.Add(cube);
    }

    private void MoveCubeUpdate()
    {
        for(int i = cubeeMoveList.Count-1; i >= 0; i--)
        {
            GameObject cube = cubeeMoveList[i];
            if (cubeMoveDict[cube].option == "Z+")
            {
                cube.transform.Translate(Vector3.forward * Time.deltaTime, cube.transform.parent);
                cubeMoveDict[cube].hasMove +=1*Time.deltaTime;
                if (cubeMoveDict[cube].hasMove >=1)
                {
                    cube.transform.Translate(0, 0, 1 - cubeMoveDict[cube].hasMove, cube.transform.parent);
                    cubeMoveDict[cube].hasMove = 1;
                }
            }
            else if (cubeMoveDict[cube].option == "Z-")
            {
                cube.transform.Translate(-Vector3.forward * Time.deltaTime, cube.transform.parent);
                cubeMoveDict[cube].hasMove +=1*Time.deltaTime;
                if (cubeMoveDict[cube].hasMove >=1)
                {
                    cube.transform.Translate(0, 0, -1 + cubeMoveDict[cube].hasMove, cube.transform.parent);
                    cubeMoveDict[cube].hasMove = 1;
                }
            }
            else if (cubeMoveDict[cube].option == "Y+")
            {
                cube.transform.Translate(Vector3.up * Time.deltaTime, cube.transform.parent);
                cubeMoveDict[cube].hasMove +=1*Time.deltaTime;
                if (cubeMoveDict[cube].hasMove >=1)
                {
                    cube.transform.Translate(0, 1 - cubeMoveDict[cube].hasMove, 0 , cube.transform.parent);
                    cubeMoveDict[cube].hasMove = 1;
                }
            }
            else if (cubeMoveDict[cube].option == "Y-")
            {
                cube.transform.Translate(-Vector3.up * Time.deltaTime, cube.transform.parent);
                cubeMoveDict[cube].hasMove +=1*Time.deltaTime;
                if (cubeMoveDict[cube].hasMove >=1)
                {
                    cube.transform.Translate(0, -1 + cubeMoveDict[cube].hasMove, 0, cube.transform.parent);
                    cubeMoveDict[cube].hasMove = 1;
                }
            }
            else if (cubeMoveDict[cube].option == "X+")
            {
                cube.transform.Translate(Vector3.right * Time.deltaTime, cube.transform.parent);
                cubeMoveDict[cube].hasMove +=1*Time.deltaTime;
                if (cubeMoveDict[cube].hasMove >=1)
                {
                    cube.transform.Translate(1 - cubeMoveDict[cube].hasMove, 0, 0, cube.transform.parent);
                    cubeMoveDict[cube].hasMove = 1;
                }
            }
            else if (cubeMoveDict[cube].option == "X-")
            {
                cube.transform.Translate(-Vector3.right * Time.deltaTime, cube.transform.parent);
                cubeMoveDict[cube].hasMove +=1*Time.deltaTime;
                if (cubeMoveDict[cube].hasMove >=1)
                {
                    cube.transform.Translate(-1 + cubeMoveDict[cube].hasMove, 0, 0, cube.transform.parent);
                    cubeMoveDict[cube].hasMove = 1;
                }
            }
        }

        for(int i=cubeeMoveList.Count-1; i>=0; i--)
        {
            if (cubeMoveDict[cubeeMoveList[i]].hasMove == 1)
            {
                cubeMoveDict.Remove(cubeeMoveList[i]);
                cubeeMoveList.RemoveAt(i);
            }
        }
    }
}
