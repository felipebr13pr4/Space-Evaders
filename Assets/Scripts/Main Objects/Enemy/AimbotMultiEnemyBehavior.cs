using UnityEngine;

public class AimbotMultiEnemyBehavior : MultiEnemyBehavior
{
    public override Color EnemyColor => EnemyColors.AimbotEnemyColor;
    public override Color EnemyBulletColor => EnemyColors.AimbotEnemyBulletColor;

    public override float FireRate { set => base.FireRate = value * 2f; }
    [SerializeField] protected AimbotModifier m_aimbotModifier;

    protected override void SetColor()
    {
        m_color = EnemyColor;
        BulletsData.Color = EnemyBulletColor;
        base.SetColor();
    }

    protected override void ShootBullet(int i)
    {
        m_aimbotModifier.ModifyBullet(PlayerBehavior.Transform.position);
        base.ShootBullet(i);
    }

}