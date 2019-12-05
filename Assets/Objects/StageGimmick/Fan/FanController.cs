using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanController : MonoBehaviour
{
    public Vector2 force;
    public BoxCollider2D box;

    bool nowFan = false;
    bool beforFan = false;

    // Start is called before the first frame update
    void Start()
    {
        //force = new Vector2(0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        var ball = GameObject.Find("Ball");
        if (ball != null)
        {
            var ballRB = ball.GetComponent<Rigidbody2D>();
            beforFan = nowFan;

            // 扇風機の上にボールがあるか判定
            nowFan = box.IsTouching(ball.GetComponent<Collider2D>());

            // 扇風機の上にいるとき、ボールが上昇する
            if (nowFan)
            {
                //force.y += 0.1f;
                ballRB.AddForce(transform.TransformDirection(force));
            }
        }

        // 扇風機の上から外れたら、forceをリセット
        //if (!nowFan && beforFan)
        //{
        //    Vector2 zero = new Vector2(0.0f, 0.0f);
        //    force = zero;
        //}
        
        // 扇風機の回転
        this.transform.Rotate(0.0f, 10.0f, 0.0f);
    }
}
