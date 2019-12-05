using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenetertor : MonoBehaviour
{
    enum eWALL
    {
        RIGHT,
        LEFT,
        TOP,
        BOTTOM,
    }

    [SerializeField]
    GameObject wallPrefab;

    GameObject[] wall = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<4;i++)
        {
            wall[i] = Instantiate(wallPrefab) as GameObject;
        }

        wall[(int)eWALL.RIGHT].transform.position = new Vector3(18.5f, -5, 0);
        wall[(int)eWALL.LEFT].transform.position = new Vector3(-18.5f, -5, 0);
        wall[(int)eWALL.TOP].transform.position = new Vector3(0, 6.5f, 0);
        wall[(int)eWALL.TOP].transform.rotation = Quaternion.Euler(0, 0, 90);
        wall[(int)eWALL.BOTTOM].transform.position = new Vector3(0, -17, 0);
        wall[(int)eWALL.BOTTOM].transform.rotation = Quaternion.Euler(0, 0, 90);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
