using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : Entity
{
    private static WaitForSeconds m_delayToMove = new(0.05f);
    private WaitForSeconds m_moveSpeedWait;
    private float m_moveSpeed;
    private Vector2 m_direction;
    [SerializeField] private LayerMask m_enemyLayerMask;
    private RaycastHit2D[] m_hit;

    public void Initialize(float moveSpeed)
    {
        m_moveSpeed = moveSpeed;

        Vector3 pos = PlayerBehavior.Transform.position;
        while (!IsPositionValid(pos))
        {
            pos = new((float)((int)Random.Range(ScreenBounds.Left, ScreenBounds.Right)) + 0.5f,
(int)Random.Range(ScreenBounds.Bottom, ScreenBounds.Top));
        }

        m_rb2d.position = pos;
        if (isActiveAndEnabled) StartCoroutine(Move());
    }

    private bool IsPositionValid(Vector3 pos)
    {
        bool isValid = true;
        if (pos.x < PlayerBehavior.Transform.position.x + 4.5f &&
            pos.x > PlayerBehavior.Transform.position.x - 4.5f &&
            pos.y < PlayerBehavior.Transform.position.y + 3.5f &&
            pos.y > PlayerBehavior.Transform.position.y - 3.5f)
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
        m_spriteRenderer.enabled = true;

        m_moveSpeedWait = new WaitForSeconds(m_moveSpeed);

        int safety = 0;

        while (true)
        {
            ClampInBounds();
            m_direction = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
            if (m_direction == Vector2.zero || CheckIfTriedToMoveOutBounds() ||
                CheckIfSomethingInWay() && safety < 15)
                { yield return null; safety++; continue;  } else if (safety > 15)
            { safety = 14; m_rb2d.position += m_direction; }

            yield return m_moveSpeedWait;
            if (!CheckIfSomethingInWay())
            {
                m_rb2d.position += m_direction;
                safety = 0;
            }
        }
    }

    protected override float YClamp(float y)
    {
        return Mathf.Clamp(y, (int)((ScreenBounds.Bottom + sizeAdjustment.y) + 2f),
                            (int)(ScreenBounds.Top - sizeAdjustment.y));
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
        if ((int)((ScreenBounds.Bottom + sizeAdjustment.y) + 2f) == transform.position.y)
        {
            if (m_direction.y < 0) itTried = true;
        }
        else if ((int)(ScreenBounds.Top - sizeAdjustment.y) == transform.position.y)
        {
            if (m_direction.y > 0) itTried = true;
        }
        else if (ScreenBounds.Left + sizeAdjustment.x == transform.position.x)
        {
            if (m_direction.x < 0) itTried = true;
        }
        else if (ScreenBounds.Right - sizeAdjustment.x == transform.position.x)
        {
            if (m_direction.x > 0) itTried = true;
        }
        return itTried;
    }

    private void OnDrawGizmos()
    {
        if (m_hit != null){
            foreach (var hit in m_hit)
            {
                Vector2 origin = transform.position;
                Gizmos.color = hit.collider != null && hit.collider.gameObject != this.gameObject
                    ? Color.green : Color.red;
                Gizmos.DrawLine(origin, origin + m_direction * 0.75f);
            }
        }
    }
}