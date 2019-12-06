using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanController : MonoBehaviour
{
    public Vector2 force;
    public BoxCollider2D box;
    public GameObject model;
    public LayerMask targetMask;

    // Start is called before the first frame update
    void Start()
    {
        //force = new Vector2(0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        var filter = new ContactFilter2D() { layerMask = targetMask.value, useLayerMask = true };
        var results = new Collider2D[50];
        var resultCount = box.OverlapCollider(filter, results);
        for (int i = 0; i < resultCount; i++)
        {
            var ball = results[i];

            var ballRB = ball.GetComponent<Rigidbody2D>();

            //force.y += 0.1f;
            ballRB.AddForce(transform.TransformDirection(force));
        }

        // 扇風機の回転
        model.transform.Rotate(0.0f, 10.0f, 0.0f);
    }
}
