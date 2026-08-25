
public class PlayerBehavior : RangedEntityBehavior
{
    protected override void Start()
    {
        base.Start();
        FireRate = 3; // for testing.
        Initialize(5); // for testing.
    }
}
