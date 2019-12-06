using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField]
    private Fade m_fade = null;

    private bool m_isFirst;

    // Start is called before the first frame update
    void Start()
    {
        m_isFirst = false;
        // 0.5秒後にフェードアウトする
        StartCoroutine(this.DelayMethod(0.5f, FadeOutMethod));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TransitionAction();
    }

    public void TransitionAction()
    {
        if (!m_isFirst)
        {
            FadeInMethod();
            // 1秒後に遷移する
            StartCoroutine(this.DelayMethod(1.0f, TransitionToClearScene));
            m_isFirst = true;
        }
    }

    // ClearScene へ遷移
    private void TransitionToClearScene()
    {
        BoltNetwork.LoadScene("ClearScene");
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
