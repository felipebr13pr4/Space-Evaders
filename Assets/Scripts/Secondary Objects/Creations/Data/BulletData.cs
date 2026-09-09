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
            m_size.x = Mathf.Clamp(m_size.x, 0.2f, 3f);
            m_size.y = Mathf.Clamp(m_size.y, 0.2f, 3f); } }
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
    : this(damage, default, Color.white, size, moveInterval, moveDistance, 0f) { }

    public BulletData(int damage, EntityType target, Color color, Vector2 size, float moveInterval, float moveDistance, float angleZ)
    {
        Damage = damage;
        Target = target;
        BulletColor = color;
        Size = size;
        MoveInterval = moveInterval;
        MoveDistance = moveDistance;
        Direction = angleZ;
        FixedDirection = angleZ;
    }

    public static BulletData DefaultAll() => new(
        DefaultBulletValues.Damage, DefaultBulletValues.Target, DefaultBulletValues.BulletColor,
        DefaultBulletValues.Size, DefaultBulletValues.MoveInterval, DefaultBulletValues.MoveDistance,
        DefaultBulletValues.AngleZ);

    public static BulletData Default() => new(
        DefaultBulletValues.Damage, DefaultBulletValues.Size, DefaultBulletValues.MoveInterval,
        DefaultBulletValues.MoveDistance);

    public void SetMainStats(int damage, Vector2 size, float moveInterval, float moveDistance)
    {
        Damage = damage;
        Size = size;
        MoveInterval = moveInterval;
        MoveDistance = moveDistance;
    }

    public void SetMainStats(BulletData data)
    => SetMainStats(data.Damage, data.Size, data.MoveInterval, data.MoveDistance);
}