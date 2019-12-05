using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnoneGenerator : MonoBehaviour
{
    public GameObject StonePrefab;
    float span = 3.0f;
    float delta = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;

        if (this.delta > this.span)
        {
            this.delta = 0;
            GameObject go = Instantiate(StonePrefab) as GameObject;

            Vector3 pos = this.transform.position;

            pos.y -= 0.5f;

            go.transform.position = pos;
            
        }
        
    }
}
