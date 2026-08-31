
using System;
using UnityEngine;

public class PlayerBehavior : RangedEntityBehavior
{
    public static Transform Transform;
    public static event Action<int, int> OnLifeChange;
    public override int Health { get => base.Health; 
        set {
            base.Health = value;
            OnLifeChange?.Invoke(Health, MaxHealth); } }

    protected override void Start()
    {
        Transform = transform;
        MaxHealth = 9;
        FireRate = 3; // for testing.
        InitializeCreations(CreationsHolder.Transform);
        StartShooting();
        base.Start();
        OnLifeChange?.Invoke(Health, MaxHealth);
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
        FireRate = 0.01f;
        MaxHealth = 999;
        GetComponent<PlayerMovement>().Speed = 10f;
    }
#endif
}
