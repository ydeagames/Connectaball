using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftFanController : MonoBehaviour
{
    GameObject ball;
    Rigidbody2D ballRB;
    Vector2 force;

    bool nowFan = false;
    bool beforFan = false;

    // Start is called before the first frame update
    void Start()
    {
        this.ball = GameObject.Find("Ball");
        this.ballRB = ball.GetComponent<Rigidbody2D>();

        force = new Vector2(0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        beforFan = nowFan;

        // 扇風機の上にボールがあるか判定
        if (
            ball.transform.position.x < this.transform.position.x &&
            ball.transform.position.y > this.transform.position.y - 2.0f &&
            ball.transform.position.y < this.transform.position.y + 2.0f
            )
        {
            nowFan = true;
        }
        else
        {
            nowFan = false;
        }

        // 扇風機の上にいるとき、ボールが上昇する
        if (nowFan)
        {
            force.x -= 0.1f;
            ballRB.AddForce(force);
        }

        // 扇風機の上から外れたら、forceをリセット
        if (!nowFan && beforFan)
        {
            Vector2 zero = new Vector2(0.0f, 0.0f);
            force = zero;
        }



        // 扇風機の回転
        this.transform.Rotate(0.0f, 10.0f, 0.0f);
    }
}
