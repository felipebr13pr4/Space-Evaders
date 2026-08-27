using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class WaveDisplay : MonoBehaviour
{
    private TextMeshProUGUI m_text;

    private void Start() => m_text = GetComponent<TextMeshProUGUI>();

    private void OnEnable() => EnemyHandler.OnWaveStartWithNumber += ChangeText;
    private void OnDisable() => EnemyHandler.OnWaveStartWithNumber -= ChangeText;

    private void ChangeText(int number)
    {
        m_text.text = "Wave: " + number.ToString();
    }
}
