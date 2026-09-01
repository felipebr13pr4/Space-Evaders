
using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerBehavior : RangedEntityBehavior
{
    public static Transform Transform;
    [SerializeField] private HealthBar m_healthBar;
    public override int Health { get => base.Health; 
        set {
            base.Health = value;
            m_healthBar.UpdateValues(Health, MaxHealth); } }

    protected override void Start()
    {
        Transform = transform;
        MaxHealth = 9;
        FireRate = 3; // for testing.
        int bulletsAmt = 100;
        m_bulletsObjs = new GameObject[bulletsAmt];
        m_bullets = new Bullet[bulletsAmt];
        InitializeCreations(CreationsHolder.Transform);
        StartShooting();
        base.Start();
        m_healthBar.UpdateValues(Health, MaxHealth);
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

#if UNITY_EDITOR
    [ContextMenu("cheat stats")]
    private void Cheat()
    {
        m_bulletData.MoveInterval = 0.04f;
        FireRate = 0.01f;
        MaxHealth = 999;
        GetComponent<PlayerMovement>().Speed = 10f;
    }
#endif
}
