using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Button m_buttonComponent;
    [SerializeField] private CardData m_cardData;
    public CardData CardData => m_cardData;
    [SerializeField] private TextMeshProUGUI[] m_effectTexts = new TextMeshProUGUI[9];
    public static event Action OnCardChosen;
    public static event Action<CardData> OnCardChosenWithData;

    private void OnEnable()
    { 
        m_buttonComponent.onClick.AddListener(ButtonClicked);

        StartCoroutine(HandleTexts());
    }

    private void OnDisable() => m_buttonComponent.onClick.RemoveListener(ButtonClicked);

    private IEnumerator HandleTexts()
    {
        yield return null;
        for (int i = 0; i < m_effectTexts.Length; i++)
        {
            if (m_cardData.Effects[i].Enabled)
                m_effectTexts[i].gameObject.SetActive(m_cardData.Effects[i].Enabled);
        }

        for (int i = 0; i < m_effectTexts.Length; i++)
        {
            if (!m_effectTexts[i].isActiveAndEnabled) continue;
            string type = m_cardData.Effects[i].IsPlayerStat ?
                m_cardData.Effects[i].PlayerStatEffect.ToString() :
                m_cardData.Effects[i].EnemyStatEffect.ToString();

            object amount = m_cardData.Effects[i].IsFloat ? m_cardData.Effects[i].AmountFloat :
                m_cardData.Effects[i].AmountInt;

            string sign = (float)amount > 0 ? "+" : "-";

            m_effectTexts[i].text = $"{type} {sign}{amount}";
        }
    }

    private void ButtonClicked()
    {
        OnCardChosen?.Invoke();
        OnCardChosenWithData?.Invoke(m_cardData);
    }
}
