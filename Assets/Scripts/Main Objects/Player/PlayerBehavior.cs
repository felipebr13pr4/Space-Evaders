
using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerBehavior : RangedEntityBehavior
{
    public static Transform Transform;
    [SerializeField] private HealthBar m_healthBar;
    public override int MaxHealth { get => base.MaxHealth;
        set { m_maxHealth = value;
            m_healthBar.UpdateValues(Health, MaxHealth); } }
    public override int Health { get => base.Health;
        set {
            base.Health = value;
            m_healthBar.UpdateValues(Health, MaxHealth); } }
    protected override float FireRateMin => 0.1f * (1 + (((float)m_multiShootModifier.MultiShotAmount / 10) / 2));
    protected override int BulletsAmount => 500;
    protected override int ShootDirection => 180;
    private float m_bulletTransparency = 1;
    public float BulletTransparency
    {   get => m_bulletTransparency;
        set { m_bulletTransparency = value;
            Color tmpColor = m_bulletColor; tmpColor.a = m_bulletTransparency;
            BulletColor = tmpColor; } }
    public static event Action OnPlayerDeath;

    public static PlayerBehavior Instance { get; private set; }
    private void Awake() => Instance = this;

    protected override void Start()
    {
        Transform = transform;
        MaxHealth = 9;
        Health = 9;
        FireRate = 3; // for testing.
        StartCoroutine(StartShooting());
        base.Start();
        m_healthBar.UpdateValues(Health, MaxHealth);
        BulletsData =
            new(1, target: EntityType.Enemy, m_bulletColor, new(0.25f, 0.25f), 0.05f, 0.1f, ShootDirection);
        BulletTransparency = PlayerPrefs.GetFloat(PrefKeys.PlayerBulletTransparency, 1f);
        m_multiShootModifier.AudioHolder = m_audioHolder;
        m_multiShootModifier.AudioIndex = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(hitter: collision.gameObject.GetComponent<EntityBehavior>(),
                       takeAndDeal: true);
        }
    }

    protected override void Die()
    {
        base.Die();
        OnPlayerDeath?.Invoke();
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
        m_audioHolder.ActivateSound(0);
    }

#if UNITY_EDITOR || DEVELOPMENT_BUILD
    [SerializeField] private Transform m_creationsHolder;

    [ContextMenu("Generate Creations")]
    private void Generate()
    {
        InitializeCreations(m_creationsHolder);
    }

    [ContextMenu("cheat stats (multi shot spam)")]
    private void Cheat()
    {
        BulletsData.MoveInterval = 0.04f;
        BulletsData.Damage = 10;
        BulletsData.Size = new(1,1);
        FireRate = 0.01f;
        MaxHealth = 9999;
        Health = 9999;
        m_multiShootModifier.MultiShotAmount = 7*8;
        m_multiShootModifier.MultiShotAngle = 18;
        GetComponent<PlayerMovement>().Speed = 5f;
    }
    [ContextMenu("cheat stats 2 (1 shot spam)")]
    private void Cheat2()
    {
        BulletsData.MoveInterval = 0.04f;
        BulletsData.Damage = 5;
        BulletsData.Size = new(1, 1);
        FireRate = 0.01f;
        MaxHealth = 9999;
        Health = 9999;
        m_aimbotModifier.enabled = true;
        GetComponent<PlayerMovement>().Speed = 5f;
    }
    [ContextMenu("cheat stats 3 (damage)")]
    private void Cheat3()
    {
        BulletsData.Damage = 500;
    }
#endif
}
