using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Entity : MonoBehaviour
{
    protected SpriteRenderer m_spriteRenderer;
    protected Rigidbody2D m_rb2d;
    protected BoxCollider2D m_boxCol2d;

    protected virtual void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>(true);
        m_boxCol2d = GetComponent<BoxCollider2D>();
    }

    protected virtual void ClampInBounds()
    {
        Vector3 pos = m_rb2d.position;
        Vector2 sizeAdjustment = new(m_spriteRenderer.size.x / 2, m_spriteRenderer.size.y / 2);
        pos.x = Mathf.Clamp(pos.x, ScreenBounds.Left + sizeAdjustment.x,
                            ScreenBounds.Right - sizeAdjustment.x);
        pos.y = Mathf.Clamp(pos.y, ScreenBounds.Bottom + sizeAdjustment.y,
                            ScreenBounds.Top - sizeAdjustment.y);
        m_rb2d.position = pos;
    }
}
