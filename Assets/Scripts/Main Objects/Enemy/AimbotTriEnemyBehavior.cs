public class AimbotTriEnemyBehavior : AimbotMultiEnemyBehavior
{
    public override float FireRate { set => base.FireRate = value * 1.5f; }

    protected override int MultiShotAngle => 22;

}