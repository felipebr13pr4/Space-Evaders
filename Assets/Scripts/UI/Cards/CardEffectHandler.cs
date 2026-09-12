using System;
using UnityEngine;

public class CardEffectHandler : MonoBehaviour
{
    [SerializeField] private EnemyHandler m_enemyHandler;
    [SerializeField] private PlayerBehavior m_playerBeh;
    [SerializeField] private PlayerMovement m_playerMov;
    public static event Action OnPlayerAimbotUnlock;

    private void OnEnable()
    {
        Card.OnCardChosenWithData += HandleCardData;
    }

    private void OnDisable()
    {
        Card.OnCardChosenWithData -= HandleCardData;
    }

    private void HandleCardData(CardData card)
    {
        foreach(EffectData effect in card.Effects)
        {
            if (!effect.Enabled) continue;
            if (effect.IsPlayerStat)
            {
                switch (effect.PlayerStatEffect)
                {
                    case PlayerStat.MaxHealth:
                        m_playerBeh.MaxHealth += effect.AmountInt;
                        continue;

                    case PlayerStat.Speed:
                        m_playerMov.Speed += effect.AmountFloat;
                        continue;

                    case PlayerStat.FireRate:
                        m_playerBeh.FireRate += effect.AmountFloat;
                        continue;

                    case PlayerStat.BulletDamage:
                        m_playerBeh.BulletsData.Damage += effect.AmountInt;
                        continue;

                    case PlayerStat.BulletSize:
                        m_playerBeh.BulletsData.Size += new Vector2(effect.AmountFloat, effect.AmountFloat);
                        continue;

                    case PlayerStat.BulletMoveInterval:
                        m_playerBeh.BulletsData.MoveInterval += effect.AmountFloat;
                        continue;

                    case PlayerStat.ShotsAmount:
                        m_playerBeh.MultiShootModifier.MultiShotAmount += effect.AmountInt;
                        continue;

                    case PlayerStat.ShotsAngle:
                        m_playerBeh.MultiShootModifier.MultiShotAngle += effect.AmountFloat;
                        continue;

                    case PlayerStat.Heal:
                        m_playerBeh.Health += effect.AmountInt;
                        continue;

                    case PlayerStat.Aimbot:
                        m_playerBeh.AimbotModifier.enabled = true;
                        OnPlayerAimbotUnlock?.Invoke();
                        continue;

                    default:
                        ErrorLogger.LogError("No stat assigned for this card");
                        continue;
                }
            }
            else
            {
                switch (effect.EnemyStatEffect)
                {
                    case EnemyStat.MaxHealth:
                        m_enemyHandler.WaveData.Healths += effect.AmountInt;
                        continue;

                    case EnemyStat.Speed:
                        m_enemyHandler.WaveData.MoveSpeeds += effect.AmountFloat;
                        continue;

                    case EnemyStat.FireRate:
                        m_enemyHandler.WaveData.FireRates += effect.AmountFloat;
                        continue;

                    case EnemyStat.BulletDamage:
                        m_enemyHandler.WaveData.Bullets.Damage += effect.AmountInt;
                        continue;

                    case EnemyStat.BulletSize:
                        m_enemyHandler.WaveData.Bullets.Size += new Vector2(effect.AmountFloat, effect.AmountFloat);
                        continue;

                    default:
                        ErrorLogger.LogError("No stat assigned for this card");
                        continue;
                }
            }
        }
    }
}
