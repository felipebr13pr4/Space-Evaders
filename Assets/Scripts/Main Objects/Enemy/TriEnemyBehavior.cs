public class TriEnemyBehavior : MultiEnemyBehavior
{
    public override float FireRate { set => base.FireRate = value * 2; }

    protected override void SetColor()
    {
        m_color = EnemyColor;
        BulletsData.Color = EnemyBulletColor;
        base.SetColor();
    }
}
