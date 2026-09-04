
using System.Collections.Generic;
using System.Linq;
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
    protected override float FireRateMin { get => 0.1f; }
    protected override int BulletsAmount => 500;
    [SerializeField] protected AimbotModifier m_aimbotModifier;
    [SerializeField] protected MultiShooting m_multiShooter;
    protected override int ShootDirection => 180;

    protected override void Start()
    {
        Transform = transform;
        MaxHealth = 9;
        FireRate = 3; // for testing.
        StartCoroutine(StartShooting());
        base.Start();
        m_healthBar.UpdateValues(Health, MaxHealth);
        Color tempColor = Color.lightBlue;
        tempColor.a = 0.5f;
        BulletsData = new(1, EntityType.Enemy, tempColor, new(0.25f, 0.25f), 0.05f, 0.1f, 180);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(hitter: collision.gameObject.GetComponent<EntityBehavior>(),
                       takeAndDeal: true);
        }
    }

    protected override void ShootBullet(int i)
    {

        if (m_aimbotModifier.isActiveAndEnabled &&
            EnemiesActive.Instance.ActiveEnemies.Count != 0)
        {
            float distance = 999;
            float tmpDistance = 0;
            GameObject target = null;
            foreach (GameObject enemy in EnemiesActive.Instance.ActiveEnemies.Values)
            {
                tmpDistance = Vector3.Distance(transform.position, enemy.transform.position);
                if (tmpDistance < distance)
                {
                    distance = tmpDistance;
                    target = enemy;
                }
            }
            if (target != null) m_aimbotModifier.ModifyBullet(target.transform.position);
        }
        else BulletsData.FixedDirection = 180;

        base.ShootBullet(i);
        
        if (m_multiShooter.isActiveAndEnabled) StartCoroutine(m_multiShooter.ShootBullet(3, 45));
    }

#if UNITY_EDITOR
    [SerializeField] private Transform m_creationsHolder;

    [ContextMenu("Generate Creations")]
    private void Generate()
    {
        InitializeCreations(m_creationsHolder);
    }

    [ContextMenu("cheat stats")]
    private void Cheat()
    {
        BulletsData.MoveInterval = 0.04f;
        FireRate = 0.01f;
        MaxHealth = 999;
        GetComponent<PlayerMovement>().Speed = 10f;
    }
#endif
}
