using UnityEngine;

public class MultiEnemyBehavior : EnemyBehavior
{
    protected virtual int MultiShotAmount => 3;
    protected virtual int MultiShotAngle => 45;
    protected override int BulletsAmount => 35;
    [SerializeField] protected MultiShooting m_multiShooter;

    protected override void SetColor()
    {
        base.SetColor();
        DarkenColor();
    }

    protected virtual void DarkenColor()
    {
        float darkenValue = 0.3f;
        Color darkenColor = new(darkenValue, darkenValue, darkenValue, 0);
        m_color -= darkenColor * 0.75f;
        BulletsData.Color -= darkenColor * 0.5f;
    }

    protected override void ShootBullet(int i)
    {
        base.ShootBullet(i);
        StartCoroutine(m_multiShooter.ShootBullet(MultiShotAmount, MultiShotAngle));
    }
}
