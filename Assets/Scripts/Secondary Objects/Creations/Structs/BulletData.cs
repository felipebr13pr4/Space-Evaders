using System;
using UnityEngine;

[Serializable]
public class BulletData
{
    private int m_damage;
    public int Damage
    {
        get => m_damage;
        set { m_damage = value; m_damage = Mathf.Clamp(m_damage, 0, 10); }
    }
    public EntityType Target;
    public Color Color;
    private Vector2 m_size;
    public Vector2 Size
    {
        get => m_size;
        set { m_size = value;
            m_size.x = Mathf.Clamp(m_size.x, 0.1f, 1f);
            m_size.y = Mathf.Clamp(m_size.y, 0.1f, 1f); } }
    private float m_moveInterval;
    public float MoveInterval
    {
        get => m_moveInterval;
        set { m_moveInterval = value; m_moveInterval = Mathf.Clamp(m_moveInterval, 0.04f, 1f); }
    }
    private float m_moveDistance;
    public float MoveDistance
    {
        get => m_moveDistance;
        set { m_moveDistance = value; m_moveDistance = Mathf.Clamp(m_moveDistance, 0.1f, 2f); }
    }
    public float Direction;
    public float FixedDirection;

    public BulletData(int damage, EntityType target, Color color, Vector2 size, float moveInterval, float moveDistance, float angleZ)
    {
        m_damage = Mathf.Clamp(damage, 0, 10);
        Target = target;
        Color = color;
        m_size.x = Mathf.Clamp(size.x, 0.1f, 1f);
        m_size.y = Mathf.Clamp(size.y, 0.1f, 1f);
        m_moveInterval = Mathf.Clamp(moveInterval, 0.04f, 1f);
        m_moveDistance = Mathf.Clamp(moveDistance, 0.1f, 2f);
        Direction = angleZ;
        FixedDirection = angleZ;
    }

    public static BulletData Default() => new(1, EntityType.Player, Color.red, new(0.25f, 0.25f), 0.05f, 0.1f, 0f);
}
