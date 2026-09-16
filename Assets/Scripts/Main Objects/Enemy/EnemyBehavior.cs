using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyBehavior : RangedEntityBehavior
{
    [SerializeField] private int m_baseHealthAdder = 0;
    [SerializeField] private float m_baseFireRateMultiplier = 1;
    public override int MaxHealth { get => base.MaxHealth; set => base.MaxHealth = value + m_baseHealthAdder; }
    public override float FireRate { set => base.FireRate = value * m_baseFireRateMultiplier; }
    protected override int BulletsAmount => 20+(m_multiShootModifier.MultiShotAmount*5);
    protected override Color DamageNumberColor => new(1, 0.2f, 0.2f, 1);
    public static event Action<AchievementType> OnDeathAchievement;
    public static event Action<int, AchievementType> OnDamageTaken;

    protected void OnEnable()
    {
        EnemiesActive.Instance.EnemyActive(this);
        if (isActiveAndEnabled) StartCoroutine(StartShooting());
    }

    protected override void Start()
    {
        base.Start();
        BulletsData =
            new(1, target: EntityType.Player, m_bulletColor, new(0.25f, 0.25f), 0.05f, 0.1f, 0);
    }

    public override void TakeDamage(int damage = 0, EntityBehavior hitter = null, bool takeAndDeal = false)
    {
        OnDamageTaken?.Invoke(damage, AchievementType.DamageDealt);
        base.TakeDamage(damage, hitter, takeAndDeal);
    }

    protected override void ShootBullet(int i)
    {
        if (m_aimbotModifier.isActiveAndEnabled) 
            m_aimbotModifier.ModifyBullet(PlayerBehavior.Transform.position);

        base.ShootBullet(i);
    }

    public override void Initialize()
    {
        base.Initialize();
        m_bullets[BulletIndex].InitializeStats(m_bulletData);
    }

    protected override void Die()
    {
        base.Die();
        m_audioHolder.ActivateStoppableSound(0);
        AchievementType type = AchievementType.None;
        if (!m_multiShootModifier.enabled && !m_aimbotModifier.enabled) type = AchievementType.BasicKilled;
        if (!m_multiShootModifier.enabled && m_aimbotModifier.enabled) type = AchievementType.AimbotKilled;
        else if (m_multiShootModifier.enabled && !m_aimbotModifier.enabled)
        {
            type = m_multiShootModifier.MultiShotAmount switch
            {
                2 => AchievementType.TriKilled,
                4 => AchievementType.FiveKilled,
                6 => AchievementType.SevenKilled,
                9 => AchievementType.TenKilled,
                19 => AchievementType.BossKilled,
                _ => AchievementType.None
            };
        }
        if (m_multiShootModifier.enabled && m_aimbotModifier.enabled)
        {
            type = m_multiShootModifier.MultiShotAmount switch
            {
                2 => AchievementType.AimbotTriKilled,
                4 => AchievementType.AimbotFiveKilled,
                6 => AchievementType.AimbotSevenKilled,
                9 => AchievementType.AimbotTenKilled,
                19 => AchievementType.AimbotBossKilled,
                _ => AchievementType.None
            };
        }
        OnDeathAchievement(type);
    }

#if UNITY_EDITOR || DEVELOPMENT_BUILD
    private void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame) Die(); // for testing.
    }
#endif
}