using System;
using UnityEngine;

[Serializable]
public struct BulletData
{
    public int Damage;
    public EntityType Target;
    public Color Color;
    public Vector2 Size;
    public float MoveInterval;
    public float MoveDistance;
    public float Direction;

    public BulletData(int damage, EntityType target, Color color, Vector2 size, float moveInterval, float moveDistance, float angleZ)
    {
        Damage = damage;
        Target = target;
        Color = color;
        Size = size;
        MoveInterval = moveInterval;
        MoveDistance = moveDistance;
        Direction = angleZ;
    }

    public static BulletData Default() => new(1, EntityType.Player, Color.red, new(0.25f, 0.25f), 0.05f, 0.1f, 0f);
}
