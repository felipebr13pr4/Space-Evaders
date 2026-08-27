using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : Entity
{
    private static WaitForSeconds m_delayToMove = new(0.05f);
    private WaitForSeconds m_moveSpeedWait;
    private float m_moveSpeed;

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

            m_rb2d.position += new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
        }
    }

    protected override void ClampInBounds()
    {
        Vector3 pos = m_rb2d.position;
        Vector2 sizeAdjustment = new(m_spriteRenderer.size.x / 2, m_spriteRenderer.size.y / 2);
        pos.x = Mathf.Clamp(pos.x, ScreenBounds.Left + sizeAdjustment.x,
                            ScreenBounds.Right - sizeAdjustment.x);
        pos.y = Mathf.Clamp(pos.y, (ScreenBounds.Bottom + sizeAdjustment.y) + 2f,
                            ScreenBounds.Top - sizeAdjustment.y);
        m_rb2d.position = pos;
    }
}