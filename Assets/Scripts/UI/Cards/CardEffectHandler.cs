using UnityEngine;

public class CardEffectHandler : MonoBehaviour
{
    [SerializeField] private EnemyHandler m_enemyHandler;
    [SerializeField] private PlayerBehavior m_player;

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
        foreach(EffectData data in card.Effects)
        {
            if (!data.Enabled) continue;
            print(data);
            switch (data.PlayerStatEffect)
            {
                case PlayerStat.MaxHealth:
                    m_player.MaxHealth += data.AmountInt;
                    continue;

                default:
                    ErrorLogger.LogError("No stat designed for this card");
                    continue;
            }
        }
    }
}
