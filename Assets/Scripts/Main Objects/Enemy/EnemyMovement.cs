using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : Entity
{
    private static WaitForSeconds m_delayToMove = new(0.05f);
    private WaitForSeconds m_moveSpeedWait;
    private float m_moveSpeed;
    private Vector2 m_direction;
    // [SerializeField] private LayerMask m_enemyLayerMask;
    // private RaycastHit2D m_hit;

    public void Initialize(float moveSpeed)
    {
        m_moveSpeed = moveSpeed;
        m_rb2d.position = new((float)((int)Random.Range(ScreenBounds.Left, ScreenBounds.Right))+0.5f,
            (int)Random.Range(ScreenBounds.Bottom, ScreenBounds.Top));
        if (isActiveAndEnabled) StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        yield return m_delayToMove;
        m_spriteRenderer.enabled = true;

        m_moveSpeedWait = new WaitForSeconds(m_moveSpeed);


        while (true)
        {
            ClampInBounds();

            yield return m_moveSpeedWait;
            m_direction = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
            // CheckIfSomethingInWay();
            m_rb2d.position += m_direction;
        }
    }

    protected override float YClamp(float y)
    {
        return Mathf.Clamp(y, (int)((ScreenBounds.Bottom + sizeAdjustment.y) + 2f),
                            (int)(ScreenBounds.Top - sizeAdjustment.y));
    }

    /*private void CheckIfSomethingInWay()
    {
        Vector2 origin = transform.position;
        m_hit = Physics2D.Raycast(origin, m_direction, 0.5f, m_enemyLayerMask);
        if (m_hit.collider != null && m_hit.collider != this)
        {
            Debug.Log("Hit: " + m_hit.collider.name);
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 origin = transform.position;
        Gizmos.color = m_hit.collider != null ? Color.green : Color.red;
        Gizmos.DrawLine(origin, origin + m_direction * 0.5f);
    }*/
}