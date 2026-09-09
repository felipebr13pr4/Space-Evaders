using System;
using UnityEngine;

[Serializable]
public class EffectData
{
    [SerializeField] private PlayerStat m_playerStatEffect;
    public PlayerStat PlayerStatEffect
    {   get => m_playerStatEffect;
        set { m_playerStatEffect = value; m_enemyStatEffect = EnemyStat.None; } }
    [SerializeField] private EnemyStat m_enemyStatEffect;
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

    public FramedAs FramedAs => HandleFramedAs();
    private FramedAs IsBuff(bool isPositive) => isPositive ? FramedAs.Buff : FramedAs.Debuff;

    [SerializeField] private bool m_enabled;
    public bool Enabled { get => m_enabled; set => m_enabled = value; }

    private FramedAs HandleFramedAs()
    {
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

                default:
                    ErrorLogger.DebugLog("No player stats yet Player Stat is true detected");
                    return FramedAs.Buff;
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

                default:
                    ErrorLogger.DebugLog("No enemy stats yet Enemy Stat is true detected");
                    return FramedAs.Buff;
            }
        }
    }
}