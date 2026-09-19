using UnityEngine;

public static class DefaultBulletValues
{
    public const int Damage = 1;
    public static readonly EntityType Target = EntityType.Player;
    public static readonly Color BulletColor = Color.red;
    public static readonly Vector2 Size = new(0.25f, 0.25f);
    public const float MoveInterval = 0.06f;
    public const float MoveDistance = 0.1f;
    public const float AngleZ = 0f;
}