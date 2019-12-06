using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(BoxCollider2D),typeof(PlatformEffector2D))]
public class FallDownFloor : Bolt.EntityBehaviour<ITransformState>
{
    [Header("スプライトがあるオブジェクト")] public GameObject spriteObj;
    [Header("プレイヤーの判定をするスクリプト")] public TriggerObject trigger;
    [Header("振動幅")] public float vibrationWidth = 0.05f;
    [Header("振動速度")] public float vibrationSpeed = 30.0f;
    [Header("落ちるまでの時間")] public float fallTime = 1.0f;
    [Header("落ちていく速度")] public float fallSpeed = 10.0f;
    [Header("落ちてから戻ってくる時間")] public float returnTime = 5.0f;

    private bool m_isOn;
    private bool m_isFall;
    private bool m_isReturn;
    private Vector3 m_spriteDefaultPos;
    private Vector3 m_floorDefaultPos;
    private BoxCollider2D m_col;
    private Rigidbody2D m_rb;
    private Vector2 m_fallVelocity;
    private SpriteRenderer m_sr;
    private float m_timer = 0.0f;
    private float m_fallingTimer = 0.0f;
    private float m_returnTimer = 0.0f;
    private float m_blinkTimer = 0.0f;

    private void Start()
    {
        m_col = GetComponent<BoxCollider2D>();
        m_rb = GetComponent<Rigidbody2D>();
        if (spriteObj != null && trigger != null && m_col != null && m_rb != null)
        {
            m_spriteDefaultPos = spriteObj.transform.position;
            m_fallVelocity = new Vector2(0, -fallSpeed);
            m_floorDefaultPos = gameObject.transform.position;
            m_sr = spriteObj.GetComponent<SpriteRenderer>();
            if (m_sr == null)
            {
                Destroy(this);
            }
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (trigger.IsBallOn())
        {
            m_isOn = true;
        }

        if (m_isOn && !m_isFall)
        {
            spriteObj.transform.position = m_spriteDefaultPos + new Vector3(Mathf.Sin(m_timer * vibrationSpeed) * vibrationWidth, 0, 0);

            if (m_timer > fallTime)
                m_isFall = true;

            m_timer += Time.deltaTime;
        }

        if (m_isReturn)
        {
            if (m_blinkTimer > 0.2f)
            {
                m_sr.enabled = true;
                m_blinkTimer = 0.0f;
            }
            else if (m_blinkTimer > 0.1f)
                m_sr.enabled = false;
            else
                m_sr.enabled = true;

            if (m_returnTimer > 1.0f)
            {
                m_isReturn = false;
                m_blinkTimer = 0f;
                m_returnTimer = 0f;
                m_sr.enabled = true;
            }
            else
            {
                m_blinkTimer += Time.deltaTime;
                m_returnTimer += Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        if (m_isFall)
        {
            m_rb.velocity = m_fallVelocity;

            if (m_fallingTimer > fallTime)
            {
                m_isReturn = true;
                transform.position = m_floorDefaultPos;
                m_rb.velocity = Vector2.zero;
                m_isFall = false;
                m_timer = 0.0f;
                m_fallingTimer = 0.0f;
            }
            else
            {
                m_fallingTimer += Time.deltaTime;
                m_isOn = false;
            }
        }
    }

    /// <summary>
    /// transformをIPlayerStateのTranformに割り当てます。
    /// </summary>
    public override void Attached()
    {
        state.SetTransforms(state.Transform, transform);
    }
}