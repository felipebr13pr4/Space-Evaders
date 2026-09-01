using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider m_slider;
    [SerializeField] private Image m_fill;
    [SerializeField] private TextMeshProUGUI m_text;
    private readonly WaitForSeconds m_flashDuration = new(0.1f);
    private readonly WaitForSeconds m_flashDurationSmaller = new(0.01f);
    private Coroutine m_flashCoroutine;

    private void OnEnable() => EntityBehavior.OnDamageTaken += PlayerTookDamage;

    private void OnDisable() => EntityBehavior.OnDamageTaken -= PlayerTookDamage;

    private void PlayerTookDamage(GameObject obj, int dmg)
    {
        if (obj.CompareTag("Player"))
        {
            if (m_flashCoroutine != null) StopCoroutine(m_flashCoroutine);
            m_flashCoroutine = StartCoroutine(FlashColor());
        }
    }

    public void UpdateValues(int health, int maxHealth)
    {
        m_slider.maxValue = maxHealth;
        m_slider.value = health;
        m_text.text = $"{health} / {maxHealth}";
    }

    private IEnumerator FlashColor()
    {
        float a = 0.1f;
        m_fill.color = new(1, 1, 1, a);
        yield return m_flashDurationSmaller;
        m_fill.color = new(1, 0, 0, a);
        yield return m_flashDurationSmaller;
        m_fill.color = new(0, 0.75f, 0, a);
        yield return m_flashDuration;
        m_fill.color = new(0, 1, 0, a);
    }
}
