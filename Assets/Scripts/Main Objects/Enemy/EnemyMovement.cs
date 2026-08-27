using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : Entity
{
    private float m_moveSpeed;
    private EnemyPattern m_path;

    public void Initialize(float moveSpeed, EnemyPattern path, bool isXFlipped = false, bool isYFlipped = false)
    {
        m_moveSpeed = moveSpeed;
        m_path = path;
        m_rb2d.position = new(isXFlipped ? 14.5f : -14.5f, 0);
        StartCoroutine(Move(isXFlipped, isYFlipped));
    }

    private IEnumerator Move(bool isXFlipped = false, bool isYFlipped = false)
    {
        while (true)
        {
            for (int i = 0; i < m_path.Movements.Count; i++)
            {
                for (int j = 0; j < m_path.Movements[i].RepeatAmount; j++)
                {
                    ClampInBounds();
                    yield return new WaitForSeconds(m_moveSpeed);

                    if (isXFlipped && m_path.Movements[i].Direction.x != 0) {
                        m_rb2d.position += -m_path.Movements[i].Direction; continue; }

                    if (isYFlipped && m_path.Movements[i].Direction.y != 0) {
                        m_rb2d.position += -m_path.Movements[i].Direction; continue; }

                    m_rb2d.position += m_path.Movements[i].Direction;
                }
            }
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