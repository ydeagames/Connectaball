using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanEffectGenerator : MonoBehaviour
{
    public GameObject FanEffectPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = Instantiate(FanEffectPrefab) as GameObject;

        Vector3 pos = this.transform.position;
        go.transform.position = pos;
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
