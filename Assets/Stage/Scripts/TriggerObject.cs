using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    private string m_ballTag = "Ball";
    private bool m_isOn = false;
    private bool m_callFixed = false;

    /// <summary>
    /// ボールが上に乗っているかどうか
    /// </summary>
    /// <returns><c>true</c>, if player on was ised, <c>false</c> otherwise.</returns>
    public bool IsBallOn()
    {
        return m_isOn;
    }

    private void FixedUpdate()
    {
        m_callFixed = true;
    }

    private void LateUpdate()
    {
        if (m_callFixed)
        {
            //フラグを元に戻します
            m_isOn = false;
            m_callFixed = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == m_ballTag)
        {
            //プレイヤーの下から４分の１が範囲内にいる時乗っているとみなします
            if (collision.transform.position.y - (collision.bounds.size.y / 4.0f) > transform.position.y)
            {
                m_isOn = true;
            }
        }
    }
}