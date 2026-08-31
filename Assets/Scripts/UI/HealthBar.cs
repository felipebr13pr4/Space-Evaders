using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider m_slider;
    [SerializeField] private TextMeshProUGUI m_text;

    private void OnEnable() => PlayerBehavior.OnLifeChange += UpdateValues;

    private void OnDisable() => PlayerBehavior.OnLifeChange -= UpdateValues;

    private void UpdateValues(int health, int maxHealth)
    {
        m_slider.maxValue = maxHealth;
        m_slider.value = health;
        m_text.text = $"{health} / {maxHealth}";
    }
}
