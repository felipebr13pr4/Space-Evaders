using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private BoxCollider2D m_boxCol;
    private SpriteRenderer m_spriteRen;

    private int m_health;

    private void Start()
    {
        m_boxCol = GetComponent<BoxCollider2D>();
        m_spriteRen = GetComponentInChildren<SpriteRenderer>(true);
    }

    public void Initialize(int health)
    {
        m_health = health;
    }

    // Yet to expand.
}
