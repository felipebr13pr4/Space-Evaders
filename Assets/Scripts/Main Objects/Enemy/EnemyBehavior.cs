
using System;
using UnityEngine;

public class EnemyBehavior : RangedEntityBehavior
{
    protected override void Start()
    {
        BulletData = new(1, EntityType.Player, Color.yellow, new(0.25f, 0.25f), 0.05f, 0.1f, 0);
        base.Start();
    }
}