
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
}