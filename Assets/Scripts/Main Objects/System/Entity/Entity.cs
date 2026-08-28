using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Entity : MonoBehaviour
{
    protected SpriteRenderer m_spriteRenderer;
    protected Rigidbody2D m_rb2d;
    protected BoxCollider2D m_boxCol2d;
    protected Vector2 sizeAdjustment;

    protected virtual void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>(true);
        m_boxCol2d = GetComponent<BoxCollider2D>();
        sizeAdjustment = new(m_spriteRenderer.size.x / 2, m_spriteRenderer.size.y / 2);
    }

    protected void ClampInBounds()
    {
        Vector3 pos = m_rb2d.position;
        pos.x = Mathf.Clamp(pos.x, ScreenBounds.Left + sizeAdjustment.x,
                            ScreenBounds.Right - sizeAdjustment.x);
        pos.y = YClamp(pos.y);
        m_rb2d.position = pos;
    }

    protected virtual float YClamp(float y)
    {
        return y = Mathf.Clamp(y, ScreenBounds.Bottom + sizeAdjustment.y,
                            ScreenBounds.Top - sizeAdjustment.y);
    }
}
