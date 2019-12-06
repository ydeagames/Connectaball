using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class MovingBlockControl : Bolt.EntityBehaviour<ITransformState>
{
    #region Constants

    private const int MaxRaycastHit = 8;

    #endregion

    #region Fields

    [SerializeField]
    private bool m_IsVertical;

    [SerializeField]
    private float m_Speed;

    [SerializeField]
    private int m_RaySize;

    [SerializeField]
    private float m_MaxRayDistance;

    [SerializeField]
    private LayerMask m_Collidable;

    [SerializeField]
    private LayerMask m_StaticCollidable;

    private BoxCollider2D m_BoxCollider;

    private Rigidbody2D m_Rigidbody;

    private readonly RaycastHit2D[] m_Results = new RaycastHit2D[MaxRaycastHit];

    #endregion

    #region Properties

    public bool IsVertical
    {
        get { return m_IsVertical; }
        set { m_IsVertical = value; }
    }

    public float Speed
    {
        get { return m_Speed; }
        set { m_Speed = value; }
    }

    public int RaySize
    {
        get { return m_RaySize; }
        set { m_RaySize = value; }
    }

    public float MaxRayDistance
    {
        get { return m_MaxRayDistance; }
        set { m_MaxRayDistance = value; }
    }

    public LayerMask Collidable
    {
        get { return m_Collidable; }
        set { m_Collidable = value; }
    }

    public LayerMask StaticCollidable
    {
        get { return m_StaticCollidable; }
        set { m_StaticCollidable = value; }
    }

    #endregion

    #region Messages

    private void Awake()
    {
        this.m_BoxCollider = GetComponent<BoxCollider2D>();
        this.m_Rigidbody = GetComponent<Rigidbody2D>();

        m_Rigidbody.isKinematic = true;
        m_Rigidbody.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnValidate()
    {
        m_RaySize = Mathf.Max(m_RaySize, 1);
        m_MaxRayDistance = Mathf.Max(m_MaxRayDistance, 0.0f);

        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.isKinematic = true;
        rigidbody.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    #endregion

    #region Methods

    private void Move()
    {
        Vector2 start, direction, range;

        CollectRayInfo(out start, out range, out direction);

        Vector2 spacing = (m_RaySize > 1 ? range / (m_RaySize - 1) : Vector2.zero);
        for (int i = 0; i < m_RaySize; i++)
        {
            Vector2 origin = start + spacing * i;

            if (!CheckSpace(origin, direction))
            {
                m_Speed = -m_Speed;

                return;
            }
        }

        Vector2 nextPosition = m_Rigidbody.position + direction * Mathf.Abs(m_Speed) * Time.fixedDeltaTime;

        m_Rigidbody.MovePosition(nextPosition);
    }


    private void CollectRayInfo(out Vector2 start, out Vector2 range, out Vector2 direction)
    {
        Bounds bounds = m_BoxCollider.bounds;
        Rect rect = new Rect(bounds.min, bounds.size);
        if (m_IsVertical)
        {
            if (m_Speed >= 0.0f)
            {
                start = new Vector2(rect.xMin, rect.yMax + Vector2.kEpsilon) + Vector2.right * Vector2.kEpsilon;
                range = Vector2.right * (rect.width - Vector2.kEpsilon * 2.0f);
                direction = Vector2.up;
            }
            else
            {
                start = new Vector2(rect.xMin, rect.yMin - Vector2.kEpsilon) + Vector2.right * Vector2.kEpsilon;
                range = Vector2.right * (rect.width - Vector2.kEpsilon * 2.0f);
                direction = Vector2.down;
            }
        }
        else
        {
            if (m_Speed >= 0.0f)
            {
                start = new Vector2(rect.xMax + Vector2.kEpsilon, rect.yMin) + Vector2.up * Vector2.kEpsilon;
                range = Vector2.up * (rect.height - Vector2.kEpsilon * 2.0f);
                direction = Vector2.right;
            }
            else
            {
                start = new Vector2(rect.xMin - Vector2.kEpsilon, rect.yMin) + Vector2.up * Vector2.kEpsilon;
                range = Vector2.up * (rect.height - Vector2.kEpsilon * 2.0f);
                direction = Vector2.left;
            }
        }

        if (m_RaySize == 1)
        {
            start += range * 0.5f;
            range = Vector2.zero;
        }
    }

    private bool CheckSpace(Vector2 origin, Vector2 direction)
    {
        int layerMask = m_Collidable.value | m_StaticCollidable.value;
        int size = Physics2D.RaycastNonAlloc(origin, direction, m_Results, m_MaxRayDistance, layerMask);

        DrawRay(origin, direction, m_MaxRayDistance, Color.green);

        float space = 0.0f;
        float offset = 0.0f;
        for (int i = 0; i < size; i++)
        {
            RaycastHit2D result = m_Results[i];
            Collider2D collider = result.collider;

            if (collider == m_BoxCollider) continue;

            float diff = result.distance - offset;
            if (diff > Mathf.Epsilon)
            {
                space += diff;
            }

            int layer = collider.gameObject.layer;
            if (((1 << layer) & m_StaticCollidable.value) != 0)
            {
                DrawRay(origin, direction, result.distance, Color.red);

                return (space > Mathf.Abs(m_Speed * Time.fixedDeltaTime));
            }

            offset = GetNextOffset(origin, collider);
        }

        return (size != MaxRaycastHit);
    }

    private float GetNextOffset(Vector2 origin, Collider2D collider)
    {
        float offset;

        if (m_IsVertical)
        {
            if (m_Speed >= 0.0f)
            {
                offset = collider.bounds.max.y - origin.y;
            }
            else
            {
                offset = origin.y - collider.bounds.min.y;
            }
        }
        else
        {
            if (m_Speed >= 0.0f)
            {
                offset = collider.bounds.max.x - origin.x;
            }
            else
            {
                offset = origin.x - collider.bounds.min.x;
            }
        }

        return offset;
    }

    [Conditional("UNITY_EDITOR")]
    private void DrawRay(Vector2 origin, Vector2 direction, float distance, Color color)
    {
        UnityEngine.Debug.DrawLine(origin, origin + direction * distance, color);
    }

    /// <summary>
    /// transformをIPlayerStateのTranformに割り当てます。
    /// </summary>
    public override void Attached()
    {
        state.SetTransforms(state.Transform, transform);
    }

    #endregion
}