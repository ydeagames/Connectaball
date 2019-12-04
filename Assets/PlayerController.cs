using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int id;
    Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis($"Joy{id}X");
        var y = Input.GetAxis($"Joy{id}Y");
        //Debug.Log($"x:{x.ToString("F2")}, y:{y.ToString("F2")}");
        rigid.AddForce(new Vector2(x, y), ForceMode2D.Impulse);
    }
}
