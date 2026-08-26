using UnityEngine;

public struct BulletData
{
    public int Damage;
    public EntityType Target;
    public Color Color;
    public Vector2 Size;
    public float MoveTime;
    public float MoveDistance;
    public float Direction;

    public BulletData(int damage, EntityType target, Color color, Vector2 size, float moveTime, float moveDistance, float angleZ)
    {
        Damage = damage;
        Target = target;
        Color = color;
        Size = size;
        MoveTime = moveTime;
        MoveDistance = moveDistance;
        Direction = angleZ;
    }

    public static BulletData Default() => new(1, EntityType.Player,Color.yellow, new(0.5f, 0.5f), 2f, 1f, 0f);
}
