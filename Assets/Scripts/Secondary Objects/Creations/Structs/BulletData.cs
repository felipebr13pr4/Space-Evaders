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
    public Color BulletColor;
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

    public BulletData(int damage, Vector2 size, float moveInterval, float moveDistance)
    {
        m_damage = Mathf.Clamp(damage, 0, 10);
        m_size.x = Mathf.Clamp(size.x, 0.1f, 1f);
        m_size.y = Mathf.Clamp(size.y, 0.1f, 1f);
        m_moveInterval = Mathf.Clamp(moveInterval, 0.04f, 1f);
        m_moveDistance = Mathf.Clamp(moveDistance, 0.1f, 2f);
    }

    public BulletData(int damage, EntityType target, Color color, Vector2 size, float moveInterval, float moveDistance, float angleZ)
    {
        m_damage = Mathf.Clamp(damage, 0, 10);
        Target = target;
        BulletColor = color;
        m_size.x = Mathf.Clamp(size.x, 0.1f, 1f);
        m_size.y = Mathf.Clamp(size.y, 0.1f, 1f);
        m_moveInterval = Mathf.Clamp(moveInterval, 0.04f, 1f);
        m_moveDistance = Mathf.Clamp(moveDistance, 0.1f, 2f);
        Direction = angleZ;
        FixedDirection = angleZ;
    }

    public static BulletData DefaultAll() => new(1, EntityType.Player, Color.red, new(0.25f, 0.25f), 0.05f, 0.1f, 0f);
    public static BulletData Default() => new(1, new(0.25f, 0.25f), 0.05f, 0.1f);
    public void SetMainStats(int damage, Vector2 size, float moveInterval, float moveDistance)
    {
        m_damage = Mathf.Clamp(damage, 0, 10);
        m_size.x = Mathf.Clamp(size.x, 0.1f, 1f);
        m_size.y = Mathf.Clamp(size.y, 0.1f, 1f);
        m_moveInterval = Mathf.Clamp(moveInterval, 0.04f, 1f);
        m_moveDistance = Mathf.Clamp(moveDistance, 0.1f, 2f);
    }
    public void SetMainStats(BulletData data)
    {
        m_damage = Mathf.Clamp(data.Damage, 0, 10);
        m_size.x = Mathf.Clamp(data.Size.x, 0.1f, 1f);
        m_size.y = Mathf.Clamp(data.Size.y, 0.1f, 1f);
        m_moveInterval = Mathf.Clamp(data.MoveInterval, 0.04f, 1f);
        m_moveDistance = Mathf.Clamp(data.MoveDistance, 0.1f, 2f);
    }
}
