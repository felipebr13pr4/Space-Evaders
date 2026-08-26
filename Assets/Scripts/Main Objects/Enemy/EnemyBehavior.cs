
using UnityEngine;

public class EnemyBehavior : RangedEntityBehavior
{
    protected override void Start()
    {
        MaxHealth = 3;
        m_fireRate = 3; // for testing.
        m_bulletData = new(1, EntityType.Player, Color.yellow, new(0.25f, 0.25f),0.05f,0.1f,0);
        base.Start();
    }

    protected override void ClampInBounds()
    {
        Vector3 pos = m_rb2d.position;
        Vector2 sizeAdjustment = new(m_spriteRenderer.size.x / 2, m_spriteRenderer.size.y / 2);
        pos.x = Mathf.Clamp(pos.x, ScreenBounds.Left + sizeAdjustment.x,
                            ScreenBounds.Right - sizeAdjustment.x);
        pos.y = Mathf.Clamp(pos.y, (ScreenBounds.Bottom + sizeAdjustment.y)+3f,
                            ScreenBounds.Top - sizeAdjustment.y);
        m_rb2d.position = pos;
    }
}