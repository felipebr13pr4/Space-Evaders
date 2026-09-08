using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : Entity
{
    private static readonly WaitForSeconds m_delayToMove = new(0.05f);
    private WaitForSeconds m_moveSpeedWait;
    private float m_moveSpeed;
    private Vector2 m_direction;
    [SerializeField] private LayerMask m_enemyLayerMask;
    private RaycastHit2D[] m_hit;
    [SerializeField] private RangedEntityBehavior m_behavior;
    [SerializeField] private SpriteRenderer m_irisSpriteRen;
    private float m_insideAnotherTimer;
    private float m_movementBias = 1;
    private bool m_shouldHigherMovementBias;
    private int m_changeSidesPriority;
    public int ChangeSidesPriority => m_changeSidesPriority;
    private static readonly WaitForSeconds m_priorityTimer = new(1f);
    private Coroutine m_priorityCoroutine;

    protected override void Start()
    {
        base.Start();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            m_insideAnotherTimer++;
            if (m_insideAnotherTimer > 30)
            {
                ClampInBounds();
                m_direction = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
                m_rb2d.position += m_direction;
                m_insideAnotherTimer = 0;
            }
        }
    }

    public void Initialize(float moveSpeed)
    {
        m_moveSpeed = moveSpeed;
        m_changeSidesPriority = Random.Range(0, 16);

        Vector3 pos = PlayerBehavior.Transform.position;
        while (!IsPositionValid(pos))
        {
            gameObject.SetActive(false);
            pos = new((float)((int)Random.Range(ScreenBounds.Left, ScreenBounds.Right)) + 0.5f,
(int)Random.Range(ScreenBounds.Bottom, ScreenBounds.Top));
        }

        gameObject.SetActive(true);
        
        if (m_priorityCoroutine != null) StopCoroutine(m_priorityCoroutine);
        m_priorityCoroutine = StartCoroutine(IncreasePriority());

        transform.position = pos;
        if (isActiveAndEnabled) StartCoroutine(Move());
    }

    private IEnumerator IncreasePriority()
    {
        while (m_changeSidesPriority < 16)
        {
            yield return m_priorityTimer;
            m_changeSidesPriority++;
        }
    }

    private bool IsPositionValid(Vector3 pos)
    {
        bool isValid = true;
        if (pos.x < PlayerBehavior.Transform.position.x + 7.5f &&
            pos.x > PlayerBehavior.Transform.position.x - 7.5f &&
            pos.y < PlayerBehavior.Transform.position.y + 4.5f &&
            pos.y > PlayerBehavior.Transform.position.y - 4.5f)
        {
            isValid = false;
            if (pos.x != PlayerBehavior.Transform.position.x &&
                pos.y != PlayerBehavior.Transform.position.y)
            {
                ErrorLogger.DebugLog("Ok, invalid.");
                ErrorLogger.DebugLog("enemy x: " + pos.x);
                ErrorLogger.DebugLog("enemy y: " + pos.y);
                ErrorLogger.DebugLog("player x: " + PlayerBehavior.Transform.position.x);
                ErrorLogger.DebugLog("player y: " + PlayerBehavior.Transform.position.y);
            }
        }
        return isValid;
    }

    private IEnumerator Move()
    {
        yield return m_delayToMove;
        m_spriteRenderer.gameObject.SetActive(true);
        m_irisSpriteRen.color = m_behavior.BulletsData.BulletColor;

        m_moveSpeedWait = new WaitForSeconds(m_moveSpeed);

        while (true)
        {
            ClampInBounds();
            int x = MathFunctions.SkewedRandom(-1, 1, m_movementBias, m_shouldHigherMovementBias);
            m_direction = new Vector2(x, Random.Range(-1, 2));

            // Once again help by Claude. A little modified.
            float angle = (Mathf.Atan2(m_direction.y, m_direction.x) * Mathf.Rad2Deg); // This i knew.
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward); // But this was what i didn't and needed, to turn it into a quaternion,
            m_irisSpriteRen.gameObject.transform.rotation = rot;
            //

            if (m_direction == Vector2.zero || CheckIfTriedToMoveOutBounds() ||
                CheckIfSomethingInWay())
                { yield return null; continue;  }

            yield return m_moveSpeedWait;
            if (!CheckIfSomethingInWay()) m_rb2d.position += m_direction;
        }
    }

    protected override float YClamp(float y)
    {
        return Mathf.Clamp(y, (int)((ScreenBounds.Bottom + m_sizeAdjustment.y) + 2f),
                            (int)(ScreenBounds.Top - m_sizeAdjustment.y));
    }

    private bool CheckIfSomethingInWay()
    {
        bool thereIs = false;
        Vector2 origin = transform.position;
        m_hit = Physics2D.RaycastAll(origin, m_direction, 0.75f, m_enemyLayerMask);
        if (m_hit != null){
            foreach (var hit in m_hit)
            {
                if (hit.collider != null && hit.collider.gameObject != gameObject)
                {
                    thereIs = true;
                }
            }
        }
        return thereIs;
    }

    private bool CheckIfTriedToMoveOutBounds()
    {
        bool itTried = false;
        if ((int)((ScreenBounds.Bottom + m_sizeAdjustment.y) + 2f) == transform.position.y)
        {
            if (m_direction.y < 0) itTried = true;
        }
        else if ((int)(ScreenBounds.Top - m_sizeAdjustment.y) == transform.position.y)
        {
            if (m_direction.y > 0) itTried = true;
        }
        else if (ScreenBounds.Left + m_sizeAdjustment.x == transform.position.x)
        {
            if (m_direction.x < 0) itTried = true;
        }
        else if (ScreenBounds.Right - m_sizeAdjustment.x == transform.position.x)
        {
            if (m_direction.x > 0) itTried = true;
        }
        return itTried;
    }

    public IEnumerator ChangeSides(bool goRight)
    {
        m_changeSidesPriority = 0;
        m_movementBias = 4;
        m_shouldHigherMovementBias = goRight;
        ErrorLogger.DebugLog("ok, active changing sides");
        yield return new WaitForSeconds(15);
        ErrorLogger.DebugLog("okay, unactive changing sides");
        m_movementBias = 1;
    }
}