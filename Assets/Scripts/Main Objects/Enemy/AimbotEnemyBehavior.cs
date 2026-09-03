using UnityEngine;

public class AimbotEnemyBehavior : EnemyBehavior
{
    public override float FireRate { set => base.FireRate = value*2; }
    [SerializeField] protected AimbotModifier m_aimbotModifier;

    protected override void SetColor()
    {
        m_color = new(0.9f, 0.75f, 1f);
        BulletsData.Color = new(0.8f, 0, 1f);
    }

    protected override void ShootBullet(int i)
    {
        m_aimbotModifier.ModifyBullet();
        base.ShootBullet(i);
    }
}
