// Had claude do these (though just placeholder values, all 1).
// Its really great at repetitive tasks.
public static class EffectsRanges
{
    // Got to remember, there are some places to also change when including a entry.
    // Player Stat Enum if a player stat. Enemy Stat Enum if a enemy stat.
    // Card Effect Handler to have it do something.
    // Effect Data putting what its framed as and the randomization value.
    // But if you forget one of these there are failsafes logerrors to warn, so if
    // you add a entry and see one of those then check if you added everything.
    // I don't quite know if it all could be generalized to a single place. Perhaps.
    // But honestly i don't know how it would work as those are not completely glued.

    public const int PlayerMaxMaxHealth = 150;
    public const int PlayerMinMaxHealth = -3;

    public const float PlayerMaxSpeed = 9;
    public const float PlayerMinSpeed = -1;

    public const float PlayerMaxFireRate = 1;
    public const float PlayerMinFireRate = -5;

    public const int PlayerMaxBulletDamage = 2;
    public const int PlayerMinBulletDamage = -1;

    public const float PlayerMaxBulletSize = 0.1f;
    public const float PlayerMinBulletSize = -0.1f;

    public const float PlayerMaxBulletMoveInterval = 0.001f;
    public const float PlayerMinBulletMoveInterval = -0.001f;

    public const int PlayerMaxShotsAmount = 5;
    public const int PlayerMinShotsAmount = 1;

    public const float PlayerMaxShotsAngle = 5;
    public const float PlayerMinShotsAngle = -40;

    public const int PlayerMaxHeal = 200;
    public const int PlayerMinHeal = -5;

    // ---

    public const int EnemyMaxMaxHealth = 5;
    public const int EnemyMinMaxHealth = -5;

    public const float EnemyMaxSpeed = 1.2f;
    public const float EnemyMinSpeed = -0.3f;

    public const float EnemyMaxFireRate = 0.8f;
    public const float EnemyMinFireRate = -0.5f;

    public const int EnemyMaxBulletDamage = 3;
    public const int EnemyMinBulletDamage = -1;

    public const float EnemyMaxBulletSize = 0.01f;
    public const float EnemyMinBulletSize = -0.01f;

}