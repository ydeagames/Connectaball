using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenetertor : MonoBehaviour
{
    public BoxCollider2D box;

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
        for (int i = 0; i < 4; i++)
            wall[i] = Instantiate(wallPrefab, transform);

        wall[(int)eWALL.TOP].transform.localPosition = new Vector3(0, box.bounds.max.y, 0);
        wall[(int)eWALL.TOP].transform.localRotation = Quaternion.Euler(0, 0, 0);
        wall[(int)eWALL.TOP].transform.localScale = new Vector3(box.bounds.extents.x, 1, 1);
        wall[(int)eWALL.LEFT].transform.localPosition = new Vector3(box.bounds.min.x, 0, 0);
        wall[(int)eWALL.LEFT].transform.localRotation = Quaternion.Euler(0, 0, 90);
        wall[(int)eWALL.LEFT].transform.localScale = new Vector3(box.bounds.extents.y, 1, 1);
        wall[(int)eWALL.BOTTOM].transform.localPosition = new Vector3(0, box.bounds.min.y, 0);
        wall[(int)eWALL.BOTTOM].transform.localRotation = Quaternion.Euler(0, 0, 180);
        wall[(int)eWALL.BOTTOM].transform.localScale = new Vector3(box.bounds.extents.x, 1, 1);
        wall[(int)eWALL.RIGHT].transform.localPosition = new Vector3(box.bounds.max.x, 0, 0);
        wall[(int)eWALL.RIGHT].transform.localRotation = Quaternion.Euler(0, 0, 270);
        wall[(int)eWALL.RIGHT].transform.localScale = new Vector3(box.bounds.extents.y, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
