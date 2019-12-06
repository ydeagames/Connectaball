using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField]
    private Fade m_fade = null;

    // Start is called before the first frame update
    void Start()
    {
        // 0.5秒後にフェードアウトする
        StartCoroutine(this.DelayMethod(0.5f, FadeOutMethod));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FadeInMethod()
    {
        m_fade.FadeIn(1.0f);
    }
    private void FadeOutMethod()
    {
        m_fade.FadeOut(1.0f);
    }
}
