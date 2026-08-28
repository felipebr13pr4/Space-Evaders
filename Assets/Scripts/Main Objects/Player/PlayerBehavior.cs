
using UnityEngine;

public class PlayerBehavior : RangedEntityBehavior
{
    public static Transform Transform;

    protected override void Start()
    {
        Transform = transform;
        MaxHealth = 9;
        FireRate = 3; // for testing.
        BulletData = new(1, EntityType.Enemy, Color.lightBlue, new(0.25f, 0.25f),0.05f,0.1f,180);
        InitializeCreations();
        StartShooting();
        base.Start();
    }
}
