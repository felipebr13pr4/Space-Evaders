using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RefreshButton : MonoBehaviour
{
    [SerializeField] private int m_maxAmount;
    private int m_amount;
    private int Amount { get => m_amount;
        set {   m_amount = value; m_text.text = $"{m_amount}/{m_maxAmount}";
                m_amount = Mathf.Clamp(m_amount, 0, m_maxAmount);
                m_button.interactable = m_amount != 0; } }
    [SerializeField] private bool m_refreshesEachWave;
    [SerializeField] private Card[] m_card;
    [SerializeField] private Button m_button;
    [SerializeField] private TextMeshProUGUI m_text;

    private void Awake()
    { if (m_refreshesEachWave) EnemyHandler.OnWaveStart += RefreshAmount; }

    private void Start() => RefreshAmount();

    private void OnDestroy()
    { if (m_refreshesEachWave) EnemyHandler.OnWaveStart -= RefreshAmount; }

    private void OnEnable() => m_button.onClick.AddListener(RefreshCards);

    private void OnDisable() => m_button.onClick.RemoveListener(RefreshCards);

    private void OnValidate() => RefreshAmount();

    private void RefreshAmount() => Amount = m_maxAmount;

    private void RefreshCards()
    {
        if (Amount != 0)
        foreach (Card card in m_card)
                StartCoroutine(card.HandleRefresh());
        Amount--;
    }
}