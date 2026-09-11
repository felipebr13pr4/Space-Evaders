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

    protected override void ShootBullet(int i)
    {
        if (m_aimbotModifier.isActiveAndEnabled) 
            m_aimbotModifier.ModifyBullet(PlayerBehavior.Transform.position);

        base.ShootBullet(i);
    }

#if UNITY_EDITOR || DEVELOPMENT_BUILD
    private void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame) Die(); // for testing.
    }
#endif
}