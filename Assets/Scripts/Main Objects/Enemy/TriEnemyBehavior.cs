public class TriEnemyBehavior : MultiEnemyBehavior
{
    public override float FireRate { set => base.FireRate = value * 2; }
}
