using System;
using UnityEngine;
using Random = UnityEngine.Random;
using static EffectsRanges;

[Serializable]
public class EffectData
{
    [SerializeField] private PlayerStat m_playerStatEffect = PlayerStat.None;
    public PlayerStat PlayerStatEffect
    {   get => m_playerStatEffect;
        set { m_playerStatEffect = value; m_enemyStatEffect = EnemyStat.None; } }
    [SerializeField] private EnemyStat m_enemyStatEffect = EnemyStat.None;
    public EnemyStat EnemyStatEffect
    {   get => m_enemyStatEffect;
        set { m_enemyStatEffect = value; m_playerStatEffect = PlayerStat.None; } }
    public bool IsPlayerStat => m_enemyStatEffect == EnemyStat.None;

    [SerializeField] private float m_amountFloat;
    public float AmountFloat
    { get => m_amountFloat; set { m_amountFloat = value; m_amountInt = 0; } }
    [SerializeField] private int m_amountInt;
    public int AmountInt
    { get => m_amountInt; set { m_amountInt = value; m_amountFloat = 0; } }
    public bool IsFloat => m_amountFloat != 0;
    public bool IsPositive => m_amountFloat > 0 || m_amountInt > 0;
    public bool AreBothAmountsZero => m_amountInt == 0 && m_amountFloat == 0;

    public FramedAs FramedAs => HandleFramedAs();
    /// <summary>
    /// Returns what the effect is framed as. If is positive true then its a buff.
    /// Invert is positive if you want positive numbers to be considered a debuff. <br/><br/>
    /// Normal IsPositive = if true(positive) its a buff, false(negative) its a debuff <br/><br/>
    /// Inverted IsPositive = if false(negative) its a buff, true(positive) its a debuff <br/><br/>
    /// </summary>
    private FramedAs IsBuff(bool isPositive) => isPositive ? FramedAs.Buff : FramedAs.Debuff;

    [SerializeField] private bool m_enabled;
    public bool Enabled { get => m_enabled; set => m_enabled = value; }



    /// <summary>
    /// Decides what the effect is framed as and if should invert.
    /// </summary>
    private FramedAs HandleFramedAs()
    {
        if (!Enabled) return FramedAs.Buff;
        if (IsPlayerStat)
        {
            switch (m_playerStatEffect)
            {
                case PlayerStat.MaxHealth:
                    return IsBuff(IsPositive);

                case PlayerStat.Speed:
                    return IsBuff(IsPositive);
                
                case PlayerStat.FireRate:
                    return IsBuff(!IsPositive);

                case PlayerStat.BulletDamage:
                    return IsBuff(IsPositive);

                case PlayerStat.BulletSize:
                    return IsBuff(IsPositive);

                case PlayerStat.BulletMoveInterval:
                    return IsBuff(!IsPositive);

                case PlayerStat.ShotsAmount:
                    return IsBuff(IsPositive);

                case PlayerStat.ShotsAngle:
                    return IsBuff(!IsPositive);

                case PlayerStat.Heal:
                    return IsBuff(IsPositive);

                case PlayerStat.Aimbot:
                    return FramedAs.Buff;

                default:
                    ErrorLogger.LogError("No player stats yet 'Player Stat is true' detected");
                    return FramedAs.None;
            }
        }
        else
        {
            switch (m_enemyStatEffect)
            {
                case EnemyStat.MaxHealth:
                    return IsBuff(!IsPositive);

                case EnemyStat.Speed:
                    return IsBuff(IsPositive);

                case EnemyStat.FireRate:
                    return IsBuff(IsPositive);

                case EnemyStat.BulletDamage:
                    return IsBuff(!IsPositive);

                case EnemyStat.BulletSize:
                    return IsBuff(!IsPositive);

                default:
                    ErrorLogger.LogError("No enemy stats yet 'Enemy Stat is true' detected");
                    return FramedAs.None;
            }
        }
    }

    // This could 100% be made better. Especially with genericity.
    // But idk that enough i'd say. Also i had claude do the repetitive switches. Its great at that.
    public void Randomize()
    {
        bool isPlayerStat = Random.Range(0,2) == 0;
        if (isPlayerStat)
        {
            PlayerStat stat = PlayerStat.None;
            while (stat == PlayerStat.None)
                stat = (PlayerStat)Random.Range(0, Enum.GetValues(typeof(PlayerStat)).Length);
            PlayerStatEffect = stat;
            switch (stat)
            {
                case PlayerStat.MaxHealth:
                    AmountInt = Random.Range(PlayerMinMaxHealth, PlayerMaxMaxHealth + 1);
                    return;

                case PlayerStat.Speed:
                    AmountFloat = Random.Range(PlayerMinSpeed, PlayerMaxSpeed);
                    return;

                case PlayerStat.FireRate:
                    AmountFloat = Random.Range(PlayerMinFireRate, PlayerMaxFireRate);
                    return;

                case PlayerStat.BulletDamage:
                    AmountInt = Random.Range(PlayerMinBulletDamage, PlayerMaxBulletDamage + 1);
                    return;

                case PlayerStat.BulletSize:
                    AmountFloat = Random.Range(PlayerMinBulletSize, PlayerMaxBulletSize);
                    return;

                case PlayerStat.BulletMoveInterval:
                    AmountFloat = Random.Range(PlayerMinBulletMoveInterval, PlayerMaxBulletMoveInterval);
                    return;

                case PlayerStat.ShotsAmount:
                    AmountInt = Random.Range(PlayerMinShotsAmount, PlayerMaxShotsAmount + 1);
                    return;

                case PlayerStat.ShotsAngle:
                    AmountFloat = Random.Range(PlayerMinShotsAngle, PlayerMaxShotsAngle);
                    return;

                case PlayerStat.Heal:
                    AmountInt = Random.Range(PlayerMinHeal, PlayerMaxHeal + 1);
                    return;

                case PlayerStat.Aimbot:
                    AmountInt = -1;
                    return;

                default:
                    ErrorLogger.DebugLog("No player stat assigned when randomizing effects.");
                    return;
            }
        }
        else
        {
            EnemyStat stat = EnemyStat.None;
            while (stat == EnemyStat.None)
                stat = (EnemyStat)Random.Range(0, Enum.GetValues(typeof(EnemyStat)).Length);
            EnemyStatEffect = stat;
            switch (stat)
            {
                case EnemyStat.MaxHealth:
                    AmountInt = Random.Range(EnemyMinMaxHealth, EnemyMaxMaxHealth + 1);
                    return;

                case EnemyStat.Speed:
                    AmountFloat = Random.Range(EnemyMinSpeed, EnemyMaxSpeed);
                    return;

                case EnemyStat.FireRate:
                    AmountFloat = Random.Range(EnemyMinFireRate, EnemyMaxFireRate);
                    return;

                case EnemyStat.BulletDamage:
                    AmountInt = Random.Range(EnemyMinBulletDamage, EnemyMaxBulletDamage + 1);
                    return;

                case EnemyStat.BulletSize:
                    AmountFloat = Random.Range(EnemyMinBulletSize, EnemyMaxBulletSize);
                    return;

                default:
                    ErrorLogger.DebugLog("No enemy stat assigned when randomizing effects.");
                    return;
            }
        }
    }
}