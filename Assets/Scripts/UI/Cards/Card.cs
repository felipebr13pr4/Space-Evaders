using System;
using System.Collections;
using TMPro;
using UnityEditor;
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
    private int m_currentWave;

    private void Awake() { EnemyHandler.OnWaveStartWithNumber += UpdateWave;
        CardEffectHandler.OnPlayerAimbotUnlock += UpdateUnlock; }

    private void OnEnable()
    { 
        m_buttonComponent.onClick.AddListener(ButtonClicked);

        StartCoroutine(HandleRefresh());

        StartCoroutine(HandleTexts());
    }

    private void OnDisable() => m_buttonComponent.onClick.RemoveListener(ButtonClicked);

    private void OnDestroy() { EnemyHandler.OnWaveStartWithNumber -= UpdateWave;
    CardEffectHandler.OnPlayerAimbotUnlock -= UpdateUnlock; }

    private void UpdateWave(int wave) => m_currentWave = wave; 
    private void UpdateUnlock() => m_cardData.HasUnlockedAimbot = true;

    private IEnumerator HandleRefresh()
    {
        yield return null;
        if (!isActiveAndEnabled) yield break;

        int waveChange = m_currentWave / 20;
        if (m_currentWave > 80) waveChange = 4;

        m_cardData.MaxBuffsAmount = 4 - waveChange;
        m_cardData.MaxDebuffsAmount = 5 + waveChange;

        m_cardData.Randomize();
    }

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
                m_cardData.Effects[i].PlayerStatEffect == PlayerStat.BulletMoveInterval ?
                "Player Bullet Speed" :
                m_cardData.Effects[i].PlayerStatEffect == PlayerStat.Aimbot ?
                "Player Unlock Aimbot" :
                "Player " + m_cardData.Effects[i].PlayerStatEffect.ToString() :
                "Enemy " + m_cardData.Effects[i].EnemyStatEffect.ToString();

            type = ObjectNames.NicifyVariableName(type);

            m_effectTexts[i].color =
                m_cardData.Effects[i].FramedAs == FramedAs.Buff ? Color.green : Color.red;

            string visibility = "0.#";
            if (m_cardData.Effects[i].IsFloat)
            {
                string valueStr = m_cardData.Effects[i].AmountFloat.ToString("F6");
                int times = 0;
                for (int j = 0; j < valueStr.Length; j++)
                {
                    if (valueStr[j] == '0') times++;
                }
                for (int j = 0; j < times; j++)
                {
                    visibility += "#";
                }
            }

            string amount = m_cardData.Effects[i].IsFloat ?
                m_cardData.Effects[i].AmountFloat.ToString(visibility) : 
                m_cardData.Effects[i].AmountInt.ToString();

            string sign = float.Parse(amount) > 0 ? "+" : "";

            if (m_cardData.Effects[i].IsPlayerStat &&
                m_cardData.Effects[i].PlayerStatEffect == PlayerStat.Aimbot)
            { amount = ""; sign = ""; }

            m_effectTexts[i].text = $"{type} {sign}{amount}";
        }
    }

    private void ButtonClicked()
    {
        OnCardChosen?.Invoke();
        OnCardChosenWithData?.Invoke(m_cardData);
    }
}
