using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : Entity
{
    private float m_moveSpeed;
    private EnemyPattern m_path;

    public void Initialize(float moveSpeed, EnemyPattern path, bool isXFlipped = false, bool isYFlipped = false)
    {
        ErrorLogger.DebugLog("initializing");
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
}