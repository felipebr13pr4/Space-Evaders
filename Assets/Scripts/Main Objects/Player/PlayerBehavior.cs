
using UnityEngine;

public class PlayerBehavior : RangedEntityBehavior
{
    public static Transform Transform;

    protected override void Start()
    {
        Transform = transform;
        MaxHealth = 9;
        FireRate = 3; // for testing.
        InitializeCreations(CreationsHolder.Transform);
        StartShooting();
        base.Start();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(hitter: collision.gameObject.GetComponent<EntityBehavior>(),
                       takeAndDeal: true);
        }
    }

    protected override void OnceEditBullet()
    {
        m_bulletData = new(1, EntityType.Enemy, Color.lightBlue, new(0.25f, 0.25f),0.05f,0.1f,180);
    }
}
